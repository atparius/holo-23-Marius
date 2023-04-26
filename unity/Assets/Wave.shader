
Shader "Wave"
{
    Properties
    {
        // Rendering options.
        _BaseColor("Base color", Color) = (0.0, 0.0, 0.0, 1.0)
        _HitPoint("Hit Point", Vector) = (0.0, 0.0, 0.0, 1.0)
        _Frequency("Frequence", Vector) = (0.0, 0.0, 0.0, 1.0)
        // Advanced options.
       [Enum(UnityEngine.Rendering.BlendMode)] _SrcBlend("Source Blend", Float) = 1                 // "One"
        [Enum(UnityEngine.Rendering.BlendMode)] _DstBlend("Destination Blend", Float) = 0            // "Zero"
        [Enum(UnityEngine.Rendering.BlendOp)] _BlendOp("Blend Operation", Float) = 0                 // "Add"
        [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("Depth Test", Float) = 4                // "LessEqual"
        [Enum(DepthWrite)] _ZWrite("Depth Write", Float) = 1                                         // "On"
        _ZOffsetFactor("Depth Offset Factor", Float) = 50
        _ZOffsetUnits("Depth Offset Units", Float) = 100
        [Enum(UnityEngine.Rendering.ColorWriteMask)] _ColorWriteMask("Color Write Mask", Float) = 15 // "All"
        [Enum(UnityEngine.Rendering.CullMode)] _CullMode("Cull Mode", Float) = 2                     // "Back"
        _RenderQueueOverride("Render Queue Override", Range(-1.0, 5000)) = -1
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }

        Pass
        {
            Name "Main"
            Blend[_SrcBlend][_DstBlend]
            BlendOp[_BlendOp]
            ZTest[_ZTest]
            ZWrite[_ZWrite]
            Cull[_CullMode]
            Offset[_ZOffsetFactor],[_ZOffsetUnits]
            ColorMask[_ColorWriteMask]

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _BaseColor;
            float3 _HitPoint;
            float3 _Frequency;

            struct v2f
            {
                float4 viewPos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            v2f vert(appdata_base v)
            { 
                UNITY_SETUP_INSTANCE_ID(v);
                v2f o;
                o.viewPos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(UNITY_MATRIX_M, v.vertex).xyz;
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                return o;
            }


            float4 frag(v2f i) : COLOR
            {
               float dist = distance(_HitPoint,i.worldPos);
               float value = (sin(dist*_Frequency)+0.99)*100;

                return float4(value,value,value,1);
            }

            ENDHLSL
        }
    }
}
