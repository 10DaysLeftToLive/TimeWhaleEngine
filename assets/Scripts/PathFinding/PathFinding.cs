using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PathFinding {	
	private static Path path;

	public static bool GetPathForPoints(Vector3 startPos, Vector3 destination, float ht){
		WayPointPath.SetupPathfinding(startPos, destination, ht);
		if (WayPointPath.CheckForPathBetweenPoints())
        	return true;
		return false;
	}

	public static bool GetPathToNode(Vector3 startPos, string name, float ht){
		WayPointPath.SetupPathfinding(startPos, name, ht);
		if (WayPointPath.CheckForPathToNode())
        	return true;
		return false;
	}



	public static Path GetPath(){
		return WayPointPath.GetPath();	
	}
}