using UnityEngine;
using System.Collections;

namespace GridMaster
{
    public class Node
    {
        //Node's position in the grid
        public int x;
        public int y;
        public int z;

        //Node's costs for pathfinding purposes
        public float hCost;
        public float gCost;
        
        public float fCost
        {
            get //the fCost is the gCost+hCost so we can get it directly this way
            {
                return gCost + hCost;
            }
        }

        public Node parentNode;
        public bool isWalkable = true;
        
        //Reference to the world object so we can have the world position of the node among other things
        public GameObject worldObject;

        //Types of nodes we can have, we will use this later on a case by case examples
        public NodeType nodeType;
        public enum NodeType
        {
            ground,
            air
        }
    }
}
