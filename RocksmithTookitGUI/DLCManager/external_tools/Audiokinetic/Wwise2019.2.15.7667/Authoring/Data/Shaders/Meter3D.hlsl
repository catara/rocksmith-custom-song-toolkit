static const float PI =        3.1415926535897932384626433832795f;
static const float TWOPI =     6.283185307179586476925286766559f;
static const float PIOVERTWO = 1.5707963267948966192313216916398f;

static const int MAX_HARMONICS = 36;
static const int MAX_ORDER = 5;

// Uniforms -------------
float sphericalMeterData[MAX_HARMONICS];
float4x4 WorldViewProj : WorldViewProjection;
float4 LevelColorsThresholds[6];
int order;

struct VSOutput
{
	float4 pos : POSITION;
	float4 colouring : TEXCOORD0;
};


float ProjectSN3DphericalHarmonics(
	float x,
	float y,
	float z,
	int in_order
	)
{
	// index <-> order-1, degree-1
	// sqrt(2 * (m-n)! / (m+n)!)
	const float k_sn3d[MAX_ORDER][MAX_ORDER] =
	{
		{ 1.f, 0.f, 0.f, 0.f, 0.f },	// order 1
		{ 5.77350269e-01f, 2.88675135e-01f, 0.f, 0.f, 0.f }, // order 2 ...
		{ 4.08248290e-01f, 1.29099445e-01f, 5.27046277e-02, 0.f, 0.f },
		{ 3.16227766e-01f, 7.45355992e-02f, 1.99204768e-02f, 7.04295212e-03f, 0.f },
		{ 2.58198890e-01f, 4.87950036e-02f, 9.96023841e-03f, 2.34765071e-03f, 7.42392339e-04f }
	};

	int in_uStrideOut = 1;
	float harmonics[MAX_HARMONICS];
	//memset(harmonics, 0, MAX_HARMONICS * sizeof(float));
	int i;
	for (i = 0; i < MAX_HARMONICS; ++i)
		harmonics[i] = 0;

	float oneovernorm = 1.f / sqrt(x*x + y*y + z*z);
	x *= oneovernorm;
	y *= oneovernorm;
	z *= oneovernorm;

	harmonics[0] = 1.f;	// W = 1
	harmonics[in_uStrideOut] = y;
	harmonics[2 * in_uStrideOut] = z;
	harmonics[3 * in_uStrideOut] = x;

	float z2 = z*z;
	if (z2 < 0.99f)
	{
		float cosphi = sqrt(1.f - z2);
		float oneovercosphi = 1.f / cosphi;
		float costheta = x * oneovercosphi;
		float sintheta = y * oneovercosphi;

		// Generic algorithm using recurrence equations.

		// Cache n degrees cos(n*theta) and sin(n*theta)
		// 
		// Init recurrent variables with results of degrees n - 1 = 0 and n = 1
		float cosn[MAX_ORDER + 1];
		float sinn[MAX_ORDER + 1];
		cosn[0] = 1.f;
		cosn[1] = costheta;
		sinn[0] = 0.f;
		sinn[1] = sintheta;
		for (int n = 2; n < in_order + 1; n++)
		{
			cosn[n] = 2 * costheta * cosn[n - 1] - cosn[n - 2];
			sinn[n] = sinn[n - 1] * costheta + cosn[n - 1] * sintheta;
		}

		// Order recurrence
		//
		// allocate scratch buffers for storing Pm-1,n and Pm,n (n = [0, order])
		// init with zeros because each new order overshoots 2 orders before by 2 degrees
		float Pm_minus1[MAX_ORDER + 1];
		float Pm[MAX_ORDER + 1];
		for (i = 0; i < in_order + 1; i++)
		{
			Pm_minus1[i] = 0;
			Pm[i] = 0;
		}

		// work buffer for inside loop, used up to mplus1+1
		float Pm_plus1[MAX_ORDER + 2];

		// init recurrent variables with results of orders m - 1 = 0 and m = 1
		Pm_minus1[0] = 1;	//Pm_minus1_0 = 1
		Pm[0] = z;			//Pm_0 = uz;
		Pm[1] = cosphi;

		// for m (current order), compute order m+1
		for (int m = 1; m < in_order; m++)
		{
			int m_plus1 = m + 1;

			// Compute n = 0 component and write it in its rightful place
			Pm_plus1[0] = ((2 * m + 1) * z * Pm[0] - m * Pm_minus1[0]) / (m + 1);
			int acn = m_plus1 * m_plus1 + m_plus1;
			harmonics[acn * in_uStrideOut] = Pm_plus1[0];

			// Compute n != 0 components
			for (int n_plus1 = 1; n_plus1 < m_plus1 + 1; n_plus1++)
			{
				float Pm_minus1_n_plus1 = Pm_minus1[n_plus1];
				float Pm_n = Pm[n_plus1 - 1];
				float Pm_plus1_n_plus1 = Pm_minus1_n_plus1 + (2 * m + 1) * cosphi * Pm_n;
				Pm_plus1[n_plus1] = Pm_plus1_n_plus1;

				float SN3D = k_sn3d[m_plus1 - 1][n_plus1 - 1];	// sqrt(2 * factorial(m_plus1 - n_plus1) / factorial(m_plus1 + n_plus1))

				harmonics[(acn + n_plus1)* in_uStrideOut] = SN3D * Pm_plus1[n_plus1] * cosn[n_plus1];
				harmonics[(acn - n_plus1)* in_uStrideOut] = SN3D * Pm_plus1[n_plus1] * sinn[n_plus1];
			}

			// update recurrent variables (just the non - zero part to avoid useless copies).
			for (i = 0; i < m_plus1 + 1; i++)
			{
				Pm_minus1[i] = Pm[i];
				Pm[i] = Pm_plus1[i];
			}
		}
	}
	else
	{
		// Degenerate case z ~ +/-1: cosphi ~ 0, x & y don't care.
		float Pm = z;
		int j = 4;
		for (float m = 2; m < in_order + 1; m++)
		{
			Pm *= z; // Pm_plus1 = ((2 * m + 1) * z * Pm - m * Pm_minus1) / (m + 1) #P2 = 1, P3 = z, P4 = 1, P5 = z, ... Pm = z**m

					 // Write n = 0 component in its rightful place, 0 elsewhere
			int acn = m * m + m;
			do
			{
				harmonics[j*in_uStrideOut] = 0;
				j++;
			} while (j < acn);
			harmonics[j*in_uStrideOut] = Pm;
			j++;
		}
	}

	// dot product "projection" (i.e. one row of the decoding matrix) with sphericalMeterData.
	float intensity = 0.0f;
	for (i = 0; i < MAX_HARMONICS; ++i)
	{
		intensity += harmonics[i] * sphericalMeterData[i];
	}
	intensity /= (in_order + 1);// normalize

	return abs(intensity);
}

