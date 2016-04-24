Shader "Custom/Wave Shader" {
    Properties {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Velocity ("Velocity", Float) = 5.0
        _Wavelength ("Wavelength", Float) = 0.5
        _Amplitude ("Amplitude", Float) = 1.0
        _TailLength ("Tail Length", Float) = 0.0
        _BoundaryLength ("Boundary Length", Float) = 1.0
        _RefractiveIndex ("Refractive Index", Float) = 1.0
        _TransitionPhase ("Transition Phase", Float) = 0.0
        _Displacement ("Displacement", Vector) = (0.0, 0.0, 0.0, 0.0)
        _Direction ("Direction", Vector) = (0.0, 0.0, 0.0, 0.0)
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
        float _TailLength;
        float _BoundaryLength;
        float _RefractiveIndex;
        float _TransitionPhase;
        float _GroupPhase;
        fixed4 _Color;
        float4 _Displacement;
        float4 _Direction;
       
        struct Input {  
            float2 uv_MainTex;
            INTERNAL_DATA
        };
             
        void setColorToWavelength() {
            float factor, red, green, blue;
            float Gamma = 0.8;
            float IntensityMax = 1.0;
            if ((_Wavelength >= 380) && (_Wavelength<440)){
                red = -(_Wavelength - 440) / (440 - 380);
                green = 0.0;
                blue = 1.0;
            } else if((_Wavelength >= 440) && (_Wavelength<490)){
                red = 0.0;
                green = (_Wavelength - 440) / (490 - 440);
                blue = 1.0;
            } else if((_Wavelength >= 490) && (_Wavelength<510)){
                red = 0.0;
                green = 1.0;
                blue = -(_Wavelength - 510) / (510 - 490);
            } else if((_Wavelength >= 510) && (_Wavelength<580)){
                red = (_Wavelength - 510) / (580 - 510);
                green = 1.0;
                blue = 0.0;
            } else if((_Wavelength >= 580) && (_Wavelength<645)){
                red = 1.0;
                green = -(_Wavelength - 645) / (645 - 580);
                blue = 0.0;
            } else if((_Wavelength >= 645) && (_Wavelength<781)){
                red = 1.0;
                green = 0.0;
                blue = 0.0;
            } else {
                red = 0.0;
                green = 0.0;
                blue = 0.0;
            }
    
            if ((_Wavelength >= 380) && (_Wavelength<420)){
                factor = 0.3 + 0.7*(_Wavelength - 380) / (420 - 380);
            } else if((_Wavelength >= 420) && (_Wavelength<701)) {
                factor = 1.0;
            } else if((_Wavelength >= 701) && (_Wavelength<781)) {
                factor = 0.3 + 0.7*(780 - _Wavelength) / (780 - 700);
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
            float phase = _Time * _Velocity * _RefractiveIndex;
            float4 wpos = mul( _Object2World, v.vertex);
            float4 vertexDisplacement = wpos - _Displacement;
            float offset = (0.25f * dot(_Direction,
                      vertexDisplacement) / length(_Direction));
            float renderWavelength = 0.0015f * _Wavelength - 0.37f;
            
            float locationAmplitude = _Amplitude;
            if (offset > _TailLength / 2.0f - _BoundaryLength) {
                locationAmplitude = (_TailLength / 2.0f) * (_Amplitude / _BoundaryLength) + offset * (-_Amplitude / _BoundaryLength);
            }

            float wavepos = sin((2.0 * 3.14159 / renderWavelength) * (phase + _GroupPhase + offset)
                          + _TransitionPhase)
                                 * locationAmplitude;
            wpos.x += wavepos * normalize(_Direction).z;
            wpos.z += wavepos * -1.0f * normalize(_Direction).x;

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
