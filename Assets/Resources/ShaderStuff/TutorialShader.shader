﻿Shader "Tutorial/TutorialShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "White" {}
		_Color("Color", Color) = (1,1,1,1)
		_DistFromLight("Distance from light", Int) = 0
	}
	
	SubShader
	{
		Pass
		{
			CGPROGRAM

			#pragma vertex vertexFunc
			#pragma fragment fragmentFunc

			#include "UnityCG.cginc"
			
			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;

			};

			struct v2f {
				float4 position : SV_POSITION;
				fixed4 color : COLOR;
				float2 uv : TEXCOORD;
			};

			fixed4 _Color;
			sampler2D _MainTexture;

			v2f vertexFunc(appdata IN)
			{
				v2f OUT;

				OUT.position = UnityObjectToClipPos(IN.vertex);
				OUT.uv = IN.uv;
				return OUT;
			}

			fixed4 fragmentFunc(v2f IN) : SV_Target
			{
				fixed4 pixelColor = tex2D(_MainTexture, IN.uv);
				
			return pixelColor *_Color;
			}

			ENDCG

		}
	}

}