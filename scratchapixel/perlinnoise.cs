// A simple implementation of Perlin and Improved Perlin Noise

// c++ -o perlinnoise -O3 -Wall perlinnoise.cpp

// Copyright (C) 2016  www.scratchapixel.com
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

//#define CALC_DERIVATIVES


using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace Noise2D;


public class PerlinNoise : BaseNoise
{
    private Vec3f[] gradients;


    public PerlinNoise(int _width, int _height, float _frequency, int _seed, uint _tableSize) : base(_width, _height, _frequency, _seed, _tableSize)
    {
        width = _width;
        height = _height;
        seed = _seed;
        frequency = _frequency;

        gradients = new Vec3f[tableSize];

        Random gen = new Random(seed);

        for (int i = 0; i < tableSize; ++i)
        {
#if false
//            // bad
//            float gradientLen2;
//            do {
//                gradients[i] = Vec3f(2 * dice() - 1, 2 * dice() - 1, 2 * dice() - 1);
//                gradientLen2 = gradients[i].length2();
//            } while (gradientLen2 > 1);
//            gradients[i].normalize();
#else
            // better
            float theta = (float)Math.Acos(2.0f * gen.NextSingle() - 1.0f);
            float phi = (float)(2.0f * gen.NextSingle() * Math.PI);

            float x = (float)(Math.Cos(phi) * Math.Sin(theta));
            float y = (float)(Math.Sin(phi) * Math.Sin(theta));
            float z = (float)Math.Cos(theta);
            gradients[i] = new Vec3f(x, y, z);
#endif
            permutationTable[i] = i;
        }

        SetPermutationTable(gen);
    }


    // Improved Noise implementation (2002)
    // This version compute the derivative of the noise function as well

    public float eval3(in Vec3f p, out Vec3f? derivs)
    {
        int xi0 = (int)(((int)Math.Floor(p.x)) & tableSizeMask);
        int yi0 = (int)(((int)Math.Floor(p.y)) & tableSizeMask);
        int zi0 = (int)(((int)Math.Floor(p.z)) & tableSizeMask);

        int xi1 = (int)((xi0 + 1) & tableSizeMask);
        int yi1 = (int)((yi0 + 1) & tableSizeMask);
        int zi1 = (int)((zi0 + 1) & tableSizeMask);

        float tx = p.x - ((int)Math.Floor(p.x));
        float ty = p.y - ((int)Math.Floor(p.y));
        float tz = p.z - ((int)Math.Floor(p.z));

        float u = NoiseGlobals.quintic(tx);
        float v = NoiseGlobals.quintic(ty);
        float w = NoiseGlobals.quintic(tz);

        // generate vectors going from the grid points to p
        float x0 = tx;
        float x1 = tx - 1.0f;
        float y0 = ty;
        float y1 = ty - 1.0f;
        float z0 = tz;
        float z1 = tz - 1.0f;

        float a = gradientDotV(hash(xi0, yi0, zi0), x0, y0, z0);
        float b = gradientDotV(hash(xi1, yi0, zi0), x1, y0, z0);
        float c = gradientDotV(hash(xi0, yi1, zi0), x0, y1, z0);
        float d = gradientDotV(hash(xi1, yi1, zi0), x1, y1, z0);
        float e = gradientDotV(hash(xi0, yi0, zi1), x0, y0, z1);
        float f = gradientDotV(hash(xi1, yi0, zi1), x1, y0, z1);
        float g = gradientDotV(hash(xi0, yi1, zi1), x0, y1, z1);
        float h = gradientDotV(hash(xi1, yi1, zi1), x1, y1, z1);

#if CALC_DERIVATIVES
        float du = NoiseGlobals.quinticDeriv(tx);
        float dv = NoiseGlobals.quinticDeriv(ty);
        float dw = NoiseGlobals.quinticDeriv(tz);
#endif
        float k0 = a;
        float k1 = (b - a);
        float k2 = (c - a);
        float k3 = (e - a);
        float k4 = (a + d - b - c);
        float k5 = (a + f - b - e);
        float k6 = (a + g - c - e);
        float k7 = (b + c + e + h - a - d - f - g);

#if CALC_DERIVATIVES
        derivs = new Vec3f();

        derivs.x = du * (k1 + k4 * v + k5 * w + k7 * v * w);
        derivs.y = dv * (k2 + k4 * u + k6 * w + k7 * v * w);
        derivs.z = dw * (k3 + k5 * u + k6 * v + k7 * v * w);
#else
        derivs = null;
#endif
        return k0 + k1 * u + k2 * v + k3 * w + k4 * u * v + k5 * u * w + k6 * v * w + k7 * u * v * w;
    }

