Shader "Custom/Highlight"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,1)
		_Glossiness ("Glossiness", Range(0,1)) = 0.5
		_PulseSpeed ("PulseSpeed", Range(1,20)) = 5
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
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}

			float4 _Color;
			float _Glossiness;
			float _PulseSpeed;
			sampler2D _MainTex;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 color = tex2D(_MainTex, i.uv);

				float scanline = (sin((i.uv.y + _Time.y)* (_PulseSpeed+3)) + 1) * _Glossiness;
				_Color *= scanline;
				color += _Color;
				return color;
			}
			ENDCG
		}
	}
}
