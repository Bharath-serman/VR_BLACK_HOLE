Shader "Custom/GravitationalLensing"
{
    Properties
    {
        _MainTex ("Background Texture", 2D) = "white" {}
        _BlackHolePosition ("Black Hole Position", Vector) = (0,0,0)
        _DistortionStrength ("Distortion Strength", Float) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            sampler2D _MainTex;
            float4 _BlackHolePosition;
            float _DistortionStrength;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float2 distort(float2 uv, float2 blackHolePos, float strength)
            {
                float2 dir = uv - blackHolePos;
                float dist = length(dir);
                float factor = strength / (dist * dist + 0.01); // Prevent division by zero
                return uv + dir * factor;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 blackHoleUV = _BlackHolePosition.xy / _ScreenParams.xy;
                float2 distortedUV = distort(i.uv, blackHoleUV, _DistortionStrength);
                return tex2D(_MainTex, distortedUV);
            }
            ENDCG
        }
    }
}
