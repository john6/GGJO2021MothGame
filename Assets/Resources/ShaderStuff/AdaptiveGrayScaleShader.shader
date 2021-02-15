Shader "Unlit/AdaptiveGrayScaleShader"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		_Light_Origin("Light Source Position", Vector) = (0, 0, 0, 0)
		_Pure_black_dist("Pure black distance", Float) = 650
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			Cull Off
			Lighting Off
			ZWrite Off
			Blend One OneMinusSrcAlpha

			Pass
			{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile _ PIXELSNAP_ON
				#include "UnityCG.cginc"

				struct appdata_t
				{
					float4 vertex   : POSITION;
					float4 color    : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					fixed4 color : COLOR;
					float2 texcoord  : TEXCOORD0;
				};

				fixed4 _Color;

				fixed4 _Light_Origin;

				float _Pure_black_dist;

				v2f vert(appdata_t IN)
				{
					v2f OUT;
					OUT.vertex = UnityObjectToClipPos(IN.vertex);
					OUT.texcoord = IN.texcoord;
					OUT.color = IN.color * _Color;
					#ifdef PIXELSNAP_ON
					OUT.vertex = UnityPixelSnap(OUT.vertex);
					#endif

					return OUT;
				}

				sampler2D _MainTex;
				sampler2D _AlphaTex;
				float _AlphaSplitEnabled;

				fixed4 SampleSpriteTexture(float2 uv)
				{
					fixed4 color = tex2D(_MainTex, uv);

	#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
					if (_AlphaSplitEnabled)
						color.a = tex2D(_AlphaTex, uv).r;
	#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

					return color;
				}

				fixed4 frag(v2f IN) : SV_Target
				{

				//get distance from inputted light Source
				float dist = distance(_Light_Origin.xy, IN.vertex.xy);


				//multiply greyscale lerp value by the proportion to max distance
				//Use a preset max distance for now DistUntilGrey = 20
				float greyRatio = max(0, min(1, abs(dist / _Pure_black_dist)));
				//float greyRatio = 0;



				//TESTING DIST 
				//fixed4 origColor = SampleSpriteTexture(IN.texcoord) * IN.color;

				//fixed4 c = (0, 0, 0, 0);

				//if (dist < 600) {
				//	c.r = 1;
				//	c.g = 1;
				//	c.b = 1;
				//}
				//else {
				//	c.r = 0;
				//	c.g = 0;
				//	c.b = 0;
				//}
				//c.a = origColor.a;
				//c.rgb *= origColor.a;

				//return c;
				//END TESTING DIST


				fixed4 origColor = SampleSpriteTexture(IN.texcoord);
				float greyValue = ((origColor.r + origColor.b + origColor.g) / 3) * IN.color * origColor.a;

				fixed4 mixedColor;
				mixedColor.r = ((origColor.r * (1 - greyRatio)) + (greyValue * greyRatio));
				mixedColor.g = ((origColor.g * (1 - greyRatio)) + (greyValue * greyRatio));
				mixedColor.b = ((origColor.b * (1 - greyRatio)) + (greyValue * greyRatio));

				fixed4 c = (0, 0, 0, 0);
				c.r = mixedColor.r;
				c.g = mixedColor.g;
				c.b = mixedColor.b;
				c.a = origColor.a;
				c.rgb *= origColor.a;
				return c;
				}
			ENDCG
			}
		}
}