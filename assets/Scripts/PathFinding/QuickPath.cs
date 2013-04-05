using UnityEngine;
using System.Collections;

public static class QuickPath {
	private static int index;
	private static bool foundPath;
	private static Node[] nodes;
	
	public static Path StraightPath(Vector3 startPos, Vector3 destination){
		Vector3[] points = {startPos, destination}; 
		int[] dir = {0,0};
		Path path = new Path(2, points, dir);
		return path;
	}
	
	public static Path ClimbablePath(Vector3 startPos, Vector3 destination, GameObject climbable, float height){
		Vector3[] climbablePoints = SetStartClimbablePosition(climbable, height, startPos);
		Vector3[] points = {startPos, climbablePoints[0], climbablePoints[1]}; 
		int[] dir = {0,0,3};
		Path path = new Path(3, points, dir);
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

}
