using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerController : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public GameObject terrain;
    public GameObject tiger;
    public GameObject door;
    private LeverPuzzle leverPuzzle;
    private Vector3 agentInitialPosition;
    private Vector3 playerInitialPosition;
    private bool hasMoved = false;

    void Start()
    {
        leverPuzzle = terrain.GetComponent<LeverPuzzle>();
        agentInitialPosition = agent.transform.position;
        playerInitialPosition = Camera.main.transform.position;
    }

    void Update()
    {
        var distance = Vector3.Distance(agent.transform.position, Camera.main.transform.position);

        if (distance >= 50.0f && hasMoved)
        {
            Debug.Log("Tiger stopped following you");
            agent.isStopped = true;
            hasMoved = false;
            Destroy(door);
            door.GetComponent<Renderer>().enabled = false;
        }

        if (leverPuzzle.IsWin() && distance < 50.0f)
        {
            hasMoved = true;
            agent.isStopped = false;
            agent.SetDestination(GetDestination());
            tiger.GetComponent<Animator>().SetTrigger("WalkTrigger");
        }

        if (distance <= 2.0f)
        {
            Debug.Log("You died");
            agent.transform.position = agentInitialPosition;
            agent.isStopped = true;
            tiger.GetComponent<Animator>().SetTrigger("StopTrigger");
            Camera.main.transform.position = playerInitialPosition;
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
                300.0f
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
