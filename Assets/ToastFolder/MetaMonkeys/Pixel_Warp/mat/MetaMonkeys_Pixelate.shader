// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "MetaMonkeys/Pixel_Warp"
{
	Properties
	{
		[NoScaleOffset]_noisetexture("noise texture", 2D) = "white" {}
		[HDR]_noisecol("noise col", Color) = (0,0,0,0)
		_pixel("pixel", Float) = 0
		_noisepow("noise pow", Float) = 0
		_noisescale("noise scale", Float) = 0
		_distortionintensity("distortion intensity", Range( 0 , 1)) = 0
		_panning("panning", Vector) = (0,0,0,0)
		[Toggle]_transparent("transparent?", Float) = 0
		[Toggle(_RAINBOW_ON)] _rainbow("rainbow?", Float) = 0
		_rainbowhue("rainbow hue", Range( 0 , 1)) = 0
		_rainbowvalue("rainbow value", Float) = 0
		_rainbowsaturation("rainbow saturation", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma shader_feature_local _RAINBOW_ON
		#pragma surface surf Unlit alpha:fade keepalpha 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _noisecol;
		uniform sampler2D _noisetexture;
		uniform float _noisescale;
		uniform float _pixel;
		uniform float2 _panning;
		uniform float _distortionintensity;
		uniform float _noisepow;
		uniform float _rainbowhue;
		uniform float _rainbowsaturation;
		uniform float _rainbowvalue;
		uniform float _transparent;


		float3 HSVToRGB( float3 c )
		{
			float4 K = float4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
			float3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
			return c.z * lerp( K.xxx, saturate( p - K.xxx ), c.y );
		}


		float3 RGBToHSV(float3 c)
		{
			float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
			float4 p = lerp( float4( c.bg, K.wz ), float4( c.gb, K.xy ), step( c.b, c.g ) );
			float4 q = lerp( float4( p.xyw, c.r ), float4( c.r, p.yzx ), step( p.x, c.r ) );
			float d = q.x - min( q.w, q.y );
			float e = 1.0e-10;
			return float3( abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
		}

		struct Gradient
		{
			int type;
			int colorsLength;
			int alphasLength;
			float4 colors[8];
			float2 alphas[8];
		};


		Gradient NewGradient(int type, int colorsLength, int alphasLength, 
		float4 colors0, float4 colors1, float4 colors2, float4 colors3, float4 colors4, float4 colors5, float4 colors6, float4 colors7,
		float2 alphas0, float2 alphas1, float2 alphas2, float2 alphas3, float2 alphas4, float2 alphas5, float2 alphas6, float2 alphas7)
		{
			Gradient g;
			g.type = type;
			g.colorsLength = colorsLength;
			g.alphasLength = alphasLength;
			g.colors[ 0 ] = colors0;
			g.colors[ 1 ] = colors1;
			g.colors[ 2 ] = colors2;
			g.colors[ 3 ] = colors3;
			g.colors[ 4 ] = colors4;
			g.colors[ 5 ] = colors5;
			g.colors[ 6 ] = colors6;
			g.colors[ 7 ] = colors7;
			g.alphas[ 0 ] = alphas0;
			g.alphas[ 1 ] = alphas1;
			g.alphas[ 2 ] = alphas2;
			g.alphas[ 3 ] = alphas3;
			g.alphas[ 4 ] = alphas4;
			g.alphas[ 5 ] = alphas5;
			g.alphas[ 6 ] = alphas6;
			g.alphas[ 7 ] = alphas7;
			return g;
		}


		float4 SampleGradient( Gradient gradient, float time )
		{
			float3 color = gradient.colors[0].rgb;
			UNITY_UNROLL
			for (int c = 1; c < 8; c++)
			{
			float colorPos = saturate((time - gradient.colors[c-1].w) / ( 0.00001 + (gradient.colors[c].w - gradient.colors[c-1].w)) * step(c, (float)gradient.colorsLength-1));
			color = lerp(color, gradient.colors[c].rgb, lerp(colorPos, step(0.01, colorPos), gradient.type));
			}
			#ifndef UNITY_COLORSPACE_GAMMA
			color = half3(GammaToLinearSpaceExact(color.r), GammaToLinearSpaceExact(color.g), GammaToLinearSpaceExact(color.b));
			#endif
			float alpha = gradient.alphas[0].x;
			UNITY_UNROLL
			for (int a = 1; a < 8; a++)
			{
			float alphaPos = saturate((time - gradient.alphas[a-1].y) / ( 0.00001 + (gradient.alphas[a].y - gradient.alphas[a-1].y)) * step(a, (float)gradient.alphasLength-1));
			alpha = lerp(alpha, gradient.alphas[a].x, lerp(alphaPos, step(0.01, alphaPos), gradient.type));
			}
			return float4(color, alpha);
		}


		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 temp_cast_0 = (( _noisescale * 0.2 )).xx;
			float2 uv_TexCoord70 = i.uv_texcoord * temp_cast_0;
			float pixelWidth71 =  1.0f / _pixel;
			float pixelHeight71 = 1.0f / _pixel;
			half2 pixelateduv71 = half2((int)(uv_TexCoord70.x / pixelWidth71) * pixelWidth71, (int)(uv_TexCoord70.y / pixelHeight71) * pixelHeight71);
			float mulTime72 = _Time.y * 0.1;
			float2 temp_output_99_0 = ( mulTime72 * _panning );
			float grayscale115 = Luminance(tex2D( _noisetexture, ( pixelateduv71 + temp_output_99_0 ) ).rgb);
			float4 temp_cast_2 = (_noisepow).xxxx;
			float4 simpleNoise63 = pow( tex2D( _noisetexture, ( pixelateduv71 + -( temp_output_99_0 * float2( 0.5,0.5 ) ) + ( grayscale115 * _distortionintensity ) ) ) , temp_cast_2 );
			Gradient gradient11 = NewGradient( 0, 8, 2, float4( 0, 1, 0.9172413, 0 ), float4( 0.4897566, 1, 0.1397059, 0.123537 ), float4( 1, 0.9730223, 0.02205884, 0.2470588 ), float4( 0.9497944, 0.6202656, 0.1821549, 0.3852903 ), float4( 0.9497944, 0.1821549, 0.1821549, 0.5588312 ), float4( 0.9497944, 0.1821549, 0.8650893, 0.7088273 ), float4( 0.1821549, 0.3145066, 0.9497944, 0.8617685 ), float4( 0.1821549, 0.8545012, 0.9497944, 0.9794156 ), float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
			float3 hsvTorgb16 = RGBToHSV( SampleGradient( gradient11, simpleNoise63.r ).rgb );
			float3 hsvTorgb17 = HSVToRGB( float3(( hsvTorgb16.x + _rainbowhue ),( hsvTorgb16.y + _rainbowsaturation ),( hsvTorgb16.z + _rainbowvalue )) );
			#ifdef _RAINBOW_ON
				float4 staticSwitch119 = float4( hsvTorgb17 , 0.0 );
			#else
				float4 staticSwitch119 = ( _noisecol * simpleNoise63 );
			#endif
			o.Emission = staticSwitch119.rgb;
			float4 temp_cast_7 = (1.0).xxxx;
			float4 lerpResult103 = lerp( temp_cast_7 , simpleNoise63 , _transparent);
			o.Alpha = lerpResult103.r;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18800
318;160;1229;581;3583.322;496.826;3.266277;True;False
Node;AmplifyShaderEditor.CommentaryNode;82;-2672.133,289.7155;Inherit;False;1833.869;749.8772;Comment;22;66;85;109;10;90;84;81;78;88;76;75;115;67;74;71;99;70;72;97;100;118;63;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;109;-2463.745,512.6841;Inherit;False;Property;_noisescale;noise scale;4;0;Create;True;0;0;0;False;0;False;0;3.9;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;118;-2251.319,423.7413;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;100;-2331.513,626.3369;Inherit;False;Constant;_Float0;Float 0;12;0;Create;True;0;0;0;False;0;False;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-2392.41,759.2464;Inherit;False;Property;_pixel;pixel;2;0;Create;True;0;0;0;False;0;False;0;82.14;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;97;-2406.753,292.1402;Inherit;False;Property;_panning;panning;6;0;Create;True;0;0;0;False;0;False;0,0;0,-0.34;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;72;-2096.738,542.1299;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;70;-2075.875,355.5609;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;99;-1878.852,498.7163;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TFHCPixelate;71;-1790.838,353.5193;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;74;-1517.869,352.5713;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;85;-1698.061,775.5865;Inherit;True;Property;_noisetexture;noise texture;0;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;None;cf6b77a025a8362488719b0d6ab3f959;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.SamplerNode;67;-1361.945,339.7155;Inherit;True;Property;_TextureSample0;Texture Sample 0;9;0;Create;True;0;0;0;False;0;False;-1;None;ab668285d368b2d4191c1d3681cb4ac7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;90;-2456.772,846.975;Inherit;False;Property;_distortionintensity;distortion intensity;5;0;Create;True;0;0;0;False;0;False;0;0.196;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCGrayscale;115;-1396.841,519.9511;Inherit;False;0;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;-1858.438,730.9683;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NegateNode;76;-1791.054,651.1703;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;88;-1532.883,505.9496;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;78;-1606.337,629.0684;Inherit;False;3;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;66;-1289.492,847.7723;Inherit;False;Property;_noisepow;noise pow;3;0;Create;True;0;0;0;False;0;False;0;2.39;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;81;-1383.207,590.8993;Inherit;True;Property;_TextureSample1;Texture Sample 1;10;0;Create;True;0;0;0;False;0;False;-1;None;caba922d6e1041849bb872ded4ffa57a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;84;-1048.595,725.1846;Inherit;False;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;61;-797.5289,680.4971;Inherit;False;1282.563;579.1328;Comment;10;11;12;22;16;25;19;24;23;18;17;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;63;-1067.966,538.5797;Inherit;False;simpleNoise;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GradientNode;11;-747.5288,785.7161;Inherit;False;0;8;2;0,1,0.9172413,0;0.4897566,1,0.1397059,0.123537;1,0.9730223,0.02205884,0.2470588;0.9497944,0.6202656,0.1821549,0.3852903;0.9497944,0.1821549,0.1821549,0.5588312;0.9497944,0.1821549,0.8650893,0.7088273;0.1821549,0.3145066,0.9497944,0.8617685;0.1821549,0.8545012,0.9497944,0.9794156;1,0;1,1;0;1;OBJECT;0
Node;AmplifyShaderEditor.GetLocalVarNode;64;-654.3063,567.7531;Inherit;False;63;simpleNoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GradientSampleNode;12;-542.5792,730.4972;Inherit;True;2;0;OBJECT;;False;1;FLOAT;0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;19;-340.9663,940.0023;Inherit;False;Property;_rainbowhue;rainbow hue;9;0;Create;True;0;0;0;False;0;False;0;0.711;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-208.1527,1044.23;Inherit;False;Property;_rainbowsaturation;rainbow saturation;11;0;Create;True;0;0;0;False;0;False;0;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-193.4527,1143.63;Inherit;False;Property;_rainbowvalue;rainbow value;10;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RGBToHSVNode;16;-173.9487,780.5038;Inherit;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.CommentaryNode;62;-560.9651,55.31388;Inherit;False;503.3807;262;Comment;2;28;30;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;24;77.84724,862.23;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;30;-510.9651,105.3139;Inherit;False;Property;_noisecol;noise col;1;1;[HDR];Create;True;0;0;0;False;0;False;0,0,0,0;0,4.296242,0.8997366,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;23;75.84724,960.2302;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;18;67.83379,761.5023;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;104;-5.645062,482.3529;Inherit;False;Property;_transparent;transparent?;7;1;[Toggle];Create;True;0;0;0;False;0;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;117;24.54621,290;Inherit;False;Constant;_Float2;Float 2;11;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-219.5844,173.5525;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;102;-42.86237,393.0121;Inherit;False;63;simpleNoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.HSVToRGBNode;17;229.0335,779.7022;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.StaticSwitch;119;87.8147,136.827;Inherit;False;Property;_rainbow;rainbow?;8;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;103;206.6732,362.739;Inherit;False;3;0;COLOR;1,0,0,0;False;1;COLOR;1,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;101;389.544,175.1198;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;MetaMonkeys/Pixel_Warp;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;118;0;109;0
WireConnection;72;0;100;0
WireConnection;70;0;118;0
WireConnection;99;0;72;0
WireConnection;99;1;97;0
WireConnection;71;0;70;0
WireConnection;71;1;10;0
WireConnection;71;2;10;0
WireConnection;74;0;71;0
WireConnection;74;1;99;0
WireConnection;67;0;85;0
WireConnection;67;1;74;0
WireConnection;115;0;67;0
WireConnection;75;0;99;0
WireConnection;76;0;75;0
WireConnection;88;0;115;0
WireConnection;88;1;90;0
WireConnection;78;0;71;0
WireConnection;78;1;76;0
WireConnection;78;2;88;0
WireConnection;81;0;85;0
WireConnection;81;1;78;0
WireConnection;84;0;81;0
WireConnection;84;1;66;0
WireConnection;63;0;84;0
WireConnection;12;0;11;0
WireConnection;12;1;64;0
WireConnection;16;0;12;0
WireConnection;24;0;16;2
WireConnection;24;1;25;0
WireConnection;23;0;16;3
WireConnection;23;1;22;0
WireConnection;18;0;16;1
WireConnection;18;1;19;0
WireConnection;28;0;30;0
WireConnection;28;1;64;0
WireConnection;17;0;18;0
WireConnection;17;1;24;0
WireConnection;17;2;23;0
WireConnection;119;1;28;0
WireConnection;119;0;17;0
WireConnection;103;0;117;0
WireConnection;103;1;102;0
WireConnection;103;2;104;0
WireConnection;101;2;119;0
WireConnection;101;9;103;0
ASEEND*/
//CHKSM=74601543523CBAF3ED7D5AE3AD643B2B7F47AAF5