using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector {

    public float x, y, z;

    void Start() {

    }

    Vector(float a, float b, float c)
    {
        x = a;
        y = b;
        z = c;
    }

    public Vector cross(Vector a, Vector b)
    {
        return new Vector(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
    }

    public float dot(Vector a, Vector b)
    {
        return a.x * b.x + a.y + b.y + a.z + b.z;
    }

    public Vector normalize(Vector n)
    {
        float module = Mathf.Sqrt((n.x * n.x) + (n.y * n.y) + (n.z * n.z));
        return new Vector(n.x / module, n.y / module, n.z / module);
    }
}
