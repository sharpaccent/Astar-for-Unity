using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GridMaster
{
    public class GridBase : MonoBehaviour
    {
        //Setting up the grid
        public int maxX = 10;
        public int maxY = 3;
        public int maxZ = 10;

        //Offset relates to the world positions only
        public float offsetX = 1;
        public float offsetY = 1;
        public float offsetZ = 1;

        public Node[, ,] grid; // our grid

        public GameObject gridFloorPrefab;

        public Vector3 startNodePosition;
        public Vector3 endNodePosition;

        public int agents;

        void Start()
        {
            //The typical way to create a grid
            grid = new Node[maxX, maxY, maxZ];

            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    for (int z = 0; z < maxZ; z++)
                    {
                        //Apply the offsets and create the world object for each node
                        float posX = x * offsetX;
                        float posY = y * offsetY;
                        float posZ = z * offsetZ;
                        GameObject go = Instantiate(gridFloorPrefab, new Vector3(posX, posY, posZ),
                            Quaternion.identity) as GameObject;
                        //Rename it
                        go.transform.name = x.ToString() + " " + y.ToString() + " " + z.ToString();
                        //and parent it under this transform to be more organized
                        go.transform.parent = transform;

                        //Create a new node and update it's values
                        Node node = new Node();
                        node.x = x;
                        node.y = y;
                        node.z = z;
                        node.worldObject = go;
                        
                         //BoxCastAll is only Unity 5.3+ remove this and it will play on all versions 5+
                        //in theory it should play with every Unity version, but i haven't tested it
                        RaycastHit[] hits = Physics.BoxCastAll(new Vector3(posX, posY, posZ), new Vector3(1,0,1), Vector3.forward);

                        for (int i = 0; i < hits.Length; i++)
                        {
                            node.isWalkable = false;           
                        }

                        //then place it to the grid
                        grid[x, y, z] = node;
                    }
                }
            }
        }

        //Just a quick and dirty way to visualize the path
        public bool start;
        void Update()
        {
            if(start)
            {
                start = false;
                //Create the new pathfinder class
               // Pathfinding.Pathfinder path = new Pathfinding.Pathfinder();
                
                //to test the avoidance, just make a node unwalkable
                grid[1, 0, 1].isWalkable = false;
                
                //pass the target nodes
                Node startNode = GetNodeFromVector3(startNodePosition);
                Node end = GetNodeFromVector3(endNodePosition);

                //path.startPosition = startNode;
                //path.endPosition = end;

                //find the path
                //List<Node> p = path.FindPath();
                startNode.worldObject.SetActive(false);

                for (int i = 0; i < agents; i++)
                {
                    Pathfinding.PathfindMaster.GetInstance().RequestPathfind(startNode, end, ShowPath);
                }             
            }
        }

        public void ShowPath(List<Node> path)
        {
            foreach (Node n in path)
            {
                n.worldObject.SetActive(false);
            }

            //Debug.Log("agent complete");
        }

        public Node GetNode(int x, int y, int z)
        {
            //Used to get a node from a grid,
            //If it's greater than all the maximum values we have
            //then it's going to return null

            Node retVal = null;

            if (x < maxX && x >= 0 &&
                y >= 0 && y < maxY &&
                z >= 0 && z < maxZ)
            {
                retVal = grid[x, y, z];
            }

            return retVal;
        }

        public Node GetNodeFromVector3(Vector3 pos)
        {
            int x = Mathf.RoundToInt(pos.x);
            int y = Mathf.RoundToInt(pos.y);
            int z = Mathf.RoundToInt(pos.z);

            Node retVal = GetNode(x, y, z);
            return retVal;
        }

        //Singleton
        public static GridBase instance;
        public static GridBase GetInstance()
        {
            return instance;
        }

        void Awake()
        {
            instance = this;
        }
    }
}
