// A simple program to demonstrate the concept of value noise

// Download the noise.cpp file to a folder.
// Open a shell/terminal, and run the following command where the file is saved:
//
// c++ -o noise noise.cpp -std=c++11 -O3
//
// Run with: ./noise. Open the file ./noise.ppm in Photoshop or any program
// reading PPM files.

// Copyright (C) 2012  www.scratchapixel.com
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

using System;


namespace Noise2D;


public class ValueNoise : BaseNoise
{
    readonly float[] r;

    public ValueNoise(int _width, int _height, float _frequency, int _seed, uint _tableSize) : base(_width, _height, _frequency, _seed, _tableSize)
    {
        width = _width;
        height = _height;
        seed = _seed;
        frequency = _frequency;

        r = new float[tableSize];

        Random gen = new Random(seed);

        // create an array of random values and initialize permutation table
        for (int k = 0; k < tableSize; ++k)
        {
            r[k] = gen.NextSingle();
            permutationTable[k] = k;
        }

        SetPermutationTable(gen);
    }
    

    public float eval2(Vec2f p)
    {
        int xi = (int)Math.Floor(p.x);
        int yi = (int)Math.Floor(p.y);

        float tx = p.x - xi;
        float ty = p.y - yi;

        int rx0 = (int)(xi & tableSizeMask);
        int rx1 = (int)((rx0 + 1) & tableSizeMask);
        int ry0 = (int)(yi & tableSizeMask);
        int ry1 = (int)((ry0 + 1) & tableSizeMask);

        // random values at the corners of the cell using permutation table
        float c00 = r[permutationTable[permutationTable[rx0] + ry0]];
        float c10 = r[permutationTable[permutationTable[rx1] + ry0]];
        float c01 = r[permutationTable[permutationTable[rx0] + ry1]];
        float c11 = r[permutationTable[permutationTable[rx1] + ry1]];

        // remapping of tx and ty using the Smoothstep function 
        float sx = NoiseGlobals.smoothstep(tx);
        float sy = NoiseGlobals.smoothstep(ty);

        // linearly interpolate values along the x axis
        float nx0 = NoiseGlobals.lerp(c00, c10, sx);
        float nx1 = NoiseGlobals.lerp(c01, c11, sx);

        // linearly interpolate the nx0/nx1 along they y axis
        return NoiseGlobals.lerp(nx0, nx1, sy);
    }


    public float[] GetWhiteNoiseBuffer()
    {
        int imageWidth = width;
        int imageHeight = height;
        float[] noiseMap = new float[imageWidth * imageHeight];
        Random gen = new Random(seed);

        // Generate white noise
        for (int j = 0; j < imageHeight; ++j)
        {
            for (int i = 0; i < imageWidth; ++i)
            {
                // generate a float in the range [0:1]
                noiseMap[j * imageWidth + i] = gen.NextSingle();
            }
        }

        return NormalizeBuffer(noiseMap);
    }

    public float[] GetNoiseBuffer()
    {
        int imageWidth = width;
        int imageHeight = height;
        float[] noiseMap = new float[imageWidth * imageHeight];

        // Generate value noise
        for (int j = 0; j < imageHeight; ++j)
        {
            for (int i = 0; i < imageWidth; ++i)
            {
                // generate a float in the range [0:1]
                noiseMap[j * imageWidth + i] = eval2(new Vec2f(i, j).mult(frequency));
            }
        }
        return NormalizeBuffer(noiseMap);
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
                Vec2f pNoise = new Vec2f(i, j) * frequency;
                float amplitude = 1.0f;
                for (int l = 0; l < numLayers; ++l)
                {
                    noiseMap[j * imageWidth + i] += eval2(pNoise) * amplitude;
                    pNoise *= fBm_lacunarity;
                    amplitude *= fBm_gain;
                }
            }
        }

        return NormalizeBuffer(noiseMap);
    }

    public float[] GetTurbulenceNoiseBuffer(float fBm_lacunarity, float fBm_gain, int numLayers)
    {
        int imageWidth = width;
        int imageHeight = height;
        float[] noiseMap = new float[imageWidth * imageHeight];

        // Generate turbulence pattern
        for (int j = 0; j < imageHeight; ++j)
        {
            for (int i = 0; i < imageWidth; ++i)
            {
                Vec2f pNoise = new Vec2f(i, j) * frequency;
                float amplitude = 1;
                for (int l = 0; l < numLayers; ++l)
                {
                    noiseMap[j * imageWidth + i] += Math.Abs(2.0f * eval2(pNoise) - 1f) * amplitude;
                    pNoise *= fBm_lacunarity;
                    amplitude *= fBm_gain;
                }
            }
        }

        return NormalizeBuffer(noiseMap);
    }

    public float[] GetMarbleNoiseBuffer(float fBm_lacunarity, float fBm_gain, int numLayers)
    {
        int imageWidth = width;
        int imageHeight = height;
        float[] noiseMap = new float[imageWidth * imageHeight];

        // Generate marble pattern
        for (int j = 0; j < imageHeight; ++j)
        {
            for (int i = 0; i < imageWidth; ++i)
            {
                Vec2f pNoise = new Vec2f(i, j) * frequency;
                float amplitude = 1;
                float noiseValue = 0;
                // compute some fractal noise
                for (int l = 0; l < numLayers; ++l)
                {
                    noiseValue += eval2(pNoise) * amplitude;
                    pNoise *= fBm_lacunarity;
                    amplitude *= fBm_gain;
                }
                // we "displace" the value i used in the sin() expression by noiseValue * 100
                noiseMap[j * imageWidth + i] = (float)Math.Sin((i + noiseValue * 100.0f) * 2.0f * Math.PI / 200.0f);
            }
        }
        return NormalizeBuffer(noiseMap);
    }
}
