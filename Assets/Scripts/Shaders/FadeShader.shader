Shader "Custom/FadeShader" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
}

SubShader
{
	ZTest Always Cull Off ZWrite Off ColorMask RGBA
	Fog { Mode off }
	
	Pass
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma fragmentoption ARB_precision_hint_fastest 
	
		#include "UnityCG.cginc"
	
		uniform sampler2D _MainTex;
		uniform sampler2D _FadeInTex;
	
		uniform float4 _MainTex_ST;
	
		uniform float4 _MainTex_TexelSize;
		uniform float _Angle;
		uniform float _InterpolationFactor;
		uniform float4 _CenterFrequencyAmplitude;
	
		struct v2f {
			float4 pos : POSITION;
			float2 uv : TEXCOORD0;
			float2 uvOrig : TEXCOORD1;
		};
	
		v2f vert (appdata_img v)
		{
			v2f o;
			o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
			//o.uv = v.texcoord - _CenterFrequencyAmplitude.xy;
			o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.uvOrig = v.texcoord;
			return o;
		}
	
		float4 frag (v2f i) : COLOR
		{
			float pi = 3.14159265358979323846264338327;
			float2 offset = i.uvOrig;

			float2 uv;
			offset.x = _CenterFrequencyAmplitude.w * 
				sin(_CenterFrequencyAmplitude.z * i.uvOrig.y * pi + _Angle);
			uv.x = i.uvOrig.x + i.uvOrig.x*(offset.x);
			uv.y = i.uvOrig.y;
			
			//uv += _CenterFrequencyAmplitude.xy;
			
			float4 oldColor = tex2D(_MainTex, uv);
			float4 newColor = tex2D(_FadeInTex, i.uvOrig);
			return lerp(oldColor, float4(newColor.rgb, 0f), _InterpolationFactor);
		}
		ENDCG
	}

	
}
	//Fallback off
}
