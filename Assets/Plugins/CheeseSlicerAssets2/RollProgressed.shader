Shader "Custom/BzKovSoft/RollProgressed_URP"
{
    Properties
    {
        [HDR] _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _Tess ("Tessellation", Range(1,32)) = 4
        _UpDir ("Up Dir", Vector) = (1,0,0,0)
        _MeshTop ("Mesh Top", Range(0,1)) = 0.5
        _RollDir ("Roll Dir", Vector) = (0,1,0,0)
        _Radius ("Radius", Range(0,1)) = 0
        _Deviation ("Deviation", Range(0,1)) = 0.5
        _PointX ("PointX", Range(0,1)) = 0
        _PointY ("PointY", Range(0,1)) = 0
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
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _Color;
            float4 _UpDir;
            float _MeshTop;
            float4 _RollDir;
            float _Radius;
            float _Deviation;
            float _PointX;
            float _PointY;
            sampler2D _MainTex;
            sampler2D _BumpMap;

            v2f vert (appdata_t v)
            {
                v2f o;
                float3 v0 = v.vertex.xyz;
                float3 upDir = normalize(_UpDir.xyz);
                float3 rollDir = normalize(_RollDir.xyz);

                float y = _PointY;
                float dP = dot(v0 - upDir * y, upDir);
                dP = max(0, dP);
                float3 fromInitialPos = upDir * dP;
                v0 -= fromInitialPos;

                float radius = _Radius + _Deviation * max(0, -(y - _MeshTop));
                float length = 2 * UNITY_PI * (radius - _Deviation * max(0, -(y - _MeshTop)) / 2);
                float r = dP / max(0, length);
                float a = 2 * r * UNITY_PI;

                float s = sin(a);
                float c = cos(a);
                float one_minus_c = 1.0 - c;

                float3 axis = normalize(cross(upDir, rollDir));
                float3x3 rot_mat = 
                {
                    one_minus_c * axis.x * axis.x + c, one_minus_c * axis.x * axis.y - axis.z * s, one_minus_c * axis.z * axis.x + axis.y * s,
                    one_minus_c * axis.x * axis.y + axis.z * s, one_minus_c * axis.y * axis.y + c, one_minus_c * axis.y * axis.z - axis.x * s,
                    one_minus_c * axis.z * axis.x - axis.y * s, one_minus_c * axis.y * axis.z + axis.x * s, one_minus_c * axis.z * axis.z + c
                };
                float3 cycleCenter = rollDir * _PointX + rollDir * radius + upDir * y;

                float3 fromCenter = v0.xyz - cycleCenter;
                float3 shiftFromCenterAxis = cross(axis, fromCenter);
                shiftFromCenterAxis = cross(shiftFromCenterAxis, axis);
                shiftFromCenterAxis = normalize(shiftFromCenterAxis);
                fromCenter -= shiftFromCenterAxis * _Deviation * dP;

                v0.xyz = mul(rot_mat, fromCenter) + cycleCenter;

                o.vertex = UnityObjectToClipPos(v0);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 c = tex2D (_MainTex, i.uv) * _Color;
                return c;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
