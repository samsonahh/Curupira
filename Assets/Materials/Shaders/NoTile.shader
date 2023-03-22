Shader "Custom/NoTile"
    {
 
    Properties{
    _MainTex ("MainTex", 2D) = "white" {}
    }
 
    SubShader
    {
    Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
 
    Pass
    {
    ZWrite Off
    Blend SrcAlpha OneMinusSrcAlpha
 
    CGPROGRAM
    #pragma vertex vert
    #pragma fragment frag
    #include "UnityCG.cginc"
 
    struct VertexInput {
    fixed4 vertex : POSITION;
    fixed2 uv:TEXCOORD0;
    fixed4 tangent : TANGENT;
    fixed3 normal : NORMAL;
    //VertexInput
    };
 
 
    struct VertexOutput {
    fixed4 pos : SV_POSITION;
    fixed2 uv:TEXCOORD0;
    //VertexOutput
    };
 
    //Variables
sampler2D _MainTex;
 
    // The MIT License
// Copyright © 2015 Inigo Quilez
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, fmodify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions: The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software. THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 
 
// One way to avoid tex2D tile repetition one using one small tex2D to cover a huge area.
// Based on Voronoise (https://www.shadertoy.com/view/Xd23Dh), a random offset is applied to
// the tex2D UVs per Voronoi cell. Distance to the cell is used to smooth the transitions
// between cells.
 
// More info here: http://www.iquilezles.org/www/articles/tex2Drepetition/tex2Drepetition.htm
 
 
fixed4 hash4( fixed2 p ) { return frac(sin(fixed4( 1.0+dot(p,fixed2(37.0,17.0)),
                                              2.0+dot(p,fixed2(11.0,47.0)),
                                              3.0+dot(p,fixed2(41.0,29.0)),
                                              4.0+dot(p,fixed2(23.0,31.0))))*103.0); }
 
 
fixed3 tex2DNoTile( sampler2D samp, in fixed2 uv, fixed v )
{
    fixed2 p = floor( uv );
    fixed2 f = frac( uv );
 
    // derivatives (for correct mipmapping)
    fixed2 _ddx = ddx( uv );
    fixed2 _ddy = ddy( uv );
 
    fixed3 va = fixed3(0.0,0.0,0.0);
    fixed w1 = 0.01;
    fixed w2 = 0.01;
    [unroll(100)]
for( int j=-1; j<=1; j++ )
    [unroll(100)]
for( int i=-1; i<=1; i++ )
    {
        fixed2 g = fixed2( fixed(i),fixed(j) );
        fixed4 o = hash4( p + g );
        fixed2 r = g - f + o.xy;
        fixed d = dot(r,r);
        fixed w = exp(-5.0*d );
        fixed3 c = tex2D( samp, uv + v*o.zw, _ddx, _ddy ).xyz;
        va += w*c;
        w1 += w;
        w2 += w*w;
    }
 
    // normal averaging --> lowers contrasts
    //return va/w1;
 
    // contrast preserving average
    fixed mean = 0;// tex2DGrad( samp, uv, ddx*16.0, ddy*16.0 ).x;
    fixed3 res = mean + (va-w1*mean)/sqrt(w2);
    return lerp( va/w1, res, v );
}
 
 
 
 
 
    VertexOutput vert (VertexInput v)
    {
    VertexOutput o;
    o.pos = UnityObjectToClipPos (v.vertex);
    o.uv = v.uv;
    //VertexFactory
    return o;
    }
    fixed4 frag(VertexOutput i) : SV_Target
    {
 
    fixed2 uv = i.uv / 1;
 
    fixed f = smoothstep( 0.4, 0.6, sin(_Time.y    ) );
    fixed s = smoothstep( 0.4, 0.6, sin(_Time.y*0.5) );
 
    fixed3 col = tex2DNoTile( _MainTex, (4.0 + 4.0*s)*uv + _Time.y*0.1, f ).xyz;
 
    return fixed4( col, 1.0 );
 
    }
    ENDCG
    }
  }
}