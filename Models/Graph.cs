using System.Collections.Generic;
using System.Linq;

namespace C200.Models
{
    public class Graph
    {
        private string[] nodes;
        public IDictionary<string, List<NodeAndDistance>> FullGraph { get; }

        public Graph(string[] nodes, IDictionary<string, List<NodeAndDistance>> initGraph)
        {
            this.nodes = nodes;
            FullGraph = Construct(nodes, initGraph);
        }   

        private IDictionary<string, List<NodeAndDistance>> Construct(string[] nodes, IDictionary<string, List<NodeAndDistance>> initGraph)
        {
            // Create a blank graph.
            IDictionary<string, List<NodeAndDistance>> graph = new Dictionary<string, List<NodeAndDistance>>();

            // Populate graph with nodes (no adjacent nodes).
            foreach (string node in nodes) 
                graph.Add(node, new List<NodeAndDistance>());

            // Update the graph with adjacent nodes.
            foreach (KeyValuePair<string, List<NodeAndDistance>> node in graph)
                graph[node.Key] = initGraph[node.Key];

            // Ensure symmetry between adjacent nodes.
            foreach (KeyValuePair<string, List<NodeAndDistance>> node in graph)
            {
                foreach (NodeAndDistance adjacentNode in node.Value)
                {
                    bool found = false;

                    if (graph[adjacentNode.Node].Any())

                        foreach (NodeAndDistance adjacentNodeOfAdjacentNode in graph[adjacentNode.Node])

                            if (node.Key == adjacentNodeOfAdjacentNode.Node)
                            {
                                found = true;
                                break;
                            }

                    if (!found)
                    {
                        graph[adjacentNode.Node].Add(new NodeAndDistance 
                        {
                            Node = node.Key, 
                            Distance = adjacentNode.Distance
                        });
                    }
                }
            }

            return graph;
        }     

        public List<string> Nodes()
        {
            List<string> nodeList = new List<string>();
            foreach (string node in nodes)
                nodeList.Add(node);

            return nodeList;
        }

        public List<string> Neighbours(string node)
        {
            List<string> connections = new List<string>();
            foreach (string outgoingNode in nodes)

                foreach (NodeAndDistance nodeDist in FullGraph[node])

                    if (nodeDist.Node == outgoingNode)
                        connections.Add(outgoingNode);

            return connections;
        }

        public int Distance(string node1, string node2)
        {
            int distance = -1;
            foreach (NodeAndDistance nodeDist in FullGraph[node1])

                if (nodeDist.Node == node2)
                    distance = nodeDist.Distance;

            return distance;
        }
    }
}