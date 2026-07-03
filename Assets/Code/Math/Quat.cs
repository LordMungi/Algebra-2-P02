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
        public static Quat identity { get { return new Quat(0, 0, 0, 1); } }
        public Vec3 eulerAngles
        {
            get
            {
                Vec3 v;
                v.x = Mathf.Atan2(2 * (w * x + y * z), 1 - 2 * (x * x + y * y));
                v.y = -Mathf.PI / 2 + 2 * Mathf.Atan2(Mathf.Sqrt(1 + 2 * (w * y - x * z)), Mathf.Sqrt(1 - 2 * (w * y - x * z)));
                v.z = Mathf.Atan2(2 * (w * z + x * y), 1 - 2 * (y * y + z * z));
                return v * Mathf.Rad2Deg;
            }
        }
        public Quat normalized 
        { 
            get
            {
                float mag = Mathf.Sqrt(w * w + x * x + y * y + z * z);
                return new Quat(x / mag, y / mag, z / mag, w / mag);
            }
        }
        #endregion

        #region Constructors
        public Quat(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Quat(Quat clone)
        {
            this = clone;
        }

        public Quat(Quaternion clone)
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

        public static Quat operator +(Quat lhs, Quat rhs)
        {
            return new Quat(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);
        }

        public static Quat operator -(Quat lhs, Quat rhs)
        {
            return new Quat(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
        }

        public static Quat operator *(Quat q, float n)
        {
            return new Quat(q.x * n, q.y * n, q.z * n, q.w * n);
        }

        public static Quat operator /(Quat q, float n)
        {
            return new Quat(q.x / n, q.y / n, q.z / n, q.w / n);
        }

        public static implicit operator Quat(Quaternion clone)
        {
            return new Quat(clone);
        }

        public static implicit operator Quaternion(Quat clone)
        {
            return new Quaternion(clone.x, clone.y, clone.z, clone.w);
        }
        #endregion

        #region Static
        public static float Angle(Quat a, Quat b)
        {
            return Mathf.Rad2Deg * (2 * Mathf.Acos(Mathf.Abs(Dot(a, b))));
        }

        public static Quat AngleAxis(float angle, Vec3 axis)
        {
            Quat q;
            axis = axis.normalized;

            float a = Mathf.Deg2Rad * (angle / 2);
            float aSin = Mathf.Sin(a);

            q.w = Mathf.Cos(a);
            q.x = aSin * axis.x;
            q.y = aSin * axis.y;
            q.z = aSin * axis.z;

            return q;
        }

        public static float Dot(Quat a, Quat b)
        {
            return a.w * b.w + a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static Quat Euler(Vec3 euler)
        {
            float ex = Mathf.Deg2Rad * (euler.x / 2);
            float ey = Mathf.Deg2Rad * (euler.y / 2);
            float ez = Mathf.Deg2Rad * (euler.z / 2);


            Quat qx = new Quat(Mathf.Sin(ex), 0, 0, Mathf.Cos(ex));
            Quat qy = new Quat(0, Mathf.Sin(ey), 0, Mathf.Cos(ey));
            Quat qz = new Quat(0, 0, Mathf.Sin(ez), Mathf.Cos(ez));

            return qz * qx * qy;
        }

        public static Quat EulerAngles(Vec3 euler)
        {
            return Euler(euler);
        }

        public static Quat EulerRotation(Vec3 euler)
        {
            return Euler(euler);
        }

        public static Quat FromToRotation(Vec3 fromDirection, Vec3 toDirection)
        {
            fromDirection = fromDirection.normalized;
            toDirection = toDirection.normalized;

            float a = Mathf.Acos(Vec3.Dot(fromDirection, toDirection)) / 2;
            Vec3 axis = Vec3.Cross(fromDirection, toDirection).normalized;

            float sina = Mathf.Sin(a);
            return new Quat(axis.x * sina, axis.y * sina, axis.z * sina, Mathf.Cos(a));
        }

        public static Quat Inverse(Quat rotation)
        {
            return new Quat(-rotation.x, -rotation.y, -rotation.z, rotation.w);
        }

        public static Quat Lerp(Quat a, Quat b, float t)
        {
            return LerpUnclamped(a, b, Mathf.Clamp(t, 0, 1));
        }

        public static Quat LerpUnclamped(Quat a, Quat b, float t)
        {
            if (Dot(a, b) < 0f)
            {
                b.x = -b.x;
                b.y = -b.y;
                b.z = -b.z;
                b.w = -b.w;
            }

            Quat q;

            q.x = a.x - t * (a.x - b.x);
            q.y = a.y - t * (a.y - b.y);
            q.z = a.z - t * (a.z - b.z);
            q.w = a.w - t * (a.w - b.w);

            return Normalize(q);
        }

        public static Quat LookRotation(Vec3 forward)
        {
            return LookRotation(forward, Vec3.Up);
        }

        public static Quat LookRotation(Vec3 forward, [DefaultValue("Vec3.up")] Vec3 upwards)
        {
            if (forward == Vec3.Zero)
            {
                return Quat.identity;
            }

            Vec3 f = forward.normalized;
            Vec3 l = Vec3.Cross(upwards, f).normalized;
            Vec3 u = Vec3.Cross(f, l).normalized;

            float m00 = f.x;
            float m01 = l.x;
            float m02 = u.x;
            float m10 = f.y;
            float m11 = l.y;
            float m12 = u.y;
            float m20 = f.z;
            float m21 = l.z;
            float m22 = u.z;
            float mtr = m00 + m11 + m22;

            Quat q;

            if (mtr > 0)
            {
                float s = Mathf.Sqrt(mtr + 1) * 2;
                q.w = 0.25f * s;
                q.x = (m21 - m12) / s;
                q.y = (m02 - m20) / s;
                q.z = (m10 - m01) / s;
            }
            else if (m00 > m11 && m00 > m22)
            {
                float s = Mathf.Sqrt(1 + m00 - m11 - m22) * 2;
                q.w = (m21 - m12) / s;
                q.x = 0.25f * s;
                q.y = (m01 + m10) / s;
                q.z = (m02 + m20) / s;
            }
            else if (m11 > m22)
            {
                float s = Mathf.Sqrt(1 + m11 - m00 - m22) * 2;
                q.w = (m02 - m20) / s;
                q.x = (m01 + m10) / s;
                q.y = 0.25f * s;
                q.z = (m12 + m21) / s;
            }
            else
            {
                float s = Mathf.Sqrt(1 + m22 - m00 - m11) * 2;
                q.w = (m10 - m01) / s;
                q.x = (m02 + m20) / s;
                q.y = (m12 + m21) / s;
                q.z = 0.25f * s;
            }

            return q;
        }

        public static Quat Normalize(Quat q)
        {
            return q.normalized;
        }

        public static Quat RotateTowards(Quat from, Quat to, float maxDegreesDelta)
        {
            float angle = Angle(from, to);
            if (angle == 0)
                return from;

            return Slerp(from, to, maxDegreesDelta / angle);
        }

        public static Quat Slerp(Quat a, Quat b, float t)
        {
            return SlerpUnclamped(a, b, Mathf.Clamp(t, 0, 1));
        }

        public static Quat SlerpUnclamped(Quat a, Quat b, float t)
        {
            a = a.normalized;
            b = b.normalized;

            if (Dot(a, b) < 0f)
            {
                b.x = -b.x;
                b.y = -b.y;
                b.z = -b.z;
                b.w = -b.w;
            }

            if (Dot(a, b) > 0.9995f)
            {
                return LerpUnclamped(a, b, t);
            }

            float angle = Mathf.Acos(Dot(a, b));
            float asin = Mathf.Sin(angle);

            return (a * Mathf.Sin((1 - t) * angle) + b * Mathf.Sin(t * angle)) / asin;
        }

        public static Vec3 ToEulerAngles(Quat rotation)
        {
            return rotation.eulerAngles;
        }
        #endregion

        #region Instance
        public void Normalize()
        {
            this = normalized;
        }

        public void Set(float newX, float newY, float newZ, float newW)
        {
            this.x = newX;
            this.y = newY;
            this.z = newZ;
            this.w = newW;
        }

        public void SetAxisAngle(Vec3 axis, float angle)
        {
            this = AngleAxis(angle, axis);
        }

        public void SetEulerAngles(Vec3 euler)
        {
            this = Euler(euler);
        }

        public void SetEulerRotation(Vec3 euler)
        {
            this = Euler(euler);
        }

        public void SetFromToRotation(Vec3 fromDirection, Vec3 toDirection)
        {
            this = FromToRotation(fromDirection, toDirection);
        }

        public void SetLookRotation(Vec3 view)
        {
            this = LookRotation(view);
        }

        public void SetLookRotation(Vec3 view, [DefaultValue("Vec3.up")] Vec3 up)
        {
            this = LookRotation(view, up);
        }

        public void ToAngleAxis(out float angle, out Vec3 axis)
        {
            angle = 2 * Mathf.Acos(w);
            float asin = Mathf.Sin(angle / 2);

            axis.x = x / asin;
            axis.y = y / asin;
            axis.z = z / asin;

            angle *= Mathf.Rad2Deg;
        }

        public Vec3 ToEuler()
        {
            return eulerAngles;
        }

        public bool Equals(Quat other)
        {
            return this == other;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return "X: " + x + " Y: " + y + " Z: " + z + " W: " + w;
        }
        #endregion
    }
}
