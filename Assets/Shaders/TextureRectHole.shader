Shader "CustomShader/TextureRectHole"
{
Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _HoleInfo ("Hole Position & Size", vector) = (0, 0, 0.5, 0.2)

    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Blend srcAlpha oneMinusSrcAlpha
        ZWrite Off
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
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _HoleInfo;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                float2 offset = _HoleInfo.xy - i.worldPos.xy;
                if(offset.x < _HoleInfo.z && offset.x > -_HoleInfo.z && offset.y < _HoleInfo.w && offset.y > -_HoleInfo.w)
                {
                	col.a = 0;
                }
                return col;
            }
            ENDCG
        }
    }
}