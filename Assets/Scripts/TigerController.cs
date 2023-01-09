using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerController : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public GameObject terrain;
    private LeverPuzzle leverPuzzle;
    private Vector3 agentInitialPosition;
    private Vector3 playerInitialPosition;
    public GameObject tiger;

    void Start()
    {
        leverPuzzle = terrain.GetComponent<LeverPuzzle>();
        agentInitialPosition = agent.transform.position;
        playerInitialPosition = Camera.main.transform.position;
    }

    void Update()
    {
        var distance = Vector3.Distance(agent.transform.position, Camera.main.transform.position);

        if (leverPuzzle.IsWin() && distance <= 100.0f)
        {
            agent.SetDestination(GetDestination());
            tiger.GetComponent<Animator>().SetTrigger("WalkTrigger");
        }

        if (distance <= 5.0f)
        {
            Debug.Log("You died");
            agent.transform.position = agentInitialPosition;
            agent.ResetPath();
            tiger.GetComponent<Animator>().SetTrigger("WalkTrigger");
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
