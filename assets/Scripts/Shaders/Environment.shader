Shader "Custom/BuildingShader" {
	Properties {
		_Saturation("Saturation Color", Color) = (1, 1, 1, 0.5)
		//Emission variables for testing, will get rid of them when I decide what a good sun location and color is.
		_Sun_Emission_Loc("Light Emission Location", Vector) = (1, 1, 1, 0.5)
		_Emission("Emission", Color) = (1, 1, 1, 1)
		_MainTex ("Texture", 2D) = "white" {}
		_Contrast("Contrast", Float) = 1
		_Brightness("Brightness", Float) = 1
		//Don't mess with this variable the values of these variables will be computed in a script.
		_Hue("Hue", Vector) = (1, 1, 1, 1)
		
		_TimeFactor("InterpolationFactor", Float) = 1
	}
	
	SubShader {
		Tags { "RenderType"="Opaque" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
		LOD 200
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			sampler2D _MainTex;
			float4 _Saturation;
			float4 _Sun_Emission_Loc;
			float4 _Emission;
			float4 _Hue;
			float _TimeFactor;
			float _Contrast;
			float _Brightness;
			
			struct a2v
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 position: POSITION;
				float2 uv : TEXCOORD0;
				float4 color : COLOR;
			};
			
			
			
			v2f vert(a2v In) : POSITION
			{
				v2f output;
				output.position = mul(UNITY_MATRIX_MVP, In.vertex);
				output.uv = In.texcoord;
				float2 lightFalloff = (output.position.xy - _Sun_Emission_Loc.xy);
				_Emission = _Emission * pow(dot(lightFalloff, lightFalloff), 0.5f) * 0.4f;
				return output;
			}
			
        	
			float4 frag (v2f In) : COLOR
			{
				float3 darkness = float3(0.22, 0.77, 0.061);
				//Gets the real texture color and stores it for later use.
				float4 texColor = tex2D(_MainTex, In.uv);
				float4 realTextureColor = texColor;
				realTextureColor.a = 1.0f;
				float3 textureColorCopy = texColor.rgb;
				
				//Contrast
				texColor.rgb /= texColor.a;
				texColor.rgb = ((texColor.rgb - 0.5f) * max(_Contrast, 0)) + 0.5f;
				
				//Brightness
				texColor.rgb += _Brightness;
				
				texColor.rgb *= texColor.a;
				
				//Change in HUE
				//texColor *= _HUE;
				
				
				//Apply sunset/sunrise saturation as time increases
				//float4 saturation = lerp(_Saturation_Color, realTextureColor, max(min(1, _TimeFactor), 0));
				//saturation = saturation * _Emission;
				//Also darken the image as time goes on
				//darkness = lerp(darkness, float3(1,1,1), max(min(1, _TimeFactor / 5), 0));   
				//float grayscale = dot(textureColorCopy.rgb, darkness);
				//texCol.rgb =  texCol.rgb * grayscale * saturation.rgb;
				
				
				return texColor;
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
