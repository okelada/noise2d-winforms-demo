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

public class ValueNoise
{

    public const uint kMaxTableSize = 256;
    public static uint kMaxTableSizeMask = kMaxTableSize - 1;
    public float[] r = new float[kMaxTableSize];
    public uint[] permutationTable = new uint[kMaxTableSize * 2];
    int width, height;
    Random? gen = null;

    public ValueNoise(int _width, int _height, int seed = 2016)
    {
        width = _width;
        height = _height;
        gen = new Random(seed);

        // create an array of random values and initialize permutation table
        for (uint k = 0; k < kMaxTableSize; ++k)
        {
            r[k] = gen.NextSingle();
            permutationTable[k] = k;
        }

        // shuffle values of the permutation table
        for (uint k = 0; k < kMaxTableSize; ++k)
        {
            uint i = (uint)(gen.Next() & kMaxTableSizeMask);
            uint temp = permutationTable[k];
            permutationTable[k] = permutationTable[i];
            permutationTable[i] = temp;
            //std::swap(permutationTable[k], permutationTable[i]);
            permutationTable[k + kMaxTableSize] = permutationTable[k];
        }
    }


    public float eval(Vec2f p)
    {
        int xi = (int)Math.Floor(p.x);
        int yi = (int)Math.Floor(p.y);

        float tx = p.x - xi;
        float ty = p.y - yi;

        int rx0 = (int)(xi & kMaxTableSizeMask);
        int rx1 = (int)((rx0 + 1) & kMaxTableSizeMask);
        int ry0 = (int)(yi & kMaxTableSizeMask);
        int ry1 = (int)((ry0 + 1) & kMaxTableSizeMask);

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

        // Generate white noise
        for (int j = 0; j < imageHeight; ++j)
        {
            for (int i = 0; i < imageWidth; ++i)
            {
                // generate a float in the range [0:1]
                noiseMap[j * imageWidth + i] = gen.NextSingle();
            }
        }

        return noiseMap;
    }

    public float[] GetValueNoiseBuffer(float frequency)
    {
        int imageWidth = width;
        int imageHeight = height;
        float[] noiseMap = new float[imageWidth * imageHeight];

        // Generate value noise
        //float frequency = 0.05f;

        for (int j = 0; j < imageHeight; ++j)
        {
            for (int i = 0; i < imageWidth; ++i)
            {
                // generate a float in the range [0:1]
                noiseMap[j * imageWidth + i] = eval(new Vec2f(i, j).mult(frequency));
            }
        }
        return noiseMap;
    }

    public float[] GetFractalNoiseBuffer(float frequency, float frequencyMult, float amplitudeMult, int numLayers)
    {
        int imageWidth = width;
        int imageHeight = height;
        float[] noiseMap = new float[imageWidth * imageHeight];

        // Generate fractal pattern
        //float frequency = 0.02f;
        //float frequencyMult = 1.8f;
        //float amplitudeMult = 0.35f;
        //int numLayers = 5;


        float maxNoiseVal = 0;
        for (int j = 0; j < imageHeight; ++j)
        {
            for (int i = 0; i < imageWidth; ++i)
            {
                Vec2f pNoise = new Vec2f(i, j) * frequency;
                float amplitude = 1;
                for (int l = 0; l < numLayers; ++l)
                {
                    noiseMap[j * imageWidth + i] += eval(pNoise) * amplitude;
                    pNoise *= frequencyMult;
                    amplitude *= amplitudeMult;
                }
                if (noiseMap[j * imageWidth + i] > maxNoiseVal) 
                    maxNoiseVal = noiseMap[j * imageWidth + i];
            }
        }
        for (int i = 0; i < imageWidth * imageHeight; ++i)
            noiseMap[i] /= maxNoiseVal;

        return noiseMap;
    }

    public float[] GetTurbulenceNoiseBuffer(float frequency, float frequencyMult, float amplitudeMult, int numLayers)
    {
        int imageWidth = width;
        int imageHeight = height;
        float[] noiseMap = new float[imageWidth * imageHeight];

        // Generate turbulence pattern
        //float frequency = 0.02f;
        //float frequencyMult = 1.8f;
        //float amplitudeMult = 0.35f;
        //int numLayers = 5;

        float maxNoiseVal = 0;
        for (int j = 0; j < imageHeight; ++j)
        {
            for (int i = 0; i < imageWidth; ++i)
            {
                Vec2f pNoise = new Vec2f(i, j) * frequency;
                float amplitude = 1;
                for (int l = 0; l < numLayers; ++l)
                {
                    noiseMap[j * imageWidth + i] += Math.Abs(2.0f * eval(pNoise) - 1) * amplitude;
                    pNoise *= frequencyMult;
                    amplitude *= amplitudeMult;
                }
                if (noiseMap[j * imageWidth + i] > maxNoiseVal) maxNoiseVal = noiseMap[j * imageWidth + i];
            }
        }
        for (int i = 0; i < imageWidth * imageHeight; ++i) noiseMap[i] /= maxNoiseVal;

        return noiseMap;
    }

    public float[] GetMarbleNoiseBuffer(float frequency, float frequencyMult, float amplitudeMult, int numLayers)
    {
        int imageWidth = width;
        int imageHeight = height;
        float[] noiseMap = new float[imageWidth * imageHeight];

        // Generate marble pattern
        //float frequency = 0.02f;
        //float frequencyMult = 1.8f;
        //float amplitudeMult = 0.35f;
        //int numLayers = 5;


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
                    noiseValue += eval(pNoise) * amplitude;
                    pNoise *= frequencyMult;
                    amplitude *= amplitudeMult;
                }
                // we "displace" the value i used in the sin() expression by noiseValue * 100
                noiseMap[j * imageWidth + i] = (float)(Math.Sin((i + noiseValue * 100.0f) * 2 * Math.PI / 200.0f) + 1.0f) / 2.0f;
            }
        }
        return noiseMap;
    }
}
