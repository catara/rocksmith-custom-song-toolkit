float objectID
	: register(c[0]);

float4 main(float4 color : COLOR0) : COLOR0
{
	return float4(objectID, color.r, 0.0, 1.0);
}