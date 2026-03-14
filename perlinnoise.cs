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

#define ANALYTICAL_NORMALS

using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace Noise2D;


public class PerlinNoise
{
    private const int tableSize = 256;
    private static int tableSizeMask = tableSize - 1;
    private Vec3f[] gradients;
    private uint[] permutationTable = new uint[tableSize * 2];
    int width, height;
    //float scale;

    public PerlinNoise(int _width, int _height, int seed = 2016)
    {
        width = _width;
        height = _height;

        gradients = new Vec3f[tableSize];
        Random dice = new Random(seed);


        for (uint i = 0; i < tableSize; ++i)
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
            float theta = (float)Math.Acos(2.0f * dice.NextSingle() - 1.0f);
            float phi = (float)(2.0f * dice.NextSingle() * Math.PI);

            float x = (float)(Math.Cos(phi) * Math.Sin(theta));
            float y = (float)(Math.Sin(phi) * Math.Sin(theta));
            float z = (float)Math.Cos(theta);
            gradients[i] = new Vec3f(x, y, z);
#endif
            permutationTable[i] = i;
        }


        // create permutation table
        for (uint i = 0; i < tableSize; ++i)
        {
            int k = dice.Next() & tableSizeMask;
            uint temp = permutationTable[k];
            permutationTable[k] = permutationTable[i];
            permutationTable[i] = temp;

            //std::swap(permutationTable[i], permutationTable[diceInt() & tableSizeMask]);
        }
        // extend the permutation table in the index range [256:512]
        for (uint i = 0; i < tableSize; ++i)
        {
            permutationTable[tableSize + i] = permutationTable[i];
        }
    }


    // Improved Noise implementation (2002)
    // This version compute the derivative of the noise function as well

    public float eval(in Vec3f p, out Vec3f derivs)
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

        float du = NoiseGlobals.quinticDeriv(tx);
        float dv = NoiseGlobals.quinticDeriv(ty);
        float dw = NoiseGlobals.quinticDeriv(tz);

        float k0 = a;
        float k1 = (b - a);
        float k2 = (c - a);
        float k3 = (e - a);
        float k4 = (a + d - b - c);
        float k5 = (a + f - b - e);
        float k6 = (a + g - c - e);
        float k7 = (b + c + e + h - a - d - f - g);

        derivs = new Vec3f();

        derivs.x = du * (k1 + k4 * v + k5 * w + k7 * v * w);
        derivs.y = dv * (k2 + k4 * u + k6 * w + k7 * v * w);
        derivs.z = dw * (k3 + k5 * u + k6 * v + k7 * v * w);

        return k0 + k1 * u + k2 * v + k3 * w + k4 * u * v + k5 * u * w + k6 * v * w + k7 * u * v * w;
    }

    // classic/original Perlin noise implementation (1985)
    public float eval(in Vec3f p)
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

    /* inline */
    private uint hash(in int x, in int y, in int z)
    {
        return permutationTable[permutationTable[permutationTable[x] + y] + z];
    }

    // Compute dot product between vector from cell corners to P with predefined gradient directions
    //
    //    perm: a value between 0 and 255
    //
    //    float x, float y, float z: coordinates of vector from cell corner to shaded point
    private float gradientDotV(uint perm, float x, float y, float z)
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


    internal PolyMesh GetPolyMesh(uint width = 3,
                                   uint height = 3,
                                   uint subdivisionWidth = 30,
                                   uint subdivisionHeight = 30)
    {
        PolyMesh poly = PolyMesh.createPolyMesh(width, height,subdivisionWidth,subdivisionHeight);

        // displace and compute analytical normal using noise function partial derivatives

        for (int i = 0; i < poly.numVertices; ++i)
        {
            Vec3f p = new Vec3f((poly.vertices[i].x + 0.5f), 0.0f, (poly.vertices[i].z + 0.5f));
            poly.vertices[i].y = eval(p, out Vec3f derivs);
#if ANALYTICAL_NORMALS
            Vec3f tangent = new Vec3f(1.0f, derivs.x, 0.0f); // tangent
            Vec3f bitangent = new Vec3f(0.0f, derivs.z, 1.0f); // bitangent
                                                               // equivalent to bitangent.cross(tangent)
            poly.normals[i] = new Vec3f(-derivs.x, 1.0f, -derivs.z);
            poly.normals[i].normalize();

#endif
        }

#if !ANALYTICAL_NORMALS
        // compute face normal if you want
        for (uint k = 0, off = 0; k < poly.numFaces; ++k)
        {
            uint nverts = poly.faceArray[k];
            Vec3f va = poly.vertices[poly.verticesArray[off]];
            Vec3f vb = poly.vertices[poly.verticesArray[off + 1]];
            Vec3f vc = poly.vertices[poly.verticesArray[off + nverts - 1]];

            Vec3f tangent = vb - va;
            Vec3f bitangent = vc - va;

            poly.normals[poly.verticesArray[off]] = bitangent.cross(tangent);
            poly.normals[poly.verticesArray[off]].normalize();

            off += nverts;
        }
#endif

        return poly;
    }


    public float[] GetNoiseBuffer(float scale)
    { 
        // output noise map to noisemap
        float[] noiseMap = new float[width * height];
        float inv_scale = 1f / scale;
        //float minValue = float.MaxValue, maxValue = float.MinValue;

        for (int j = 0; j < height; ++j)
        {
            for (int i = 0; i < width; ++i)
            {
                float sample = eval(new Vec3f(i * inv_scale , 0.0f, j * inv_scale), out Vec3f derivs);

                //if (sample < minValue)
                //    minValue = sample;
                //if (sample > maxValue)
                //    maxValue = sample;

                noiseMap[j * width + i] = (sample + 1.0f ) * 0.5f;
            }
        } 
        
        //Debug.WriteLine($"Perlin min:{minValue} max:{maxValue}");

        return noiseMap;
    }
}

