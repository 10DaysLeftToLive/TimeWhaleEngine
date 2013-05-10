Shader "Custom/BackgroundShader" {
	Properties {
		//Emission variables for testing, will get rid of them when I decide what a good sun location and color is.
		_MainTex ("Texture", 2D) = "white" {}
		_GradinetMap ("Texture", 2D) = "white" {}
		_Contrast("Contrast", Range(0, 1)) = 1
		_Brightness("Brightness", Range(0,1)) = 1
		//Don't mess with this variable the values of these variables will be computed in a script.
		//_Hue("Hue", Range(0, 360)) = 0
		_GreenFilter("GreenFilter", Float) = 0
		_Hue("Hue", Float) = 0
		_Saturation("Saturation", Range(0,1)) = 0
		_TimeFactor("InterpolationFactor", Range(0,1)) = 1
	}
	
	SubShader {
		Tags { "RenderType"="Opaque" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
		LOD 200
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			uniform sampler2D _MainTex;
			uniform sampler2D _GradientMap;
			uniform float _Hue;
			uniform float _Saturation;
			uniform float _TimeFactor;
			uniform float _Contrast;
			uniform float _Brightness;
			uniform float _GreenFilter;
			
			const float3 lumCoeff = float3(0.2125, 0.7154, 0.0721);
			
			struct a2v
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				float4 color : COLOR;
			};
			
			struct v2f
			{
				float4 position: POSITION;
				float2 uv : TEXCOORD0;
				float4 color : COLOR;
			};
			
			float3x3 QuaternionToMatrix(float4 quat)
			{
    			float3 cross = quat.yzx * quat.zxy;
    			float3 square= quat.xyz * quat.xyz;
    			float3 wimag = quat.w * quat.xyz;

    			square = square.xyz + square.yzx;

    			float3 diag = 0.5 - square;
    			float3 a = (cross + wimag);
    			float3 b = (cross - wimag);

    			return float3x3(
    			2.0 * float3(diag.x, b.z, a.y),
    			2.0 * float3(a.z, diag.y, b.x),
    			2.0 * float3(b.y, a.x, diag.z));
			}
			
			float3 ComputeContrast(float3 texColor) {
				return (texColor.xyz - 0.5) *(_Contrast + 1.0) + 0.5;
			}
			
			float3 ComputeHue(float4 texColor) {
				float3 root3 = float3(0.57735, 0.57735, 0.57735);
				float half_angle = 0.5 * radians(_Hue); // Hue is radians of 0 to 360 degrees
        		float4 rot_quat = float4( (root3 * sin(half_angle)), cos(half_angle));
        		float3x3 rot_Matrix = QuaternionToMatrix(rot_quat);     
        		texColor.rgb = mul(rot_Matrix, texColor.rgb);
        		return texColor.rgb;
			} 
			
			v2f vert(a2v In) : POSITION
			{
				v2f output;
				output.position = mul(UNITY_MATRIX_MVP, In.vertex);
				output.uv = In.texcoord;
				output.color = In.color;
				return output;
			}
        	
			float4 frag (v2f In) : COLOR
			{
				float3 darkness = float3(0.22, 0.77, 0.061);
				
				
				//Gets the real texture color and stores it for later use.
				float4 texColor = tex2D(_MainTex, In.uv);
				float4 gradientMapColor = tex2D(_GradientMap, In.uv);
				
//				float VSU = _Value*_Saturation*cos(_Hue*3.14159/180);
//				float VSW = _Value*_Saturation*sin(_Hue*3.14159/180);
//				texColor.r = ((0.299*_Value+0.701*VSU+0.168*VSW)*texColor.r +
//						     (0.587*-0.587*VSU+0.330*VSW)*texColor.g +
//						     (0.114*_Value-0.114*VSU+0.497*VSW)*texColor.b);
//						     
//				texColor.g = ((0.299*_Value-0.299*VSU-0.328*VSW)*texColor.r +
//							 (0.587*_Value+0.413*VSU+0.035*VSW)*texColor.g +
//							 (0.114*_Value-0.114*VSU+0.292*VSW)*texColor.b);
//							 
//    			texColor.b = ((0.299*_Value-0.3*VSU+1.25*VSW)*texColor.r +
//        					 (0.587*_Value-0.588*VSU-1.05*VSW)*texColor.g +
//        				     (0.114*_Value+0.886*VSU-0.203*VSW)*texColor.b);
//        		
//        		texColor.rgb = fmod(texColor.rgb, float3(1,1,1));
//        		texColor.rgb = abs(texColor.rgb);
 				
 				texColor.rgb = ComputeHue(texColor);
 				gradientMapColor.rgb = lerp(float3(0,0,0), gradientMapColor.rgb, _TimeFactor);

        		//Contrast
        		texColor.rgb = ComputeContrast(texColor.rgb);
        		//Brightness  
        		texColor.rgb = texColor.rgb + _Brightness;         
        		float3 intensity = float3(dot(texColor.rgb, lumCoeff));
        		//Color saturate between the old color and the new color
        		texColor.rgb = lerp(intensity, texColor.rgb, _Saturation );
//        		float colMix = dot(texColor.rgb, gradientMapColor.rgb);
//        		
//        		texColor.r += gradientMapColor.r * colMix;
//        		texColor.g += gradientMapColor.g * colMix;
//        		texColor.b += gradientMapColor.b * colMix;
				texColor.g = texColor.g - _GreenFilter;
				return texColor;
				
				//Testing to see which color corresponds to which vertex in the script that uses this shader.
				//return In.color;
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
