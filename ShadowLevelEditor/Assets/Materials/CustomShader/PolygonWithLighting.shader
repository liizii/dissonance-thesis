Shader "Custom/PolygonWithLighting" {
    SubShader {
      Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
      Blend SrcAlpha OneMinusSrcAlpha
      CGPROGRAM
      #pragma surface surf SimpleLambert alpha
      struct Input {
          float4 color : COLOR;
      };
      
      half4 LightingSimpleLambert (SurfaceOutput s, half3 lightDir, half atten) {
              half NdotL = dot (s.Normal, lightDir);
              half4 c;
              c.rgb = s.Albedo;
              c.a = saturate(NdotL * atten);
              return c;
          }
      
      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = .2f;
      }
      ENDCG
    }
    Fallback "Diffuse"
  }