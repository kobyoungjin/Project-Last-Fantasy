Shader "Custom/OutLine"
{
	Properties
	{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_OutLineColor("OutLine Color", Color) = (0,0,0,0)          // 색 변경
		_OutLineWidth("OutLine Width", Range(0.001, 0.03)) = 0.03  // 굵기 조절
	}
		SubShader
		{
			Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
			LOD 200

			cull front  // 뒷면만 렌더링
			zwrite off  // zbuffer 끄고
			CGPROGRAM
			#pragma surface surf NoLight vertex:vert noshadow noambient  // 필요없는 환경광 그림자 제거
			#pragma target 3.0  // HLSL 소스를 다른 “셰이더 모델”로 컴파일

			float4 _OutLineColor;
			float _OutLineWidth;

			//vertex를 normal 방향으로 확장
			void vert(inout appdata_full v) 
			{
				v.vertex.xyz += v.normal.xyz * _OutLineWidth;
			}

			struct Input
			{
				float4 color;
			};

			void surf(Input IN, inout SurfaceOutput o)
			{
				// 검은색으로 만들기 때문에 비우고
			}

			// 기본적인 라이팅 구조
			float4 LightingNoLight(SurfaceOutput s, float3 lightDir, float atten){
				return float4(_OutLineColor.rgb, 1);
			}
			ENDCG

			cull back
			zwrite on  // zbuffer 키고
			CGPROGRAM
			#pragma surface surf Lambert
			#pragma target 3.0

			sampler2D _MainTex;
			struct Input
			{
				float2 uv_MainTex;
			};

			void surf(Input IN, inout SurfaceOutput o)
			{
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
				o.Albedo = c.rgb;
				o.Alpha = c.a;
			}
			ENDCG
		}
			FallBack "Diffuse"
}
