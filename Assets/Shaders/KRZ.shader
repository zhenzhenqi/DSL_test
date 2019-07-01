// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// shadertype=unity
Shader "SMOOOOZE" {
	Properties {
		_Color ("Main Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_MainTex ("Color (RGBA)", 2D) = "white" {}
		_LightCutoff ("Light Cutoff", Range(0.0, 1.0)) = 0.2
		_DustDirection("Dust Direction", Vector) = (0.0,0.0,0.0)
		_DustColor("Dust Color", Color) = (0.0, 0.0, 0.0, 0.0)
		_DustAmount("Dust Amount", Range(0.0, 1.0)) = 0.0
		_AttenScaler("Attenuation Scaler", Range(0.0, 3.0)) = 2.0
       // _FogColor("Fog Color", Color) = (1.0,1.0,1.0,1.0)
     //   _FogHeight("Fog Height", Range(0.0, 10.0)) = 0.0
      _DistorationIntensity ("_Distoration Intensity", Range(0.0,10.0)) = 0.0
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf KRZ fullforwardshadows finalcolor:finalcolor vertex:vert

		sampler2D _MainTex;
		float4 _Color;
		float _LightCutoff;
		float4 _FogColor;
		float _FogHeight;
		fixed4 _DustDirection;
		fixed4 _DustColor;
		fixed _DustAmount;
		float _AttenScaler;
        float _DistorationIntensity;

		struct SurfaceOutputKRZ {
			fixed3 Albedo;
			fixed Alpha;
			fixed3 Emission;
			fixed3 Normal;
			fixed Specular;
		};

		struct Input {
			float2 uv_MainTex;
			//float4 localPos;
			float4 pos;
			float3 normal;
		};
		
		void vert(inout appdata_full v, out Input o){

            float3 lerpedVer = lerp(0, 1, v.vertex);
            v.vertex.z += sin(lerpedVer.x * 20 + _Time.y*20) * _DistorationIntensity;

           
			float4 hpos = UnityObjectToClipPos (v.vertex);
            o.pos = mul(unity_ObjectToWorld, v.vertex);
            o.uv_MainTex = v.texcoord.xy;
            o.normal = v.normal;
		}

		void surf (Input IN, inout SurfaceOutputKRZ o) {
			fixed4 tex = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            float dustDot = -dot(mul((float3x3)unity_WorldToObject, WorldNormalVector(IN, o.Normal)), normalize(_DustDirection.xyz));
			o.Albedo = lerp(tex.rgb, _DustColor, clamp(dustDot,0.0,1.0) * _DustAmount);
			o.Alpha = tex.a;
			o.Emission = fixed3(0.0,0.0,0.0); // Stop DX11 complaining.
		}


		inline fixed4 LightingKRZ (SurfaceOutputKRZ s, fixed3 lightDir, fixed3 viewDir, fixed atten){
			float4 c;
			atten = step(_LightCutoff, atten) * atten;
			c.rgb = (_LightColor0.rgb * s.Albedo) * (atten*_AttenScaler);
           
			c.a = s.Alpha;
			return c;
		}
		
		void finalcolor(Input IN, SurfaceOutputKRZ o, inout fixed4 color){
			#ifndef UNITY_PASS_FORWARDADD
            float lerpValue = clamp(IN.pos.y / _FogHeight, 0, 1);
            color.rgb = lerp (_FogColor.rgb, color.rgb, lerpValue);
            #endif		
        }

		ENDCG
	}
	FallBack "VertexLit"
}