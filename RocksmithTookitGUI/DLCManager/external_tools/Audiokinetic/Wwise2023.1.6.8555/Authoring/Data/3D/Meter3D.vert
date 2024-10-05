layout(location = 0) in vec3 vertexPosition;

out float ourIntensity;

const int kNumHarmonics = 36;
const int kNumHarmonicsPacked = kNumHarmonics / 4;
const int kNumOrders = 5;

uniform mat4 mvpMatrix;
uniform int order;
uniform vec4 sphericalMeterData[kNumHarmonicsPacked];

// index <-> order-1, degree-1
// sqrt(2 * (m-n)! / (m+n)!)
const float k_sn3d[kNumOrders * kNumOrders] = float[kNumOrders * kNumOrders](
	1.0, 0.0, 0.0, 0.0, 0.0,                       // order 1
	5.77350269e-01, 2.88675135e-01, 0.0, 0.0, 0.0, // order 2 ...
	4.08248290e-01, 1.29099445e-01, 5.27046277e-02, 0.0, 0.0,
	3.16227766e-01, 7.45355992e-02, 1.99204768e-02, 7.04295212e-03, 0.0,
	2.58198890e-01, 4.87950036e-02, 9.96023841e-03, 2.34765071e-03, 7.42392339e-04
);

float ProjectSN3DphericalHarmonics(float x, float y, float z, int in_order)
{
	float harmonics[kNumHarmonics];
	for (int i = 0; i < kNumHarmonics; ++i)
		harmonics[i] = 0.0;

	float oneovernorm = 1.0 / sqrt(x*x + y*y + z*z);
	x *= oneovernorm;
	y *= oneovernorm;
	z *= oneovernorm;

	harmonics[0] = 1.0;	// W = 1
	harmonics[1] = y;
	harmonics[2] = z;
	harmonics[3] = x;

	float z2 = z*z;
	if (z2 < 0.99)
	{
		float cosphi = sqrt(1.0 - z2);
		float oneovercosphi = 1.0 / cosphi;
		float costheta = x * oneovercosphi;
		float sintheta = y * oneovercosphi;

		// Generic algorithm using recurrence equation

		// Cache n degrees cos(n*theta) and sin(n*theta)
		// Init recurrent variables with results of degrees n - 1 = 0 and n = 1
		float cosn[kNumOrders + 1];
		float sinn[kNumOrders + 1];
		cosn[0] = 1.0;
		cosn[1] = costheta;
		sinn[0] = 0.0;
		sinn[1] = sintheta;
		for (int n = 2; n < in_order + 1; n++)
		{
			cosn[n] = 2.0 * costheta * cosn[n - 1] - cosn[n - 2];
			sinn[n] = sinn[n - 1] * costheta + cosn[n - 1] * sintheta;
		}

		// Order recurrence
		// allocate scratch buffers for storing Pm-1,n and Pm,n (n = [0, order])
		// init with zeros because each new order overshoots 2 orders before by 2 degrees
		float Pm_minus1[kNumOrders + 1];
		float Pm[kNumOrders + 1];
		for (int i = 0; i < in_order + 1; i++)
		{
			Pm_minus1[i] = 0.0;
			Pm[i] = 0.0;
		}

		// work buffer for inside loop, used up to mplus1+1
		float Pm_plus1[kNumOrders + 2];

		// init recurrent variables with results of orders m - 1 = 0 and m = 1
		Pm_minus1[0] = 1.0;	//Pm_minus1_0 = 1
		Pm[0] = z;			//Pm_0 = uz;
		Pm[1] = cosphi;

		// for m (current order), compute order m+1
		for (int m = 1; m < in_order; m++)
		{
			float m_f = float(m);
			int m_plus1 = m + 1;

			// Compute n = 0 component and write it in its rightful place
			Pm_plus1[0] = ((2.0 * m_f + 1.0) * z * Pm[0] - m_f * Pm_minus1[0]) / (m_f + 1.0);
			int acn = m_plus1 * m_plus1 + m_plus1;
			harmonics[acn] = Pm_plus1[0];

			// Compute n != 0 components
			for (int n_plus1 = 1; n_plus1 < m_plus1 + 1; n_plus1++)
			{
				float Pm_minus1_n_plus1 = Pm_minus1[n_plus1];
				float Pm_n = Pm[n_plus1 - 1];
				float Pm_plus1_n_plus1 = Pm_minus1_n_plus1 + (2.0 * m_f + 1.0) * cosphi * Pm_n;
				Pm_plus1[n_plus1] = Pm_plus1_n_plus1;

				float SN3D = k_sn3d[(m_plus1 - 1) * kNumOrders + n_plus1 - 1];	// sqrt(2 * factorial(m_plus1 - n_plus1) / factorial(m_plus1 + n_plus1))

				harmonics[(acn + n_plus1)] = SN3D * Pm_plus1[n_plus1] * cosn[n_plus1];
				harmonics[(acn - n_plus1)] = SN3D * Pm_plus1[n_plus1] * sinn[n_plus1];
			}

			// update recurrent variables (just the non - zero part to avoid useless copies).
			for (int i = 0; i < m_plus1 + 1; i++)
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
		for (int m = 2; m < in_order + 1; m++)
		{
			Pm *= z; // Pm_plus1 = ((2 * m + 1) * z * Pm - m * Pm_minus1) / (m + 1) #P2 = 1, P3 = z, P4 = 1, P5 = z, ... Pm = z**m
					 // Write n = 0 component in its rightful place, 0 elsewhere
			int acn = m * m + m;
			do
			{
				harmonics[j] = 0.0;
				j++;
			} while (j < acn);
			harmonics[j] = Pm;
			j++;
		}
	}

	// dot product "projection" (i.e. one row of the decoding matrix) with sphericalMeterData.
	float intensity = 0.0f;

	for (int i = 0; i < kNumHarmonicsPacked; ++i)
	{
		int offset = i * 4;

		intensity += harmonics[offset + 0] * sphericalMeterData[i].r;
		intensity += harmonics[offset + 1] * sphericalMeterData[i].g;
		intensity += harmonics[offset + 2] * sphericalMeterData[i].b;
		intensity += harmonics[offset + 3] * sphericalMeterData[i].a;
	}

	intensity /= float(in_order + 1);// normalize

	return abs(intensity);
}

void main()
{
	// Expects right handed coordinates
	float intensity = ProjectSN3DphericalHarmonics(vertexPosition.z, -vertexPosition.x, vertexPosition.y, order);

	ourIntensity = intensity;

	gl_Position = mvpMatrix * vec4(vertexPosition, 1.0);
}
