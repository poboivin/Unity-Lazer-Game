Shader "Custom/CustomImageEffect" 
{
	Properties
	{
		_MainTex("Texture",2D) = "white" {}
		_SecondTex("displace",2D) = "white" {}
		_Tween("magnitude",Range(0,1)) = 0
		_saturation("saturation",Range(0,1)) = 0
		_test("Color",Color) = (1,1,1,1)
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
			float	_saturation;
			float4 frag(v2f i) : SV_Target
			{
				//_Tween = pingPong(_Tween,_time.x);
				float2 disp = tex2D(_SecondTex,i.uv).xy;
				disp = ((disp * 2) - 1) * _Tween;
				// * float4(i.uv.r,i.uv.g,1,1)
				float4 color = tex2D(_MainTex, i.uv + disp);
				float lum = Luminance(color);
				float4 grayscale = float4(lum, lum, lum, color.a);
				float4 output = lerp(grayscale,color, _saturation);
				return output;
			}
			ENDCG
		}
	}
}
