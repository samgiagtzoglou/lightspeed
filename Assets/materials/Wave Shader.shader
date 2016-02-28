Shader "Custom/Wave Shader" {
    Properties {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Velocity ("Velocity", Float) = 10.0
        _Wavelength ("Wavelength", Float) = 10.0
        _Amplitude ("Amplitude", Float) = 2.0
        _RefractiveIndex ("Refractive Index", Float) = 1.0
        _TransitionPhase ("Transition Phase", Float) = 0.0
        _Displacement ("Displacement", Float) = 0.0
        _GroupPhase ("Group Phase", Float) = 0.0
    }
    SubShader {
        Tags { "RenderType"="Opaque" "IgnoreProjector"="True" }
        LOD 300

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        float _Velocity;
        float _Wavelength;
        float _Amplitude;
        float _RefractiveIndex;
        float _TransitionPhase;
        float _Displacement;
        float _GroupPhase;
        fixed4 _Color;
       
        struct Input {  
            float2 uv_MainTex;
            INTERNAL_DATA
        };
             
        void setColorToWavelength() {
            float factor, red, green, blue;
            float Gamma = 0.8;
            float IntensityMax = 1.0;
            float scaledWavelength = (((_Wavelength - .485) * 13.79) + 0.380) * 1000;
            if ((scaledWavelength >= 380) && (scaledWavelength<440)){
                red = -(scaledWavelength - 440) / (440 - 380);
                green = 0.0;
                blue = 1.0;
            } else if((scaledWavelength >= 440) && (scaledWavelength<490)){
                red = 0.0;
                green = (scaledWavelength - 440) / (490 - 440);
                blue = 1.0;
            } else if((scaledWavelength >= 490) && (scaledWavelength<510)){
                red = 0.0;
                green = 1.0;
                blue = -(scaledWavelength - 510) / (510 - 490);
            } else if((scaledWavelength >= 510) && (scaledWavelength<580)){
                red = (scaledWavelength - 510) / (580 - 510);
                green = 1.0;
                blue = 0.0;
            } else if((scaledWavelength >= 580) && (scaledWavelength<645)){
                red = 1.0;
                green = -(scaledWavelength - 645) / (645 - 580);
                blue = 0.0;
            } else if((scaledWavelength >= 645) && (scaledWavelength<781)){
                red = 1.0;
                green = 0.0;
                blue = 0.0;
            } else {
                red = 0.0;
                green = 0.0;
                blue = 0.0;
            }
    
            if ((scaledWavelength >= 380) && (scaledWavelength<420)){
                factor = 0.3 + 0.7*(scaledWavelength - 380) / (420 - 380);
            } else if((scaledWavelength >= 420) && (scaledWavelength<701)) {
                factor = 1.0;
            } else if((scaledWavelength >= 701) && (scaledWavelength<781)) {
                factor = 0.3 + 0.7*(780 - scaledWavelength) / (780 - 700);
            } else {
                factor = 0.0;
            }
    
            if (red != 0){
                red = IntensityMax * pow(red * factor, Gamma);
            }
            if (green != 0){
                green = IntensityMax * pow(green * factor, Gamma);
            }
            if (blue != 0){
                blue = IntensityMax * pow(blue * factor, Gamma);
            }
    
            _Color = fixed4(red, green, blue, 1.0);
        }

        void vert (inout appdata_full v) {
             float phase = (float) _Time * _Velocity * _RefractiveIndex * 0.1;
             float4 wpos = mul( _Object2World, v.vertex);
             float offset = _GroupPhase + (wpos.x - _Displacement);
             wpos.z += sin((2.0 * 3.14159 / _Wavelength) * (phase + offset) + _TransitionPhase) * _Amplitude;
             v.vertex = mul(_World2Object, wpos);
        }
    
        void surf (Input IN, inout SurfaceOutputStandard o) {
             // Albedo comes from a texture tinted by color
             setColorToWavelength();
             fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
             o.Albedo = c.rgb;
             o.Alpha = c.a;
        }
        ENDCG
    }
}
