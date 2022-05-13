// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "AV/Skybox/Gradient"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,0)
		[HDR]_Color2("Color 2", Color) = (0.1,0.1,0.1,0)
		_Hardness("Hardness", Range( 0 , 10)) = 1
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Background+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow exclude_path:deferred 
		struct Input
		{
			float3 viewDir;
		};

		uniform float4 _Color;
		uniform float4 _Color2;
		uniform float _Hardness;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float smoothstepResult37 = smoothstep( -_Hardness , _Hardness , i.viewDir.y);
			float4 lerpResult7 = lerp( _Color , _Color2 , smoothstepResult37);
			o.Emission = lerpResult7.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18706
770.2858;691.4286;1423;480;1758.645;-13.03741;1.364697;True;False
Node;AmplifyShaderEditor.RangedFloatNode;38;-939.4194,498.2584;Inherit;False;Property;_Hardness;Hardness;3;0;Create;True;0;0;False;0;False;1;1;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode;39;-644.4194,503.2583;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;32;-673.4194,345.2584;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ColorNode;1;-471.3554,40.80949;Inherit;False;Property;_Color;Color;1;0;Create;True;0;0;False;0;False;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;37;-474.4196,395.2584;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;2;-468.7147,215.0163;Inherit;False;Property;_Color2;Color 2;2;1;[HDR];Create;True;0;0;False;0;False;0.1,0.1,0.1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;7;-200.5533,46.30302;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;AV/Skybox/Gradient;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Opaque;;Background;ForwardOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;39;0;38;0
WireConnection;37;0;32;2
WireConnection;37;1;39;0
WireConnection;37;2;38;0
WireConnection;7;0;1;0
WireConnection;7;1;2;0
WireConnection;7;2;37;0
WireConnection;0;2;7;0
ASEEND*/
//CHKSM=C799D7C5CAAFB70A9A36A6346718DF7C32970808