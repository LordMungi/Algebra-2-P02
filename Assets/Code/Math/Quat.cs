using System;
using System.ComponentModel;
using UnityEngine;

namespace CustomMath
{
    public struct Quat
    {
        #region Variables
        public float x;
        public float y;
        public float z;
        public float w;
        #endregion

        #region Constructors
        Quat(float x, float y, float z, float w)
        {
            throw new NotImplementedException();
        }

        Quat(Quat clone)
        {
            throw new NotImplementedException();
        }

        Quat(Quaternion clone)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Operators
        public static Quat operator ==(Quat lhs, Quat rhs)
        {
            throw new NotImplementedException();
        }

        public static Quat operator !=(Quat lhs, Quat rhs)
        {
            throw new NotImplementedException();
        }

        public static Quat operator *(Quat rotation, Vec3 point)
        {
            throw new NotImplementedException();
        }

        public static Quat operator *(Quat lhs, Quat rhs)
        {
            throw new NotImplementedException();
        }

        public static implicit operator Quat(Quaternion clone)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Static
        public static float Angle(Quat a, Quat b)
        {
            throw new NotImplementedException();
        }

        public static Quat AngleAxis(float angle, Vec3 axis)
        {
            throw new NotImplementedException();
        }

        public static float Dot(Quat a, Quat b)
        {
            throw new NotImplementedException();
        }

        public static Quat Euler(Vec3 euler)
        {
            throw new NotImplementedException();
        }

        public static Quat EulerAngles(Vec3 euler)
        {
            throw new NotImplementedException();
        }

        public static Quat EulerRotation(Vec3 euler)
        {
            throw new NotImplementedException();
        }

        public static Quat FromToRotation(Vec3 fromDirection, Vec3 toDirection)
        {
            throw new NotImplementedException();
        }

        public static Quat Inverse(Quat rotation)
        {
            throw new NotImplementedException();
        }

        public static Quat Lerp(Quat a, Quat b, float t)
        {
            throw new NotImplementedException();
        }

        public static Quat LerpUnclamped(Quat a, Quat b, float t)
        {
            throw new NotImplementedException();
        }

        public static Quat LookRotation(Vec3 forward)
        {
            throw new NotImplementedException();
        }

        public static Quat LookRotation(Vec3 forward, [DefaultValue("Vec3.up")] Vec3 upwards)
        {
            throw new NotImplementedException();
        }

        public static Quat Normalize(Quat q)
        {
            throw new NotImplementedException();
        }

        public static Quat RotateTowards(Quat from, Quat to, float maxDegreesDelta)
        {
            throw new NotImplementedException();
        }

        public static Quat Slerp(Quat a, Quat b, float t)
        {
            throw new NotImplementedException();
        }

        public static Quat SlerpUnclamped(Quat a, Quat b, float t)
        {
            throw new NotImplementedException();
        }

        public static Vec3 ToEulerAngles(Quat rotation)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Instance
        public void Normalize()
        {
            throw new NotImplementedException();
        }

        public void Set(float newX, float newY, float newZ, float newW)
        {
            throw new NotImplementedException();
        }

        public void SetAxisAngle(Vec3 axis, float angle)
        {
            throw new NotImplementedException();
        }

        public void SetEulerAngles(Vec3 euler)
        {
            throw new NotImplementedException();
        }

        public void SetEulerRotation(Vec3 euler)
        {
            throw new NotImplementedException();
        }

        public void SetFromToRotation(Vec3 fromDirection, Vec3 toDirection)
        {
            throw new NotImplementedException();
        }

        public void SetLookRotation(Vec3 view)
        {
            throw new NotImplementedException();
        }

        public void SetLookRotation(Vec3 view, [DefaultValue("Vec3.up")] Vec3 up)
        {
            throw new NotImplementedException();
        }

        public void ToAngleAxis(out float angle, out Vec3 axis)
        {
            throw new NotImplementedException();
        }

        public Vec3 ToEuler()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
