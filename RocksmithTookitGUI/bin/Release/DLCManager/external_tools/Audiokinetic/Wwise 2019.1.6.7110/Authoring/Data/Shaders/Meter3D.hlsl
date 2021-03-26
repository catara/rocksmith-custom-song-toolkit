static const float PI =        3.1415926535897932384626433832795f;
static const float TWOPI =     6.283185307179586476925286766559f;
static const float PIOVERTWO = 1.5707963267948966192313216916398f;

// Uniforms -------------
float t;
float sphericalMeterData[32];
float4x4 WorldViewProj : WorldViewProjection;
float4 LevelColorsThresholds[6];

struct VSOutput
{
	float4 pos : POSITION;
	float4 colouring : TEXCOORD0;
};

float3 ambiSphericalToWorldCartesian(float r, float in_fAzimuth, float in_fElevation)
{
	float mappedAzimuth = in_fAzimuth;
	float mappedElevation = fmod(in_fElevation + PIOVERTWO, TWOPI);
	const float x = r * cos(mappedAzimuth) * sin(mappedElevation);
	const float y = -r * sin(mappedAzimuth) * sin(mappedElevation);
	const float z = -r * cos(mappedElevation);
	
	//return float3(-y, -z, x);
	return float3(y, z, x);
}

VSOutput mainVS(float4 pos : POSITION0, float4 normal : NORMAL)
{
	const float sortedPoints[32][3/*Azimuth, Inclination, Index in original array*/] =
	{
		{ -2.5831, 0, 19},         { -2.3562, 0.6109, 30},     { -2.3562, -0.6109, 31},   { -1.9373, 0, 11},         
		{ -PIOVERTWO, 1.2043, 2},  { -PIOVERTWO, -1.2043, 3},  { -PIOVERTWO, 0.5585, 22}, { -PIOVERTWO, -0.5585, 21}, 
		{ -1.2043, 0, 9},          { -0.7854, 0.6109, 26},     { -0.7854, -0.6109, 27},   { -0.5585, 0, 17},          
		{ 0, 0.3665, 4},           { 0, -0.3665, 5},           { 0, 1.0123, 12},          { 0, -1.0123, 13},
		{ 0.5585, 0, 16},          { 0.7854, 0.6109, 24},      { 0.7854, -0.6109, 25},    { 1.2043, 0, 8},           
		{ PIOVERTWO, 1.2043, 0},   { PIOVERTWO, -1.2043, 1},   { PIOVERTWO, 0.5585, 20},  { PIOVERTWO, -0.5585, 21}, 
		{ 1.9373, 0, 10},          { 2.3562, 0.6109, 28},      { 2.3562, -0.6109, 29},    { 2.5831, 0, 18},
		{ PI, 0.3665, 6},          { PI, -0.3665, 7},          { PI, 1.0123, 13},         { PI, -1.0123, 14}
	};
	
	const float sortedAzimuths[16][2/*Azimuth, index in sorted array*/] = 
	{
		{-2.5831, 0}, {-2.3562, 1}, {-1.9373, 3}, {-PIOVERTWO, 4}, {-1.2043, 8}, {-0.7854, 9}, {-0.5585, 11}, {0.0, 12},
		{0.5585, 16}, {0.7854, 17}, {1.2043, 19}, {PIOVERTWO, 20}, {1.9373, 24}, {2.3562, 25}, {2.5831, 27}, {PI, 28}
	};
	float angle = 0.0f;
	float3 ambiPos = pos.xyz;
	float zeroDivideGuard = (float)(length(ambiPos) <= 0.0);
	ambiPos += zeroDivideGuard;
	float r = sqrt(pow(ambiPos.x, 2.0f) + pow(ambiPos.y, 2.0f) + pow(ambiPos.z, 2.0f));

	// Iterate through all points, adding intensity for the distance to each point, weighted by the meter data...
	float intensity = 0.0f;
	for (int i = 0; i < 32; ++i)
	{
		float3 pentakisPoint = ambiSphericalToWorldCartesian(r, sortedPoints[i][0], sortedPoints[i][1]);
		float meterData = sphericalMeterData[sortedPoints[i][2]];
		float dist = distance(ambiPos, pentakisPoint);
		intensity += clamp(1.0f - dist, 0.0f, 1.0f) * meterData;
	}

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