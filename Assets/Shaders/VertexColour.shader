Shader "Custom/vertexColor" 
{
	Properties
	{
		_Color("Color", Color) = (1, 1, 1, 1)

		[Header(PBR maps)]
		_MainTex("Albedo (RGB), Alpha (A)", 2D) = "white" {}
		_BumpMap("NormalMap", 2D) = "bump" {}
		_MetallicMap("MetallicMap (RGB), Smoothness (A)", 2D) = "white" {}
		_HeightMap("HeightMap", 2D) = "grey" {}
		_OcclusionMap("OcclusionMap", 2D) = "white" {}
		_EmissionMap("EmissionMap (RGB)", 2D) = "black" {}
	}

	SubShader {
		Pass {
		

			CGPROGRAM
			#pragma vertex VShader
			#pragma fragment FShader

			struct vertexInput 
			{
			    float4 color : COLOR;
				float4 vertex : POSITION;
			};

			struct fragmentInput 
			{
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
			};

			fragmentInput VShader (vertexInput vertInput)
			{
				fragmentInput fragInput;
				fragInput.vertex = UnityObjectToClipPos(vertInput.vertex);
				fragInput.color = vertInput.color;
				return fragInput;
			}
			
			fixed4 FShader (fragmentInput fragInput) : COLOR 
			{
				return fragInput.color;
			}
			ENDCG
		}
	}
}

