Shader "Custom/vertexColor" 
{
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

