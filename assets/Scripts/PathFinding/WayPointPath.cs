using UnityEngine;
using System.Collections;

public class WayPointPath {
	private static Vector3[] points;
	private static int[] wayPoints;
	private static Vector3 startPosition, destPosition;
	private static int index;
	private static float height;
	private static int currentAge;
	private static float[,] pathDistance;
	public static int[,] pathPoints;
	private static int startPoint, endPoint;
	private static WayPoints start, end;
	private static bool skipWayPoints = false;
	private static bool noPath = false;

	public static void Initialize(){
		pathDistance = new float[Graph.wayPointCount,Graph.wayPointCount];
		pathPoints = new int[Graph.wayPointCount,Graph.wayPointCount];
		for (int i = 0; i < Graph.wayPointCount; i++){
			Search.ShortestPath(i, i);
			Search.Compute();
			for (int j = 0; j < Graph.wayPointCount; j++){
				pathDistance[i,j] = Search.GetPathSize(j);
				pathPoints[i,j] = Search.GetPathPoints(j);
			}
		}
	}

	public static void SetupPathfinding(Vector3 startPos, Vector3 destination, float ht, RaycastHit hit){
		noPath = false;
		skipWayPoints = false;
		startPoint = -1;
		endPoint = -1;
		startPosition = startPos;
		startPosition.y -= height/2;
		destPosition = destination;
		height = ht;
		points = new Vector3[30];
		wayPoints = new int[30];
		index = 0;
		AddPoint(startPos, -1);
		int mask = (1 << 15); // wayPoint layer
		Vector3 heading = SetHeading(startPosition);
		GameObject startLeft = GetPoint(startPosition, mask, heading);
		GameObject startRight = GetPoint(startPosition, mask, heading*-1);
		if (startLeft == null || startRight == null){
			WayPoints[] ageWaypoints = LevelManager.GetCurrentAgeWaypoints();
			
			WayPoints closest = ageWaypoints[0];
			float closestDistance = 100;
			float currentDistance;
			
			foreach (WayPoints waypoint in ageWaypoints){
				if (waypoint.pointAge != CharacterAgeManager.currentAge) continue;
				currentDistance = Vector3.Distance(waypoint.transform.position, startPosition);
				if (currentDistance < closestDistance){
					closestDistance = currentDistance;
					closest = waypoint;
				}
			}
			startPosition = closest.transform.position;
			heading = SetHeading(startPosition);
			startLeft = GetPoint(startPosition, mask, heading);
			startRight = GetPoint(startPosition, mask, heading*-1);
		}
		
		
		heading = Quaternion.AngleAxis(90, new Vector3(0,0,1)) * hit.normal;
		if (heading.x > 0){ heading *= -1;} // heading will sometimes point wrong direction on slopes
		GameObject endLeft = GetPoint(destination, mask, heading);
		GameObject endRight = GetPoint(destination, mask, heading*-1);

		WayPoints startLeftScript, startRightScript, endLeftScript, endRightScript;
		startLeftScript = GetScript(startLeft);
		startRightScript = GetScript(startRight);
		endLeftScript = GetScript(endLeft);
		endRightScript = GetScript(endRight);

		//if (startLeft != null) Debug.Log("startLeft " + startLeft.name);
		//if (startRight != null) Debug.Log("startRight " + startRight.name);
		//if (endLeft != null) Debug.Log("endLeft " + endLeft.name);
		//if (endRight != null) Debug.Log("endRight " + endRight.name);
		
		if ( (startLeft != null && startRight != null && endLeft != null && endRight != null) 
			&& (startLeft.name == endLeft.name && startRight.name == endRight.name)){
			skipWayPoints = true;	
		}

		float minDistance = 999;
		minDistance = CheckDistance(startLeftScript, endLeftScript, minDistance);
		minDistance = CheckDistance(startLeftScript, endRightScript, minDistance);
		minDistance = CheckDistance(startRightScript, endLeftScript, minDistance);
		minDistance = CheckDistance(startRightScript, endRightScript, minDistance);
		startPoint = start.id;
		endPoint = end.id;
		currentAge = (int)start.pointAge;
		if (minDistance == 999){
			noPath = true;
		}
	}

	public static bool CheckForPathBetweenPoints(){		
		// if moving small distance and wont use waypoints
		if (skipWayPoints){
			return AddPoint(new Vector3(destPosition.x, destPosition.y + height -.2f, destPosition.z), -1);
		}
		
		if (noPath)
			return false;

		if (startPoint == -1 || endPoint == -1)
			return false; 

		return AddArray(startPoint, endPoint, destPosition);
	}
	private static float CheckDistance(WayPoints st, WayPoints ed, float minDistance){
		if ((st != null && ed != null) && (pathDistance[st.id, ed.id] + Vector3.Distance(st.GetFloorPosition(), startPosition) + Vector3.Distance(ed.GetFloorPosition(), destPosition)) < minDistance){
			minDistance = pathDistance[st.id, ed.id] + Vector3.Distance(st.GetFloorPosition(), startPosition) + Vector3.Distance(ed.GetFloorPosition(), destPosition);
			start = st;
			end = ed;
		}
		return minDistance;
	}

	private static Vector3 SetHeading(Vector3 pos1){
		Vector3 heading = CheckPositionForSlope(pos1);
		return heading;
	}

	private static bool AddArray(int start, int stop, Vector3 destination){
		Vector3[] temp = Search.GetVectors(stop, start);
		int[] wayPointTemp = Search.GetWayPointIndices(stop, start);
		for (int i = 0; i < Search.index; i++){
			AddPoint(new Vector3 (temp[i].x, temp[i].y + height + LevelManager.levelYOffSetFromCenter*currentAge,temp[i].z), wayPointTemp[i]);
		}
		AddPoint(new Vector3(destination.x, destination.y + height-.2f, destination.z), -1);
		return true;
	}

	private static GameObject GetPoint(Vector3 pos, int mask, Vector3 heading){
		RaycastHit hit;
		float offset;
		if (heading.x == 0){
			offset = 0;
		} else {
			offset = heading.x*.1f;	
		}
		Vector3 rayStart = new Vector3(pos.x-offset, pos.y, pos.z);
		//Debug.DrawRay(pos,heading,Color.red,20);
		if (Physics.Raycast(rayStart, heading , out hit, Mathf.Infinity, mask)){
			GameObject wayPoint = hit.collider.gameObject;
			return wayPoint;
		}
		return null;
	}

	private static Vector3 CheckPositionForSlope(Vector3 startPos){
		int mask = (1 << 9); //ground
		RaycastHit hit;
		if (Physics.Raycast(startPos, Vector3.down, out hit, 5f, mask)) {
			Vector3 rotationVector = Quaternion.AngleAxis(90, new Vector3(0,0,1)) * hit.normal;
			return rotationVector;
		}
		return Vector3.left;
	}

	private static WayPoints GetScript(GameObject point){
		if (point != null){
			WayPoints script = point.GetComponent<WayPoints>();
			return script;
			}
		return null;
	}

	private static bool AddPoint(Vector3 pos, int wayPoint){
		points[index] = pos;
		wayPoints[index] = wayPoint;
		index++;
		return true;
	}

	public static Path GetPath(){
		Path path = new Path(index, points, wayPoints);
		return path;
	}

	//Debugging
	private static void PrintPoints(){
		for(int i = 0; i < index; i++){
		 Debug.Log("Point " + i + " at " + points[i]);	
		}
	}


}