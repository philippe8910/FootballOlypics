using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : MonoBehaviour
{
    public Transform target;
    public Collider collider;
    
    void Start()
    {
        collider = GetComponent<Collider>();
        
        collider.isTrigger = true;

        StartCoroutine(startOwo());
    }

    IEnumerator startOwo()
    {
        transform.position = Vector3.Lerp(transform.position , target.position , 0.1f);
        
        if (Vector3.Distance(transform.position , target.position) >= 0.1f)
        {
            yield return new WaitForSeconds(0.01f);
            StartCoroutine(startOwo());
        }
        else
        {
            collider.isTrigger = false;
            yield return null;
        }
    }
    
}
