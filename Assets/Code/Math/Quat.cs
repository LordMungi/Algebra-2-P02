using System;
using System.ComponentModel;
using UnityEngine;

namespace CustomMath
{
    public struct Quat : IEquatable<Quat>, IFormattable
    {
        #region Variables
        public float x;
        public float y;
        public float z;
        public float w;
        #endregion

        #region Properties
        public static Quat identity { get { throw new NotImplementedException(); } }
        public Vec3 eulerAngles { get { throw new NotImplementedException(); } }
        public Quat normalized { get { throw new NotImplementedException(); } }
        #endregion

        #region Constructors
        Quat(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        Quat(Quat clone)
        {
            this = clone;
        }

        Quat(Quaternion clone)
        {
            this.x = clone.x;
            this.y = clone.y;
            this.z = clone.z;
            this.w = clone.w;
        }
        #endregion

        #region Operators
        public static bool operator ==(Quat lhs, Quat rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z && lhs.w == rhs.w;
        }

        public static bool operator !=(Quat lhs, Quat rhs)
        {
            return !(lhs == rhs);
        }

        public static Vec3 operator *(Quat rotation, Vec3 point)
        {
            Vec3 v = new Vec3(rotation.x, rotation.y, rotation.z);
            return point + 2 * rotation.w * (Vec3.Cross(v, point)) + 2 * (Vec3.Cross(v, Vec3.Cross(v, point)));
        }

        public static Quat operator *(Quat lhs, Quat rhs)
        {
            Quat q;

            q.w = lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z;
            q.x = lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y;
            q.y = lhs.w * rhs.y - lhs.x * rhs.z + lhs.y * rhs.w + lhs.z * rhs.x;
            q.z = lhs.w * rhs.z + lhs.x * rhs.y - lhs.y * rhs.x + lhs.z * rhs.w;

            return q;
        }

        public static implicit operator Quat(Quaternion clone)
        {
            return new Quat(clone);
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

        public bool Equals(Quat other)
        {
            throw new NotImplementedException();
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
