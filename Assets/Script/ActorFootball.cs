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
    
    [SerializeField] private Vector3 upOffset, rightOffset, leftOffset;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        
    }
    
    public bool GetAreaTrigger(FootBallAreaType listeningType) //足球的相對位置是否觸發
    {
        var output = false;
        
        switch (listeningType)
        {
            case FootBallAreaType.UpArea:
                output = Physics.CheckSphere(GetUpTiggerPosition(), collisionRadius, footLayerMask);
                break;
            
            case FootBallAreaType.RightArea:
                output = Physics.CheckSphere(GetRightTiggerPosition(), collisionRadius, footLayerMask);
                break;

            case FootBallAreaType.LeftArea:
                output = Physics.CheckSphere(GetLeftTiggerPosition(), collisionRadius, footLayerMask);
                break;
            
            default:
                Debug.LogError("Wrong Type!!!");
                break;
        }

        return output;
    }

    public GameObject GetTriggerEnterObject(FootBallAreaType listeningType) //觸發的物件 不分類物件
    {
        var output = new Collider[]{};

        switch (listeningType)
        {
            case FootBallAreaType.UpArea:
                output = Physics.OverlapSphere(GetUpTiggerPosition(), collisionRadius);
                break;
            
            case FootBallAreaType.RightArea:
                output = Physics.OverlapSphere(GetRightTiggerPosition(), collisionRadius);
                break;
            
            case FootBallAreaType.LeftArea:
                output = Physics.OverlapSphere(GetLeftTiggerPosition(), collisionRadius);
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
    
    public bool GetTriggerEnterObject(FootBallAreaType listeningType , FootType footType) //觸發的物件 判斷是哪個腳
    {
        var _colliders = new Collider[]{};

        switch (listeningType)
        {
            case FootBallAreaType.UpArea:
                _colliders = Physics.OverlapSphere(GetUpTiggerPosition(), collisionRadius, footLayerMask);
                break;
            
            case FootBallAreaType.RightArea:
                _colliders = Physics.OverlapSphere(GetRightTiggerPosition(), collisionRadius, footLayerMask);
                break;
            
            case FootBallAreaType.LeftArea:
                _colliders = Physics.OverlapSphere(GetLeftTiggerPosition(), collisionRadius, footLayerMask);
                break;
        }

        if (_colliders.Length != 0)
        {
            if (footType == FootType.RightFoot)
            {
                return _colliders[0].CompareTag("RightFoot");
            }
            else
            {
                return _colliders[0].CompareTag("LeftFoot");
            }
        }
        else
        {
            return false;
        }

    }

    public float GetTriggerRadius()
    {
        return collisionRadius;
    }
    
    public Vector3 GetUpTiggerPosition()
    {
        return transform.position + upOffset;
    }
    
    public Vector3 GetRightTiggerPosition()
    {
        return transform.position + rightOffset;
    }
    
    public Vector3 GetLeftTiggerPosition()
    {
        return transform.position + leftOffset;
    }
    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(GetUpTiggerPosition() , GetTriggerRadius());
        Gizmos.DrawWireSphere(GetRightTiggerPosition() , GetTriggerRadius());
        Gizmos.DrawWireSphere(GetLeftTiggerPosition() , GetTriggerRadius());

    }

    
}
