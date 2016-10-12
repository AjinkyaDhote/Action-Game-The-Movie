Shader "Hidden/NewImageEffectShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Magnitude("Magnitude", Range(0, 0.1)) = 1
	}
	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			float rand()
			{
				return frac(sin(_Time.y)*1e4);
			}

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;

				return o;
			}
			
			sampler2D _MainTex;
			float _Magnitude;

			fixed4 frag (v2f i) : SV_Target
			{
				//// glitch effect

				float glitch = _Magnitude - 0.02;
				
				if (glitch < 0.01)
				{
					fixed4 color = tex2D(_MainTex, i.uv);
					return color;
				}
				else
				{
					float2 uv = i.uv;
					float2 uvR = uv;
					float2 uvB = uv;

					uvR.x = uv.x * 1.0 - rand() * glitch * 0.8;
					uvB.y = uv.y * 1.0 + rand() * glitch * 0.8;

					if ((uv.y < rand()) && (uv.y >(rand() - 0.1)) && (sin(_Time.y) < 0.0))
					{
						uv.x = uv.x + glitch * rand();
					}

					float4 color;
					color.r = tex2D(_MainTex, uvR).r;
					color.g = tex2D(_MainTex, uv).g;
					color.b = tex2D(_MainTex, uvB).b;
					color.a = 1.0;

					//
					float scanline = sin(uv.y * 800.0 * rand()) / 30.0;
					color *= 1.0 - scanline;

					//vignette
					//float vegDist = length((0.5, 0.5) - uv);
					//color.gba *= 1.0 - vegDist * 0.6;

					return color;
				}
			}
			ENDCG
		}
	}
}
