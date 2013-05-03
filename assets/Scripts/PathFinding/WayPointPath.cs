using UnityEngine;
using System.Collections;

public class WayPointPath {

	private static float NEXTFLOOR = 3f; // how close will the y positions be until looking for up/down paths

	private static Vector3[] points;
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

	public static void SetupPathfinding(Vector3 startPos, Vector3 destination, float ht){
		noPath = false;
		skipWayPoints = false;
		startPoint = -1;
		endPoint = -1;
		startPosition = startPos;
		destPosition = destination;
		height = ht;
		points = new Vector3[30];
		index = 0;
		AddPoint(startPos);
		int mask = (1 << 15); // wayPoint layer
		Vector3 heading = SetHeading(startPos);
		GameObject startLeft = GetLeft(startPos, mask, heading);
		GameObject startRight = GetRight(startPos, mask, heading);

		heading = SetHeading(destination);
		GameObject endLeft = GetLeft(destination, mask, heading);
		GameObject endRight = GetRight(destination, mask, heading);

		WayPoints startLeftScript, startRightScript, endLeftScript, endRightScript;
		startLeftScript = GetScript(startLeft);
		startRightScript = GetScript(startRight);
		endLeftScript = GetScript(endLeft);
		endRightScript = GetScript(endRight);

		/*if (startLeft != null) Debug.Log("startLeft " + startLeft.name);
		if (startRight != null) Debug.Log("startRight " + startRight.name);
		if (endLeft != null) Debug.Log("endLeft " + endLeft.name);
		if (endRight != null) Debug.Log("endRight " + endRight.name);*/
		
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
		if (minDistance == 999)
			noPath = true;
	}
	
	public static void SetupPathfinding(Vector3 startPos, string name, float ht){
		noPath = false;
		startPosition = startPos;
		height = ht;
		points = new Vector3[30];
		index = 0;
		AddPoint(startPos);
		int mask = (1 << 15); // wayPoint layer
		Vector3 heading = SetHeading(startPos);
		GameObject startLeft = GetLeft(startPos, mask, heading);
		GameObject startRight = GetRight(startPos, mask, heading);
		WayPoints startLeftScript, startRightScript, endScript;
		startLeftScript = GetScript(startLeft);
		startRightScript = GetScript(startRight);
		endScript = GetScript(Graph.FindWayPointByName(name));

		/*if (startLeft != null) Debug.Log("startLeft " + startLeft.name);
		if (startRight != null) Debug.Log("startRight " + startRight.name);
		if (endLeft != null) Debug.Log("endLeft " + endLeft.name);
		if (endRight != null) Debug.Log("endRight " + endRight.name);*/

		float minDistance = 999;
		minDistance = CheckDistance(startLeftScript, endScript, minDistance);
		minDistance = CheckDistance(startRightScript, endScript, minDistance);
		startPoint = start.id;
		endPoint = end.id;
		currentAge = (int)start.pointAge;
		if (minDistance == 999)
			noPath = true;
		
	}

	public static bool CheckForPathBetweenPoints(){		
		// if moving small distance and wont use waypoints
		if (skipWayPoints){
			return AddPoint(destPosition);
		}
		
		if (noPath)
			return false;

		if (startPoint == -1 || endPoint == -1) 
			return false; 

		return AddArray(startPoint, endPoint, destPosition);
	}

	public static bool CheckForPathToNode(){
		if (noPath)
			return false;
		
		return AddArray(startPoint, endPoint);
	}

	private static float CheckDistance(WayPoints st, WayPoints ed, float minDistance){
		if ((st != null && ed != null) && (pathDistance[st.id, ed.id] + Vector3.Distance(st.GetFloorPosition(), startPosition) + Vector3.Distance(ed.GetFloorPosition(), destPosition)) < minDistance){
			minDistance = pathDistance[st.id, ed.id] + Vector3.Distance(st.GetFloorPosition(), startPosition) + Vector3.Distance(ed.GetFloorPosition(), destPosition);
			start = st;
			end = ed;
		}
		return minDistance;
	}
	
	private static float CheckDistanceFromStart(WayPoints st, WayPoints ed, float minDistance){
		if ((st != null && ed != null) && (pathDistance[st.id, ed.id] + Vector3.Distance(st.GetFloorPosition(), startPosition)) < minDistance){
			minDistance = pathDistance[st.id, ed.id] + Vector3.Distance(st.GetFloorPosition(), startPosition);
			start = st;
			end = ed;
		}
		return minDistance;
	}

	private static Vector3 SetHeading(Vector3 pos1){
		Vector3 heading = CheckPositionForSlope(pos1);
		//Debug.DrawRay(pos1,heading,Color.green,20);
		return heading;
	}

	private static bool AddArray(int start, int stop, Vector3 destination){
		Vector3[] temp = Search.GetVectors(stop, start);
		for (int i = 0; i < Search.index; i++)
			AddPoint(new Vector3 (temp[i].x, temp[i].y + height + 50*currentAge,temp[i].z));
		AddPoint(destination);
		return true;
	}

	private static bool AddArray(int start, int stop){
		Vector3[] temp = Search.GetVectors(stop, start);
		for (int i = 0; i < Search.index; i++)
			AddPoint(new Vector3 (temp[i].x, temp[i].y + height + 50*currentAge,temp[i].z));
		return true;
	}


	private static GameObject GetLeft(Vector3 pos, int mask, Vector3 heading){
		RaycastHit hit;
		//Debug.DrawRay(pos,heading,Color.red,20);
		if (Physics.Raycast(pos, heading , out hit, Mathf.Infinity, mask)){
			GameObject wayPoint = hit.collider.gameObject;
			return wayPoint;
		}
		return null;
	}

	private static GameObject GetRight(Vector3 pos, int mask, Vector3 heading){
		RaycastHit hit;
		//Debug.DrawRay(pos,heading*-1,Color.red,20);
		if (Physics.Raycast(pos, heading*-1 , out hit, Mathf.Infinity, mask)){
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

	private static bool AddPoint(Vector3 pos){
		points[index] = pos;
		index++;
		return true;
	}

	private static bool PathToOtherFloor(Vector3 start, Vector3 end){
		if (Mathf.Abs(start.y - end.y) > NEXTFLOOR)
			return true;
		return false;
	}

	public static Path GetPath(){
		Path path = new Path(index, points);
		return path;
	}

	//Debugging
	private static void PrintPoints(){
		for(int i = 0; i < index; i++){
		 Debug.Log("Point " + i + " at " + points[i]);	
		}
	}


}