// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/NewSurfaceShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_BumpMap ("Bumpmap", 2D) = "bump" {} 
		_Emission("Emission", Range(0,1)) = 0.0
		//_Transparency ("Transparency", Range(0.0, 0.5)) = 0.25

		//new variables
		//_Scale ("Scale", float) = 1
		_Scale ("Scale", Range(3, 10)) = 3
		_Speed ("Speed", float) = 1
		_Freq ("Freq", float) = 1
				}

	//We can create another subshader to run on older other graphics cards if we want
	SubShader {

		//tags to determine the rendering order and parameters 
		//geometry+1 = after opaquue objects, but before transparent objects
		//forceNoShadowCasting so this will never cast any shadows
		Tags { "RenderType" = "Transparent" "Queue"="Geometry+1" "ForceNoShadowCasting"="True"}
		LOD 200
		//"Transparent" "Queue"

		//not to render to the depth buffer, for transparent objects
		//Zwrite off
		//Blend srcAlpha OneMinusSrcAlpha 

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard vertex:vert// fullforwardshadows
		//#pragma surface serf Lambert vertex:vert 
		//compilation target level
		// Use shader model 3.0 target, to get nicer looking lighting
		// level 3.5 and above for XB1/PS4 and stuff
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		float _Scale, _Speed, _Freq;

		float _Speed1, _Speed2, _Speed3, _Speed4, _Speed5;
		float _Scale1,  _Scale2,  _Scale3,  _Scale4,  _Scale5;
		float _Freq1, _Freq2, _Freq3, _Freq4, _Freq5;

		float _OffsetX1,  _OffsetX2,  _OffsetX3,  _OffsetX4,  _OffsetX5;
		float _OffsetZ1, _OffsetZ2,  _OffsetZ3,  _OffsetZ4,  _OffsetZ5;
		float _Distance1, _Distance2, _Distance3, _Distance4, _Distance5;
		float _Wave1, _Wave2, _Wave3, _Wave4, _Wave5;
		float _Time1, _Time2, _Time3, _Time4, _Time5;

		float _xImpact1, _zImpact1, _xImpact2, _zImpact2, _xImpact3, _zImpact3, _xImpact4, _zImpact4, _xImpact5, _zImpact5;
		//float _xImpact, _zImpact;

		struct Input 
		{
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float3 customValue;
		};

		half _Glossiness;
		half _Metallic;
		half _Emission;
		fixed4 _Color;

		//for adjusting the vertices
		void vert (inout appdata_full v, out Input o)
		{

		UNITY_INITIALIZE_OUTPUT(Input, o);

		//Circular wave modification along the vertices 
		half offsetvert = ((v.vertex.x * v.vertex.x) + (v.vertex.z * v.vertex.z));

		//constant ripple from the center of the plane
		//half value0 = _Scale * sin(_Time.w * _Speed + _Freq * offsetvert);

		//5 different values with diffferent offsets so we can have multiple waves, this will create our cymatics effect
		half value1 = _Scale1 * sin(_Time1 * _Speed + _Freq1 * offsetvert + (v.vertex.x * _OffsetX1) + (v.vertex.z * _OffsetZ1));
		half value2 = _Scale2 * sin(_Time2 * _Speed + _Freq2 * offsetvert + (v.vertex.x * _OffsetX2) + (v.vertex.z * _OffsetZ2));
		half value3 = _Scale3 * sin(_Time3 * _Speed + _Freq3 * offsetvert + (v.vertex.x * _OffsetX3) + (v.vertex.z * _OffsetZ3));
		half value4 = _Scale4 * sin(_Time4 * _Speed + _Freq4 * offsetvert + (v.vertex.x * _OffsetX4) + (v.vertex.z * _OffsetZ4));
		half value5 = _Scale5 * sin(_Time5 * _Speed + _Freq5 * offsetvert + (v.vertex.x * _OffsetX5) + (v.vertex.z * _OffsetZ5));

		//world space coordinates vertices
		float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;


		if(sqrt(pow(worldPos.x - _xImpact1, 2) + pow(worldPos.z - _zImpact1, 2)) < _Distance1)
			{
		//add sum to values
		v.vertex.y += (value1 * _Wave1);
		v.normal.y += (value1 * _Wave1);
		o.customValue += (value1 * _Wave1);

			} 

			if(sqrt(pow(worldPos.x - _xImpact2, 2) + pow(worldPos.z - _zImpact2, 2)) < _Distance2)
			{
		//add sum to values
		v.vertex.y += (value2 * _Wave2);
		v.normal.y += (value2 * _Wave2);
		o.customValue += (value2 * _Wave2);

			}

			  if(sqrt(pow(worldPos.x - _xImpact3, 2) + pow(worldPos.z - _zImpact3, 2)) < _Distance3)
			{
		//add sum to values
		v.vertex.y += (value3 * _Wave3);
		v.normal.y += (value3 * _Wave3);
		o.customValue += (value3 * _Wave3);

			} 

			if(sqrt(pow(worldPos.x - _xImpact4, 2) + pow(worldPos.z - _zImpact4, 2)) < _Distance4)
			{
		//add sum to values
		v.vertex.y += (value4 * _Wave4);
		v.normal.y += (value4 * _Wave4);
		o.customValue += (value4 * _Wave4);

			} 

			if(sqrt(pow(worldPos.x - _xImpact5, 2) + pow(worldPos.z - _zImpact5, 2)) < _Distance5)
			{
		//add sum to values
		v.vertex.y += (value5 * _Wave5);
		v.normal.y += (value5 * _Wave5);
		o.customValue += (value5 * _Wave5);

			}
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;// * _Transparency;
			o.Albedo = c.rgb;

			//thought i could make this an emissive material, this way i wouldnt need to add another light source... doesnt work, maybe because it's a 2D surface?
			//c.rgb + o.Emission;

			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;

			//c.a = _Transparency;
			//o.Emission = c.customValue;// * tex2D(_Illum, IN.uv_Illum).a;
			//o.Emission = _Emission;
			//o.Emission = c.rgb * tex2D(_Illum, IN.uv_Illum).a;
			//o.Alpha = c.a;

			//Since were changing a 2D mesh, it is not affected by lighting, so we need to change the normals ourself
			//we need these normals changed to affect the lighting
			o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap) * 0.2);
			o.Normal.y = IN.customValue;//30;

			}

		ENDCG
	}
	FallBack "Diffuse"
}
