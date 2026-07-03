using UnityEngine;
using CustomMath;

public class CubeFragment : MonoBehaviour
{
    private Mat4x4 TRS;
    private Mat4x4 lastTRS;
    private Mat4x4 toTRS;

    private void Awake()
    {
        TRS.SetTRS(transform.position, transform.rotation, Vec3.One);
        toTRS = TRS;
        lastTRS = TRS;
        Debug.Log(TRS);
    }

    public void SetTargetRotation(Vec3 centerAxis, Vector3 eulerAngle)
    {
        Vec3 pos = TRS.GetPosition();
        Quat rot = Quat.Euler(eulerAngle);

        Vec3 offset = pos - centerAxis;
        offset = rot * offset;
        pos = offset + centerAxis;

        TRS.SetTRS(pos, rot * TRS.rotation, Vec3.One);
        SetTRStoTransform();

        Debug.Log(toTRS);
    }

    public void MoveTowardsTarget(Vec3 centerAxis, float speed)
    {
        Vec3 pos = lastTRS.GetPosition();
        Quat lastRot = TRS.rotation;
        //Quat rot = Quat.Slerp(lastTRS.rotation, toTRS.rotation, speed);
        Quat rot = Quat.Euler(new Vec3(0, 90, 0)) * (Quat)TRS.rotation;

        Vec3 offset = pos - centerAxis;
        offset = rot * offset;
        pos = offset + centerAxis;

        TRS.SetTRS(pos, rot, Vec3.One);

        SetTRStoTransform();
    }

    private void SetTRStoTransform()
    {
        transform.position = TRS.GetPosition();
        transform.rotation = TRS.rotation;
        transform.localScale = Vec3.One;
    }
}
