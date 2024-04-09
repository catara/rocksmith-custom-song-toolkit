// For ANGLE compatibility
precision mediump float;

in float ourIntensity;

const int NUM_LEVEL_COLORS = 6;

uniform vec4 levelColors[NUM_LEVEL_COLORS]; // The alpha channel is the amplitude threshold

out vec4 fragmentColor;

void main()
{
	const float ambLighting = 0.2;
	const vec3 amb = vec3(ambLighting, ambLighting, ambLighting);
	vec3 c = vec3(0.0, 0.0, 0.0);
	float maxThreshold = -1.0;
	vec3 currentCol = levelColors[0].rgb;
	vec3 previousCol = amb;
	float previousThresh = 0.0;

	for (int ampLevelIndex = 0; ampLevelIndex < NUM_LEVEL_COLORS; ++ampLevelIndex)
	{
		float ampThreshold = levelColors[ampLevelIndex].a;
		if (ampThreshold > maxThreshold)
		{
			currentCol = levelColors[ampLevelIndex].rgb;
			previousThresh = maxThreshold;
			maxThreshold = ampThreshold;

			float mask = float(ourIntensity >= previousThresh && ourIntensity < ampThreshold);
			float interp = (ourIntensity - previousThresh) / (ampThreshold - previousThresh);
			vec3 col = mask * mix(previousCol, currentCol, interp);
			c += col;
		}
		if (ampLevelIndex == (NUM_LEVEL_COLORS - 1))
		{
			float endMask = float(ourIntensity >= maxThreshold);
			vec3 endCol = endMask * currentCol;
			c += endCol;
		}

		previousCol = currentCol;
	}

	fragmentColor = vec4(c, 1.0);
}
