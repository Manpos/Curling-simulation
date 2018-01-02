using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyQuaternion {

    public float x, y, z, w;

    public MyQuaternion()
    {
        x = 0f;
        y = 0f;
        z = 0f;
        w = 0f;
    }

    public MyQuaternion(float _x, float _y, float _z, float _w)
    {
        x = _x;
        y = _y;
        z = _z;
        w = _w;
    }

    public static MyQuaternion operator *(MyQuaternion q, MyQuaternion r)
    {
        MyQuaternion t = new MyQuaternion();
        t.w = (r.w * q.w - r.x * q.x - r.y * q.y - r.z * q.z);
        t.x = (r.w * q.x + r.x * q.w - r.y * q.z + r.z * q.y);
        t.y = (r.w * q.y + r.x * q.z + r.y * q.w - r.z * q.x);
        t.z = (r.w * q.z - r.x * q.y + r.y * q.x + r.z * q.w);
        return t;
    }

    public static MyQuaternion operator *(MyQuaternion q, float f)
    {
        return new MyQuaternion(q.x * f, q.y * f, q.z * f, q.w * f); ;
    }

    public static MyQuaternion operator +(MyQuaternion q, MyQuaternion r)
    {
        MyQuaternion t = new MyQuaternion();
        t.w = (q.x + r.x);
        t.x = (q.y + r.y);
        t.y = (q.z + r.z);
        t.z = (q.w + r.w);
        return t;
    }


    public static MyQuaternion axisAngle(Vector v, float angle)
    {
        angle = angle * Mathf.Deg2Rad;
        v = v.normalize();
        MyQuaternion q = new MyQuaternion();
        q.x = v.x * Mathf.Sin(angle / 2.0f);
        q.y = v.y * Mathf.Sin(angle / 2.0f);
        q.z = v.z * Mathf.Sin(angle / 2.0f);
        q.w = Mathf.Cos(angle / 2.0f);

        return q;
    }

    public static MyQuaternion QuatToMyQuat(Quaternion q)
    {
        MyQuaternion mQ = new MyQuaternion();
        mQ.x = q.x;
        mQ.y = q.y;
        mQ.z = q.z;
        mQ.w = q.w;
        return mQ;
    }

    public MyQuaternion normalized()
    {
        return this * (1f / module());
    }

    public float module()
    {
        return Mathf.Sqrt(x * x + y * y + z * z + w * w);
    }

    public MyQuaternion Conjugate()
    {
        MyQuaternion result = new MyQuaternion();
        result.x = x * -1;
        result.y = y * -1;
        result.z = z * -1;
        result.w = w;

        return result;
    }

    public MyQuaternion Inverse()
    {
        return normalized().Conjugate();
    }

}
