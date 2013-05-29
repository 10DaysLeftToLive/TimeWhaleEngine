Shader "Custom/FadeShader" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
}

SubShader
{
	ZTest Always Cull Off ZWrite Off
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
		uniform fixed _InterpolationFactor;
		uniform half4 _CenterFrequencyAmplitude;
	
		struct v2f {
			half4 pos : POSITION;
			fixed2 uv : TEXCOORD0;
			fixed2 uvOrig : TEXCOORD1;
		};
	
		v2f vert (appdata_img v)
		{
			v2f o;
			o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
			//o.uv = v.texcoord + _CenterFrequencyAmplitude.xy;
			o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.uvOrig = v.texcoord;
			return o;
		}
	
		fixed4 frag (v2f i) : COLOR
		{
			fixed2 uv = fixed2(i.uvOrig.x + i.uvOrig.y * _CenterFrequencyAmplitude.w * 
				sin(_CenterFrequencyAmplitude.z * i.uvOrig.y * 3.14 + _Angle),
				 i.uvOrig.y);

			//uv.xy -= _CenterFrequencyAmplitude.xy;
			
			fixed4 oldColor = tex2D(_MainTex, uv);
			fixed4 newColor = tex2D(_FadeInTex, i.uvOrig);
			return lerp(oldColor, fixed4(newColor.rgb, 0), _InterpolationFactor);
		}
		ENDCG
	}

	
}
}
