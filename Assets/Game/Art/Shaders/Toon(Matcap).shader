// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/Toon(Matcap)"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_ShadingColor("ShadingColor", Color) = (0,0,0,1)
		_MainTex("MainTex", 2D) = "white" {}
		_Emission("Emission", Range( 0 , 1)) = 0
		_RampThreshold("RampThreshold", Range( 0 , 1)) = 0.5
		_RampSmooth("RampSmooth", Range( 0 , 1)) = 0.1
		_Matcap("Matcap", 2D) = "white" {}
		_Matcap_Intensity("Matcap_Intensity", Range( 0 , 1)) = 0
		_ShadowColor("ShadowColor", Color) = (1,1,1,1)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "UnityShaderVariables.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
			float3 viewDir;
			float3 worldNormal;
		};

		struct SurfaceOutputCustomLightingCustom
		{
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half Metallic;
			half Smoothness;
			half Occlusion;
			half Alpha;
			Input SurfInput;
			UnityGIInput GIData;
		};

		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform float4 _ShadingColor;
		uniform float4 _Color;
		uniform float _RampThreshold;
		uniform float _RampSmooth;
		uniform float _Emission;
		uniform float _Matcap_Intensity;
		uniform sampler2D _Matcap;
		uniform float4 _ShadowColor;

		inline half4 LightingStandardCustomLighting( inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi )
		{
			UnityGIInput data = s.GIData;
			Input i = s.SurfInput;
			half4 c = 0;
			#ifdef UNITY_PASS_FORWARDBASE
			float ase_lightAtten = data.atten;
			if( _LightColor0.a == 0)
			ase_lightAtten = 0;
			#else
			float3 ase_lightAttenRGB = gi.light.color / ( ( _LightColor0.rgb ) + 0.000001 );
			float ase_lightAtten = max( max( ase_lightAttenRGB.r, ase_lightAttenRGB.g ), ase_lightAttenRGB.b );
			#endif
			#if defined(HANDLE_SHADOWS_BLENDING_IN_GI)
			half bakedAtten = UnitySampleBakedOcclusion(data.lightmapUV.xy, data.worldPos);
			float zDist = dot(_WorldSpaceCameraPos - data.worldPos, UNITY_MATRIX_V[2].xyz);
			float fadeDist = UnityComputeShadowFadeDistance(data.worldPos, zDist);
			ase_lightAtten = UnityMixRealtimeAndBakedShadows(data.atten, bakedAtten, UnityComputeShadowFade(fadeDist));
			#endif
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float4 tex2DNode73 = tex2D( _MainTex, uv_MainTex );
			float4 lerpResult138 = lerp( tex2DNode73 , ( tex2DNode73 * _Color ) , _Color.a);
			float3 normalizeResult118 = normalize( ( float3(0,1,0) + i.viewDir ) );
			float3 ase_worldNormal = i.worldNormal;
			float dotResult69 = dot( normalizeResult118 , ase_worldNormal );
			float smoothstepResult120 = smoothstep( _RampThreshold , ( _RampThreshold + _RampSmooth ) , (dotResult69*0.5 + 0.5));
			float4 lerpResult119 = lerp( ( tex2DNode73 * _ShadingColor ) , lerpResult138 , ( 1.0 - ( _ShadingColor.a * ( 1.0 - smoothstepResult120 ) ) ));
			float4 temp_output_80_0 = ( ( lerpResult119 + ( lerpResult119 * _Emission ) ) + ( ( ( 1.0 - _Emission ) * _Matcap_Intensity ) * tex2D( _Matcap, ((mul( unity_WorldToCamera, float4( ase_worldNormal , 0.0 ) ).xyz).xy*0.5 + 0.5) ) ) );
			float lerpResult114 = lerp( 1.0 , ase_lightAtten , _ShadowColor.a);
			float4 lerpResult111 = lerp( ( temp_output_80_0 * _ShadowColor ) , temp_output_80_0 , lerpResult114);
			c.rgb = saturate( lerpResult111 ).rgb;
			c.a = 1;
			return c;
		}

		inline void LightingStandardCustomLighting_GI( inout SurfaceOutputCustomLightingCustom s, UnityGIInput data, inout UnityGI gi )
		{
			s.GIData = data;
		}

		void surf( Input i , inout SurfaceOutputCustomLightingCustom o )
		{
			o.SurfInput = i;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf StandardCustomLighting keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.viewDir = worldViewDir;
				surfIN.worldNormal = IN.worldNormal;
				SurfaceOutputCustomLightingCustom o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputCustomLightingCustom, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18500
504;80;878;748;4741.958;4179.91;7.440907;True;False
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;115;-2595.773,-212.0294;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;116;-2532.145,-541.1635;Inherit;False;Constant;_Vector0;Vector 0;7;0;Create;True;0;0;False;0;False;0,1,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode;117;-2335.461,-378.2471;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NormalizeNode;118;-2173.646,-359.5339;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WorldNormalVector;68;-2351.757,63.67435;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;121;-1820.637,-356.9443;Inherit;False;Property;_RampThreshold;RampThreshold;4;0;Create;True;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;122;-1833.232,-281.3756;Inherit;False;Property;_RampSmooth;RampSmooth;5;0;Create;True;0;0;False;0;False;0.1;0.1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;69;-1973.791,-95.01416;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;123;-1484.778,-289.7723;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;70;-1640.543,-131.2557;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0.5;False;2;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;120;-1248.272,-234.9879;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;73;-1545.544,-1308.327;Inherit;True;Property;_MainTex;MainTex;2;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldToCameraMatrix;55;-2092.241,564.9913;Inherit;False;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.ColorNode;74;-1576.477,-871.0992;Inherit;False;Property;_Color;Color;0;0;Create;True;0;0;False;0;False;1,1,1,1;0.7406262,0.681,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;128;-1049.129,-239.9636;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;71;-1529.282,-482.0835;Inherit;False;Property;_ShadingColor;ShadingColor;1;0;Create;True;0;0;False;0;False;0,0,0,1;0.4485334,0.4198113,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;125;-937.0685,-384.5083;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;56;-1967.895,673.3144;Inherit;False;2;2;0;FLOAT4x4;0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;140;-1212.457,-888.9434;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;127;-815.5526,-409.6146;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;77;-645.6926,-457.9146;Inherit;False;Property;_Emission;Emission;3;0;Create;True;0;0;False;0;False;0;0.304;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;138;-961.8742,-818.5493;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;137;-1101.197,-578.97;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ComponentMaskNode;57;-1675.178,832.0155;Inherit;False;True;True;False;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;119;-753.4194,-665.065;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;58;-1379.69,822.0996;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT;0.5;False;2;FLOAT;0.5;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;97;-338.1432,-16.49026;Inherit;False;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;84;-1295.229,305.5981;Inherit;False;Property;_Matcap_Intensity;Matcap_Intensity;7;0;Create;True;0;0;False;0;False;0;0.192;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;51;-939.262,568.8572;Inherit;True;Property;_Matcap;Matcap;6;0;Create;True;0;0;False;0;False;-1;None;eb87cd85996e6425ab8822691874f5d9;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;130;-197.353,-689.956;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;98;-281.2062,217.1478;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;85;-114.1616,282.062;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;129;-22.09099,-764.6533;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;110;190.2106,347.6188;Inherit;False;Property;_ShadowColor;ShadowColor;8;0;Create;True;0;0;False;0;False;1,1,1,1;0,0.2715657,0.4528302,0.3607843;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LightAttenuation;100;1.402496,168.4716;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;80;265.0238,-345.3408;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;101;575.3983,221.0013;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;114;387.9609,161.6746;Inherit;False;3;0;FLOAT;1;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;111;701.2221,-81.79791;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;67;-2370.512,-190.2273;Inherit;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SaturateNode;109;884.0313,-77.3847;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;103;1080.32,-308.3655;Float;False;True;-1;2;ASEMaterialInspector;0;0;CustomLighting;Custom/Toon(Matcap);False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;117;0;116;0
WireConnection;117;1;115;0
WireConnection;118;0;117;0
WireConnection;69;0;118;0
WireConnection;69;1;68;0
WireConnection;123;0;121;0
WireConnection;123;1;122;0
WireConnection;70;0;69;0
WireConnection;120;0;70;0
WireConnection;120;1;121;0
WireConnection;120;2;123;0
WireConnection;128;0;120;0
WireConnection;125;0;71;4
WireConnection;125;1;128;0
WireConnection;56;0;55;0
WireConnection;56;1;68;0
WireConnection;140;0;73;0
WireConnection;140;1;74;0
WireConnection;127;0;125;0
WireConnection;138;0;73;0
WireConnection;138;1;140;0
WireConnection;138;2;74;4
WireConnection;137;0;73;0
WireConnection;137;1;71;0
WireConnection;57;0;56;0
WireConnection;119;0;137;0
WireConnection;119;1;138;0
WireConnection;119;2;127;0
WireConnection;58;0;57;0
WireConnection;97;1;77;0
WireConnection;51;1;58;0
WireConnection;130;0;119;0
WireConnection;130;1;77;0
WireConnection;98;0;97;0
WireConnection;98;1;84;0
WireConnection;85;0;98;0
WireConnection;85;1;51;0
WireConnection;129;0;119;0
WireConnection;129;1;130;0
WireConnection;80;0;129;0
WireConnection;80;1;85;0
WireConnection;101;0;80;0
WireConnection;101;1;110;0
WireConnection;114;1;100;0
WireConnection;114;2;110;4
WireConnection;111;0;101;0
WireConnection;111;1;80;0
WireConnection;111;2;114;0
WireConnection;109;0;111;0
WireConnection;103;13;109;0
ASEEND*/
//CHKSM=304F4C284354D8B88DCB272D4D58FBD489554D04