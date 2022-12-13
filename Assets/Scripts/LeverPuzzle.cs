using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LeverPuzzleUtil;

public class LeverPuzzle : MonoBehaviour
{
    public List<GameObject> levers = new List<GameObject>();
    public GameObject mainCamera;
    private Graph graph;
    private Dictionary<GameObject, Node> leverToNode;

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
        graph.AddEdge(graph.Nodes[2], graph.Nodes[1]);
        graph.AddEdge(graph.Nodes[2], graph.Nodes[3]);
        graph.AddEdge(graph.Nodes[2], graph.Nodes[0]);
        graph.AddEdge(graph.Nodes[1], graph.Nodes[3]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (var lever in levers)
            {
                if (lever.GetComponent<MoveLever>().IsLeverInFront(mainCamera.transform.position, mainCamera.transform.forward, levers.IndexOf(lever)))
                {
                    bool[] prev_flags = new bool[graph.Nodes.Count];
                    for (int i = 0; i < graph.Nodes.Count; i++)
                    {
                        prev_flags[i] = graph.Nodes[i].Flag;
                    }
                    
                    graph.SetFlagsForInput(leverToNode[lever]);

                    for (int i = 0; i < graph.Nodes.Count; i++)
                    {
                        if (prev_flags[i] != graph.Nodes[i].Flag)
                        {
                            print("Lever " + (i + 1) + " is " + (graph.Nodes[i].Flag ? "on" : "off"));
                            levers[i].GetComponent<Animator>().SetTrigger("Move");
                        }
                    }
                    break;
                }
            }
        }
        
        if (graph.IsSolved())
        {
            print("You win!");
            graph.ResetFlags();
        }
    }
}
