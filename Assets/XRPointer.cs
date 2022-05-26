using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class XRPointer : MonoBehaviour
{
    [SerializeField] private float m_defaultLength = 5.0f;

    [SerializeField] private GameObject m_dot;

    [SerializeField] private LineRenderer m_lineRenderer = null;
    // Start is called before the first frame update
    void Start()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        var targetLength = m_defaultLength;

        RaycastHit hit = CreateRaycastHit(targetLength);
        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        if (hit.collider)
        {
            endPosition = hit.point;
        }

        m_dot.transform.position = endPosition;
        
        m_lineRenderer.SetPosition(0 , transform.position);
        m_lineRenderer.SetPosition(1 , endPosition);
    }

    private RaycastHit CreateRaycastHit(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        Physics.Raycast(ray , out hit , m_defaultLength);

        return hit;
    }
    
}
