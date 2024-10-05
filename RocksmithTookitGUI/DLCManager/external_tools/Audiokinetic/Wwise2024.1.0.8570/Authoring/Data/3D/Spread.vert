layout(location = 0) in vec3 vertexPosition;

out vec4 ourColor;

const float PI = 3.1415926535897932384626433832795;
const int MAX_OBJECTS = 120;
const int SELECTED_INDEX = 0;
const int UNSELECTED_INDEX = 1;
const float EDGE_BLEND = 0.05;
const float MIN_SPREAD = PI * 0.03;
const float MAX_SPREAD = 3.15;

uniform mat4 mvpMatrix;
uniform float selectedMultiplier;
uniform float unselectedMultiplier;
uniform vec4 selectedSpreadColor;
uniform vec4 unselectedSpreadColor;
uniform vec4 sphereColor;
uniform int selectedCount;
uniform int unselectedCount;
uniform vec4 objectSpreads[MAX_OBJECTS]; // the w component is the spread value

float SphereAngle(vec3 a, vec3 b)
{
	float num = dot(a, b);
	return acos(num);
}

vec4 AlphaBlend(vec4 in_srcOverColor, vec4 in_dstUnderColor)
{
	float alpha = in_srcOverColor.a + in_dstUnderColor.a * (1.0 - in_srcOverColor.a);
	vec3 color =
		(in_srcOverColor.rgb * in_srcOverColor.a + in_dstUnderColor.rgb * in_dstUnderColor.a * (1.0 - in_srcOverColor.a))
			/ alpha;
	return vec4(color.rgb, alpha);
}

float CalcSpreadMask(vec4 in_objectSpread, float in_alpha)
{
	float angle = SphereAngle(vertexPosition.xyz, in_objectSpread.xyz);
	// Give the spread circle a minimum size
	float spread = clamp(in_objectSpread.w, MIN_SPREAD, MAX_SPREAD);
	// Blend the edge of the circle a little to reduce the jaggy look
	return smoothstep(spread, spread - EDGE_BLEND, angle) * in_alpha;
}

void main()
{
	//-----------------------------
	// Add Unselected spread color
	float unselectedSpreadMask = 0.0;
	for(int i = 0; i < unselectedCount; ++i)
	{
		unselectedSpreadMask += CalcSpreadMask(objectSpreads[i], unselectedSpreadColor.a);
	}
	// Clamp mask and multiply to reduce the maximum applicaiton of the unselected spreads
	unselectedSpreadMask = clamp(unselectedSpreadMask, 0.0, 1.0) * unselectedMultiplier;

	// Set the unselected color's alpha mask
	vec4 unselectedColor = unselectedSpreadColor;
	unselectedColor.a = unselectedSpreadMask;

	// Blend Unselected Spread on top of Sphere color
	vec4 coloring = AlphaBlend(unselectedColor, sphereColor);

	//-----------------------------
	// Add Selected spread color
	float selectedSpreadMask = 0.0;
	for(int j = unselectedCount; j < unselectedCount + selectedCount; ++j)
	{
		selectedSpreadMask += CalcSpreadMask(objectSpreads[j], selectedSpreadColor.a);
	}
	selectedSpreadMask = clamp(selectedSpreadMask, 0.0, 1.0) * selectedMultiplier;

	// Set the unselected color's alpha mask
	vec4 selectedColor = selectedSpreadColor;
	selectedColor.a = selectedSpreadMask;

	// Blend Selected Spread on top
	ourColor = AlphaBlend(selectedColor, coloring);

	// Make sure this position is consistent with right/left handed coordinates
	// now that we use GLSL instead of HLSL
	gl_Position = mvpMatrix * vec4(vertexPosition, 1.0);
}
