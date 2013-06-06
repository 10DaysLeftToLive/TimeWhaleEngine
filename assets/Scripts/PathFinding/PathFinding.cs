using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PathFinding {	
	private static Path path;

	public static bool GetPathForPoints(Vector3 startPos, Vector3 destination, float ht, RaycastHit hit){
		WayPointPath.SetupPathfinding(startPos, destination, ht, hit);
		if (WayPointPath.CheckForPathBetweenPoints())
        	return true;
		return false;
	}

	public static Path GetPath(){
		return WayPointPath.GetPath();	
	}
}