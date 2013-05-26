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
	
		uniform half4 _MainTex_ST;
	
		uniform half4 _MainTex_TexelSize;
		uniform half _Angle;
		uniform half _InterpolationFactor;
		uniform half4 _CenterFrequencyAmplitude;
	
		struct v2f {
			half4 pos : POSITION;
			half2 uv : TEXCOORD0;
			half2 uvOrig : TEXCOORD1;
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
	
		half4 frag (v2f i) : COLOR
		{
			half pi = 3.14159265358979323846264338327;
			half2 offset = i.uvOrig;

			half2 uv;
			offset.x = _CenterFrequencyAmplitude.w * 
				sin(_CenterFrequencyAmplitude.z * i.uvOrig.y * pi + _Angle);
			uv.x = i.uvOrig.x + i.uvOrig.x*(offset.x);
			uv.y = i.uvOrig.y;
			
			//uv += _CenterFrequencyAmplitude.xy;
			
			half4 oldColor = tex2D(_MainTex, uv);
			half4 newColor = tex2D(_FadeInTex, i.uvOrig);
			return lerp(oldColor, half4(newColor.rgb, 0f), _InterpolationFactor);
		}
		ENDCG
	}

	
}
	//Fallback off
}
