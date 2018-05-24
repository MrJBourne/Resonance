// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/RippleWater" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BumpMap ("Bumpmap", 2D) = "bump" {}
		_Color ("Color", Color) = (1,1,1,1)
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Scale ("Scale", float) = 1
		_Speed ("Speed", float) = 1
		_Frequency ("Frequency", float) = 1

		//prevent these variables from showing up in the inspector
		[HideInInspector]_WaveAmplitude1 ("WaveAmplitude1", float) = 0

  
  [HideInInspector]_Distance1 ("Distance1", float) = 0


	}

	//we can create another subshader to run on older graphics cards if we want 
	SubShader {

		//tags to determine the rendering order and parameters 
		//geometry+1 = after opaquue objects, but before transparent objects
		//forceNoShadowCasting so this will never cast any shadows
		Tags { "RenderType"="Opaque" "Queue"="Geometry+1" "ForceNoShadowCasting"="True" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows vertex:vert
		#pragma target 4.0 //compilation target level

		sampler2D _MainTex;
		sampler2D _BumpMap;
		half _Glossiness;
		half _Metallic;
		float _Scale, _Speed, _Frequency;
		half4 _Color;
		float _WaveAmplitude1, _WaveAmplitude2, _WaveAmplitude3, _WaveAmplitude4, _WaveAmplitude5, _WaveAmplitude6, _WaveAmplitude7, _WaveAmplitude8;
		float _OffsetX1, _OffsetZ1, _OffsetX2, _OffsetZ2, _OffsetX3, _OffsetZ3,_OffsetX4, _OffsetZ4,_OffsetX5, _OffsetZ5,_OffsetX6, _OffsetZ6,_OffsetX7, _OffsetZ7,_OffsetX8, _OffsetZ8;
		float _Distance1, _Distance2 , _Distance3, _Distance4, _Distance5, _Distance6, _Distance7, _Distance8;
		float _xImpact1, _zImpact1, _xImpact2, _zImpact2,_xImpact3, _zImpact3,_xImpact4, _zImpact4,_xImpact5, _zImpact5,_xImpact6, _zImpact6,
		_xImpact7, _zImpact7,_xImpact8, _zImpact8;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float3 customValue;
		};
		
		void vert( inout appdata_full v, out Input o)
		{
		UNITY_INITIALIZE_OUTPUT(Input, o);
		half offsetvert = ((v.vertex.x * v.vertex.x) + (v.vertex.z * v.vertex.z));
		//half offsetvert2 = v.vertex.x + v.vertex.z; //diagonal waves
		//half offsetvert2 = v.vertex.x; //horizontal waves
		
		half value0 = _Scale * sin(_Time.y * _Speed + _Frequency * offsetvert );

		//vertex to world space
		float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
		
		//v.vertex.y += value0; //remove for no waves
		o.customValue += value0;
		
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}


		ENDCG
	} 
	FallBack "Diffuse"
}
