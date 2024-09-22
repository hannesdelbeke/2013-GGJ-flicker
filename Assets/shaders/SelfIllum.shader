Shader "Custom/Self-Illumination vertex" {
	Properties {
		//_Color ("Color Tint", Color) = (1,1,1,1)
		//_MainTex ("SelfIllum Color (RGB) Alpha (A)", 2D) = "white"
		
		
        _Color ("Main Color", Color) = (0.3,0.3,0.3,0.5)
        _Ambient ("Ambient", Color) = (0.3,0.3,0.3,0.5)
        _SpecColor ("Spec Color", Color) = (0,0,0,0)
        _Emission ("Emmisive Color", Color) = (0.3,0.3,0.3,0)
        _Shininess ("Shininess", Range (0.01, 1)) = 0.7
        _MainTex ("Base (RGB)", 2D) = "white" { }
	}
	Category {
	   Lighting On
	   SeparateSpecular On
	   ZWrite on
	   //Cull back
	  // Blend SrcAlpha OneMinusSrcAlpha
	  // Tags {Queue=Transparent}
	 Tags { "RenderType" = "Opaque" }
	   SubShader {
            Material {
	        //   Emission [_Color]
	           //------------
	            Diffuse [_Color]
                Ambient [_Ambient]
                Shininess [_Shininess]
                Specular [_SpecColor]
                Emission [_Emission]
            }
            Pass {
	           SetTexture [_MainTex] {
	                 // Combine Texture * Primary, Texture * Primary
                  Combine texture * primary DOUBLE, texture * constant
                }
            }
        } 
    }
}