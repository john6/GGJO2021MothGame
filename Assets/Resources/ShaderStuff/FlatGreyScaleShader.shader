Shader "Unlit/FlatGreyScaleShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
			_Color("Tint", Color) = (1,1,1,1)
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

				UNITY_INSTANCING_BUFFER_START(MyProps)
				UNITY_DEFINE_INSTANCED_PROP(fixed4, unity_SpriteRendererColorArray)
				UNITY_INSTANCING_BUFFER_END(MyProps)


				struct v2f
				{
					float4 vertex : SV_POSITION;
					fixed4 color : COLOR;
					float2 uv : TEXCOORD0;
				};

				struct appdata
				{
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float2 uv : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID // store instanced ID
				};

				float4 _MainTex_ST;


				////this one just returns a single color sprite
				//float4 vertFunc(float4 vertex : POSITION) : SV_POSITION
				//{
				//	return UnityObjectToClipPos(vertex);
				//}

				fixed4 _Color;

				v2f vertFunc(appdata v)
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					o.vertex = UnityObjectToClipPos(v.vertex);
					//o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					o.uv = v.uv;
					// get the instanced property
					o.color = v.color * _Color; //UNITY_ACCESS_INSTANCED_PROP(MyProps, unity_SpriteRendererColorArray);
					return o;
				}


				fixed4 fragFunc(v2f i) : SV_Target
				{
					// sample the texture
					fixed4 col = tex2D(_MainTex, i.uv) * i.color;
					fixed4 greyCol;
					//greyCol.r = i.color.r;
					//greyCol.g = i.color.g;
					//greyCol.b = i.color.b;

					greyCol.r = ((col.r + col.b + col.g) / 3);
					greyCol.g = ((col.r + col.b + col.g) / 3);
					greyCol.b = ((col.r + col.b + col.g) / 3);
					return greyCol;
				}

				ENDCG
			}
		}
}
