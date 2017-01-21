Shader "Custom/MySpriteShader01"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
    }

    SubShader
    {
        Tags
        { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Fog { Mode Off }
        Blend One OneMinusSrcAlpha

        Pass
        {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile DUMMY PIXELSNAP_ON
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
                fixed4 color    : COLOR;
                half2 texcoord  : TEXCOORD0;
            };

            fixed4 _Color;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color;
                #ifdef PIXELSNAP_ON
                OUT.vertex = UnityPixelSnap (OUT.vertex);
                #endif

                return OUT;
            }

            sampler2D _MainTex;
			sampler2D _BlockAtlas;
			fixed4 _BlockType;

            fixed4 frag(v2f IN) : SV_Target
            {
				//fixed4 c = tex2D(_MainTex, IN.texcoord) * IN.color;
                //c.rgb *= c.a;
				fixed4 c = (
					_BlockType.r * tex2D(_BlockAtlas, IN.texcoord * 0.5) + // Top Left
					_BlockType.g * tex2D(_BlockAtlas, half2(IN.texcoord.x * 0.5 + 0.5, IN.texcoord.y * 0.5)) + // Top Right
					_BlockType.b * tex2D(_BlockAtlas, half2(IN.texcoord.x * 0.5, IN.texcoord.y * 0.5 + 0.5)) + // Bottom Left
					_BlockType.a * tex2D(_BlockAtlas, half2(IN.texcoord.x * 0.5 + 0.5, IN.texcoord.y * 0.5 + 0.5)) // Bottom Right
				);
			return c;// tex2D(_BlockAtlas, IN.texcoord);
            }
			ENDCG
        }
    }
}