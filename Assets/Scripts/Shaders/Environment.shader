Shader "Custom/BackgroundShader" {
	Properties {
		//Emission variables for testing, will get rid of them when I decide what a good sun location and color is.
		_MainTex ("Texture", 2D) = "white" {}
		_GradinetMap ("Texture", 2D) = "white" {}
		_Contrast("Contrast", Range(0,1)) = 1
		_Brightness("Brightness", Range(0,1)) = 0.4328358
		//Don't mess with this variable the values of these variables will be computed in a script.
		//_Hue("Hue", Range(0, 360)) = 0
		_GreenFilter("GreenFilter", Range(0,1)) = 0.25
		_Hue("Hue", Float) = 360
		_Saturation("Saturation", Range(0,1)) = 0.5820895
		_TimeFactor("InterpolationFactor", Range(0,1)) = 0
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
			uniform half _Hue;
			uniform half _Saturation;
			uniform half _TimeFactor;
			uniform half _Contrast;
			uniform half _Brightness;
			uniform half _GreenFilter;
			
			const half3 lumCoeff = half3(0.2125, 0.7154, 0.0721);
			
			struct a2v
			{
				half4 vertex : POSITION;
				half2 texcoord : TEXCOORD0;
				half4 color : COLOR;
			};
			
			struct v2f
			{
				half4 position: POSITION;
				half2 uv : TEXCOORD0;
				half4 color : COLOR;
			};
			
			half3x3 QuaternionToMatrix(half4 quat)
			{
    			half3 cross = quat.yzx * quat.zxy;
    			half3 square= quat.xyz * quat.xyz;
    			half3 wimag = quat.w * quat.xyz;

    			square = square.xyz + square.yzx;

    			half3 diag = 0.5 - square;
    			half3 a = (cross + wimag);
    			half3 b = (cross - wimag);

    			return half3x3(
    			2.0 * half3(diag.x, b.z, a.y),
    			2.0 * half3(a.z, diag.y, b.x),
    			2.0 * half3(b.y, a.x, diag.z));
			}
			
			half3 ComputeContrast(half3 texColor) {
				return (texColor.xyz - 0.5) *(_Contrast + 1.0) + 0.5;
			}
			
			half3 ComputeHue(half4 texColor) {
				half3 root3 = half3(0.57735, 0.57735, 0.57735);
				half half_angle = 0.5 * radians(_Hue); // Hue is radians of 0 to 360 degrees
        		half4 rot_quat = half4( (root3 * sin(half_angle)), cos(half_angle));
        		half3x3 rot_Matrix = QuaternionToMatrix(rot_quat);     
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
        	
			half4 frag (v2f In) : COLOR
			{
				half3 darkness = half3(0.22, 0.77, 0.061);
				
				
				//Gets the real texture color and stores it for later use.
				half4 texColor = tex2D(_MainTex, In.uv);
				half4 gradientMapColor = tex2D(_GradientMap, In.uv);
				
//				half VSU = _Value*_Saturation*cos(_Hue*3.14159/180);
//				half VSW = _Value*_Saturation*sin(_Hue*3.14159/180);
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
//        		texColor.rgb = fmod(texColor.rgb, half3(1,1,1));
//        		texColor.rgb = abs(texColor.rgb);

				//Contrast
        		texColor.rgb = ComputeContrast(texColor.rgb);
        		//Brightness  
        		texColor.rgb = texColor.rgb + _Brightness;         
        		half3 intensity = half3(dot(texColor.rgb, lumCoeff));
        		//Color saturate between the old color and the new color
        		texColor.rgb = lerp(intensity, texColor.rgb, _Saturation );
 				
 				texColor.rgb = ComputeHue(texColor);
 				
 				gradientMapColor.rgb = lerp(half3(0,0,0), gradientMapColor.rgb, _TimeFactor);

        		
//        		half colMix = dot(texColor.rgb, gradientMapColor.rgb);
//        		
        		//texColor.r += gradientMapColor.r * colMix;
        		//texColor.g += gradientMapColor.g * colMix;
        		
        		
        		texColor.rgb += gradientMapColor.rgb;
        		
        		
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
