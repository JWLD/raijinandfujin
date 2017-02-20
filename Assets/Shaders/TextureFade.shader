Shader "Custom/TextureFade"
{
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
	_SecondTex("Second (RBG) Trans (A),", 2D) = "white"{}
	_CutTex("Cutout (A)", 2D) = "white" {}
	_Cutoff("Alpha cutoff", Range(0,1)) = 0.0
		_DissolveAlphaMargin("Dissolve Alpha Margin", Range(0,1.0)) = 0.0
		/*	_RampTex("Shading Ramp", 2D) = "gray" {}
		_LightCutoff("Maximum distance", Float) = 5.0*/
	}

		SubShader{
		Tags{ "Queue" = "Geometry" "IgnoreProjector" = "True" "RenderType" = "Geometry" }
		LOD 200
		Lighting On
		ZWrite On

		CGPROGRAM
#include "AutoLight.cginc"

#pragma surface surf Lambert vertex   //if don't wan to use toon lighting, go back to this line, and delete everything under here up to line 52
		//#pragma surface surf Ramp 
		//
		//		uniform float _LightCutoff;
		//		sampler2D _RampTex;
		//
		//		half4 LightingRamp(SurfaceOutput s, half3 lightDir, half atten) {
		//			half NdotL = dot(s.Normal, lightDir);
		//			atten = atten * NdotL * 10;
		//
		//			// Find position to lookup in ramp texture
		//			float2 lookUpPos = (saturate(atten), saturate(atten));
		//			// Lookup value at position in the ramp texture
		//			atten = (tex2D(_RampTex, lookUpPos));
		//
		//			// Get the lowest value in the ramp texture
		//			float lowVal = tex2D(_RampTex, float2(0, 0));
		//			// Check to see if the attenuation is less than the lowest value in the
		//			// ramp texture
		//			if (atten < lowVal)
		//			{
		//				atten = 0;
		//			}
		//
		//			half vMax = (max(max(s.Albedo.r, s.Albedo.g), s.Albedo.b));
		//			half3 colorAdjust = vMax > 0 ? s.Albedo / vMax : 1;
		//			half4 c;
		//			c.rgb = _LightColor0.rgb * atten * colorAdjust;
		//			c.a = s.Alpha;
		//			return c;
		//		}

		sampler2D _MainTex;
	sampler2D _CutTex;
	sampler2D _SecondTex;
	fixed4 _Color;
	float _Cutoff;
	float _DissolveAlphaMargin;

	struct Input {
		float2 uv_MainTex;
	};

	void surf(Input IN, inout SurfaceOutput o) {

		fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
		fixed4 s = tex2D(_SecondTex, IN.uv_MainTex); //float?

													 //o.Albedo = c.rgb;
		o.Alpha = 1;

		float ca = tex2D(_CutTex, IN.uv_MainTex);
		float margin = (_DissolveAlphaMargin)* (1 - _Cutoff);
		float marginMaximum = ca + margin;


		if (marginMaximum > _Cutoff)
		{
			o.Albedo = c;
		}
		else
		{
			o.Albedo = s;
		}



	}
	ENDCG
	}

		Fallback "VertexLit"
}