Shader "Custom/BlackHoleDistortion"
{
    Properties
    {
        _DistortionStrength ("Distortion Strength", Range(0, 1)) = 0
        _BlackHolePosition ("Black Hole Position", Vector) = (0.5, 0.5, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _DistortionStrength;
            float2 _BlackHolePosition;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float2 distort(float2 uv, float2 center, float strength)
            {
                float2 delta = uv - center;
                float distance = length(delta);
                float distortion = exp(-distance * 10) * strength; 
                return uv + normalize(delta) * distortion;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                uv = distort(uv, _BlackHolePosition, _DistortionStrength);
                return tex2D(_MainTex, uv);
            }
            ENDHLSL
        }
    }
}
