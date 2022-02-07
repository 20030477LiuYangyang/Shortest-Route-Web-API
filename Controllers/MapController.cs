using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using C200.Models;
using C200.Util;

namespace C200.Controllers
{
    public class MapController : Controller
    {
        private IDictionary<string, int> NodesCost;
        private IDictionary<string, string> PreviousNodes;
        private List<string> ShortestPath;
        private int TotalCost; 

        [HttpPost]
        [Route("map/route")]
        public IActionResult Post(string start, string target)
        {
            Graph graph = InitialGraph.Create();
            DijkstraAlgorithm(graph, start);
            ProcessAlgorithm(PreviousNodes, NodesCost, start, target);

            string output = "";
            foreach (string node in ShortestPath)
            {
                output += node + ",";
            }
            output = output.Substring(0, output.Length - 1);

            var response = new { Route=output };

            return Json(response);
        }

        public void DijkstraAlgorithm(Graph graph, string start) 
        {
            List<string> unvisited = graph.Nodes();
            NodesCost = new Dictionary<string, int>();
            PreviousNodes = new Dictionary<string, string>();

            foreach (string node in unvisited)

                if (node == start)
                    NodesCost.Add(node, 0);

                else
                    NodesCost.Add(node, Int32.MaxValue);

            while (unvisited.Any())
            {
                string currentMinNode = null;

                foreach (string node in unvisited)
                {
                    if (currentMinNode == null)
                        currentMinNode = node;
                    
                    else if (NodesCost[node] < NodesCost[currentMinNode])
                        currentMinNode = node;
                }

                List<string> neighbours = graph.Neighbours(currentMinNode);
                foreach (string neighbour in neighbours)
                {
                    int tentativeValue = NodesCost[currentMinNode] + graph.Distance(currentMinNode, neighbour);
                    
                    if (tentativeValue < NodesCost[neighbour])
                    {
                        NodesCost[neighbour] = tentativeValue;
                        PreviousNodes[neighbour] = currentMinNode;
                    }
                }

                unvisited.Remove(currentMinNode);
            }
        }

        public void ProcessAlgorithm(IDictionary<string, string> previousNodes, IDictionary<string, int> nodesCost, string start, string target)
        {
            List<string> path = new List<string>();
            string node = target;

            while (node != start)
            {
                path.Add(node);
                node = previousNodes[node];
            }

            path.Add(start);

            List<string> output = new List<string>();
            for (int i = path.Count() - 1; i >= 0; i--)
                output.Add(path[i]);

            ShortestPath = output;
            TotalCost = nodesCost[target];
        }
    }
}