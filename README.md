# A Star Pathfinding for Unity

This is an A* Pathfinder for Unity. It inlcudes 3 axis pathfinding and multi-threading.

Code has comments but if you need more context, take a look at this two videos  
https://www.youtube.com/watch?v=w9HZhlF8cb0  
https://www.youtube.com/watch?v=an8g7x6pIWw  
  
# Usage
* Drop the components Gridbase and PathfindMaster on a gameObject in your scene.
* Call the PathfinderMaster.RequestPathFind(startingNode, targetNode, callback).
* For callback, use a function that takes a list of Nodes
* The returned list is your path
  
For more uses of this A* project you can follow me at   
https://www.youtube.com/channel/UCq9_1E5HE4c_xmhzD3r7VMw
