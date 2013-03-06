using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PathFinding {	
	public enum Direction {
		left = 0, 
		right = 1, 
		up = 2, 
		down = 3, 
		none = 4, 
		fall = 5
	};
	
	private static Direction currentDirection;
	private static float testTimer;
	private static int index;
	private static bool foundPath;
	private static Node[] nodes;
	private static GameObject grabableToMoveThrough = null; // only set if we are grabing an object
	
	private static float MECHANICSBUFFER = .4f;
	
	private static float levelDifferenceMax = .25f;
	private static float distanceToLook = 9999f;
	
	public static bool StartPath(Vector3 startPos, Vector3 destination, float height){
		nodes = new Node[15];
		index = 0;
		currentDirection = Direction.none;
		foundPath = false;
		nodes[0] = new Node((int)currentDirection, startPos, destination);
		int mask = LevelMasks.ClimbableMask | LevelMasks.MechanicsMask;
		RaycastHit hit;
		if (Physics.Raycast(new Vector3(startPos.x, startPos.y, startPos.z-2), Vector3.forward, out hit, Mathf.Infinity, mask)){
			if (hit.transform.tag == Strings.tag_Climbable) {
				nodes[0].hitClimbable = true;
			}
		}
		if (FindAPath(nodes, destination, height)){
			return true;
		}
		return false;
	}
	
	public static bool StartPathWithGrabable(Vector3 startPos, Vector3 destination, float height, GameObject grabable){
		grabableToMoveThrough = grabable;
		return (StartPath(startPos, destination, height));
	}
	
	public static Path GetPath(){
		Vector3[] points = new Vector3[index+1]; 
		int[] dir = new int[index+1];
		for (int i = 0; i <= index; i++){
			Debug.Log("nodes of " + i + " = " + nodes[i]);
			points[i] = nodes[i].curr;
			dir[i] = nodes[i].past;
		}
		Path path = new Path(index + 1, points, dir);
		return path;
	}
	
	private static bool FindAPath(Node[] nodes, Vector3 destination, float height){
		Debug.Log("----------------------------");
		GetPath();
		Debug.Log("----------------------------");
		
		Vector3 heading = Vector3.right;
		if (CheckDestination(destination, heading, height)){
			return foundPath;
		}
			
		currentDirection = (Direction)nodes[index].NewDirection();
		if (currentDirection == Direction.none || index > 7){
			index--;
			return foundPath;
		}
		
		bool hit1Test = false, hit2Test = false, hit3Test = false;
		RaycastHit hit, hit2, hit3;
		int mask = LevelMasks.ClimbableMask | LevelMasks.MechanicsMask;
		float distance = 9999;
		float x, y, z;
		float zOffset = .4f;
		x = nodes[index].curr.x;
		y = nodes[index].curr.y;
		z = nodes[index].curr.z;
		
		switch(currentDirection){
			case Direction.left: 
				heading = Vector3.left; 
				mask = LevelMasks.ClimbableMask | LevelMasks.ImpassableMask; 
				break;	
			case Direction.right: 
				heading = Vector3.right; 
				mask = LevelMasks.ClimbableMask | LevelMasks.ImpassableMask; 
				break;	
			case Direction.up: 
				heading = Vector3.up; 
				mask = LevelMasks.LadderTopMask; 
				y += height; 
				break;	
			case Direction.down: 
				heading = Vector3.down; 
				mask = LevelMasks.GroundMask | LevelMasks.LadderTopMask; 
				y -= height*2; 
				break;	
		}
		
		Debug.Log("Currently heading " + currentDirection + " on " + nodes[index]);
		
		if (nodes[index].hitClimbable && (currentDirection == Direction.up || currentDirection == Direction.down)){
			Debug.Log("I am in a climable");
			Vector3 pointToClimbTo = GetClimbablePositionToGoto(nodes[index].climbableIn, height, currentDirection, nodes[index].GetPos());
			Debug.Log("point to climb to = " + pointToClimbTo);
			if (pointToClimbTo != Vector3.zero){
				index++;
				nodes[index] = new Node((int) currentDirection, pointToClimbTo, destination);
			}
			FindAPath(nodes, destination, height);
			return (foundPath);
		}
		
		Debug.Log("Heading in " + currentDirection + " x = " + x + " y = " + y + " z = " + z);
		
		if (Physics.Raycast(new Vector3(x,y,z), heading, out hit, Mathf.Infinity, mask)) {
			hit1Test = true;
		}
		
		if (Physics.Raycast(new Vector3(x,y,z+zOffset), heading, out hit2, Mathf.Infinity, mask)) {
			hit2Test = true;
		}
		
		if (Physics.Raycast(new Vector3(x,y,z-zOffset), heading, out hit3, Mathf.Infinity, mask)) {
			hit3Test = true;
		}
			
		//get shortest hit distance
		if (hit1Test){
			distance = hit.distance; 
			Debug.Log("hit1");
		}
		if (hit2Test && (hit2.distance < distance || distance == 9999)){
			distance = hit2.distance;
			hit = hit2;
			Debug.Log("hit2");
		}
		if (hit3Test && (hit3.distance < distance || distance == 9999)){
			distance = hit3.distance;
			hit = hit3;
			Debug.Log("hit3");
		}
		
		Debug.Log("Hit1Test = " + hit1Test + "Hit2Test = " + hit2Test + " Hit3Test = " + hit3Test);
		
		if (!hit1Test && !hit2Test && !hit3Test){
			Debug.Log("I hit nothing");
			FindAPath(nodes, destination, height);
		} else{	// if we hit either a climable or mechanics item
			Debug.Log("I hit " + hit.transform.name);
			if (currentDirection == Direction.left || currentDirection == Direction.right){
				if (CheckGround(nodes[index].curr, hit.point, heading, height)){
					Debug.Log("Check ground was false");
					FindAPath(nodes, destination, height);
					return foundPath;
					// TODO maybe mark that we should go here
				}
			} 
			index++;
			nodes[index] = HitInfo.CheckHit((int)currentDirection, hit, destination, nodes[index-1], height);
			FindAPath(nodes, destination, height);
		}
		return foundPath;
	}
	
	// Will check if it is possible to walk to the end point from the start
	private static bool CheckGround(Vector3 start, Vector3 end, Vector3 heading, float height){
		Debug.Log("CheckGround");
		Debug.Log("Start = " + start);
		Debug.Log("End = " + end);
		Debug.Log("Heading = " + heading);
		
		int mask = LevelMasks.GroundMask | LevelMasks.ImpassableMask;
		
		float distance = Mathf.Abs(start.x - end.x);
		RaycastHit hit;
		if (nodes[index].hitClimbable){
			start += heading;
		}
		if (Physics.Raycast(new Vector3(start.x,start.y-height,start.z), heading, out hit, distance, mask)){
			Debug.Log("Lack of ground at " + hit.point);
			return true;
		}
		return false;
	}
	
	private static bool CheckDestination(Vector3 destination, Vector3 heading, float height){
		Debug.Log("CheckDestination");
		if (nodes[index].curr.y + height < destination.y || nodes[index].curr.y - height > destination.y){
			return false;
		}
		Debug.Log("CheckDestination was okay");
		
		RaycastHit debugHit;
		int mask = LevelMasks.GroundMask | LevelMasks.ImpassableMask;
		if (foundPath || !Physics.Linecast(nodes[index].curr, destination, out debugHit, mask)){ // if we found our path or we cannot walk to the goal
			Debug.Log("can walk to the goal");
			Debug.Log("currentDirection = " + currentDirection);
			
			mask = LevelMasks.MechanicsMask;
			
			if (Physics.Linecast(nodes[index].curr, destination, out debugHit, mask)){ // if we intersect with a mechanics object
				Debug.Log("Hit a mechanics object");
				if (debugHit.collider.bounds.Contains(destination)){ // if that mechanic object contains our destination
					if (debugHit.transform.position.x < nodes[index].curr.x){ // if the current node is to the right of the object
						destination.x = destination.x + debugHit.collider.bounds.size.x/2 + MECHANICSBUFFER;
					} else {
						destination.x = destination.x - debugHit.collider.bounds.size.x/2 - MECHANICSBUFFER;
					}
				} else { // if we have a path to the goal but there is a mechanic object in the way then we cannot reach the goal
					if (grabableToMoveThrough != null && debugHit.transform.gameObject == grabableToMoveThrough){ // if it is the pushable object
						// we can go through it
					} else {
						// then we should just move on
						return (false); //TODO remember that we saw this and move the player to a point next to this instead of moving on
					}
				}
			}
			/*
			if (currentDirection == Direction.down || currentDirection == Direction.up || currentDirection == Direction.none){
				currentDirection = Direction.left;
				
				if (nodes[index].curr.x > destination.x){
					heading = Vector3.left;
				} else if (CheckGround(nodes[index].curr, destination, heading, height)){
					return false;
				}
			} else {
				currentDirection = Direction.up;
			}*/
			Debug.Log("Found Path betweem " + nodes[index].curr + "  and  " + destination);
			index++;
			nodes[index] = new Node((int)currentDirection, destination, destination);
			foundPath = true;
			return foundPath;
		}
		Debug.Log("Cannot reach goal");
		return false;
	}
	
	private static void Print(){
		for (int i = 0; i <= index; i++){
			Debug.Log("nodes of " + i + " = " + nodes[i]);
		}
	}
	
	
	private static Vector3 GetTopOfLadder(GameObject ladder){
		return (new Vector3(ladder.transform.position.x, ladder.transform.position.y + ladder.collider.bounds.size.y/2, ladder.transform.position.z));
	}
	
	private static Vector3 GetBottomOfLadder(GameObject ladder){
		return (new Vector3(ladder.transform.position.x, ladder.transform.position.y - ladder.collider.bounds.size.y/2, ladder.transform.position.z));
	}
	
	private static Vector3 GetClimbablePositionToGoto(GameObject climbable, float height, Direction direction, Vector3 current){
		Vector3[] possiblePositions = GetPossibleClimbablePositionToGoto(climbable, height, direction);
		
		Vector3 top = possiblePositions[0];
		Vector3 bottom = possiblePositions[1];
		Debug.Log("Top = " + top);
		Debug.Log("bot = " + bottom);
		Debug.Log("current = " + current);
		
		
		if (direction == Direction.up){
			if (Utils.CalcDistance(current.y, top.y) < levelDifferenceMax){ // if at top already
				Debug.Log("Already at top");
				return (Vector3.zero);
			} else {
				return (top);
			}
		} else {
			if (Utils.CalcDistance(current.y, bottom.y) < levelDifferenceMax){ // if at top already
				Debug.Log("Already at bottom");
				return (Vector3.zero);
			} else {
				return (bottom);
			}
		}
	}
	
	
	// Will find the bottom and top node positions of a given climbable. 
	// 0 entry will be the top, 1 entry is bottom
	private static Vector3[] GetPossibleClimbablePositionToGoto(GameObject climbable, float height, Direction direction){		
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
}