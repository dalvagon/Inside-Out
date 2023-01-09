using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerController : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public GameObject terrain;
    private LeverPuzzle leverPuzzle;

    void Start()
    {
        leverPuzzle = terrain.GetComponent<LeverPuzzle>();
    }

    void Update()
    {
        var distance = Vector3.Distance(agent.transform.position, Camera.main.transform.position);

        if (leverPuzzle.IsWin() && distance <= 100.0f)
        {
            agent.SetDestination(GetDestination());
        }
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
