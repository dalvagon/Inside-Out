using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public GameObject cabin;
    public GameObject terrain;
    private MoldPuzzle moldPuzzle;
    private static float WANDER_RADIUS = 50.0f;
    private static float WANDER_TIMER = 10.0f;
    private Vector3 agentInitialPosition;
    private Vector3 playerInitialPosition;
    private float timer;

    void Start()
    {
        timer = WANDER_TIMER;
        moldPuzzle = terrain.GetComponent<MoldPuzzle>();
        agentInitialPosition = agent.transform.position;
        playerInitialPosition = Camera.main.transform.position;
    }

    void Update()
    {
        var distance = Vector3.Distance(agent.transform.position, Camera.main.transform.position);
        GetComponent<Animator>().SetTrigger("WalkTrigger");

        if (distance >= 50.0f)
        {
            Debug.Log("The alien is wandering");
            timer += Time.deltaTime;
            if (timer >= WANDER_TIMER)
            {
                Vector3 newPos = RandomNavSphere(cabin.transform.position, WANDER_RADIUS, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
        }

        if (distance > 0.0f && distance < 50.0f && moldPuzzle.IsWin())
        {
            Debug.Log("The alien is after you");
            agent.SetDestination(Camera.main.transform.position);
        }

        if (distance <= 0.0f)
        {
            Debug.Log("You died");
            GetComponent<Animator>().SetTrigger("StopTrigger");
            agent.transform.position = agentInitialPosition;
            Camera.main.transform.position = playerInitialPosition;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        UnityEngine.AI.NavMeshHit navHit;

        UnityEngine.AI.NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
