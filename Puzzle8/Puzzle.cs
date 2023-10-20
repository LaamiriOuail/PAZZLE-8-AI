using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Puzzle8
{
    internal class Puzzle
    {
        public Node InitialeState {  get; set; }
        public Node TargetState { get; set; }
        public List<Node> Nodes { get; set; }   
        public Puzzle()
        {
            Nodes = new List<Node>();
            InitialeState = new Node("Null", new int[3, 3] { { 3,2,5 }, { 7,1,0 }, { 4,6,8 } });
                /*new Node("Null", new int[3, 3] { { 6,1,4 }, { 7,0,3 }, { 2,5,8 } })*/
            TargetState = new Node("Null", new int[3, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 } });
            Nodes.Add(InitialeState);
                
        } 
        public bool IsTargetExist()
        {
            foreach (var node in Nodes)
            {
                if (node.Equals(TargetState))
                {
                    return true;
                    
                }
            }
            return false;
        }
        
        public void GetStateSpace()
        {
            bool TargetExist = InitialeState.Equals(TargetState);
            List<Node> currentNodes;
            while (!TargetExist)
            {
                currentNodes = new List<Node>(Nodes); // Create a copy
                foreach (var node in currentNodes)
                {
                    if (!node.Status)
                    {
                        if (TargetExist = node.GetChildrenNode(Nodes, TargetState))
                        {
                            break;
                        }
                    }
                }

            }
        }
        private Node GetNodeById(string Id)
        {
            foreach(var node in Nodes)
            {
                if(node.Id == Id)
                {
                    return node;
                }
            }
            return null;
        }
        private string GetTargetNodeId()
        {
            foreach (var node in Nodes)
            {
                if (node.Equals(TargetState))
                {
                    return node.Id;
                }
            }
            return "Null";
        }
        

        public void PrintStateSpace()
        {
            foreach (var node in Nodes)
            {
                node.PrintNode();
            }

        }
        public List<Node> PathToTarget()
        {
            List<Node> Path;
            Node NewNode;
            string TargetNodeId=GetTargetNodeId();
            int i = 1;
            Path = new List<Node>();
            while (TargetNodeId != "Null")
            {
                NewNode=new Node(GetNodeById(TargetNodeId));
                TargetNodeId = NewNode.ParentId;
                NewNode.Id="Node "+ i++;
                if(NewNode.ParentId!="Null")
                    NewNode.ParentId = "Node " + i;
                Path.Add(NewNode);
                
            }
            return Path;
        }
        public void PrintPath()
        {
            List<Node> PathNodes =new List<Node>(PathToTarget()) ;
            Console.WriteLine("\nNombre des etapes : "+PathNodes.Count+"\n");
            for(int i = (int)(PathNodes?.Count - 1); i >= 0 ; i--)
            {
                Console.WriteLine("--------------------------");
                PathNodes[i].PrintNode();
                Console.WriteLine("--------------------------");

            }
        }
    }
}
