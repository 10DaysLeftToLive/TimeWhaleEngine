using UnityEngine;
using System.Collections;

public class WayPointPath {
	
	private static float NEXTFLOOR = 3f; // how close will the y positions be until looking for up/down paths
														
	private static Vector3[] points;
	private static int index;
	private static float height;
	
	public static bool CheckForPath(Vector3 startPos, Vector3 destination, float ht){
		height = ht;
		points = new Vector3[30];
		index = 0;
		AddPoint(startPos);
		int mask = (1 << 15); // wayPoint layer
		RaycastHit hit;
		Vector3 heading = SetHeading(startPos,destination);
		
		GameObject start = GetPoint(startPos, mask, heading);
		heading = SetHeading(destination, startPos);
		GameObject end = GetPoint(destination, mask, heading);
		if (start == null || end == null) return false;
		WayPoints startScript = GetScript(start);
		WayPoints endScript = GetScript(end);
		if (Vector3.Distance(startPos, startScript.GetFloorPosition()) > Vector3.Distance(startPos, destination) && !PathToOtherFloor(startPos,destination)) 
			return AddPoint(destination);
		Search.ShortestPath(startScript.id, endScript.id);
		Search.Compute();
		return AddArray(destination);


		return false;
	}
	
	private static Vector3 SetHeading(Vector3 pos1, Vector3 pos2){
		Vector3 heading;
		if (pos1.x > pos2.x){
			heading = Vector3.left;
		}else{
			heading = Vector3.right;
		}		
		heading.y += CheckPositionForSlope(pos1)*heading.x;
		return heading;
	}
	
	private static bool AddArray(Vector3 destination){
		Vector3[] temp = Search.GetVectors();
		for (int i = 0; i < Search.index; i++){
			AddPoint(new Vector3 (temp[i].x, temp[i].y + height,temp[i].z));
		}
		AddPoint(destination);
		bool checkStart = false;
		if (index > 4)
			checkStart = CheckExtraPoints(0,1,2);
		bool checkEnd = CheckExtraPoints(Search.index-1,Search.index,Search.index+1);
		if (checkStart || checkEnd){
			ReoraganizeArray(points);
		}
		return true;
	}
	
	private static bool CheckExtraPoints(int first, int second, int third){
		if (CheckAngle(first,second) || CheckAngle (second, third)){
			return false;
		}
		float dif1 = Utils.CalcDifference(points[first].x, points[second].x);
		dif1 /= Mathf.Abs(dif1);
		
		float dif2 = Utils.CalcDifference(points[second].x, points[third].x);
		dif2 /= Mathf.Abs(dif2);
		Debug.Log("dif: " + dif1 + "  " + dif2);
		if (dif1 != dif2){
			points[second] = Vector3.zero;
			return true;
		}
		return false;
	}
	
	private static void ReoraganizeArray(Vector3[] temp){
		Vector3[] newArray = new Vector3[index];
		int newSize = 0;
		for(int i = 0; i < index; i++){
			if (temp[i] != Vector3.zero){
				newArray[newSize] = temp[i];
				newSize++;
			}
		}
		index = newSize;
		for (int i = 0; i < index; i++){
			points[i] = newArray[i];
		}
	}

	private static bool HorizontalMovement(GameObject point, Vector3 startPos, Vector3 destination, float height, Vector3 heading){
		WayPoints pointScript = GetScript(point);
		do{		
			AddPoint(new Vector3 (pointScript.GetFloorPosition().x, pointScript.GetFloorPosition().y + height,pointScript.GetFloorPosition().z));
			if (heading.x > 0 && pointScript.CheckRight()){
				point = pointScript.GetRight();
			}else if (heading.x <= 0 && pointScript.CheckLeft()){
				point = pointScript.GetLeft();
			}else{
				AddPoint(destination);
				return true;
			}
			pointScript = GetScript(point);	
		}while ((pointScript.GetFloorPosition().x - destination.x)*heading.x <= 0);
		AddPoint(destination);
		return true;
	}
	
	private static GameObject GetPoint(Vector3 pos, int mask, Vector3 heading){
		RaycastHit hit;
		//Debug.DrawRay(pos,heading,Color.red,20);
		//Debug.DrawRay(pos,heading*-1,Color.red,20);
		if (Physics.Raycast(pos, heading , out hit, Mathf.Infinity, mask)){
			GameObject wayPoint = hit.collider.gameObject;
			return wayPoint;
		}else if (Physics.Raycast(pos, heading*-1 , out hit, Mathf.Infinity, mask)){
			GameObject wayPoint = hit.collider.gameObject;
			return wayPoint;
		}else if (Physics.Raycast(pos, Vector3.up , out hit, 8, mask)){
			GameObject wayPoint = hit.collider.gameObject;
			return wayPoint;
		}
		else if (Physics.Raycast(pos, Vector3.down , out hit, 8, mask)){
			GameObject wayPoint = hit.collider.gameObject;
			return wayPoint;
		}
		return null;
	}

	private static float CheckPositionForSlope(Vector3 startPos){
		int mask = (1 << 9); //ground
		RaycastHit hit;
		if (Physics.Raycast(startPos, Vector3.down, out hit, 5f, mask)) {
			return hit.transform.rotation.z*2.5f;
		}
		return 0;
	}

	private static WayPoints GetScript(GameObject point){
		WayPoints script = point.GetComponent<WayPoints>();
		return script;
	}

	private static bool AddPoint(Vector3 pos){
		points[index] = pos;
		index++;
		return true;
	}
	
	private static bool CheckAngle(int a, int b){
		float angle = (Mathf.Abs( points[a].y - points[b].y))/( Mathf.Abs(points[a].x - points[b].x));
		if (angle*45 > 70){
			//Debug.Log("bad angle " + (angle*45));
			return true;
		}
		return false;
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
