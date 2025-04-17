Shader "Custom/BlackHoleRings"
{
    Properties
    {
        _EmissionColor ("Emission Color", Color) = (1,1,0,1) // Adjust glow color
        _EmissionIntensity ("Emission Intensity", Float) = 5.0
        _BackgroundColor ("Background Color", Color) = (0,0,0,1) // Ensure black background
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
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

            float4 _EmissionColor;
            float _EmissionIntensity;
            float4 _BackgroundColor;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                
                // Generate a ring pattern based on UV coordinates
                float ringPattern = sin(uv.y * 50.0) * 0.5 + 0.5; // Creates rings
                float emissionStrength = smoothstep(0.4, 0.6, ringPattern); // Smooth edges
                
                // Set default background color (black)
                float4 col = _BackgroundColor;

                // Apply emission only to the rings
                col.rgb += emissionStrength * _EmissionColor.rgb * _EmissionIntensity;

                return col;
            }
            ENDHLSL
        }
    }
}
