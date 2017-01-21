Shader "Custom/FillSpriteShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform fixed4 _Color;
			
            struct v2f {
                float2 uv : TEXCOORD0;
            };

            v2f vert (
                float4 vertex : POSITION, // vertex position input
                float2 uv : TEXCOORD0, // texture coordinate input
                out float4 outpos : SV_POSITION // clip space position output
                )
            {
                v2f o;
                o.uv = uv;
                outpos = UnityObjectToClipPos(vertex);
                return o;
            }
			
			fixed4 frag (v2f IN, UNITY_VPOS_TYPE screenPos : VPOS) : COLOR
			{
				fixed4 c = tex2D(_MainTex, IN.uv);
				//fixed4 c = fixed4(screenPos.x / 1024, 0, screenPos.y / 768, 1);
				c = lerp(_Color, c, step(screenPos.y / 768, cos(screenPos.x / 1024 * 2 * UNITY_PI + _Time.y * 10) * 0.025 + 0.25 + 1 - _Time.x * 10));
				return c;
			}
			ENDCG
		}
	}
}
