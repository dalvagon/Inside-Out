using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeverPuzzleUtil
{
    class Node
    {
        public int Value { get; set; }
        public bool Flag { get; set; }

        public Node()
        {
            Value = 0;
            Flag = false;
        }

        public Node(int value, bool flag)
        {
            Value = value;
            Flag = flag;
        }

        public override string ToString()
        {
            return "Node " + Value + " Flag " + Flag;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Node node = obj as Node;
            if (node == null)
            {
                return false;
            }

            return Value == node.Value;
        }

        public override int GetHashCode()
        {
            return Value;
        }
    }

    class Edge
    {
        public Node From { get; set; }
        public Node To { get; set; }

        public Edge()
        {
            From = new Node();
            To = new Node();
        }

        public Edge(Node from, Node to)
        {
            From = from;
            To = to;
        }

        public override string ToString()
        {
            return "Edge " + From + " " + To;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Edge edge = obj as Edge;
            if (edge == null)
            {
                return false;
            }

            return From.Equals(edge.From) && To.Equals(edge.To);
        }

        public override int GetHashCode()
        {
            return From.GetHashCode() + To.GetHashCode();
        }
    }

    class Graph
    {
        public List<Node> Nodes { get; set; }
        public List<Edge> Edges { get; set; }

        public Graph()
        {
            Nodes = new List<Node>();
            Edges = new List<Edge>();
        }

        public void AddNode(Node node)
        {
            Nodes.Add(node);
        }

        public void AddEdge(Edge edge)
        {
            Edges.Add(edge);
        }

        public void AddEdge(Node from, Node to)
        {
            Edges.Add(new Edge { From = from, To = to });
        }

        public void SetFlagsForInput(Node node)
        {
            node.Flag = !node.Flag ? true : false;
            foreach (var edge in Edges)
            {
                if (edge.From.Value == node.Value)
                {
                    Debug.Log("hereeeeee " + edge);
                    edge.To.Flag = !edge.To.Flag ? true : false;
                }
            }
        }

        public void SetFlagsForInput(int value)
        {
            foreach (var node in Nodes)
            {
                if (node.Value == value)
                {
                    node.Flag = !node.Flag ? true : false;
                    foreach (var edge in Edges)
                    {
                        if (edge.From.Value == node.Value)
                        {
                            edge.To.Flag = !edge.To.Flag ? true : false;
                        }
                    }
                }
            }
        }

        public void RemoveNode(Node node)
        {
            Nodes.Remove(node);
        }

        public void RemoveEdge(Edge edge)
        {
            Edges.Remove(edge);
        }

        public void RemoveEdge(Node from, Node to)
        {
            foreach (var edge in Edges)
            {
                if (edge.From == from && edge.To == to)
                {
                    Edges.Remove(edge);
                }
            }
        }

        public void ResetFlags()
        {
            foreach (var node in Nodes)
            {
                node.Flag = false;
            }
        }

        public bool IsSolved()
        {
            foreach (var node in Nodes)
            {
                if (node.Flag == false)
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            string result = "Graph: ";
            foreach (var node in Nodes)
            {
                result += node + " ";
            }
            result += "\n";
            foreach (var edge in Edges)
            {
                result += edge + " ";
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Graph graph = obj as Graph;
            if (graph == null)
            {
                return false;
            }

            if (Nodes.Count != graph.Nodes.Count || Edges.Count != graph.Edges.Count)
            {
                return false;
            }

            foreach (var node in Nodes)
            {
                if (!graph.Nodes.Contains(node))
                {
                    return false;
                }
            }

            foreach (var edge in Edges)
            {
                if (!graph.Edges.Contains(edge))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int result = 0;
            foreach (var node in Nodes)
            {
                result += node.GetHashCode();
            }
            foreach (var edge in Edges)
            {
                result += edge.GetHashCode();
            }
            return result;
        }
    }   
}