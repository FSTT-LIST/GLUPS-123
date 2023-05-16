Shader "Custom/ConditionalBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Range(0.0, 10.0)) = 10.0
        _BlurAmount ("Blur Amount", Range(0.0, 10.0)) = 10.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
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
            float _BlurSize;
            float _BlurAmount;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 blurredColor = fixed4(0, 0, 0, 0);
                float blurPixelCount = 0.0;

                for (float x = -_BlurSize; x <= _BlurSize; x += _BlurAmount)
                {
                    for (float y = -_BlurSize; y <= _BlurSize; y += _BlurAmount)
                    {
                        blurredColor += tex2D(_MainTex, i.uv + float2(x, y) * 0.01);
                        blurPixelCount += 1.0;
                    }
                }

                blurredColor /= blurPixelCount;

                // Apply black light effect
                blurredColor *= 0.5; // Decrease the multiplier to make it darker

                return blurredColor;
            }
            ENDCG
        }
    }
}
