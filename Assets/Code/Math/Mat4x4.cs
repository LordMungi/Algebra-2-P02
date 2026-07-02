using System;
using UnityEngine;

namespace CustomMath
{
    public struct Mat4x4 : IEquatable<Mat4x4>, IFormattable
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
        public static Mat4x4 Zero { get { return new Mat4x4(new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 0)); } }
        public static Mat4x4 Identity { get { return new Mat4x4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1)); } }
        public Mat4x4 inverse 
        { 
            get 
            {
                if (determinant == 0)
                    return this;

                Mat4x4 id = Identity;
                float[,] mat =
                {
                    { m00, m01, m02, m03, id.m00, id.m01, id.m02, id.m03 },
                    { m10, m11, m12, m13, id.m10, id.m11, id.m12, id.m13 },
                    { m20, m21, m22, m23, id.m20, id.m21, id.m22, id.m23 },
                    { m30, m31, m32, m33, id.m30, id.m31, id.m32, id.m33 },
                };

                for (int col = 0; col < 4; col++)
                {
                    int pivotRow = col;
                    float max = 0;


                    for (int row = col; row < 4; row++)
                    {
                        float val = Mathf.Abs(mat[row, col]);
                        if (val > max)
                        {
                            max = val;
                            pivotRow = row;
                        }
                    }

                    if (pivotRow != col)
                    { 
                        for (int j = 0; j < 8; j++)
                        {
                            float aux = mat[col, j];
                            mat[col, j] = mat[pivotRow, j];
                            mat[pivotRow, j] = aux;
                        }
                    }

                    float div = mat[col, col];

                    for (int j = 0; j < 8; j++)
                    {
                        mat[col, j] /= div;
                    }

                    for (int row = 0; row < 4; row++)
                    {
                        if (row == col)
                            continue;

                        float val = mat[row, col];

                        for (int j = 0; j < 8; j++)
                        {
                            mat[row, j] -= val * mat[col, j];
                        }
                    }
                }

                return new Mat4x4(
                    new Vector4(mat[0, 4], mat[0, 5], mat[0, 6], mat[0, 7]),
                    new Vector4(mat[1, 4], mat[1, 5], mat[1, 6], mat[1, 7]),
                    new Vector4(mat[2, 4], mat[2, 5], mat[2, 6], mat[2, 7]),
                    new Vector4(mat[3, 4], mat[3, 5], mat[3, 6], mat[3, 7])
                    );
            } 
        }
        public float determinant
        {
            get
            {
                float[,] mat =
                {
                    { m00, m01, m02, m03 },
                    { m10, m11, m12, m13 },
                    { m20, m21, m22, m23 },
                    { m30, m31, m32, m33 },
                };
                float sign = 1f;

                for (int col = 0; col < 4; col++)
                {

                    int pivotRow = col;
                    float max = 0;

                    for (int row = col; row < 4; row++)
                    {
                        float val = Mathf.Abs(mat[row, col]);
                        if (val > max)
                        {
                            max = val;
                            pivotRow = row;
                        }
                    }

                    if (pivotRow != col)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            float aux = mat[col, j];
                            mat[col, j] = mat[pivotRow, j];
                            mat[pivotRow, j] = aux;
                        }
                        sign *= -1f;
                    }

                    float pivot = mat[col, col];
                    for (int row = col + 1; row < 4; row++)
                    {
                        float val = mat[row, col] / pivot;

                        for (int j = col; j < 4; j++)
                        {
                            mat[row, j] -= val * mat[col, j];
                        }
                    }
                }

                return sign * mat[0, 0] * mat[1, 1] * mat[2, 2] * mat[3, 3];
            }
        }

        public bool isIdentity { get { return this == Identity; } }
        public Quat rotation
        {
            get
            {
                Vec3 s = lossyScale;
                float r00 = m00 / s.x;
                float r10 = m10 / s.x;
                float r20 = m20 / s.x;

                float r01 = m01 / s.y;
                float r11 = m11 / s.y;
                float r21 = m21 / s.y;

                float r02 = m02 / s.z;
                float r12 = m12 / s.z;
                float r22 = m22 / s.z;
                float mtr = r00 + r11 + r22;

                Quat q;

                if (mtr > 0)
                {
                    float sqr = Mathf.Sqrt(mtr + 1) * 2;
                    q.w = 0.25f * sqr;
                    q.x = (r21 - r12) / sqr;
                    q.y = (r02 - r20) / sqr;
                    q.z = (r10 - r01) / sqr;
                }
                else if (r00 > r11 && r00 > r22)
                {
                    float sqr = Mathf.Sqrt(1 + r00 - r11 - r22) * 2;
                    q.w = (r21 - r12) / sqr;
                    q.x = 0.25f * sqr;
                    q.y = (r01 + r10) / sqr;
                    q.z = (r02 + r20) / sqr;
                }
                else if (r11 > r22)
                {
                    float sqr = Mathf.Sqrt(1 + r11 - r00 - r22) * 2;
                    q.w = (r02 - r20) / sqr;
                    q.x = (r01 + r10) / sqr;
                    q.y = 0.25f * sqr;
                    q.z = (r12 + r21) / sqr;
                }
                else
                {
                    float sqr = Mathf.Sqrt(1 + r22 - r00 - r11) * 2;
                    q.w = (r10 - r01) / sqr;
                    q.x = (r02 + r20) / sqr;
                    q.y = (r12 + r21) / sqr;
                    q.z = 0.25f * sqr;
                }

                return q;
            }
        }
        public Vec3 lossyScale
        {
            get
            {
                Vec3 s;
                s.x = Mathf.Sqrt(m00 * m00 + m10 * m10 + m20 * m20);
                s.y = Mathf.Sqrt(m01 * m01 + m11 * m11 + m21 * m21);
                s.z = Mathf.Sqrt(m02 * m02 + m12 * m12 + m22 * m22);
                return s;
            }
        }
        public Mat4x4 transpose
        {
            get
            {
                return new Mat4x4(
                    new Vector4(m00, m10, m20, m30),
                    new Vector4(m01, m11, m21, m31),
                    new Vector4(m02, m12, m22, m32),
                    new Vector4(m03, m13, m23, m33)
                    );
            }
        }
        #endregion

        #region Constructors
        Mat4x4(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
        {
            m00 = column0.x;
            m10 = column0.y;
            m20 = column0.z;
            m30 = column0.w;
            m01 = column1.x;
            m11 = column1.y;
            m21 = column1.z;
            m31 = column1.w;
            m02 = column2.x;
            m12 = column2.y;
            m22 = column2.z;
            m32 = column2.w;
            m03 = column3.x;
            m13 = column3.y;
            m23 = column3.z;
            m33 = column3.w;
        }

        Mat4x4(Matrix4x4 clone)
        {
            m00 = clone.m00;
            m10 = clone.m10;
            m20 = clone.m20;
            m30 = clone.m30;
            m01 = clone.m01;
            m11 = clone.m11;
            m21 = clone.m21;
            m31 = clone.m31;
            m02 = clone.m02;
            m12 = clone.m12;
            m22 = clone.m22;
            m32 = clone.m32;
            m03 = clone.m03;
            m13 = clone.m13;
            m23 = clone.m23;
            m33 = clone.m33;
        }
        #endregion

        #region Operators
        public static Vector4 operator *(Mat4x4 lhs, Vector4 vector)
        {
            return new Vector4(
                lhs.m00 * vector.x + lhs.m01 * vector.y + lhs.m02 * vector.z + lhs.m03 * vector.w,
                lhs.m10 * vector.x + lhs.m11 * vector.y + lhs.m12 * vector.z + lhs.m13 * vector.w,
                lhs.m20 * vector.x + lhs.m21 * vector.y + lhs.m22 * vector.z + lhs.m23 * vector.w,
                lhs.m30 * vector.x + lhs.m31 * vector.y + lhs.m32 * vector.z + lhs.m33 * vector.w
                );
        }

        public static Mat4x4 operator *(Mat4x4 lhs, Mat4x4 rhs)
        {
            return new Mat4x4(
                new Vector4(
                    lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20 + lhs.m03 * rhs.m30,
                    lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20 + lhs.m13 * rhs.m30,
                    lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20 + lhs.m23 * rhs.m30,
                    lhs.m30 * rhs.m00 + lhs.m31 * rhs.m10 + lhs.m32 * rhs.m20 + lhs.m33 * rhs.m30
                    ),
                new Vector4(
                    lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21 + lhs.m03 * rhs.m31,
                    lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31,
                    lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31,
                    lhs.m30 * rhs.m01 + lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31
                    ),
                new Vector4(
                    lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22 + lhs.m03 * rhs.m32,
                    lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32,
                    lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32,
                    lhs.m30 * rhs.m02 + lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32
                    ),
                new Vector4(
                    lhs.m00 * rhs.m03 + lhs.m01 * rhs.m13 + lhs.m02 * rhs.m23 + lhs.m03 * rhs.m33,
                    lhs.m10 * rhs.m03 + lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33,
                    lhs.m20 * rhs.m03 + lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33,
                    lhs.m30 * rhs.m03 + lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33
                    )
                );
        }

        public static bool operator ==(Mat4x4 lhs, Mat4x4 rhs)
        {
            return lhs.m00 == rhs.m00 &&
            lhs.m10 == rhs.m10 &&
            lhs.m20 == rhs.m20 &&
            lhs.m30 == rhs.m30 &&
            lhs.m01 == rhs.m01 &&
            lhs.m11 == rhs.m11 &&
            lhs.m21 == rhs.m21 &&
            lhs.m31 == rhs.m31 &&
            lhs.m02 == rhs.m02 &&
            lhs.m12 == rhs.m12 &&
            lhs.m22 == rhs.m22 &&
            lhs.m32 == rhs.m32 &&
            lhs.m03 == rhs.m03 &&
            lhs.m13 == rhs.m13 &&
            lhs.m23 == rhs.m23 &&
            lhs.m33 == rhs.m33;
        }

        public static bool operator !=(Mat4x4 lhs, Mat4x4 rhs)
        {
            return !(lhs == rhs);
        }

        public static implicit operator Mat4x4(Matrix4x4 clone)
        {
            return new Mat4x4(clone);
        }
        #endregion

        #region Static
        public static float Determinant(Mat4x4 m)
        {
            return m.determinant;
        }

        public static Mat4x4 Inverse(Mat4x4 m)
        {
            return m.inverse;
        }

        public static Mat4x4 LookAt(Vec3 from, Vec3 to, Vec3 up)
        {
            return Mat4x4.TRS(from, Quaternion.LookRotation(to - from, up), Vec3.One);
        }

        public static Mat4x4 Rotate(Quat q)
        {
            return new Mat4x4(
                new Vector4(
                    1 - 2 * (q.y * q.y + q.z * q.z),
                    2 * (q.x * q.y + q.w * q.z),
                    2 * (q.x * q.z - q.w * q.y),
                    0
                    ),
                new Vector4(
                    2 * (q.x * q.y - q.w * q.z),
                    1 - 2 * (q.x * q.x + q.z * q.z),
                    2 * (q.y * q.z + q.w * q.x),
                    0
                    ),
                new Vector4(
                    2 * (q.x * q.z + q.w * q.y),
                    2 * (q.y * q.z - q.w * q.x),
                    1 - 2 * (q.x * q.x + q.y * q.y),
                    0
                    ),
                new Vector4(0, 0, 0, 1)
                );
        }

        public static Mat4x4 Scale(Vec3 vector)
        {
            return new Mat4x4(
                new Vector4(vector.x, 0, 0, 0),
                new Vector4(0, vector.y, 0, 0),
                new Vector4(0, 0, vector.z, 0),
                new Vector4(0, 0, 0, 1)
            );
        }

        public static Mat4x4 Translate(Vec3 vector)
        {
            return new Mat4x4(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(vector.x, vector.y, vector.z, 1)
            );
        }

        public static Mat4x4 Transpose(Mat4x4 m)
        {
            return m.transpose;
        }

        public static Mat4x4 TRS(Vec3 pos, Quat q, Vec3 s)
        {
            return Translate(pos) * Rotate(q) * Scale(s);
        }
        #endregion

        #region Instance
        public Vec3 GetPosition()
        {
            return new Vec3(m03, m13, m23);
        }

        public Vector4 GetRow(int index)
        {
            switch (index)
            {
                case 0:
                    return new Vector4(m00, m01, m02, m03);
                case 1:
                    return new Vector4(m10, m11, m12, m13);
                case 2:
                    return new Vector4(m20, m21, m22, m23);
                case 3:
                    return new Vector4(m30, m31, m32, m33);
                default:
                    return Vector4.zero;
            }
        }

        public Vec3 MultiplyPoint(Vec3 point)
        {
            Vector4 result = this * new Vector4(point.x, point.y, point.z, 1);

            if (result.w != 0)
            {
                float ww = 1 / result.w;
                return new Vec3(result.x * ww, result.y * ww, result.z * ww);
            }

            return new Vec3(result.x, result.y, result.z);
        }

        public Vec3 MultiplyPoint3x4(Vec3 point)
        {
            return new Vec3(
                m00 * point.x + m01 * point.y + m02 * point.z + m03,
                m10 * point.x + m11 * point.y + m12 * point.z + m13,
                m20 * point.x + m21 * point.y + m22 * point.z + m23
            );
        }

        public Vec3 MultiplyVector(Vec3 vector)
        {
            Vector4 result = this * new Vector4(vector.x, vector.y, vector.z, 0);
            return new Vec3(result.x, result.y, result.z);
        }

        public void SetColumn(int index, Vector4 column)
        {
            switch (index)
            {
                case 0:
                    m00 = column.x;
                    m10 = column.y;
                    m20 = column.z;
                    m30 = column.w;
                    break;
                case 1:
                    m01 = column.x;
                    m11 = column.y;
                    m21 = column.z;
                    m31 = column.w;
                    break;
                case 2:
                    m02 = column.x;
                    m12 = column.y;
                    m22 = column.z;
                    m32 = column.w;
                    break;
                case 3:
                    m03 = column.x;
                    m13 = column.y;
                    m23 = column.z;
                    m33 = column.w;
                    break;
                default:
                    break;
            }
        }

        public void SetRow(int index, Vector4 row)
        {
            switch (index)
            {
                case 0:
                    m00 = row.x;
                    m01 = row.y;
                    m02 = row.z;
                    m03 = row.w;
                    break;
                case 1:
                    m10 = row.x;
                    m11 = row.y;
                    m12 = row.z;
                    m13 = row.w;
                    break;
                case 2:
                    m20 = row.x;
                    m21 = row.y;
                    m22 = row.z;
                    m23 = row.w;
                    break;
                case 3:
                    m30 = row.x;
                    m31 = row.y;
                    m32 = row.z;
                    m33 = row.w;
                    break;
                default:
                    break;
            }
        }

        public void SetTRS(Vec3 pos, Quat q, Vec3 s)
        {
            this = TRS(pos, q, s);
        }

        public bool ValidTRS()
        {
            if (m30 != 0 || m31 != 0 || m32 != 0 || m33 != 1)
                return false;

            Vec3 x = new Vec3(m00, m10, m20);
            Vec3 y = new Vec3(m01, m11, m21);
            Vec3 z = new Vec3(m02, m12, m22);

            float mx = x.magnitude;
            float my = y.magnitude;
            float mz = z.magnitude;

            if (mx == 0 || my == 0 || mz == 0)
                return false;

            x /= mx;
            y /= my;
            z /= mz;

            if (Mathf.Abs(Vec3.Dot(x, y)) != 0 || Mathf.Abs(Vec3.Dot(x, z)) != 0 || Mathf.Abs(Vec3.Dot(y, z)) != 0)
                return false;

            return true;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return "[ " + m00 + " " + m01 + " " + m02 + " " + m03 + " ] " + "[ " + m10 + " " + m11 + " " + m12 + " " + m13 + " ] " +
                "[ " + m20 + " " + m21 + " " + m22 + " " + m23 + " ] " + "[ " + m30 + " " + m31 + " " + m32 + " " + m33 + " ]";
        }

        public bool Equals(Mat4x4 other)
        {
            return this == other;
        }
        #endregion
    }
}
