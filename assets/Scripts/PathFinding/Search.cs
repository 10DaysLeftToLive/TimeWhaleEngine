using UnityEngine;
using System.Collections;

public static class Search {

    private static bool[] visited; // has the vertex been visted by algorithm
    private static float[] path; // distance between 2 vertecies
    private static int[] last; // last vertex visited
	private static int[] completePath;
    private static int vertex; // size of graph
    private static int start;
	private static int destination;
	public static int index;

	public static void ShortestPath(int startPoint, int destinationPoint)
    {
		index = 0;
       	vertex = Graph.wayPointCount;
		visited = new bool [vertex];
		path = new float [vertex];
		last = new int [vertex];
		completePath = new int [vertex];
       	start = startPoint;
	   	destination = destinationPoint;
       	for (int i = 0; i < vertex; i++)
       	{
			path[i] = Mathf.Infinity;
			visited[i] = false;
			last[i] = -1;

       	}
       	path[start] = 0; // Sets the starting location of Dijkstra’s algorithm
    }

	// Finds shortest path from start to destination
    public static void Compute()
    {
	   int count;
	   int closest;
	   for (count = 0; count < vertex; count++)
	   {
	       closest = Minimum();
	       visited[closest] = true;
	       for (int i = 0; i < vertex; i++)
	       {
	          if (Graph.IsEdge(closest,i) > 0 && !visited[i])
	          {
				  if (path[i] > path[closest] + Graph.IsEdge(closest,i))
				  {
	              path[i] = path[closest] + Graph.IsEdge(closest,i);
	              last[i] = closest;
				  }
	          }

	       }    
	   }
    }

	// Returns adjacent vertex with minimum edge
    private static int Minimum()
    {
	    float min = 999;
	    int point = 0;
	    for (int i = 0; i < vertex; i++)
	    {
			if (!visited[i] && min >= path[i])
			{
				min = path[i];
				point = i;
			}
	    }
	    return point;
    }

	private static void PrintPath(int i)
		{
			if (i == start)
			{
				Debug.Log(i + " -> ");
			}
			else if(last[i] == -1)
			{
				Debug.Log("no path found");
			}
			else {
				PrintPath(last[i]);
				if (i == destination)
				{
					Debug.Log(i);
				}
				else
				{
				Debug.Log(i + " -> ");
				}
			}
		}

	public static void Output(int dest)
	{
		//PrintPath(destination);
		Debug.Log("path size of " + path[dest]);
	}

	public static float GetPathSize(int a){
		return path[a];	
	}

	public static int GetPathPoints(int a){
		return last[a];	
	}

	public static void OrganizePath(int i){
		if (i != start)
			OrganizePath(last[i]);
		completePath[index] = i;
		index++;
	}

	public static void OrganizePath(int i, int start){
		if (i != start)
			OrganizePath(WayPointPath.pathPoints[start,i], start);
		completePath[index] = i;
		index++;
	}

	public static Vector3[] GetVectors(int dest, int start){
		index = 0;
		completePath = new int [vertex];
		OrganizePath(dest, start);
		Vector3[] vectorPath = new Vector3[index];
		for (int i = 0; i < index; i++){
			vectorPath[i] = Graph.wayPointPosition[completePath[i]];
			//Debug.Log(vectorPath[i]);
		}
		return vectorPath;
	}
	
	public static int[] GetWayPointIndices(int dest, int start){
		index = 0;
		completePath = new int [vertex];
		OrganizePath(dest, start);
		int[] wayPointPath = new int[index];
		for (int i = 0; i < index; i++){
			wayPointPath[i] = completePath[i];
		}
		return wayPointPath;
	}

	public static void print(){
		for (int i = 0; i < index; i++){
			Debug.Log(completePath[i]);
		}
	}


}
