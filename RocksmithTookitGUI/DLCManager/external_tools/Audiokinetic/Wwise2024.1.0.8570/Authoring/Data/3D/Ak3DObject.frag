// For ANGLE compatibility
precision mediump float;

//***********************************************
// Attributes
//***********************************************

#ifdef USE_ILLUMINATION
in vec3 ourPosition;
in vec3 ourNormal;
#endif

#ifdef USE_VERTEX_COLOR
in vec4 ourColor;
#endif

#ifdef USE_TEXTURE
in vec2 ourTextureCoordinates;
#endif

//***********************************************
// Outputs
//***********************************************

out vec4 fragmentColor;

//***********************************************
// Uniforms
//***********************************************

#ifdef USE_OBJECT_COLOR
uniform vec4 objectColor;
#endif

#ifdef USE_TEXTURE
uniform sampler2D textureData;
#endif

#ifdef RENDER_SELECTION_ID
uniform float objectID;
#endif

//***********************************************
// Procedure
//***********************************************

#ifdef USE_ILLUMINATION
// Defined in Illumination.frag
vec3 ComputeBlinnPhong(in vec3 in_position, in vec3 in_normal);
vec3 ComputeBlinnPhong(in vec3 in_position, in vec3 in_normal, in vec3 in_color);
#endif

void main()
{
	// The object color may modulate the texture color (see wa::ShaderConfig)
	// By default, we fully maintain the original texture color
	fragmentColor = vec4(1.0, 1.0, 1.0, 1.0);

#ifdef USE_OBJECT_COLOR
	fragmentColor = objectColor;
#endif

#ifdef USE_VERTEX_COLOR
	fragmentColor = ourColor;
#endif

#ifdef USE_TEXTURE
	fragmentColor = texture(textureData, ourTextureCoordinates) * fragmentColor;
#endif

#ifdef USE_ILLUMINATION
	#if defined USE_OBJECT_COLOR || defined USE_VERTEX_COLOR || defined USE_TEXTURE
		fragmentColor = vec4(ComputeBlinnPhong(ourPosition, ourNormal, fragmentColor.rgb), fragmentColor.a);
	#else
		fragmentColor = vec4(ComputeBlinnPhong(ourPosition, ourNormal), 1.0);
	#endif
#endif

#ifdef RENDER_SELECTION_ID
	// R = object ID, G = primitive ID
	fragmentColor = vec4(objectID, fragmentColor.r, 0.0, 1.0);
#endif

#ifdef RENDER_SELECTED_OBJECT
	// White for now, but should ideally take into account the original fragmentColor
	fragmentColor = vec4(1.0, 1.0, 1.0, 1.0);
#endif
}
