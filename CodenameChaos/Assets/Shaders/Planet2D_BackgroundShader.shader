Shader "Custom/Planet2D_Background"
{
	Properties
	{
		[NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
		_StarPos0 ("Star Position", vector) = (0,0,0,0)
		_StarPos1 ("Star Position", vector) = (0,0,0,0)
		_StarPos2 ("Star Position", vector) = (0,0,0,0)
		_StarPos3 ("Star Position", vector) = (0,0,0,0)
		_StarPos4 ("Star Position", vector) = (0,0,0,0)
		_StarRadius ("Star Radius", float) = 0.1
		_PlanetPos1 ("Planet Pos 1", vector) = (0,0,0,0)
		_PlanetPriColor1 ("Planet Primary Color 1", Color) = (0.2, 0.2, 1, 1)
		_PlanetSecColor1 ("Planet Secondary Color 1", Color) = (0.4, 0.4, 1, 1)
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
			// make fog work
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float4 worldVertex : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _StarPos0;
			float4 _StarPos1;
			float4 _StarPos2;
			float4 _StarPos3;
			float4 _StarPos4;
			float _StarRadius;
			float4 _PlanetPos1;
			float4 _PlanetPriColor1;
			float4 _PlanetSecColor1;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldVertex = mul(unity_ObjectToWorld, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			float EaseCubicInOut(float p)
			{
				if (p < 0.5)
				{
					return 4 * p * p * p;
				}
				else
				{
					float f = ((2 * p) - 2);
					return 0.5f * f * f * f + 1;
				}
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);

				//col.r = i.uv.x;
				// col.g = i.uv.y;
				//col.r = 1;
				//col.g = 1;
				//col.b = 1;
				// col.r = sin(i.worldVertex.x);
				// col.b = sin(i.worldVertex.y);
				//col.g = i.vertex.y;
				//col.b = i.vertex.z;
				
				float3 loopSpacePos = fmod(abs(i.worldVertex.xyz), 2);
				float starBrightness = 0;
				
				{
					float3 starVec = _StarPos0.xyz - loopSpacePos;
					float starDist = length(starVec);
					starBrightness += clamp(1 - EaseCubicInOut(starDist / _StarRadius), 0, 1);
				}
				
				{
					float3 starVec = _StarPos1.xyz - loopSpacePos;
					float starDist = length(starVec);
					starBrightness += clamp(1 - EaseCubicInOut(starDist / _StarRadius), 0, 1);
				}
				
				{
					float3 starVec = _StarPos2.xyz - loopSpacePos;
					float starDist = length(starVec);
					starBrightness += clamp(1 - EaseCubicInOut(starDist / _StarRadius), 0, 1);
				}
				
				{
					float3 starVec = _StarPos3.xyz - loopSpacePos;
					float starDist = length(starVec);
					starBrightness += clamp(1 - EaseCubicInOut(starDist / _StarRadius), 0, 1);
				}
				
				{
					float3 starVec = _StarPos4.xyz - loopSpacePos;
					float starDist = length(starVec);
					starBrightness += clamp(1 - EaseCubicInOut(starDist / _StarRadius), 0, 1);
				}
				
				starBrightness = clamp(starBrightness, 0, 1);
				float4 starColor = float4(starBrightness, starBrightness, starBrightness, 1);
				
				float3 planetVec = _PlanetPos1.xyz - i.worldVertex.xyz;
				float planetDist = length(planetVec);
				float4 atmoColor = lerp(_PlanetPriColor1, _PlanetSecColor1, clamp(1 - EaseCubicInOut(planetDist / 200), 0, 1));
				float4 atmoThickness = clamp(1 - EaseCubicInOut((planetDist-60) / 100), 0, 1);
				
				col = lerp(starColor, atmoColor, atmoThickness);

				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
