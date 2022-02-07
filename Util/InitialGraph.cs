using System.Collections.Generic;
using C200.Models;

namespace C200.Util
{
    public class InitialGraph
    {
        public static Graph Create()
        {
            string[] nodes = { "A", "B", "C", "D", "E" };
            IDictionary<string, List<NodeAndDistance>> initGraph = new Dictionary<string, List<NodeAndDistance>>();

            List<NodeAndDistance> neighboursNodeA = new List<NodeAndDistance>();
            neighboursNodeA.Add(new NodeAndDistance 
            {
                Node = "B", 
                Distance = 1
            });
            initGraph.Add("A", neighboursNodeA);
            
            List<NodeAndDistance> neighboursNodeB = new List<NodeAndDistance>(); 
            neighboursNodeB.Add(new NodeAndDistance
            {
                Node = "C", 
                Distance = 1
            });
            neighboursNodeB.Add(new NodeAndDistance 
            {
                Node = "D",
                Distance = 1
            });
            initGraph.Add("B", neighboursNodeB);

            List<NodeAndDistance> neighboursNodeC = new List<NodeAndDistance>();
            initGraph.Add("C", neighboursNodeC);

            List<NodeAndDistance> neighboursNodeD = new List<NodeAndDistance>();
            neighboursNodeD.Add(new NodeAndDistance
            {
                Node = "E",
                Distance = 1
            });
            initGraph.Add("D", neighboursNodeD);

            List<NodeAndDistance> neighboursNodeE = new List<NodeAndDistance>();
            initGraph.Add("E", neighboursNodeE);

            return new Graph(nodes, initGraph);
        }    
    }
}