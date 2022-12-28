using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerController : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(GetDestination());
    }

    private Vector3 GetDestination()
    {
        RaycastHit hit;

        if (
            Physics.Raycast(
                Camera.main.transform.position,
                Camera.main.transform.forward,
                out hit,
                100.0f
            )
        )
        {
            if (hit.collider)
            {
                return hit.point;
            }
        }

        return Camera.main.transform.position;
    }
}
