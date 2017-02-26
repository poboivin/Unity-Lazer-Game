Shader "Custom/testShade" 
{
	Properties
	{
		_MainTex("Texture",2D)= "white" {}
		_SecondTex("Texture",2D) = "white" {}
		_Tween("Tween",Range(0,1)) = 0
		_test("Color",Color)= (1,1,1,1)
	}
	SubShader 
	{	
	
		Tags
	{

		"Queue" = "Transparent"
	}
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			
			struct appdata
			{
				float4 vertex :  POSITION;
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
			sampler2D _MainTex;
			sampler2D _SecondTex;
			float4 _test;
			float _Tween;
			float4 frag(v2f i) : SV_Target
			{
				
				float4 color = tex2D(_MainTex, i.uv);
				float4 color2 = tex2D(_SecondTex, i.uv);
				float4 output = (1 - _Tween) * color + _Tween * color2;
				output *= _test;
				return output;
			}
			ENDCG

		}
	}
}
