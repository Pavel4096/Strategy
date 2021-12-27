Shader "Selection"
{
    Properties
    {
        _Color ("Selection color", Color) = (1.0, 1.0, 0.0, 0.3)
    }

    SubShader
    {
        Tags {"Queue" = "Transparent"}

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha, Zero One

            HLSLPROGRAM
            #pragma vertex vertexShader
            #pragma fragment fragmentShader

            #include "UnityCG.cginc"

            struct VSData
            {
                float3 position : POSITION;
                float3 normal : NORMAL;
            };

            struct PSData
            {
                float4 position : SV_POSITION;
            };

            const float offset = 0.00001;
            float4 _Color;

            PSData vertexShader(VSData input)
            {
                PSData output;

                float3 newPosition = input.position + input.normal * offset;
                output.position = UnityObjectToClipPos(newPosition);

                return output;
            }

            float4 fragmentShader(PSData input) : SV_TARGET
            {
                return _Color;
            }
            ENDHLSL
        }
    }
}
