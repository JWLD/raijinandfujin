Shader "Custom/TransparentFade"
{
	Properties //Variables
	{
		_MainTex("Main Texture (RGBA)", 2D) = "white" {} //2D texture picker
	_DissolveTexture("Dissolve (RGBA)", 2D) = "white"{}
	_DissolveAmount("Dissolve Amount", Range(0.0,1.0)) = 0.0
		_DissolveAlphaMargin("Dissolve Alpha Margin", Range(0,1.0)) = 0.0
	}

		SubShader
	{
		//Lighting On

		Tags{ "Queue" = "Transparent" }
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha //Blend mode - transparency - after all calculations, this defines how the object blends with other layers below
		LOD 100

		Pass
	{
		CGPROGRAM //Start 'CG' section
#pragma vertex vert //define vertex and fragment functions
#pragma fragment frag
#include "UnityCG.cginc"

				  //import variable to CG
		sampler2D _MainTex;
	float4 _MainTex_ST;
	sampler2D _DissolveTexture;
	float _DissolveAmount;
	float _DissolveAlphaMargin;

	struct vertexInput //pass in values
	{
		float4 vertex : POSITION; //object vertices (float4 as (x,y,z,w))
		float2 uv : TEXCOORD0; //to wrap texture around it (float 2 as UV is (u,v))
	};

	struct vertexOutput
	{
		float2 uv : TEXCOORD0;
		float4 pos : SV_POSITION; //'SV_' so that it works across multiple platforms
	};


	vertexOutput vert(vertexInput v) //convert vertex to fragment
	{
		vertexOutput o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex); //mvp = model view projection
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		return o; //return, cycle through all vertices and update them
	}

	fixed4 frag(vertexOutput i) : SV_Target //'SV_Target' target to screen
	{
		fixed4 mainTexture = tex2D(_MainTex, i.uv); //multiply colour by texture
	fixed4 outputTexture = (1 - mainTexture.a) + mainTexture;
	float dissolveTex = tex2D(_DissolveTexture, i.uv); // -0.08
	float margin = (_DissolveAlphaMargin)* (1 - _DissolveAmount);
	float marginMaximum = dissolveTex + margin;


	if (marginMaximum > _DissolveAmount) //if the upper end of the alpha margin is greater than the alpha of the dissolve image
	{
		float alphaOutputTexture = (_DissolveAmount - dissolveTex) * (1 / margin);/// _PaperTexM;
		outputTexture.a = alphaOutputTexture;
	}

	if (dissolveTex > _DissolveAmount) //if the texture alpha is greater than the range on dissolve, set it to 0 anyway
		outputTexture.a = 0;

	return outputTexture; //return a colour, cycle through all pixels and colour them in
	}
		ENDCG
	}
	}
		Fallback "Diffuse"
}
