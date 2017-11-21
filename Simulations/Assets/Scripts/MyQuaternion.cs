using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyQuaternion {

    public float x, y, z, w;

    MyQuaternion(float _x, float _y, float _z, float angle)
    {
        x = _x * Mathf.Sin(angle / 2.0f);
        y = _y * Mathf.Sin(angle / 2.0f);
        z = _z * Mathf.Sin(angle / 2.0f);
        w = Mathf.Cos(angle / 2.0f);
    }

}
