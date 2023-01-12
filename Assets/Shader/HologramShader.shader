Shader "Custom/HologramShader"
{
	Properties
	{
		_BaseColor("Color", Color) = (0, 1, 1, 1)
		_BaseTexture("Base (RGB)", 2D) = "white" {}
		_AlphaMask("Alpha Mask (R)", 2D) = "white" {}
		//Alpha Mask Properties
		_AlphaTiling("Alpha Tiling", Float) = 3
		_AlphaScrollSpeed("Alpha Scroll Speed", Range(0, 5.0)) = 1.0
		// Glow
		_GlowIntensity("Glow Intensity", Range(0.01, 1.0)) = 0.5
		// Glitch
		_GlitchSpeed("Glitch Speed", Range(0, 50)) = 50.0
		_GlitchIntensity("Glitch Intensity", Range(0.0, 0.1)) = 0
	}

	SubShader
	{
		Tags{ "Queue" = "Overlay" "IgnoreProjector" = "True" "RenderType" = "Transparent" }

		Pass
		{
			Lighting Off
			ZWrite On
			Blend SrcAlpha One
			Cull Back

			CGPROGRAM

				#pragma vertex vertexFunc
				#pragma fragment fragmentFunc

				# include "UnityCG.cginc"

				struct VertexInput
				{
					float4 vertex : POSITION;
					float2 uv_texture : TEXCOORD0;
					float3 normal : NORMAL;
				};

				struct VertexOutput
				{
					float4 position : SV_POSITION;
					float2 uv_texture : TEXCOORD0;
					float3 positionOfGrab : TEXCOORD1;
					float3 directoryOfView : TEXCOORD2;
					float3 worldNormal : NORMAL;
				};

				fixed4 _BaseColor, _BaseTexture_ST;
				sampler2D _BaseTexture, _AlphaMask;
				half _AlphaTiling, _AlphaScrollSpeed, _GlowIntensity, _GlitchSpeed, _GlitchIntensity;

				VertexOutput vertexFunc(VertexInput DataInput)
				{
					VertexOutput DataOutput;

					//Glitch
					DataInput.vertex.z += sin(_Time.y * _GlitchSpeed * 5 * DataInput.vertex.y) * _GlitchIntensity;

					DataOutput.position = UnityObjectToClipPos(DataInput.vertex);
					DataOutput.uv_texture = TRANSFORM_TEX(DataInput.uv_texture, _BaseTexture);

					//Alpha mask coordinates
					DataOutput.positionOfGrab = UnityObjectToViewPos(DataInput.vertex);

					//Scroll Alpha mask uv_texture
					DataOutput.positionOfGrab.y += _Time * _AlphaScrollSpeed;

					DataOutput.worldNormal = UnityObjectToWorldNormal(DataInput.normal);
					DataOutput.directoryOfView = normalize(UnityWorldSpaceViewDir(DataOutput.positionOfGrab.xyz));

					return DataOutput;
				}

				fixed4 fragmentFunc(VertexOutput DataInput) : SV_Target{

					half currentVertexDirection = (dot(DataInput.positionOfGrab, 1.0) + 1) / 2;

					fixed4 transparencyColor = tex2D(_AlphaMask, DataInput.positionOfGrab.xy * _AlphaTiling);
					fixed4 pixelColor = tex2D(_BaseTexture, DataInput.uv_texture);
					pixelColor.w = transparencyColor.w;

					// Rim Light
					half rimLight = 1.0 - saturate(dot(DataInput.directoryOfView, DataInput.worldNormal));

					return pixelColor * _BaseColor * (rimLight + _GlowIntensity);
				}

			ENDCG
		}
	}
}