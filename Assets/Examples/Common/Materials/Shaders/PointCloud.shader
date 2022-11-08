// UNITY_SHADER_NO_UPGRADE
Shader "PreviewSample/PointCloud" {
Properties{
        _PointSize("Point Size", Float) = 10.0
        _Color ("Point Color", Color) = (1.0, 0.945, 0.263, 1.0)
}
  SubShader {
     Pass {
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag

        #include "UnityCG.cginc"

        struct VertexInput
        {
           float4 pos : POSITION;
        };

        struct VertexOutput
        {
           float4 pos : SV_POSITION;
           float size : PSIZE;
        };

        float _PointSize;
        fixed4 _Color;

        VertexOutput vert (VertexInput vi)
        {
           VertexOutput vo;
           vo.pos = mul(UNITY_MATRIX_MVP, vi.pos);
           vo.size = _PointSize;

           return vo;
        }

        fixed4 frag (VertexOutput vo) : SV_Target
        {
           return _Color;
        }
        ENDCG
     }
  }
}
