using UnityEngine;
using System.Collections;

public class WayPointPath {
	
	private static float NEARTHRESHOLD = .2f; // how close will the y positions be until looking for slopes
	private static float NEXTFLOOR = 3f; // how close will the y positions be until looking for up/down paths
	private static float MAXCLIMBABLEANGLE = 49; // maximum angle to climb (doesnt consider if angle between 2 waypoints) only small movement angle
	private static float SLOPEDMOVEMENTTHRESHOLD = 1f; // how close must the guestimation for small slope movement before moving
														// small movement = movement within 2 waypoints
	private static Vector3[] points;
	private static int index;
	
	
	public static bool CheckForPath(Vector3 startPos, Vector3 destination, float height){
		points = new Vector3[30];
		index = 0;
		AddPoint(startPos);
		int mask = (1 << 15); // wayPoint layer
		RaycastHit hit;
		Vector3 heading;
		if (startPos.x > destination.x){
			heading = Vector3.left;
		}else{
			heading = Vector3.right;
		}		
		heading.y += CheckStart(startPos)*heading.x;
		//Debug.Log("heading " + heading);
		//Debug.DrawRay(startPos,heading,Color.red,20);
		if (Physics.Raycast(startPos, heading , out hit, Mathf.Abs(startPos.x - destination.x), mask)){
			GameObject wayPoint = hit.collider.gameObject;
			//Debug.Log(wayPoint.name);
			return ConnectTheDots(wayPoint, startPos, destination, height, heading);
		}else if (Mathf.Abs(startPos.y - destination.y) < NEARTHRESHOLD){ // handles small movements on flat ground
			AddPoint(destination);
			return true;
		}else { // handles small movement on sloped ground
			float distance = Vector3.Distance(startPos, destination);
			heading *= distance;
			startPos += heading;
			//Debug.Log("estimated destination " + (startPos));
			if (Vector3.Distance(startPos,destination) < SLOPEDMOVEMENTTHRESHOLD){
				AddPoint(destination);
				return CheckAngle();
			}
		}
		
		return false;
	}
	
	public static Path GetPath(){
		Path path = new Path(index, points);
		return path;
	}
	
	private static bool ConnectTheDots(GameObject point, Vector3 startPos, Vector3 destination, float height, Vector3 heading){
		WayPoints pointScript = GetScript(point);
		
		do{		
			AddPoint(new Vector3(pointScript.GetFloorPosition().x,pointScript.GetFloorPosition().y + height, pointScript.GetFloorPosition().z));
			if (startPos.y < destination.y && Mathf.Abs(startPos.y - destination.y) > NEXTFLOOR && pointScript.CheckUp()){
				point = pointScript.GetUp();
			}else if (startPos.y > destination.y && Mathf.Abs(startPos.y - destination.y) > NEXTFLOOR && pointScript.CheckDown()){
				point = pointScript.GetDown();
			}else if (heading.x > 0 && pointScript.CheckRight()){
				point = pointScript.GetRight();
			}else if (heading.x <= 0 && pointScript.CheckLeft()){
				point = pointScript.GetLeft();
			}else{
				AddPoint(destination);
				if (!CheckAngle())
					return false;
				return true;
			}
			pointScript = GetScript(point);	
		}while ((pointScript.GetFloorPosition().x - destination.x)*heading.x <= 0);
		AddPoint(destination);
		if (!CheckAngle())
			return false;
		return true;
	}
	
	private static float CheckStart(Vector3 startPos){
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
	
	private static void AddPoint(Vector3 pos){
		points[index] = pos;
		index++;
	}
	
	private static bool CheckAngle(){
		float angle = (Mathf.Abs( points[index-2].y - points[index-1].y))/( Mathf.Abs(points[index-2].x - points[index-1].x));
		if (angle*45 > MAXCLIMBABLEANGLE){
			Debug.Log("bad angle " + (angle*45));
			return false;
		}
		return true;
	}
	
	//Debugging
	private static void PrintPoints(){
		for(int i = 0; i < index; i++){
		 Debug.Log("Point " + i + " at " + points[i]);	
		}
	}
	
}
