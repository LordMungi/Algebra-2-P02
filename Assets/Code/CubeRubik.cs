using UnityEngine;
using CustomMath;
using System.Collections.Generic;

public class CubeRubik : MonoBehaviour
{
    [SerializeField] private float Speed = 100;

    private List<CubeFragment> fragments = new List<CubeFragment>();

    void Start()
    {
        foreach (Transform child in transform)
        {
            fragments.Add(child.gameObject.AddComponent<CubeFragment>());
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (CubeFragment f in fragments)
            {
                f.SetTargetRotation(transform.position, new Vector3(0, 90, 0));
            }
        }

        foreach (CubeFragment f in fragments)
        {
            f.MoveTowardsTarget(transform.position, Speed * Time.deltaTime);
        }
    }
}
