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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noise2D;


// Simple class to define a polygonal mesh
internal class PolyMesh
{
    public Vec3f[] vertices;
    public Vec2f[] st;
    public Vec3f[] normals;
    public uint[] faceArray;
    public uint[] verticesArray;
    public uint numVertices = new uint();
    public uint numFaces;

    public static PolyMesh createPolyMesh(
       uint width = 1,
       uint height = 1,
       uint subdivisionWidth = 40,
       uint subdivisionHeight = 40)
    {
        PolyMesh poly = new PolyMesh();
        poly.numVertices = (subdivisionWidth + 1) * (subdivisionHeight + 1);
        poly.vertices = new Vec3f[poly.numVertices];
        poly.normals = new Vec3f[poly.numVertices];
        poly.st = new Vec2f[poly.numVertices];

        float invSubdivisionWidth = 1.0f / subdivisionWidth;
        float invSubdivisionHeight = 1.0f / subdivisionHeight;
        for (uint j = 0; j <= subdivisionHeight; ++j)
        {
            for (uint i = 0; i <= subdivisionWidth; ++i)
            {
                poly.vertices[j * (subdivisionWidth + 1) + i] = new Vec3f(width * (i * invSubdivisionWidth - 0.5f), 0.0f, height * (j * invSubdivisionHeight - 0.5f));
                poly.st[j * (subdivisionWidth + 1) + i] = new Vec2f(i * invSubdivisionWidth, j * invSubdivisionHeight);
            }

        }

        poly.numFaces = subdivisionWidth * subdivisionHeight;
        poly.faceArray = new uint[poly.numFaces];
        for (uint i = 0; i < poly.numFaces; ++i)
            poly.faceArray[i] = 4;

        poly.verticesArray = new uint[4 * poly.numFaces];
        for (uint j = 0, k = 0; j < subdivisionHeight; ++j)
        {
            for (uint i = 0; i < subdivisionWidth; ++i)
            {
                poly.verticesArray[k] = j * (subdivisionWidth + 1) + i;
                poly.verticesArray[k + 1] = j * (subdivisionWidth + 1) + i + 1;
                poly.verticesArray[k + 2] = (j + 1) * (subdivisionWidth + 1) + i + 1;
                poly.verticesArray[k + 3] = (j + 1) * (subdivisionWidth + 1) + i;
                k += 4;
            }
        }

        return poly;
    }

}
