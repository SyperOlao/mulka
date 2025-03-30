Shader "Unlit/PlaneShader"
{
    Properties
    {
        _Color ("Grid Color", Color) = (0,0,0,1)
        _MainTex ("Texture", 2D) = "green" {}
        _GridSize ("Grid Size", Float) = 10
        _LineWidth ("Line Width", Float) = 100
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _Color;
            float _GridSize;
            float _LineWidth;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * _GridSize;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 grid = frac(i.uv);
                float line1 = step(grid.x, _LineWidth) + step(grid.y, _LineWidth);
                line1 = min(line1, 1);
                return lerp(tex2D(_MainTex, i.uv), _Color, line1);
            }
            ENDCG
        }
    }
}
