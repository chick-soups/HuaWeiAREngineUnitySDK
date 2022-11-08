Shader "HuaweiAR/ARBackground"
{
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
		_UvLeftTopBottom ("UV of left corners",Vector) = (0,1,0,0)
		_UvRightTopBottom ("UV of right corners",Vector) = (1,1,1,0)
    }
	
    // For GLES3
    SubShader
    {
        Pass
        {
            ZWrite Off

            GLSLPROGRAM

            #pragma only_renderers gles3

            #ifdef SHADER_API_GLES3
            #extension GL_OES_EGL_image_external_essl3 : require
            #endif

            uniform vec4 _UvLeftTopBottom;
            uniform vec4 _UvRightTopBottom;

            #ifdef VERTEX

            varying vec2 textureCoord;

            void main()
            {
                #ifdef SHADER_API_GLES3
                vec2 uvLeft = mix(_UvLeftTopBottom.xy, _UvLeftTopBottom.zw, gl_MultiTexCoord0.y);
                vec2 uvRight = mix(_UvRightTopBottom.xy, _UvRightTopBottom.zw, gl_MultiTexCoord0.y);
                textureCoord = mix(uvLeft, uvRight, gl_MultiTexCoord0.x);

                gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
                #endif
            }

            #endif

            #ifdef FRAGMENT
            varying vec2 textureCoord;
            uniform samplerExternalOES _MainTex;

            void main()
            {
                #ifdef SHADER_API_GLES3
                gl_FragColor = texture(_MainTex, textureCoord);
                #endif
            }

            #endif

            ENDGLSL
        }
    }

    FallBack Off
}
