using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyQuaternion {

    public float x, y, z, w;

    MyQuaternion()
    {
        x = 0;
        y = 0;
        z = 0;
        w = 0;
    }

    MyQuaternion(float _x, float _y, float _z, float angle)
    {
        x = _x * Mathf.Sin(angle / 2.0f);
        y = _y * Mathf.Sin(angle / 2.0f);
        z = _z * Mathf.Sin(angle / 2.0f);
        w = Mathf.Cos(angle / 2.0f);
    }

    public static MyQuaternion operator *(MyQuaternion q, MyQuaternion r)
    {
        MyQuaternion t = new MyQuaternion();
        t.w = (r.w * q.w - r.x * q.x - r.y * q.y - r.z * q.z);
        t.x = (r.w * q.x + r.x * q.w - r.y * q.z - r.z * q.y);
        t.y = (r.w * q.y + r.x * q.z - r.y * q.w - r.z * q.x);
        t.z = (r.w * q.z - r.x * q.y - r.y * q.x - r.z * q.w);
        return t;
    }

}