VSOutput mainVS(float4 pos : POSITION0, float4 normal : NORMAL)
{
	float3 ambiPos = pos.xyz;

	float x = ambiPos[0];
	float y = ambiPos[1];
	float z = ambiPos[2];
	// Expects right handed coordinates; 3DX is left handed.
	float intensity = ProjectSN3DphericalHarmonics(z, -x, y, order);

	VSOutput outVS = (VSOutput)0;
	float4 position = mul(float4(ambiPos.xyz, 1.0), WorldViewProj);
	outVS.pos = position;
	outVS.colouring = float4(intensity,/*diffuse*/0.0f,0.0f,0.0f);
	return outVS;
}

float4 mainPS(float4 colouring : TEXCOORD0) : COLOR 
{
	const float ambLighting = 0.2f;
	const float3 amb = float3(ambLighting, ambLighting, ambLighting);
	float3 c = float3(0.0f, 0.0f, 0.0f);
	float maxThreshold = -1.0f;
	float3 currentCol = LevelColorsThresholds[0].rgb;
	float3 previousCol = amb;
	float previousThresh = 0.0f;
	for (int ampLevelIndex = 0; ampLevelIndex < 6; ++ampLevelIndex)
	{
		float ampThreshold = LevelColorsThresholds[ampLevelIndex].a;
		if (ampThreshold > maxThreshold)
		{
			currentCol = LevelColorsThresholds[ampLevelIndex].rgb;
			previousThresh = maxThreshold;
			maxThreshold = ampThreshold;

			float mask = (float)(colouring.x >= previousThresh && colouring.x < ampThreshold);
			float interp = (colouring.x - previousThresh) / (ampThreshold - previousThresh);
			float3 col = mask * lerp(previousCol, currentCol, float3(interp, interp, interp));
			c += col;
		}
		if (ampLevelIndex == 5)
		{
			float endMask = (float)(colouring.x >= maxThreshold);
			float3 endCol = endMask * currentCol;
			c += endCol;
		}

		previousCol = currentCol;
	}
	float4 col = float4(c, 1.0f);
	return col;
}