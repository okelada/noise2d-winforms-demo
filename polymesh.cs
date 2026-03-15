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

#define ANALYTICAL_NORMALS

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

    internal PolyMesh createRandomPolyMesh(uint width,
                               uint height,
                               uint subdivisionWidth,
                               uint subdivisionHeight,float frequency, int seed, uint tablesize)
    {
        PolyMesh poly = PolyMesh.createFlatPolyMesh(width, height, subdivisionWidth, subdivisionHeight);
       
        // displace and compute analytical normal using noise function partial derivatives
        PerlinNoise perlinNoise = new PerlinNoise((int)subdivisionWidth+1, (int)subdivisionHeight+1, frequency,seed, tablesize);

        for (int i = 0; i < poly.numVertices; ++i)
        {
            Vec3f p = new Vec3f((poly.vertices[i].x + 0.5f), 0.0f, (poly.vertices[i].z + 0.5f)) * frequency;
            poly.vertices[i].y = perlinNoise.eval3(p, out Vec3f derivs);

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

    public static PolyMesh createFlatPolyMesh(
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
