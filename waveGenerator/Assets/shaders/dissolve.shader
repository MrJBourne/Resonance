Shader "Unlit/dissolve"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_DisolveTexture("Disolve Texture", 2D) = "white" {}
		_DisolveY("Current Y", Float) = 0
		_DisolveSize("dissolve size", Float) = 2
		_StartingY("starting Y", Float) = -10
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;

			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 worldPos : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _DisolveTexture;
			float _DisolveY;
			float _DisolveSize;
			float _StartingY;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

				return o;
			}

			//loop through the frag shader
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				float transition = _DisolveY - i.worldPos.y;

				//clip is only going to render the pixels we tell it to
				//clip(1-(i.vertex.y % 2));//if clip number i < 0, dont render pixel

				clip(_StartingY + (transition)+ (tex2D(_DisolveTexture, i.uv)*_DisolveSize)); 

				//lip(1-((i.vertex.x * i.vertex.x) + (i.vertex.z * i.vertex.z)));
				//((v.vertex.x * v.vertex.x) + (v.vertex.z * v.vertex.z))

				return col;
			}
			ENDCG
		}
	}
}
