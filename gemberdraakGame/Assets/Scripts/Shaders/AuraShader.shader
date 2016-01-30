Shader "Custom/Aura Shader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Opacity ("Opacity", Range(0, 1)) = 1
		_Height ("Height", Range(0, 1)) = 1
	}
	SubShader
	{

		Tags { "Queue" = "Transparent" "RenderType"="Transparent" }

		Blend SrcAlpha One 

		cull Front

		Pass
		{

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
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
			float _Opacity;
			float _Height;
			
			v2f vert (appdata_full v)
			{
				v2f o;

				v.vertex.y *= _Height;
				v.vertex.y -= 1 - _Height;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				UNITY_TRANSFER_FOG(o, o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D( _MainTex, float2(i.uv.x + _Time.x, i.uv.y) );

				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return fixed4(col.r, col.g, col.b, col.a * _Opacity);
			}
			ENDCG
		}

		cull Back

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
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
			float _Opacity;
			float _Height;
			
			v2f vert (appdata_full v)
			{
				v2f o;

				v.vertex.y *= _Height;
				v.vertex.y -= 1 - _Height;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				UNITY_TRANSFER_FOG(o, o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D( _MainTex, float2(i.uv.x + _Time.x, i.uv.y) );

				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return fixed4(col.r, col.g, col.b, col.a * _Opacity);
			}
			ENDCG
		}
	}
}
