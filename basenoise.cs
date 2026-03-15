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


public class BaseNoise
{
    protected uint tableSize;
    protected uint tableSizeMask;
    protected int[] permutationTable;

    protected int width, height;
    protected int seed;
    protected float frequency;

    //https://stackoverflow.com/questions/19593587/closest-power-of-2
    uint next_power_of_two(uint x)
    {
        x = x - 1;
        x = x | (x >> 1);
        x = x | (x >> 2);
        x = x | (x >> 4);
        x = x | (x >> 8);
        return x + 1;
    }

    public BaseNoise(int _width, int _height, float _frequency, int _seed, uint _tableSize)
    {
        width = _width;
        height = _height;
        seed = _seed;
        frequency = _frequency;
        tableSize = next_power_of_two(_tableSize);
        tableSizeMask = tableSize - 1;
        permutationTable = new int[tableSize * 2];
    }

    protected void SetPermutationTable(Random gen)
    {
        // shuffle values of the permutation table
        for (uint k = 0; k < tableSize; ++k)
        {
            uint i = (uint)(gen.Next() & tableSizeMask);
            int temp = permutationTable[k];
            permutationTable[k] = permutationTable[i];
            permutationTable[i] = temp;
            //std::swap(permutationTable[k], permutationTable[i]);
            permutationTable[k + tableSize] = permutationTable[k];
        }
    }

    public float[] NormalizeBuffer(float[] noiseMap)
    {
        float minValue = float.MaxValue, maxValue = float.MinValue;

        for (int i = 0; i < noiseMap.Length; ++i)
        {
            if (noiseMap[i] > maxValue)
                maxValue = noiseMap[i];
            if (noiseMap[i] < minValue)
                minValue = noiseMap[i];
        }

        float[] normalizedMap = new float[noiseMap.Length];

        for (int i = 0; i < noiseMap.Length; ++i)
        {
            normalizedMap[i] = (noiseMap[i] - minValue) / (maxValue - minValue);
        }
        return normalizedMap;
    }

}

