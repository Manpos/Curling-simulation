using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector {

    public float x, y, z;

    public Vector()
    {
        x = 0f;
        y = 0f;
        z = 0f;
    }

    public Vector(float a, float b, float c)
    {
        x = a;
        y = b;
        z = c;
    }

    public static Vector cross(Vector a, Vector b)
    {
        return new Vector((a.y * b.z) - (a.z * b.y), -((a.x * b.z) - (b.x * a.z)), (a.x * b.y) - (b.x * a.y));
    }

    public static float dot(Vector a, Vector b)
    {
        return a.x * b.x + a.y * b.y + a.z * b.z;
    }

    public static Vector normalize(Vector n)
    {
        float module = Vector.module(n);
        return new Vector(n.x / module, n.y / module, n.z / module);
    }

    public Vector normalize()
    {
        float module = this.module();
        if (module != 0f)
        {
            return new Vector(x / module, y / module, z / module);
        }
        else return new Vector();
        
    }

    public static float module(Vector v)
    {
        return Mathf.Sqrt((v.x * v.x) + (v.y * v.y) + (v.z * v.z));
    }

    public float module()
    {
        return Mathf.Sqrt((x * x) + (y * y) + (z * z));
    }

    public static float distance(Vector n1, Vector n2)
    {
        return module(n2-n1);
    }

    public static Vector vector3ToVector(Vector3 v3)
    {
        return new Vector(v3.x, v3.y, v3.z);
    }

    public static Vector3 vectorToVector3(Vector v)
    {
        return new Vector3(v.x, v.y, v.z);
    }

    public static Vector operator +(Vector v1, Vector v2)
    {
        return new Vector(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
    }

    public static Vector operator -(Vector v1, Vector v2)
    {
        return new Vector(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
    }

    public static Vector operator *(float f, Vector v)
    {
        return new Vector(v.x * f, v.y * f, v.z * f);
    }

    public static Vector operator *(Vector v, float f)
    {
        return new Vector(v.x * f, v.y * f, v.z * f);
    }

    public static Vector operator /(Vector v, float f)
    {
        return new Vector(v.x / f, v.y / f, v.z / f);
    }
}
