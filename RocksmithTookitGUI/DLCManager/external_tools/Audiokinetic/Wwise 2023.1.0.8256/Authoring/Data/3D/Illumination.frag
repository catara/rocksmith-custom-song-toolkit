// For ANGLE compatibility
precision mediump float;

// https://learn.microsoft.com/en-us/windows/win32/direct3d9/lights-and-materials
struct Material
{
	vec3 ambient;  // Ambient reflection of surface
	vec3 diffuse;  // Diffuse reflection of surface
	vec3 specular; // Specular reflection of surface
	float shininess;
};

struct DirectionalLight
{
	vec3 ambient;   // Ambient color of light
	vec3 diffuse;   // Diffuse color of light
	vec3 specular;  // Specular color of light
	vec3 direction; // Direction in world space
};

#define MAX_NUM_LIGHTS 4

uniform Material material;
uniform vec3 cameraPosition;
uniform vec3 ambientLight;
uniform DirectionalLight lights[MAX_NUM_LIGHTS];

// Modified Phong reflection model
vec3 ComputeBlinnPhong(in vec3 in_position, in vec3 in_normal, in Material in_material)
{
	vec3 norm = normalize(in_normal);
	vec3 cameraDir = normalize(cameraPosition - in_position);

	vec3 totalLightAmbient = vec3(0, 0, 0);
	vec3 totalLightDiffuse = vec3(0, 0, 0);
	vec3 totalLightSpecular = vec3(0, 0, 0);

	for (int i = 0; i < MAX_NUM_LIGHTS; ++i)
	{
		DirectionalLight light = lights[i];

		vec3 lightDir = normalize(-light.direction);

		// Ambient
		// https://learn.microsoft.com/en-us/windows/win32/direct3d9/ambient-lighting
		totalLightAmbient += light.ambient;

		// Diffuse
		// https://learn.microsoft.com/en-us/windows/win32/direct3d9/diffuse-lighting
		totalLightDiffuse += light.diffuse * max(dot(norm, lightDir), 0.0);

		// Specular (Blinn-Phong)
		// https://learn.microsoft.com/en-us/windows/win32/direct3d9/specular-lighting
		if (light.specular != vec3(0, 0, 0))
		{
			vec3 halfwayDir = normalize(lightDir + cameraDir);
			totalLightSpecular += light.specular * pow(max(dot(norm, halfwayDir), 0.0), in_material.shininess);
		}
	}

	vec3 ambient = in_material.ambient * (ambientLight + totalLightAmbient);
	vec3 diffuse = in_material.diffuse * totalLightDiffuse;
	vec3 specular = in_material.specular * totalLightSpecular;

	// https://learn.microsoft.com/en-us/windows/win32/direct3d9/mathematics-of-lighting
	return ambient + diffuse + specular;
}

vec3 ComputeBlinnPhong(in vec3 in_position, in vec3 in_normal)
{
	return ComputeBlinnPhong(in_position, in_normal, material);
}

vec3 ComputeBlinnPhong(in vec3 in_position, in vec3 in_normal, in vec3 in_color)
{
	// See Ak3DObject::SetColor
	Material m = material;
	m.ambient = m.diffuse = in_color;

	return ComputeBlinnPhong(in_position, in_normal, m);
}