    // classic/original Perlin noise implementation (1985)
    public float eval3(in Vec3f p)
    {
        int xi0 = (int)(((int)Math.Floor(p.x)) & tableSizeMask);
        int yi0 = (int)(((int)Math.Floor(p.y)) & tableSizeMask);
        int zi0 = (int)(((int)Math.Floor(p.z)) & tableSizeMask);

        int xi1 = (int)((xi0 + 1) & tableSizeMask);
        int yi1 = (int)((yi0 + 1) & tableSizeMask);
        int zi1 = (int)((zi0 + 1) & tableSizeMask);

        float tx = p.x - ((int)Math.Floor(p.x));
        float ty = p.y - ((int)Math.Floor(p.y));
        float tz = p.z - ((int)Math.Floor(p.z));

        float u = NoiseGlobals.smoothstep(tx);
        float v = NoiseGlobals.smoothstep(ty);
        float w = NoiseGlobals.smoothstep(tz);

        // gradients at the corner of the cell
        Vec3f c000 = gradients[hash(xi0, yi0, zi0)];
        Vec3f c100 = gradients[hash(xi1, yi0, zi0)];
        Vec3f c010 = gradients[hash(xi0, yi1, zi0)];
        Vec3f c110 = gradients[hash(xi1, yi1, zi0)];

        Vec3f c001 = gradients[hash(xi0, yi0, zi1)];
        Vec3f c101 = gradients[hash(xi1, yi0, zi1)];
        Vec3f c011 = gradients[hash(xi0, yi1, zi1)];
        Vec3f c111 = gradients[hash(xi1, yi1, zi1)];

        // generate vectors going from the grid points to p
        float x0 = tx;
        float x1 = tx - 1.0f;
        float y0 = ty;
        float y1 = ty - 1.0f;
        float z0 = tz;
        float z1 = tz - 1.0f;

        Vec3f p000 = new Vec3f(x0, y0, z0);
        Vec3f p100 = new Vec3f(x1, y0, z0);
        Vec3f p010 = new Vec3f(x0, y1, z0);
        Vec3f p110 = new Vec3f(x1, y1, z0);

        Vec3f p001 = new Vec3f(x0, y0, z1);
        Vec3f p101 = new Vec3f(x1, y0, z1);
        Vec3f p011 = new Vec3f(x0, y1, z1);
        Vec3f p111 = new Vec3f(x1, y1, z1);

        // linear interpolation
        float a = NoiseGlobals.lerp(NoiseGlobals.dot(c000, p000), NoiseGlobals.dot(c100, p100), u);
        float b = NoiseGlobals.lerp(NoiseGlobals.dot(c010, p010), NoiseGlobals.dot(c110, p110), u);
        float c = NoiseGlobals.lerp(NoiseGlobals.dot(c001, p001), NoiseGlobals.dot(c101, p101), u);
        float d = NoiseGlobals.lerp(NoiseGlobals.dot(c011, p011), NoiseGlobals.dot(c111, p111), u);

        float e = NoiseGlobals.lerp(a, b, v);
        float f = NoiseGlobals.lerp(c, d, v);

        return NoiseGlobals.lerp(e, f, w); // g
    }


    private int hash(in int x, in int y, in int z)
    {
        return permutationTable[permutationTable[permutationTable[x] + y] + z];
    }

    // Compute dot product between vector from cell corners to P with predefined gradient directions
    //
    //    perm: a value between 0 and 255
    //
    //    float x, float y, float z: coordinates of vector from cell corner to shaded point
    private float gradientDotV(int perm, float x, float y, float z)
    {
        switch (perm & 15)
        {
            case 0:
                return x + y; // (1,1,0)
            case 1:
                return -x + y; // (-1,1,0)
            case 2:
                return x - y; // (1,-1,0)
            case 3:
                return -x - y; // (-1,-1,0)
            case 4:
                return x + z; // (1,0,1)
            case 5:
                return -x + z; // (-1,0,1)
            case 6:
                return x - z; // (1,0,-1)
            case 7:
                return -x - z; // (-1,0,-1)
            case 8:
                return y + z; // (0,1,1),
            case 9:
                return -y + z; // (0,-1,1),
            case 10:
                return y - z; // (0,1,-1),
            case 11:
                return -y - z; // (0,-1,-1)
            case 12:
                return y + x; // (1,1,0)
            case 13:
                return -x + y; // (-1,1,0)
            case 14:
                return -y + z; // (0,-1,1)
            case 15:
                return -y - z; // (0,-1,-1)
        }

        return 0;
    }



    public float[] GetNoiseBuffer()
    {
        // output noise map to noisemap
        float[] noiseMap = new float[width * height];

        for (int j = 0; j < height; ++j)
        {
            for (int i = 0; i < width; ++i)
            {
                float sample = eval3(new Vec3f(i * frequency, 0.0f, j * frequency), out Vec3f? derivs);
                noiseMap[j * width + i] = sample;
            }
        }

        //return NormalizeBuffer(noiseMap);
        return noiseMap;
    }

    public float[] GetFractalNoiseBuffer(float fBm_lacunarity, float fBm_gain, int numLayers)
    {
        int imageWidth = width;
        int imageHeight = height;
        float[] noiseMap = new float[imageWidth * imageHeight];

        for (int j = 0; j < imageHeight; ++j)
        {
            for (int i = 0; i < imageWidth; ++i)
            {
                Vec3f pNoise = new Vec3f(i * frequency, 0.0f, j * frequency);
                float amplitude = 1.0f;

                for (int l = 0; l < numLayers; ++l)
                {
                    noiseMap[j * imageWidth + i] += eval3(pNoise, out Vec3f? derivs) * amplitude;
                    pNoise *= fBm_lacunarity;
                    amplitude *= fBm_gain;
                }
            }
        }

        //return NormalizeBuffer(noiseMap);
        return noiseMap;
    }
}

