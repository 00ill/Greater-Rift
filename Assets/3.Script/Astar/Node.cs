using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astar
{
    public enum Walkable
    {
        Blocked,
        Passable,
        Impassable
    }

    public class Node : IHeapItem<Node>
    {
        private Walkable _walkable;
        public Walkable Walkable
        {
            get => _walkable;
            set
            {
                _walkable = value;
                if (NodeMesh != null)
                {
                    //NodeMesh.GetComponent<Gridcor>().enabled = false;
                }
            }

        }
        public readonly Vector3 WorldPostion;
        public readonly int GridX;
        public readonly int GridY;
        public float Height;
        public readonly int MovementPenalty;

        public int GCost;
        public int HCost;
        public Node Parent;

        public GameObject NodeMesh;

        public Node(Walkable walkable, Vector3 worldPos, int gridX, int gridY, float height, int penalty)
        {
            Walkable = walkable;
            WorldPostion = worldPos;
            GridX = gridX;
            GridY = gridY;
            Height = height;
            MovementPenalty = penalty;
        }

        public int FCost => GCost + HCost;

        public int HeapIndex { get; set; }


        public int CompareTo(Node nodeToCompare)
        {
            var compare  = FCost.CompareTo(nodeToCompare.FCost);

            if(compare == 0)
            {
                compare = HCost.CompareTo(nodeToCompare.HCost);
            }
            return -compare;
        }

        public override bool Equals(object obj) => WorldPostion ==((Node)obj).WorldPostion;

        public override int GetHashCode()
        {
            return WorldPostion.GetHashCode();
        }
    }
}
