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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            foreach (CubeFragment f in fragments)
            {
                f.SetTargetRotation(transform.position, new Vector3(0, 90, 0));
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            foreach (CubeFragment f in fragments)
            {
                f.SetTargetRotation(transform.position, new Vector3(90, 0, 0));
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (CubeFragment f in fragments)
            {
                f.SetTargetRotation(transform.position, new Vector3(0, 0, 90));
            }
        }

        foreach (CubeFragment f in fragments)
        {
            //f.MoveTowardsTarget(transform.position, Speed * Time.deltaTime);
        }
    }
}
