Shader "Custom/Lightning Shader"
{
	Properties
	{
		_Color ("Color", Color) = (1, 1, 1, 1)
		_Theta ("Theta", Float) = 0
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }

		Pass
		{
			ZWrite On
			ColorMask A
		}

		Pass
		{
//			Cull Back
			ZWrite Off
			ZTest LEqual
			ColorMask RGB

			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 color : Color;
			};

			float4 _Color;
			float _Theta;
			
			v2f vert (appdata_base v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = float4(_Color.r, _Color.g, _Color.b, - _Theta + mul(_Object2World, v.vertex).y);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = i.color;
				//col.a *= i.color.a;

				return col;
			}

			ENDCG
		}
	}
}
