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


namespace Noise2D;

public class Vec2f
{
    public Vec2f()
    {

    }

    public Vec2f(float xx, float yy)
    {
        this.x = xx;
        this.y = yy;
    }

    public static Vec2f operator *(Vec2f This, in float r)
    {
        return new Vec2f(This.x * r, This.y * r);
    }

    public Vec2f mult(float m)
    {
        x *= m;
        y *= m;
        return this;
    }

    public float x = default(float);
    public float y = default(float);
}

public class Vec3f
{
    public Vec3f()
    {

    }
    public Vec3f(float xx, float yy, float zz)
    {
        this.x = xx;
        this.y = yy;
        this.z = zz;
    }

    public static Vec3f operator *(Vec3f This, in float r)
    {
        return new Vec3f(This.x * r, This.y * r, This.z * r);
    }

    public static Vec3f operator -(Vec3f This, in Vec3f v)
    {
        return new Vec3f(This.x - v.x, This.y - v.y, This.z - v.z);
    }

    public float length2()
    {
        return x * x + y * y + z * z;
    }

    public Vec3f mult(float m)
    {
        x *= m;
        y *= m;
        z *= m;
        return this;
    }


    public Vec3f cross(in Vec3f v)
    {
        return new Vec3f(y * v.z - z * v.y, z * v.x - x * v.z, x * v.y - y * v.x);
    }
    public Vec3f normalize()
    {
        float len2 = length2();
        if (len2 > 0.0f)
        {
            float invLen = (1.0f) / (float)Math.Sqrt(len2);
            x *= invLen; y *= invLen; z *= invLen;
        }
        return this;
    }

    public float x = default(float);
    public float y = default(float);
    public float z = default(float);
}

public static class NoiseGlobals
{
    public static float dot(in Vec3f a, in Vec3f b)
    {
        return a.x * b.x + a.y * b.y + a.z * b.z;
    } 
    
    public delegate float Interpolator(in float lo, in float hi,in float t);
    public static float lerp(in float lo, in float hi, in float t)
    {
        return lo * (1.0f - t) + hi * t;
    }

    public delegate float Smoother(in float t);
    public static float smoothstep( in float t)
    {
        return t * t * (3.0f - 2.0f * t);
    }

    public static float quintic( in float t)
    {
        return t * t * t * (t * (t * 6.0f - 15.0f) + 10.0f);
    }

    public static float smoothstepDeriv( in float t)
    {
        return t * (6.0f - 6.0f * t);
    }

    public static float quinticDeriv( in float t)
    {
        return 30.0f * t * t * (t * (t - 2.0f) + 1.0f);
    }

}
