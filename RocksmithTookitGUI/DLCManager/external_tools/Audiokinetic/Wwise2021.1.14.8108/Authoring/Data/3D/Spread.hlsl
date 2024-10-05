static const float PI = 3.1415926535897932384626433832795f;

static const int CONST_MAX_OBJECTS = 120;

// Data from outside

float4 SelectedMultiplier
	: register(c[0]);
float4 UnSelectedMultiplier
	: register(c[4]);
float4 SelectedSpreadColor
	: register(c[8]);
float4 UnSelectedSpreadColor
	: register(c[12]);
float4 SphereColor
	: register(c[16]);

float4x4 WorldViewProj : WorldViewProjection
	: register(c[20]);

static const int SELECTED = 0;
static const int UNSELECTED = 1;
float4 ObjectSelectionCount
	: register(c[24]);

float4 ObjectSpreads[CONST_MAX_OBJECTS]
	: register(c[28]);

float sphere_angle(float3 a, float3 b)
{
	float num = dot(a,b);
	return acos(num);
}

float4 alpha_blend(float4 src_over_color, float4 dst_under_color)
{
	float out_a = src_over_color.a + dst_under_color.a * (1 - src_over_color.a);
	float3 out_rgb = (src_over_color.rgb * src_over_color.a + dst_under_color.rgb * dst_under_color.a * (1 - src_over_color.a)) / out_a;
	float4 out_color = {out_rgb.r, out_rgb.g, out_rgb.b, out_a};
	return out_color;
}

struct VSOutput
{
	float4 pos : POSITION;
	float4 coloring : TEXCOORD0;
};

VSOutput mainVS(float4 pos : POSITION0, float4 normal : NORMAL)
{
	// Start with Sphere Color, then alpha blend the spread colors on top
	float4 coloring = SphereColor;
	float edge_blend = 0.05;
	float min_spread = PI * 0.03;

	//-----------------------------
	// Add Unselected spread color
	float unselected_spread_mask = 0.0f;
	for(int i = 0; i < ObjectSelectionCount[UNSELECTED]; ++i)
	{
		float angle = sphere_angle(pos.xyz, ObjectSpreads[i].xyz);
		// Give the spread circle a minimum size
		float spread = clamp(ObjectSpreads[i][3], min_spread, 3.15);
		// Blend the edge of the circle a little to reduce the jaggy look
		unselected_spread_mask += smoothstep(spread, spread - edge_blend, angle) 
								* UnSelectedSpreadColor.a;
	}
	// Clamp mask and multiply to reduce the maximum applicaiton of the unselected spreads
	unselected_spread_mask = clamp(unselected_spread_mask, 0, 1) * UnSelectedMultiplier.a;

	// Set the unselected color's alpha mask
	float4 unselected_color = UnSelectedSpreadColor;
	unselected_color.a = unselected_spread_mask;

	// Blend Unselected Spread on top of Sphere color
	coloring = alpha_blend(unselected_color, coloring);

	//-----------------------------
	// Add Selected spread color
	float selected_spread_mask = 0.0f;
	for(int j = ObjectSelectionCount[UNSELECTED]; j < ObjectSelectionCount[UNSELECTED] + ObjectSelectionCount[SELECTED]; ++j)
	{
		float angle = sphere_angle(pos.xyz, ObjectSpreads[j].xyz);
		// Give the spread circle a minimum size
		float spread = clamp(ObjectSpreads[j][3], min_spread, 3.15);
		// Blend the edge of the circle a little to reduce the jaggy look
		selected_spread_mask += smoothstep(spread, spread - edge_blend, angle) 
								* SelectedSpreadColor.a;
	}
	selected_spread_mask = clamp(selected_spread_mask, 0, 1) * SelectedMultiplier.a;

	// Set the unselected color's alpha mask
	float4 selected_color = SelectedSpreadColor;
	selected_color.a = selected_spread_mask;

	// Blend Selected Spread on top
	coloring = alpha_blend(selected_color, coloring);

	//----------------
	// Set outputs to be used by the pixel shader
	VSOutput outVS = (VSOutput)0;
	float4 position = mul(float4(pos.xyz, 1.0), WorldViewProj);
	outVS.pos = position;
	outVS.coloring = coloring;
	return outVS;
}

float4 mainPS(float4 coloring : TEXCOORD0) : COLOR 
{
	return coloring;
}