Shader "Custom/Planet2D_Background"
{
	Properties
	{
		[NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
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
			
			float rand(float co) { return frac(sin(co*(91.3458)) * 47453.5453); }
			
			float hash( float n )
			{
				return frac(sin(n)*43758.5453);
			}
			
			float noise( float3 x )
			{
				// The noise function returns a value in the range -1.0f -> 1.0f
				
				float3 p = floor(x);
				float3 f = frac(x);
				
				f       = f*f*(3.0-2.0*f);
				float n = p.x + p.y*57.0 + 113.0*p.z;
				
				return lerp(lerp(lerp( hash(n+0.0), hash(n+1.0),f.x),
							   lerp( hash(n+57.0), hash(n+58.0),f.x),f.y),
						   lerp(lerp( hash(n+113.0), hash(n+114.0),f.x),
							   lerp( hash(n+170.0), hash(n+171.0),f.x),f.y),f.z);
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				
				float3 loopSpacePos = float3(fmod(abs(i.worldVertex.x), 8), fmod(abs(i.worldVertex.y), 8), fmod(abs(i.worldVertex.z), 8));
				int loopSpaceGridX = floor(i.worldVertex.x / 8);
				int loopSpaceGridY = floor(i.worldVertex.y / 8);
				if (i.worldVertex.x < 0) { loopSpacePos.x = 8 - loopSpacePos.x; }
				if (i.worldVertex.y < 0) { loopSpacePos.y = 8 - loopSpacePos.y; }
				if (i.worldVertex.z < 0) { loopSpacePos.z = 8 - loopSpacePos.z; }
				float starBrightness = 0;
				
				float2 starPositions[5] = {
					float2(0.2 + rand((float)loopSpaceGridX + loopSpaceGridY*3  + 0.0)*7.6, 0.2 + rand((float)loopSpaceGridY + loopSpaceGridX*31 + 0.0)*7.6),
					float2(0.2 + rand((float)loopSpaceGridX + loopSpaceGridY*7  + 0.1)*7.6, 0.2 + rand((float)loopSpaceGridY + loopSpaceGridX*17 + 0.1)*7.6),
					float2(0.2 + rand((float)loopSpaceGridX + loopSpaceGridY*13 + 0.2)*7.6, 0.2 + rand((float)loopSpaceGridY + loopSpaceGridX*7  + 0.2)*7.6),
					float2(0.2 + rand((float)loopSpaceGridX + loopSpaceGridY*17 + 0.3)*7.6, 0.2 + rand((float)loopSpaceGridY + loopSpaceGridX*13 + 0.3)*7.6),
					float2(0.2 + rand((float)loopSpaceGridX + loopSpaceGridY*31 + 0.4)*7.6, 0.2 + rand((float)loopSpaceGridY + loopSpaceGridX*3  + 0.4)*7.6)
				};
				
				for (int sIndex = 0; sIndex < 5; sIndex++)
				{
					float2 starDirVec = starPositions[sIndex] - loopSpacePos;
					float starDir = atan2(starDirVec.y, starDirVec.x) + sIndex*0.33;
					float numPoints = 6; //lerp(3, 8, (1 + sin(_Time.y))/2);
					float starRadius = _StarRadius * rand((float)sIndex)*3.0 * (0.6 + (1 + cos(starDir*numPoints))/2 * 0.9);
					float starLumin = 0.1 + rand((float)sIndex+loopSpaceGridX*13+loopSpaceGridY*3+5)*0.9;
					starLumin *= 0.3 + (1 + sin(_Time.y*4 + sIndex))/2 * 0.7;
					starBrightness += clamp(1 - EaseCubicInOut(length(starDirVec) / starRadius), 0, starLumin);
				}
				
				starBrightness = clamp(starBrightness, 0, 1);
				float4 starColor = float4(starBrightness, starBrightness, starBrightness, 1);
				starColor.r *= 0.4 + noise(i.worldVertex.xyz/17)*0.6;
				starColor.g *= 0.4 + noise(i.worldVertex.xyz/13)*0.6;
				starColor.b *= 0.4 + noise(i.worldVertex.xyz/7)*0.6;
				
				float3 planetVec = _PlanetPos1.xyz - i.worldVertex.xyz;
				float planetDist = length(planetVec);
				float4 atmoColor = lerp(_PlanetPriColor1, _PlanetSecColor1, clamp(1 - EaseCubicInOut(planetDist / 200), 0, 1));
				float4 atmoThickness = clamp(1 - EaseCubicInOut((planetDist-60) / 100), 0, 1);
				
				col = lerp(starColor, atmoColor, atmoThickness);
				// col.r = rand((float)loopSpaceGridX);
				// if ((loopSpaceGridX+loopSpaceGridY) % 2 == 0) { col = float4(0, 1, 0, 1); }
				// if (loopSpacePos.x < 0.05) { col = float4(1, 0, 0, 1); }
				// if (loopSpacePos.y < 0.05) { col = float4(1, 0, 0, 1); }
				
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
