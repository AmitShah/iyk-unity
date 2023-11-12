Shader "Unlit/rgbshift"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Shift ("RGB Shift", Range(0,1)) = 0.5
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Shift;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
             fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 r = tex2D(_MainTex, i.uv + fixed2(_Shift, 0));
                fixed4 g = tex2D(_MainTex, i.uv);
                fixed4 b = tex2D(_MainTex, i.uv - fixed2(_Shift, 0));
                return fixed4(r.r, g.g, b.b, col.a);
            }
            ENDCG
        }
    }
}
