using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Puzzle8
{
    internal class Node
    {
        public static int NodeId=1;
        public string Id { get; set; }
        public string ParentId { get; set; }

        public int[,] Data { get; set; }
        public bool Status { get; set; } = false;
        public List<Position> PossibleMovePosition { get; set; }
        public Node(string ParentId, int[,] Data)
        {
            this.Id = "Node"+NodeId++;
            this.ParentId = ParentId;
            this.Data = Data;
        }
        public Node(Node node)
        {
            this.Id = node.Id;
            this.ParentId = node.ParentId;
            this.Data= node.Data;
        }
        private Position GetPositionVide()
        {
            Position position = new Position(-1, -1);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Data[i, j] == 0)
                    {
                        position = new Position(i, j);
                        break;
                    }
                }
                if (position.X != -1)
                {
                    break;
                }
            }
            return position;
        }
        private List<Position> GetPossibleMovePosition()
        {
            Position Pos;
            PossibleMovePosition = new List<Position>();
            Position PositionVideCase = this.GetPositionVide();
            if (PositionVideCase.X - 1 >= 0)
            {
                Pos = new Position(PositionVideCase.X - 1, PositionVideCase.Y);
                PossibleMovePosition.Add(Pos);
            }
            if (PositionVideCase.Y - 1 >= 0)
            {
                Pos = new Position(PositionVideCase.X, PositionVideCase.Y - 1);
                PossibleMovePosition.Add(Pos);
            }
            if (PositionVideCase.X + 1 <= 2)
            {
                Pos = new Position(PositionVideCase.X + 1, PositionVideCase.Y);
                PossibleMovePosition.Add(Pos);
            }
            if (PositionVideCase.Y + 1 <= 2)
            {
                Pos = new Position(PositionVideCase.X, PositionVideCase.Y + 1);
                PossibleMovePosition.Add(Pos);
            }

            return PossibleMovePosition;
        }
        public override string ToString()
        {
            return "Id = " + this.Id + "\t ParentId = " + this.ParentId + "`\t Data =  " ;
        }
        public bool GetChildrenNode(List<Node> Nodes,Node TargetNode)
        {
            bool TargetExist = this.Equals(TargetNode);
            Position PositionVide=this.GetPositionVide();
            List<Position> PossibleMovePosition = this.GetPossibleMovePosition();
            Node CurrentNode = new Node(this);
            Node NewNode;
            //Console.WriteLine("------------------------------------");
            //Console.WriteLine("Current Node : ");
            //CurrentNode.PrintNode();
            //Console.WriteLine("Children : ");
            foreach (var pos in PossibleMovePosition)
            {

                int[,] data = new int[3,3];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        data[i, j]= CurrentNode.Data[i, j];
                    }
                }
                data[PositionVide.X, PositionVide.Y] = data[pos.X, pos.Y];
                data[pos.X, pos.Y] = 0;
                NewNode = new Node(CurrentNode.Id, data);
                if (!NewNode.ExistIn(Nodes))
                {
                    //NewNode.PrintNode();
                    Nodes.Add(NewNode);
                    if (TargetExist=NewNode.Equals(TargetNode))
                    {
                        break;
                    }
                }
            }
            this.Status = true;
            //Console.WriteLine("------------------------------------");
            return TargetExist;

        }
        public bool ExistIn(List<Node> Nodes)
        {
            foreach (var node in Nodes)
            {
                if (this.Equals(node))
                {
                    return true;
                }
            }
            return false;
        }
        /*
        public bool GetChildrenNode(Dictionary<string, Node> Nodes, Node TargetNode)
        {
            bool TargetExist = false;
            Position PositionVide = this.GetPositionVide();
            List<Position> PossibleMovePosition = this.GetPossibleMovePosition();
            Node CurrentNode = this;
            Node NewNode;
            //Console.WriteLine("------------------------------------");
            //Console.WriteLine("Current Node : ");
            //CurrentNode.PrintNode();
            //Console.WriteLine("Children : ");
            foreach (var pos in PossibleMovePosition)
            {

                int[,] data = new int[3, 3];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        data[i, j] = CurrentNode.Data[i, j];
                    }
                }
                bool existNode = false;
                data[PositionVide.X, PositionVide.Y] = data[pos.X, pos.Y];
                data[pos.X, pos.Y] = 0;
                NewNode = new Node(CurrentNode.Id, data);
                foreach (KeyValuePair<String, Node> kvp in Nodes)
                {
                    if (NewNode.Equals(Nodes[kvp.Key]))
                    {
                        existNode = true;
                        break;
                    }
                }
                if (!existNode)
                {
                    NewNode.PrintNode();
                    Nodes[NewNode.Id] = NewNode;
                    TargetExist=NewNode.GetChildrenNode(Nodes, TargetNode);
                }
                if (NewNode.Equals(TargetNode))
                {
                    TargetExist = true;
                    break;
                }

            }
            this.Status = true;
            //Console.WriteLine("------------------------------------");
            return TargetExist;

        }
        */
        public bool Equals(Node node)
        {
            //if (obj == null || GetType() != obj.GetType())
            //{
            //    return false;
            //}

            //Node otherNode = (Node)obj;

            //// Compare the Data property
            //if (Data.GetLength(0) != otherNode.Data.GetLength(0) || Data.GetLength(1) != otherNode.Data.GetLength(1))
            //{
            //    return false;
            //}

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Data[i, j] != node.Data[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        public void  PrintNode()
        {
            Console.WriteLine(this.ToString());
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(Data[i, j] + " ");
                }
                Console.WriteLine(); // Move to the next row
            }
            Console.WriteLine(); // Move to the next row

        }
    }
        
}
