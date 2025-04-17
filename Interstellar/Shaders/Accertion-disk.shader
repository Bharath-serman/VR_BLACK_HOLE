Shader "Custom/AccretionDiskShader"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (1,1,1,1)
        _EmissionStrength ("Emission Strength", Float) = 2.0
        _Speed ("Rotation Speed", Float) = 0.5
        _NoiseScale ("Noise Scale", Float) = 5.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Pass
        {
            Blend SrcAlpha One  // Additive blending for glow effect
            ZWrite Off
            Cull Back

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
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float _Speed;
            float _NoiseScale;
            float _EmissionStrength;
            fixed4 _MainColor;

            // Simple Perlin Noise function
            float hash(float2 p)
            {
                return frac(sin(dot(p, float2(127.1, 311.7))) * 43758.5453123);
            }

            float perlin(float2 p)
            {
                float2 i = floor(p);
                float2 f = frac(p);
                float2 u = f * f * (3.0 - 2.0 * f);

                return lerp(lerp(hash(i + float2(0,0)), hash(i + float2(1,0)), u.x),
                            lerp(hash(i + float2(0,1)), hash(i + float2(1,1)), u.x), u.y);
            }

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Convert UV to radial coordinates
                float2 centeredUV = i.uv - 0.5;
                float r = length(centeredUV);
                float theta = atan2(centeredUV.y, centeredUV.x);

                // Animate rotation of the disk
                theta += _Time.y * _Speed;

                // Generate procedural noise
                float noise = perlin(float2(r * _NoiseScale, theta * _NoiseScale));

                // Create a radial gradient
                float diskMask = smoothstep(0.2, 0.5, r) * smoothstep(1.0, 0.6, r);

                // Apply noise as an emission effect
                float intensity = noise * diskMask * _EmissionStrength;
                fixed4 color = _MainColor * intensity;
                color.a = diskMask; // Alpha for transparency

                return color;
            }
            ENDHLSL
        }
    }
}
