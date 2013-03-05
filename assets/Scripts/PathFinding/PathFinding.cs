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
	private static List<Node> nodes;
	
	private static Queue<Node> nodeQueue;
	
	private static GameObject grabableToMoveThrough = null; // only set if we are grabing an object
	
	private static float MECHANICSBUFFER = .4f;
	
	private static float levelDifferenceMax = .25f;
	private static float distanceToLook = 9999f;
	
	public static bool StartPath(Vector3 startPos, Vector3 destination, float height){
		nodes = new List<Node>();
		nodeQueue = new Queue<Node>();
		
		Node start = CreateNodeAt(startPos);
		Node goal = CreateNodeAt(destination);
		
		nodeQueue.Enqueue(start);
		
		if (FindPath(goal, height)){// FindAPath(nodes, destination, height)){
			//return false;//true;
		}
		
		Debug.Log("--------------Path---------------");
		
		int i = 0;
		
		foreach (Node n in nodeQueue){
			Debug.Log("Node[" + i++ + "] = " + n);
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
			points[i] = nodes[i].curr;
			dir[i] = nodes[i].past;
		}
		Path path = new Path(index + 1, points, dir);
		return path;
	}
	
	/*
	private static bool FindAPath(List<Node> nodes, Vector3 destination, float height){
		Vector3 heading = Vector3.right;
		if (CheckDestination(destination, heading, height))
			return foundPath;
			
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
				mask = LevelMasks.LadderTopMask; y += height; 
				break;	
			case Direction.down: 
				heading = Vector3.down; 
				mask = LevelMasks.GroundMask | LevelMasks.LadderTopMask; 
				y-= height*2; 
				break;	
		}
		
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
		}
		if (hit2Test && hit2.distance < distance || distance == 9999){
			distance = hit2.distance;
			hit = hit2;
		}
		if (hit3Test && hit3.distance < distance || distance == 9999){
			distance = hit3.distance;
			hit = hit3;
		}
		
		if (!hit1Test && !hit2Test && !hit3Test){
			FindAPath(nodes, destination, height);
		} else{
			if (currentDirection == Direction.left || currentDirection == Direction.right){
				if (CheckGround(nodes[index].curr, hit.point, heading, height))
					return foundPath;
			}
			index++;
			//nodes[index] = HitInfo.CheckHit((int)currentDirection, hit, destination, nodes[index-1], height);
			FindAPath(nodes, destination, height);
		}
		return foundPath;
	}*/
	
	private static bool FindPath(Node goalNode, float height){
		Node currentNode;
		Debug.Log("Looking for " + goalNode);
		
		while (true){
			if (nodeQueue.Count == 0){
				Debug.Log("Ran out of nodes to look at.");
				return false;
			} else if (nodeQueue.Count > 7) {
				Debug.Log("I am up to 7 nodes doesn't look like I'm getting there.");
				return false;
			}
			
			currentNode = nodeQueue.Peek();
			Debug.Log("I am currently on: " + currentNode);
						
			if (OnSameLevel(currentNode, goalNode) && CanWalkToGoal(currentNode, goalNode)){
				Debug.Log("I found a way to the goal.");
				currentNode.LinkToNode(goalNode);
				nodeQueue.Enqueue(goalNode);
				return (true);
			} else {
				Debug.Log("I need to look for a way to change levels.");
				// If we cannot reach the goal on the current level we need to look at shifting up or down from the current node
				if (currentNode.IsDeadEnd()){
					Debug.Log("I was at a dead end.");
					nodeQueue.Dequeue();
					continue;
				}
				
				RaycastHit objectHit; 
				
				Direction nextDirection = GetNextDirection(currentNode, goalNode);			
				Direction otherDirection = (nextDirection == Direction.left ? Direction.right : Direction.left);
				
				if (HitClimbableInDirection(currentNode, nextDirection, out objectHit)){
					Debug.Log("I looked " + nextDirection + " and I hit " + objectHit.transform.gameObject.name);
					Debug.Log("Moving to " + GetTopOfLadder(objectHit.transform.gameObject));
				} else if (HitClimbableInDirection(currentNode, otherDirection, out objectHit)){
					Debug.Log("I looked " + otherDirection + " and I hit " + objectHit.transform.gameObject.name);
				} else {
					Debug.Log("I looked both " + nextDirection + " and " + otherDirection + ". I was not able to find a climbable.");
					// TODO look to fall off nearby floor
				}
				return false;
			}
		}
		return (true);
	}
	
	private static void MoveOverALevel(GameObject climbable){
		//TODO
		Node currentNode = AddNodeAtCurrentPoint();
		MarkWhereGoingOnNode();
		Vector3 nextPoint = CalculateNextNode(climbable);
		AddNextPoint(nextPoint);
	}
	
	private static Node AddNodeAtCurrentPoint(){
		return new Node();
	}
	
	private static Node AddNodeAtPoint(Vector3 position){
		return new Node();
		
	}
	
	private static void MarkWhereGoingOnNode(){
		
	}
	
	private static Vector3 CalculateNextNode(GameObject climbable){
		return (GetTopOfLadder(climbable));
	}
	
	private static void AddNextPoint(Vector3 nextPoint){
		
	}
	
	private static Node CreateNodeAt(Vector3 position){
		Node newNode = new Node(position);
		return (newNode);
	}
	
	private static Vector3 GetTopOfLadder(GameObject ladder){
		return (new Vector3(ladder.transform.position.x, ladder.transform.position.y + ladder.collider.bounds.size.y/2, ladder.transform.position.z));
	}
	
	private static bool CanWalkToGoal(Node currentNode, Node goalNode){
		int mask = LevelMasks.GroundMask | LevelMasks.ImpassableMask;
		return (!Physics.Linecast(currentNode._position, goalNode._position, mask));
	}
	
	private static bool HitClimbableInDirection(Node currentNode, Direction directionToCheck, out RaycastHit objectHit){
		int mask = LevelMasks.ClimbableMask;
		
		Vector3 heading;
		
		switch(directionToCheck){
			case Direction.left: 
				heading = Vector3.left; 
				break;	
			case Direction.right: 
				heading = Vector3.right; 
				break;	
			case Direction.up: 
				heading = Vector3.up; 
				break;	
			default:
				heading = Vector3.down; 
				break;	
		}
		
		Debug.Log("HitClimbableInDirection" + currentNode._position + ", " + directionToCheck);
			
		return (Physics.Raycast(currentNode._position, heading, out objectHit, distanceToLook, mask));
	}
	
	private static bool OnSameLevel(Node currentNode, Node goalNode){
		return (Utils.CalcDistance(currentNode.curr.y, goalNode.curr.y) < levelDifferenceMax); 
	}

	private static Direction GetNextDirection(Node currentNode, Node goalNode){
		return (currentNode.curr.x > goalNode.curr.x ? Direction.left : Direction.right);
	}
	
	private static bool NoNodes(){
		return (nodes.Count == 0);
	}
	
	private static bool CheckGround(Vector3 start, Vector3 end, Vector3 heading, float height){
		float distance = Mathf.Abs(start.x - end.x);
		RaycastHit hit;
		if (nodes[index].hitClimbable)
			start += heading;
		if (Physics.Raycast(new Vector3(start.x,start.y-height,start.z), heading, out hit, distance)){//, mask)){
			return true;
		}
		return false;
	}
	
	private static bool CheckDestination(Vector3 destination, Vector3 heading, float height){
		if (nodes[index].curr.y + height < destination.y || nodes[index].curr.y - height > destination.y)
			return false;
		
		RaycastHit debugHit;
		int mask = LevelMasks.GroundMask | LevelMasks.ImpassableMask;
		if (foundPath || !Physics.Linecast(nodes[index].curr, destination, out debugHit, mask)){ // if we can draw a line to the goal without a barrier
			mask = LevelMasks.MechanicsMask;
			
			if (Physics.Linecast(nodes[index].curr, destination, out debugHit, mask)){ // if we intersect with a mechanics object
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
			
			if (currentDirection == Direction.down || currentDirection == Direction.up || currentDirection == Direction.none){
				currentDirection = Direction.left;
				
				if (nodes[index].curr.x > destination.x){
					heading = Vector3.left;
				} else if (CheckGround(nodes[index].curr, destination, heading, height)){
					return false;
				}
			} else {
				currentDirection = Direction.up;
			}
			index++;
			nodes.Add(new Node((int)currentDirection, destination, destination));
			foundPath = true;
			return foundPath;
		}
		return false;
	}
}