using System;
using UnityEngine;

namespace CustomMath
{
    public class Mat4x4
    {
        #region Variables
        public float m00;
        public float m01;
        public float m02;
        public float m03;
        public float m10;
        public float m11;
        public float m12;
        public float m13;
        public float m20;
        public float m21;
        public float m22;
        public float m23;
        public float m30;
        public float m31;
        public float m32;
        public float m33;
        #endregion

        #region Properties
        public static Mat4x4 Zero { get { throw new NotImplementedException(); } }
        public static Mat4x4 Identity { get { throw new NotImplementedException(); } }
        public Mat4x4 inverse { get { throw new NotImplementedException(); } }
        public Mat4x4 determinant { get { throw new NotImplementedException(); } }
        public bool isIdentity { get { throw new NotImplementedException(); } }
        public Quat rotation { get { throw new NotImplementedException(); } }
        public Vec3 lossyScale { get { throw new NotImplementedException(); } }
        public Mat4x4 transpose { get { throw new NotImplementedException(); } }
        #endregion

        #region Constructors
        Mat4x4(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
        {
            throw new NotImplementedException();
        }

        Mat4x4(Matrix4x4 clone)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Operators
        public static Mat4x4 operator *(Mat4x4 lhs, Vector4 vector)
        {
            throw new NotImplementedException();
        }

        public static Mat4x4 operator *(Mat4x4 lhs, Mat4x4 rhs)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(Mat4x4 lhs, Mat4x4 rhs)
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(Mat4x4 lhs, Mat4x4 rhs)
        {
            throw new NotImplementedException();
        }

        public static implicit operator Mat4x4(Matrix4x4 clone)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Static
        public static float Determinant(Mat4x4 m)
        {
            throw new NotImplementedException();
        }

        public static Mat4x4 Inverse(Mat4x4 m)
        {
            throw new NotImplementedException();
        }

        public static Mat4x4 LookAt(Vec3 from, Vec3 to, Vec3 up)
        {
            throw new NotImplementedException();
        }

        public static Mat4x4 Rotate(Quat q)
        {
            throw new NotImplementedException();
        }

        public static Mat4x4 Scale(Vec3 vector)
        {
            throw new NotImplementedException();
        }

        public static Mat4x4 Translate(Vec3 vector)
        {
            throw new NotImplementedException();
        }

        public static Mat4x4 Transpose(Mat4x4 m)
        {
            throw new NotImplementedException();
        }

        public static Mat4x4 TRS(Vec3 pos, Quat q, Vec3 s)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Instance
        public Vec3 GetPosition()
        {
            throw new NotImplementedException();
        }

        public Vector4 GetRow(int index)
        {
            throw new NotImplementedException();
        }

        public Vec3 MultiplyPoint(Vec3 point)
        {
            throw new NotImplementedException();
        }

        public Vec3 MultiplyPoint3x4(Vec3 point)
        {
            throw new NotImplementedException();
        }

        public Vec3 MultiplyVector(Vec3 vector)
        {
            throw new NotImplementedException();
        }

        public void SetColumn(int index, Vector4 column)
        {
            throw new NotImplementedException();
        }

        public void SetRow(int index, Vector4 row)
        {
            throw new NotImplementedException();
        }

        public void SetTRS(Vec3 pos, Quat q, Vec3 s)
        {
            throw new NotImplementedException();
        }

        public bool ValidTRS()
        {
            throw new NotImplementedException();
        }

        public string ToString()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
