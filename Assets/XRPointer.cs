using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Valve.VR;

public class XRPointer : MonoBehaviour
{
    [SerializeField] private float m_defaultLength = 5.0f;

    [SerializeField] private GameObject m_dot;

    [SerializeField] private LineRenderer m_lineRenderer = null;

    [SerializeField] private SteamVR_Action_Boolean inputActionBoolean;
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

    private void UpdateInputController(RaycastHit hit)
    {
        //inputActionBoolean[SteamVR_Input_Sources.Any].active
        
        if (inputActionBoolean[SteamVR_Input_Sources.Any].active)
        {
            if (hit.collider.GetComponent<Button>())
            {
                var button = hit.collider.GetComponent<Button>();
                
                button.onClick.Invoke();
            }
        }
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

        UpdateInputController(hit);
    }

    private RaycastHit CreateRaycastHit(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        Physics.Raycast(ray , out hit , m_defaultLength);

        return hit;
    }
    
}
