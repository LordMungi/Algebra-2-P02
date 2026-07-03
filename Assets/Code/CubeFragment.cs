using UnityEngine;
using CustomMath;

public class CubeFragment : MonoBehaviour
{
    private Mat4x4 TRS;
    private Mat4x4 lastTRS;
    private Mat4x4 toTRS;

    private float rotProgress;

    private void Awake()
    {
        TRS.SetTRS(transform.position, transform.rotation, Vec3.One);
        toTRS = TRS;
        lastTRS = TRS;
    }

    public void SetTargetRotation(Vec3 centerAxis, Vector3 eulerAngle)
    {
        lastTRS = toTRS;
        rotProgress = 0;

        Vec3 pos = toTRS.GetPosition();
        Quat rot = Quat.Euler(eulerAngle);

        Vec3 offset = pos - centerAxis;
        offset = rot * offset;
        pos = offset + centerAxis;

        toTRS.SetTRS(pos, rot * toTRS.rotation, Vec3.One);
        Debug.Log(toTRS);
    }

    public void MoveTowardsTarget(Vec3 centerAxis, float speed)
    {
        rotProgress += speed;

        Quat rot = Quat.Slerp(lastTRS.rotation, toTRS.rotation, rotProgress);
        Quat rotDelta = rot * Quat.Inverse(lastTRS.rotation);


        Vec3 pos = lastTRS.GetPosition(); 
        Vec3 offset =  pos - centerAxis;

        offset = rotDelta * offset;
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
