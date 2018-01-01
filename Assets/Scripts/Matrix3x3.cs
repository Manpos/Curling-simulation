using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix3x3 {
    public float m11, m12, m13, m21, m22, m23, m31, m32, m33;
    
    public Matrix3x3(float _m11, float _m12, float _m13, float _m21, float _m22, float _m23, float _m31, float _m32, float _m33)
    {
        m11 = _m11;
        m12 = _m12;
        m13 = _m13;
        m21 = _m21;
        m22 = _m22;
        m23 = _m23;
        m31 = _m31;
        m32 = _m32;
        m33 = _m33;
    }

    public Matrix3x3()
    {
        m11 = 1.0f;
        m12 = 0.0f;
        m13 = 0.0f;
        m21 = 0.0f;
        m22 = 1.0f;
        m23 = 0.0f;
        m31 = 0.0f;
        m32 = 0.0f;
        m33 = 1.0f;
    }

    public Matrix3x3(Matrix3x3 m)
    {
        m11 = m.m11;
        m12 = m.m12;
        m13 = m.m13;
        m21 = m.m21;
        m22 = m.m22;
        m23 = m.m23;
        m31 = m.m31;
        m32 = m.m32;
        m33 = m.m33;
    }

    public static Matrix3x3 operator +(Matrix3x3 m1, Matrix3x3 m2)
    {
        return new Matrix3x3(m1.m11 + m2.m11, m1.m12 + m2.m12, m1.m13 + m2.m13, m1.m21 + m2.m21, m1.m22 + m2.m22, m1.m23 + m2.m23, m1.m31 + m2.m31, m1.m32 + m2.m32, m1.m33 + m2.m33);
    }

    public static Matrix3x3 operator -(Matrix3x3 m1, Matrix3x3 m2)
    {
        return new Matrix3x3(m1.m11 - m2.m11, m1.m12 - m2.m12, m1.m13 - m2.m13, m1.m21 - m2.m21, m1.m22 - m2.m22, m1.m23 - m2.m23, m1.m31 - m2.m31, m1.m32 - m2.m32, m1.m33 - m2.m33);
    }

    public static Matrix3x3 operator *(Matrix3x3 m1, Matrix3x3 m2)
    {
        Matrix3x3 result = new Matrix3x3();
        result.m11 = m1.m11 * m2.m11 + m1.m12 * m2.m21 + m1.m13 * m2.m31;
        result.m12 = m1.m11 * m2.m12 + m1.m12 * m2.m22 + m1.m13 * m2.m32;
        result.m13 = m1.m11 * m2.m13 + m1.m12 * m2.m23 + m1.m13 * m2.m33;

        result.m21 = m1.m21 * m2.m11 + m1.m22 * m2.m21 + m1.m23 * m2.m31;
        result.m22 = m1.m21 * m2.m12 + m1.m22 * m2.m22 + m1.m23 * m2.m32;
        result.m23 = m1.m21 * m2.m13 + m1.m22 * m2.m23 + m1.m23 * m2.m33;

        result.m31 = m1.m31 * m2.m11 + m1.m32 * m2.m21 + m1.m33 * m2.m31;
        result.m32 = m1.m31 * m2.m12 + m1.m32 * m2.m22 + m1.m33 * m2.m32;
        result.m33 = m1.m31 * m2.m13 + m1.m32 * m2.m23 + m1.m33 * m2.m33;
        return result;
    }

    public static Matrix3x3 operator *(Matrix3x3 m1, float c)
    {
        return new Matrix3x3(m1.m11 * c, m1.m12 * c, m1.m13 * c, m1.m21 * c, m1.m22 * c, m1.m23 * c, m1.m31 * c, m1.m32 * c, m1.m33 * c);
    }

    public static Matrix3x3 operator /(Matrix3x3 m1, float c)
    {
        return new Matrix3x3(m1.m11 / c, m1.m12 / c, m1.m13 / c, m1.m21 / c, m1.m22 / c, m1.m23 / c, m1.m31 / c, m1.m32 / c, m1.m33 / c);
    }

    public float Determinant()
    {
        return m11 * m22 * m33 + m21 * m32 * m13 + m12 * m23 * m31 - (m13 * m22 * m31 + m12 * m21 * m33 + m23 * m32 * m11);
    }

    public Matrix3x3 Transposed()
    {
        return new Matrix3x3(m11, m21, m31, m12, m22, m32, m13, m23, m33);
    }

    public Matrix3x3 Adjunct()
    {
        Matrix3x3 result = new Matrix3x3();

        result.m11 = m22 * m33 - (m32 * m23);
        result.m12 = -(m21 * m33 - (m31 * m23));
        result.m13 = m21 * m32 - (m31 * m22);
        result.m21 = -(m12 * m33 - (m32 * m13));
        result.m22 = m11 * m33 - (m31 * m13);
        result.m23 = -(m11 * m32 - (m31 * m12));
        result.m31 = m12 * m23 - (m22 * m13);
        result.m32 = -(m11 * m23 - (m21 * m13));
        result.m33 = m11 * m22 - (m21 * m12);

        return result;
    }

    public Matrix3x3 Inverse()
    {
        if (Determinant() != 0)
        {
            return (Transposed().Adjunct())/Determinant();
        }
        else return new Matrix3x3();      
    }


}
