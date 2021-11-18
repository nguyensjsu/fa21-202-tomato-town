Shader "Custom/Blink"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _BlinkSpeed("Blink Speed", Int) = 2
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Fog { Mode Off }
        Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
        #pragma surface surf Lambert alpha vertex:vert
        #pragma multi_compile DUMMY PIXELSNAP_ON

        sampler2D _MainTex;
        float _BlinkSpeed;

        struct Input
        {
            float2 uv_MainTex;
            fixed4 color;
        };

        void vert(inout appdata_full v, out Input o)
        {
            #if defined(PIXELSNAP_ON) && !defined(SHADER_API_FLASH)
            v.vertex = UnityPixelSnap(v.vertex);
            #endif
            v.normal = float3(0,0,-1);

            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.color = (1,1,1,1);
        }

        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * IN.color;
            //o.Albedo = lerp(c.rgb,float3(1.0,1.0,1.0),_FlashAmount);
            //o.Albedo = lerp(c.rgb,float3(1.0,1.0,1.0),0);
            //o.Emission = 0;
            //o.Emission = lerp(c.rgb,float3(1.0,1.0,1.0),_FlashAmount) * _SelfIllum;
            if (int(_Time.y*10) % _BlinkSpeed) o.Alpha = 0;
            else o.Alpha = c.a;
        }
        ENDCG
    }
        Fallback "Transparent/VertexLit"
}