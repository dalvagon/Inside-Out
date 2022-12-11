using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField]
    private Transform startPoint;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        lineRenderer.SetPosition(0, startPoint.position);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit))
        {
            if (hit.collider)
            {
                lineRenderer.SetPosition(1, hit.point);
            }
        }
        else
        {
            lineRenderer.SetPosition(1, -transform.up * 5000);
        }
    }
}
