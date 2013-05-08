using UnityEngine;
using System.Collections;

public static class QuickPath {
	private static float MINSLOPE = .2f; // amount to increase y position after hitting a slope (smaller number means smaller slopes found and more precise but more raycasts)
	
	
	public static Path StraightPath(Vector3 startPos, Vector3 destination, float height){
		Path path;
		/*if (Mathf.Abs(startPos.y - destination.y) > NEARTHRESHOLD)
		{
			path = FindSlope(startPos, destination, height);
			return path;
		}*/
		Vector3[] points = {startPos, destination};
		int[] wayPoints = {-1, -1};
		path = new Path(2, points, wayPoints);

		return path;
	}	
	
	public static Path ClimbablePath(Vector3 startPos, Vector3 destination, GameObject climbable, float height){
		Vector3[] climbablePoints = SetStartClimbablePosition(climbable, height, startPos);
		Vector3[] points = {startPos, climbablePoints[0], climbablePoints[1]};
		int[] wayPoints = {-1, -1, -1};
		Path path = new Path(3, points, wayPoints);
		return path;
	}
	
	private static Vector3 GetTopOfLadder(GameObject ladder){
		return (new Vector3(ladder.transform.position.x, ladder.transform.position.y + ladder.collider.bounds.size.y/2, ladder.transform.position.z));
	}
	
	private static Vector3 GetBottomOfLadder(GameObject ladder){
		return (new Vector3(ladder.transform.position.x, ladder.transform.position.y - ladder.collider.bounds.size.y/2, ladder.transform.position.z));
	}
	
	// Will find the bottom and top node positions of a given climbable. 
	// 0 entry will be the top, 1 entry is bottom
	private static Vector3[] GetPossibleClimbablePositionToGoto(GameObject climbable, float height){		
		Vector3 top = new Vector3();
		Vector3 bottom = new Vector3();
		if (climbable.transform.localRotation.z == 0){ // TODO better detection
			// if it has no rotation it is a ladder
			top = GetTopOfLadder(climbable);
			bottom = GetBottomOfLadder(climbable);
			
			top.y -= height; // add in the height where the player will goto
			bottom.y += height;
		} else {			
			float sizeX = climbable.transform.localScale.x;
			float sizeY = climbable.transform.localScale.y;

	        float x = sizeX*.5f;
	        float y = sizeY*.5f;
	        Vector3 topRight = climbable.transform.TransformPoint(x/sizeX,y/sizeY,0);
	        Vector3 bottomRight = climbable.transform.TransformPoint(x/sizeX,-y/sizeY,0);
	        Vector3 bottomLeft = climbable.transform.TransformPoint(-x/sizeX,-y/sizeY,0);
	        Vector3 topLeft = climbable.transform.TransformPoint(-x/sizeX,y/sizeY,0);
			
			float theta = climbable.transform.localRotation.z;

			if (theta < 90){
				bottom = bottomRight;
				top = topRight;
			} else if (theta > 270){
				bottom = bottomLeft;
				top = topLeft;
			} // TODO other angles
			
			top.y += height; // add in the height where the player will goto
			bottom.y += height;
		}
		
		
		
		Vector3[] pair = new Vector3[2]; // TODO do a union class
		pair[0] = top;
		pair[1] = bottom;
		
		return (pair);
	}
	
	// Reorganize possiblePosition vectors
	// 0 is start 1 is end positions
	private static Vector3[] SetStartClimbablePosition(GameObject climbable,float height, Vector3 current){
		Vector3[] possiblePositions = GetPossibleClimbablePositionToGoto(climbable, height);
		
		Vector3 top = possiblePositions[0];
		Vector3 bottom = possiblePositions[1];
		
		if (Vector3.Distance(top, current) > Vector3.Distance(bottom, current)){
			possiblePositions[0] = bottom;
			possiblePositions[1] = top;
		}
		
		return possiblePositions;
	}
	
	private static Path FindSlope(Vector3 startPos, Vector3 destination, float height){
		int index = 1;
		Vector3 bottomPos, topPos, heading;
		Vector3[] points = new Vector3[15];
		int[] wayPoints = new int[15];
		int[] dir = new int[15];
		if (startPos.x > destination.x){
			dir = SetupDir(dir, 1);
		}else{
			dir = SetupDir(dir, 0);
		}
		if (startPos.y < destination.y){
			bottomPos = startPos;
			topPos = destination;
		}else{
			bottomPos = destination;
			topPos = startPos;
		}
		if (bottomPos.x > topPos.x){
			heading = Vector3.left;
		}else{
			heading = Vector3.right;
		}
		points[0] = bottomPos;
		wayPoints[0] = -1;
		bottomPos.y -= height;
		bottomPos.y += MINSLOPE;
		//Debug.Log(bottomPos.y);
		int mask = (1 << 9);
		RaycastHit hit;
		float distance;
		do{
			distance = (topPos.x-bottomPos.x)*heading.x;
			Debug.Log(distance);
			if (distance <= 0){
				points[index] = topPos;
				wayPoints[index] = -1;
				Debug.Log(topPos);

				if (topPos == startPos) // flip points
				{
					//Debug.Log("index of " + index);
					points = ReverseArray(points, index+1);
					
				}
				
				Path path = new Path(index+1, points, wayPoints);
				return path;
			}
			if (Physics.Raycast(bottomPos, heading , out hit, distance, mask)) {
				Debug.Log(hit.transform.position + "  "  + hit.collider.bounds.size.x);
				bottomPos = hit.point;
				points[index] = new Vector3 (bottomPos.x, bottomPos.y + height, bottomPos.z);
				bottomPos.y += MINSLOPE;
				if (points[index -1].x == bottomPos.x){
					Debug.Log("Stop");	
				}
				index++;
			}else {
				bottomPos = topPos;
			}
		}while(true);
		
	}
	
	private static Vector3[] ReverseArray(Vector3[] array, int size){
		Vector3[] temp = new Vector3[size];
		for (int i = 0; i < size; i++){
			//Debug.Log("Point " + i + " " + array[i]);
			temp[i] = array[size-i-1];
			//Debug.Log("Point " + i + " " + temp[i]);
		}
		return temp;
	}
	
	private static int[] SetupDir(int[] dir, int direction){
		for (int i = 0; i < 15; i++){
			dir[i] = direction;
		}
		return dir;
	}

}