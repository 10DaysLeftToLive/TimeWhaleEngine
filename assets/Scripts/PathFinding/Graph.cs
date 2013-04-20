using UnityEngine;
using System.Collections;

public static class Graph {
	
	public static int wayPointCount = 14;
	public static Vector3[] wayPointPosition;
	private static float[,] distance;
	private static int[] closedPoints;
	
	public static void Init(GameObject point){
		wayPointPosition = new Vector3[wayPointCount];
		closedPoints = new int[wayPointCount];
		distance = new float[wayPointCount,wayPointCount];
		for (int i = 0; i < wayPointCount; i++) 
			{
			closedPoints[i] = 0;
			for (int j = 0; j < wayPointCount; j++)
				distance[i,j] = 0;
			}
		MakeGraph(point);

	}
	
	private static void RemoveEdge(int i, int j) 
	{
		if (i >= 0 && i < wayPointCount && j > 0 && j < wayPointCount) 
		{
			distance[i,j] = 0;
			distance[j,i] = 0;
		}
	}
	
	private static void AddEdge(int i, int j, float dist) 
	{
		if (i >= 0 && i < wayPointCount && j >= 0 && j < wayPointCount) 
		{
			distance[i,j] = dist;
			distance[j,i] = dist;
			//Debug.Log("added " + i + ", " + j + " of " + dist);
		}
	}
	
	public static float IsEdge(int i, int j) 
	{
		if (i >= 0 && i < wayPointCount && j >= 0 && j < wayPointCount){
			return distance[i,j];
		}else
			return 0;
	}
	
	private static void MakeGraph(GameObject point)
	{
		GameObject temp;
		WayPoints tempScript;
		WayPoints pointScript = GetScript(point);
		wayPointPosition[pointScript.id] = pointScript.GetFloorPosition();
		closedPoints[pointScript.id] = 1;
		if (pointScript.CheckLeft()){
			temp = pointScript.GetLeft();
			tempScript = GetScript(temp);
			if (!CheckPastPoints(tempScript.id)){
				AddEdge(pointScript.id, tempScript.id, pointScript.leftDistance);
				closedPoints[tempScript.id] = 1;
				MakeGraph(temp);
			}
		}
		if (pointScript.CheckRight()){
			temp = pointScript.GetRight();
			tempScript = GetScript(temp);
			if (!CheckPastPoints(tempScript.id)){
				AddEdge(pointScript.id, tempScript.id, pointScript.rightDistance);
				closedPoints[tempScript.id] = 1;
				MakeGraph(temp);
			}
		}
		if (pointScript.CheckUp()){
			temp = pointScript.GetUp();
			tempScript = GetScript(temp);
			if (!CheckPastPoints(tempScript.id)){
				AddEdge(pointScript.id, tempScript.id, pointScript.upDistance);
				closedPoints[tempScript.id] = 1;
				MakeGraph(temp);
			}
		}
		if (pointScript.CheckDown()){
			temp = pointScript.GetDown();
			tempScript = GetScript(temp);
			if (!CheckPastPoints(tempScript.id)){
				AddEdge(pointScript.id, tempScript.id, pointScript.downDistance);
				closedPoints[tempScript.id] = 1;
				MakeGraph(temp);
			}
		}
	}
	
	private static WayPoints GetScript(GameObject point){
		WayPoints script = point.GetComponent<WayPoints>();
		return script;
	}
	
	private static bool CheckPastPoints(int id){
		if(closedPoints[id] != 0)
			return true;
		return false;
	}
	
	public static void graphOutput(int age)
	{
		
		for (int i = 0; i < wayPointCount; i++)
		{
			for (int j = 0; j < wayPointCount; j++)
			{
				if (distance[i,j] != 0)
					Debug.Log("i: " + i + ", j " + j + " distance " + distance[i,j]);
			}
		}
	}
}
