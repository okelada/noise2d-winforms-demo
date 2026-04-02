using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Noise2D;

//not used

public class valuenoise1D
{
    const int kMaxVertices = 256;
    const int kMaxVerticesMask = kMaxVertices - 1;
    float[] r = new float[kMaxVertices];
    Random? gen;
    //Func<float,float,float,float> F;
    NoiseGlobals.Interpolator fF;
    //public valueNoise1D(Func<float, float, float, float> _F,int seed = 2016)
    public valuenoise1D(NoiseGlobals.Interpolator _F, int seed = 2016)
    {
        fF = _F;
        gen = new Random(seed);

        for (uint i = 0; i < kMaxVertices; ++i)
        {
            r[i] = gen.NextSingle();
        }
    }

    // Evaluate the noise function at position x
    public float eval(float x)
    {
        // Floor
        int xi = (int)x - ((x < 0.0f && x != (int)x) ? 1 : 0);
        float t = x - xi;
        // Modulo using &
        int xMin = xi & kMaxVerticesMask;
        int xMax = (xMin + 1) & kMaxVerticesMask;

        return fF(r[xMin], r[xMax], t);
    }



    public static void TestModulo()
    {
        valuenoise1D valueNoise1D = new valuenoise1D(NoiseGlobals.lerp);

        const int numSteps = 10;

        for (int i = 0; i < numSteps; ++i)
        {
            // x varies from -10 to 10
            float x = (i - 5) * 256f;
            Debug.WriteLine($"Noise at {x}: {valueNoise1D.eval(x)}");
        }
    }
}


