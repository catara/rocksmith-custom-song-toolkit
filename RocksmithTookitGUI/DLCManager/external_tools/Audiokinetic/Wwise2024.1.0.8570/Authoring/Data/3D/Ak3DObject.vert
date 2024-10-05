//***********************************************
// Attributes
//***********************************************

// https://doc.magnum.graphics/magnum/structMagnum_1_1Shaders_1_1GenericGL.html
layout(location = 0) in vec3 vertexPosition;

#ifdef USE_ILLUMINATION
layout(location = 5) in vec3 vertexNormal;
#endif

#ifdef USE_VERTEX_COLOR
layout(location = 2) in vec4 vertexColor;
#endif

#ifdef USE_TEXTURE
layout(location = 1) in vec2 textureCoordinates;
#endif

//***********************************************
// Outputs
//***********************************************

#ifdef USE_ILLUMINATION
out vec3 ourPosition;
out vec3 ourNormal;
#endif

#ifdef USE_VERTEX_COLOR
out vec4 ourColor;
#endif

#ifdef USE_TEXTURE
out vec2 ourTextureCoordinates;
#endif

//***********************************************
// Uniforms
//***********************************************

uniform mat4 modelMatrix;
uniform mat4 viewProjectionMatrix;

#ifdef USE_ILLUMINATION
uniform mat3 normalMatrix;
#endif

//***********************************************
// Procedures
//***********************************************

void main()
{
	vec3 worldPosition = vec3(modelMatrix * vec4(vertexPosition, 1.0));
	gl_Position = viewProjectionMatrix * vec4(worldPosition, 1.0);

#ifdef USE_ILLUMINATION
	ourPosition = worldPosition;
	ourNormal = normalMatrix * vertexNormal;
#endif

#ifdef USE_VERTEX_COLOR
	ourColor = vertexColor;
#endif

#ifdef USE_TEXTURE
	ourTextureCoordinates = textureCoordinates;
#endif
}
