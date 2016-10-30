Shader "Custom/3DPrinter" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
			Cull Off
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#include "UnityCG.cginc" 
		#include "UnityPBSLighting.cginc" // for _LightColor0
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Custom fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
			float3 viewDir;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float _ConstructY;
		fixed4 _ConstructColor;
		float _ConstructGap;		
		int building;

		void surf (Input IN, inout SurfaceOutputStandard o)
		{
			viewDir = IN.viewDir;

			float s = +sin((IN.worldPos.x * IN.worldPos.z) * 60 + _Time[3] + o.Normal) / 120;

			if (IN.worldPos.y > _ConstructY + s + _ConstructGap)
				discard;

			if (IN.worldPos.y < _ConstructY)
			{
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = c.rgb;
				o.Alpha = c.a;

				building = 0;
			}
			else
			{
				o.Albedo = _ConstructColor.rgb;
				o.Alpha = _ConstructColor.a;

				building = 1;
			}
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;		 	
		}
		
		inline half4 LightingUnlit(SurfaceOutputStandard s, half3 lightDir, half atten)
		{
			return _ConstructColor;
		}

		inline half4 LightingCustom(SurfaceOutputStandard s, half3 lightDir, UnityGI gi)
		{
			if (!building)
				return LightingStandard(s, lightDir, gi); // Unity5 PBR			

			return _ConstructColor; // Unlit
		}
		inline void LightingCustom_GI(SurfaceOutputStandard s, UnityGIInput data, inout UnityGI gi)
		{
			LightingStandard_GI(s, data, gi);
		}
		


		ENDCG
	}
	FallBack "Diffuse"
}
