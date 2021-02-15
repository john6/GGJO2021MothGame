// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/2DSpriteShaderTest"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_DistFromLight("Distance from light", Int) = 0
    }
    SubShader
    {
        Cull Off

        Pass
        {

            CGPROGRAM
            
			#pragma vertex vertFunc
            #pragma fragment fragFunc
            #include "UnityCG.cginc"

			sampler2D _MainTex;

			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			//this one just returns a single color sprite
			float4 vertFunc(float4 vertex : POSITION) : SV_POSITION
			{
				return UnityObjectToClipPos(vertex);
			}

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

			float4 _MainTex_ST;

			v2f vertFunc(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
            




            fixed4 fragFunc (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 greyCol;
				greyCol.r = (col.r + col.b + col.g) / 3;
				greyCol.g = (col.r + col.b + col.g) / 3;
				greyCol.b = (col.r + col.b + col.g) / 3;
				//greyCol.a = 0;
                return greyCol;
            }

            ENDCG
        }
    }
}
