using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarcker : MonoBehaviour
{
    [SerializeField] Transform trackerTransform;

    private void Start()
    {
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        transform.position = trackerTransform.position;
    }
}
