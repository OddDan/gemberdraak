Shader "Hidden/LightningImageEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv + 0.001 * float2(sin(_Time.y * i.uv.y), cos(_Time.y * i.uv.x)) + 0.01 * float2(sin(10 * _Time.y), cos(10 * _Time.y)));

				fixed4 luminosity = (col.r + col.g + col.b) * 0.333f;

				luminosity = 1 - step(luminosity, 0.4f);

				fixed4 final = lerp(col, luminosity, min(0.2, sin(10 * 3.1415 * _Time.y)));

				return final;
			}
			ENDCG
		}
	}
}
