//Shader "Custom/TextureDecal" {
//	Properties {
//		_MainTex ("Base (RGB)", 2D) = "white" {}
//	}
//	SubShader {
//		Tags { "RenderType"="Opaque" }
//		LOD 200
//		
//		CGPROGRAM
//		#pragma surface surf Lambert
//
//		sampler2D _MainTex;
//
//		struct Input {
//			float2 uv_MainTex;
//		};
//
//		void surf (Input IN, inout SurfaceOutput o) {
//			half4 c = tex2D (_MainTex, IN.uv_MainTex);
//			o.Albedo = c.rgb;
//			o.Alpha = c.a;
//		}
//		ENDCG
//	} 
//	FallBack "Diffuse"
//}
Shader "Custom/TextureDecal" 
{ 
    Properties
    { 
		_Cutoff ("Alpha cutoff", Range (0,1)) = 0.5
        _Color ("Main Color", Color) = (1,1,1) 
     //   _Emission ("Emmisive Color", Color) = (0,0,0) 
        _MainTex ("Base (RGBA)", 2D) = "black"
        _DecalHelmTex ("DecalHelm (RGBA)", 2D) = "black"
        _DecalShirtTex ("DecalShirt (RGBA)", 2D) = "black"
     //   _DecalBootsTex ("DecalBoots (RGBA)", 2D) = "black"
        _DecalGloveTex ("DecalGlove (RGBA)", 2D) = "black"
        _DecalArmTex ("DecalArm (RGBA)", 2D) = "black"
        _DecalPantsTex ("DecalPants (RGBA)", 2D) = "black"
    	//6 decals max
    } 
	Category {
	   Lighting On
	//   SeparateSpecular On
	//   ZWrite on
	   //Cull back
	   Blend SrcAlpha OneMinusSrcAlpha
	   Tags {Queue=Transparent}
	 	Tags { "RenderType" = "Opaque" }
	   
	   SubShader 
	   {
			AlphaTest Greater [_Cutoff]
            Material 
            {
	          	Emission [_Color]
	            Diffuse [_Color]
               // Ambient [_Ambient]
              //  Shininess [_Shininess]
             //   Specular [_SpecColor]
           //     Emission [_Emission]
            }
            Pass 
            { 
	 		//	Lighting On
		    //    SeparateSpecular off
		     //   Material
		     //  { 
		     //      Diffuse [_Color] 
		        //    Ambient [_Color] 
		       //     Emission [_Emission] 
		     //  } 
		        SetTexture[_MainTex]
		        {
		          	ConstantColor[_Color]
		          	Combine texture Lerp(texture) constant
		        } 
		        SetTexture[_DecalShirtTex] 
		        {
		        	Combine texture Lerp(texture) previous
		        }
		        SetTexture[_DecalPantsTex] 
		        {
		        	Combine texture Lerp(texture) previous
		        }
		        SetTexture[_DecalArmTex] 
		        {
		        	Combine texture Lerp(texture) previous
		        }
		        SetTexture[_DecalGloveTex] 
		        {
		        	Combine texture Lerp(texture) previous
		        }
		        SetTexture[_DecalHelmTex] 
		        {
		        	Combine texture Lerp(texture) previous
		        }
		        SetTexture[_DecalGloveTex] 
		        {
		        	Combine texture Lerp(texture) previous
		       		constantColor [_Color]
		       		Combine previous * constant, constant
		        }
		    //    SetTexture[_DecalBootsTex] 
		     //   {
		     //   	Combine texture Lerp(texture) previous
		    //    }
		    
		        SetTexture[_] {
		        constantColor [_Color]
		        Combine previous * primary Double }
            }
        } 
    }
}
