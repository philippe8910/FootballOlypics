using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ActorFootball : MonoBehaviour
{
    [SerializeField] private float collisionRadius;
    
    [SerializeField] private Rigidbody rigidbody;
    
    [SerializeField] private LayerMask footLayerMask;
    
    [BoxGroup]
    
    [SerializeField] private Vector3 upOffect, rightOffect, leftOffect;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    
    public bool GetTrigger(FootBallDetectedType listeningType)
    {
        var output = false;
        
        switch (listeningType)
        {
            case FootBallDetectedType.Up:
                output = Physics.CheckSphere(GetUpTiggerPosition(), collisionRadius, footLayerMask);
                break;
            
            case FootBallDetectedType.Right:
                output = Physics.CheckSphere(GetRightTiggerPosition(), collisionRadius, footLayerMask);
                break;

            case FootBallDetectedType.Left:
                output = Physics.CheckSphere(GetLeftTiggerPosition(), collisionRadius, footLayerMask);
                break;
            
            default:
                Debug.LogError("");
                break;
        }

        return output;
    }

    public GameObject GetTriggerEnterObject(FootBallDetectedType listeningType)
    {
        var output = new Collider[]{};
        
        switch (listeningType)
        {
            case FootBallDetectedType.Up:
                output = Physics.OverlapSphere(GetUpTiggerPosition(), collisionRadius, footLayerMask);
                break;

            case FootBallDetectedType.Right:
                output = Physics.OverlapSphere(GetRightTiggerPosition(), collisionRadius, footLayerMask);
                break;

            case FootBallDetectedType.Left:
                output = Physics.OverlapSphere(GetLeftTiggerPosition(), collisionRadius, footLayerMask);
                break;

            default:
                Debug.LogError("");
                break;
        }

        if (output.Length != 0)
        {
            return output[0].gameObject;
        }
        else
        {
            return null;
        }
    }

    public float GetTriggerRadius()
    {
        return collisionRadius;
    }
    
    public Vector3 GetUpTiggerPosition()
    {
        return transform.position + upOffect;
    }
    
    public Vector3 GetRightTiggerPosition()
    {
        return transform.position + rightOffect;
    }
    
    public Vector3 GetLeftTiggerPosition()
    {
        return transform.position + leftOffect;
    }
    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(GetUpTiggerPosition() , GetTriggerRadius());
        Gizmos.DrawWireSphere(GetRightTiggerPosition() , GetTriggerRadius());
        Gizmos.DrawWireSphere(GetLeftTiggerPosition() , GetTriggerRadius());

    }

    
}
