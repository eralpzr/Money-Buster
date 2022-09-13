Shader "Custom/Mask Texture"
{
    Properties
    {
        _Color ("Main Color", Color) = (1, 1, 1, 1)
        _MainTex ("Base (RGB) Transparency (A)", 2D) = "" { }
        _Mask ("Culling Mask", 2D) = "white" {}
        _Cutoff ("Alpha cutoff", Range (0,1)) = 0.1
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent" "Queue" = "Transparent"
        }
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        AlphaTest GEqual [_Cutoff]
        Pass
        {

            SetTexture [_Mask] {combine texture}

            SetTexture [_MainTex] { combine texture, previous }

            SetTexture [_MainTex] {
            constantColor [_Color]
            combine previous * constant
            }
        }
    }
}