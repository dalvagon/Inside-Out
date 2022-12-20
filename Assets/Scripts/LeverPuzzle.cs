using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LeverPuzzleUtil;

public class LeverPuzzle : MonoBehaviour
{
    public List<GameObject> levers = new List<GameObject>();
    public GameObject mainCamera;
    public GameObject gate;
    private Graph graph;
    private Dictionary<GameObject, Node> leverToNode;
    private bool win = false;

    // Start is called before the first frame update
    void Start()
    {
        graph = new Graph();
        leverToNode = new Dictionary<GameObject, Node>();
        for (int i = 0; i < levers.Count; i++)
        {
            graph.AddNode(new Node { Value = i, Flag = false });
            leverToNode.Add(levers[i], graph.Nodes[i]);
        }
        graph.AddEdge(graph.Nodes[0], graph.Nodes[1]);
        graph.AddEdge(graph.Nodes[0], graph.Nodes[2]);
        graph.AddEdge(graph.Nodes[3], graph.Nodes[0]);
        graph.AddEdge(graph.Nodes[3], graph.Nodes[2]);
        graph.AddEdge(graph.Nodes[2], graph.Nodes[0]);
        graph.AddEdge(graph.Nodes[4], graph.Nodes[5]);

        // 0 (top-left), 3 (top-right), 2 (bottom-left), 4 (bottom-right)
    }

    void FadeOutDoor()
    {
        Color color1 = Color.white;
        Color color2 = new Color(1f, 1f, 1f, 0f);
        float duration = 1f;
        float t = 0f;
        float fadeSpeed = 0.5f;
        while (t < duration)
        {
            t += Time.deltaTime * fadeSpeed;
            gate.GetComponent<Renderer>().material.color = Color.Lerp(color1, color2, t / duration);
        }

        gate.GetComponent<Renderer>().material.color = color2;

        // destroy gate object
        Destroy(gate);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !win)
        {
            GameObject minLever = null;
            float minDistance = float.MaxValue;

            foreach (var lever in levers)
            {
                if (lever.GetComponent<MoveLever>().IsLeverInFront(mainCamera.transform.position, mainCamera.transform.forward, levers.IndexOf(lever)))
                {
                    float distance = Vector3.Distance(mainCamera.transform.position, lever.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        minLever = lever;
                    }
                }
            }

            bool[] prev_flags = new bool[graph.Nodes.Count];
            for (int i = 0; i < graph.Nodes.Count; i++)
            {
                prev_flags[i] = graph.Nodes[i].Flag;
            }
            
            graph.SetFlagsForInput(leverToNode[minLever]);

            for (int i = 0; i < graph.Nodes.Count; i++)
            {
                if (prev_flags[i] != graph.Nodes[i].Flag)
                {
                    print("Lever " + (i + 1) + " is " + (graph.Nodes[i].Flag ? "on" : "off"));
                    levers[i].GetComponent<Animator>().SetTrigger("Move");
                }
            }
        }
        
        if (graph.IsSolved() && !win)
        {
            win = true;
            print("You win!");
            FadeOutDoor();
            graph.ResetFlags();
        }
    }
}
