Shader "Custom/Self-Illumination surface" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
      _ColorTint ("Tint", Color) = (0.5, 0.5, 0.5, 1.0)
   //  _EmissionTint ("Emission", Color) = (1.0, 0.6, 0.6, 1.0)
      _EmisPower ("Brightness", Range(0,5)) = 1.5
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		//the default lighting shader
		//#pragma surface surf Lambert finalcolor:mycolor 
		//the order to execute the functions
		//#pragma surface surfaceFunction lightModel [optionalparams]
		//my own simple lambert shader
		//  #pragma surface surf SimpleLambert nodirlightmap nolightmap novertexlights dualforward
		  //finalcolor:mycolor					

    //  half4 LightingSimpleLambert (SurfaceOutput s, half3 lightDir, half atten) { //what s this atten?
      //    half NdotL = dot (s.Normal, lightDir);								//how much light/shadow
     //     half4 c;																//create 4 channel tex
     //     c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten * 2);			
     //     c.a = s.Alpha;
     //     return c;
     // }
     
	#pragma surface surf Lambert 
	
		struct Input {															//the input struct
			float2 uv_MainTex;													//texture diffuse
      	//float3 viewDir;
		};
		
		fixed4 _ColorTint;														//define colortint
		//void mycolor (Input IN, SurfaceOutput o, inout fixed4 color)			//final color function (changes color after lighting is applied
      //{
        //  color *= _ColorTint;													//multiply with tint
      //}      	
      	sampler2D _MainTex;      												//define diffuse tex		
     	float4 _EmissionTint; 													//define emmission color      float4 = fixed4 (does the same)
     	float _EmisPower;														//define emissive power
      
		void surf (Input IN, inout SurfaceOutput o) { 							//the surface function
			half4 c = tex2D (_MainTex, IN.uv_MainTex); 							//he uses the uv to decide wat he needs to use from the texture
			half3 endColor = tex2D (_MainTex, IN.uv_MainTex).rgb*_ColorTint;
			o.Albedo =  	endColor;		//output color
			o.Alpha = c.a; 														//output aplha
       //   half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal)); 	//IN.viewDir , use the things from the input struct
        fixed4 _EmissionTint = (0.1, 0.1, 0.1, 1.0);
          o.Emission = _EmissionTint.rgb *  (_EmisPower-0.5) * endColor;
		}
		ENDCG
	} 
	FallBack "Diffuse" 															//if the shader does not work, use the diffuse shader
}
