Shader "HuaweiAR/ARBackgroundWithMask"
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
            uniform float _UseBodyMask;



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
            uniform sampler2D _BodyMaskTex;
            void main()
            {
                #ifdef SHADER_API_GLES3
                vec4 oriColor = texture(_MainTex, textureCoord);
                vec4 maskColor = texture2D(_BodyMaskTex,textureCoord);
                if (_UseBodyMask==1.0)
                {
                    oriColor = oriColor*(1.0 - maskColor.r);
                }
                gl_FragColor = oriColor;
                #endif
            }

            #endif

            ENDGLSL
        }
    }

    FallBack Off
}
