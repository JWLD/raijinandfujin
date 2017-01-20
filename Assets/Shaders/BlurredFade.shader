Shader "Custom/BlurredFade"
{
	Properties
	{
		_Color("Tint", Color) = (1,1,1,1)
		_MainTex("Main Texture (RGBA)", 2D) = "white" {} //2D texture picker
	_DissolveTexture("Dissolve (RGBA)", 2D) = "white"{}
	_DissolveAmount("Dissolve Amount", Range(0.0,1.0)) = 0.0
		_DissolveAlphaMargin("Dissolve Alpha Margin", Range(0,1.0)) = 0.0
		_Blur("Blur Amount", Range(0.0, 0.05)) = 0.0
	}

		SubShader
	{
		Tags
	{
		"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "PreviewType" = "Plane" "CanUseSpriteAtlas" = "True"
	}

		Cull Off Lighting Off ZWrite Off Blend SrcAlpha OneMinusSrcAlpha

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma target 2.0
#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
#include "UnityCG.cginc"

	struct appdata_t
	{
		float4 vertex   : POSITION;
		float4 color    : COLOR;
		float2 texcoord : TEXCOORD0;
	};

	struct v2f
	{
		float4 vertex   : SV_POSITION;
		fixed4 color : COLOR;
		float2 texcoord  : TEXCOORD0;
	};

	fixed4 _Color;
	sampler2D _MainTex;
	float4 _MainTex_ST;
	sampler2D _DissolveTexture;
	float _DissolveAmount;
	float _DissolveAlphaMargin;
	uniform float _Blur;

	v2f vert(appdata_t IN)
	{
		v2f OUT;
		OUT.vertex = UnityObjectToClipPos(IN.vertex);
		OUT.texcoord = IN.texcoord;
		OUT.color = IN.color * _Color;
		return OUT;
	}

	fixed4 SampleSpriteTexture(float2 uv)
	{
		fixed4 color = tex2D(_MainTex, uv);
		return color;
	}

	fixed4 frag(v2f IN) : SV_Target
	{

		//fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
		float4 c = tex2D(_MainTex, IN.texcoord.xy, _Blur, _Blur)* IN.color;

		float dissolveTex = tex2D(_DissolveTexture, IN.texcoord);
		float margin = (_DissolveAlphaMargin)* (1 - _DissolveAmount);
		float marginMaximum = dissolveTex + margin;

		if (marginMaximum > _DissolveAmount) //if the upper end of the alpha margin is greater than the alpha of the dissolve image
		{
			float alphaOutputTexture = (_DissolveAmount - dissolveTex) * (1 / margin);
			c.a = alphaOutputTexture;
		}

		if (dissolveTex > _DissolveAmount) //if the texture alpha is greater than the range on dissolve, set it to 0 anyway
			c.a = 0;

		//c.rgb *= c.a; //preserve transparency 
		return c; //return a colour, cycle through all pixels and colour them in
	}
		ENDCG
	}
	}
}