using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PathFinding {
	#region Layers
	private static int ClimbableLayer = 8;
	private static int GroundLayer = 9;
	private static int ImpassableLayer = 10;
	private static int LadderTopLayer = 13;
	#endregion
	
	#region Mashs
	private static int ClimbableMask = (1 << ClimbableLayer);
	private static int GroundMask = (1 << GroundLayer);
	private static int ImpassableMask = (1 << ImpassableLayer);
	private static int LadderTopMask = (1 << LadderTopLayer);
	#endregion
	
	private enum Direction {
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
	
	public static bool StartPath(Vector3 startPos, Vector3 destination, float height){
		nodes = new List<Node>();
		index = 0;
		currentDirection = Direction.none;
		foundPath = false;
		nodes.Add(new Node((int)currentDirection, startPos, destination));
		int mask = ClimbableMask;
		RaycastHit hit;
		if (Physics.Raycast(new Vector3(startPos.x, startPos.y, startPos.z-2), Vector3.forward, out hit, Mathf.Infinity, mask)){
			if (hit.transform.tag == Strings.tag_Climbable) {
				nodes[0].hitClimbable = true;
			}
		}
		if (FindAPath(nodes, destination, height)){
			return true;
		}
		Debug.Log("No Path was Found");
		return false;
	}
	
	public static Path GetPath(){
		Vector3[] points = new Vector3[index+1]; 
		int[] dir = new int[index+1];
		for (int i = 0; i <= index; i++){
			points[i] = nodes[i].curr;
			dir[i] = nodes[i].past;
			Debug.Log("Point " + i + " at " + points[i] + " heading " + dir[i]);
		}
		Path path = new Path(index + 1, points, dir);
		return path;
	}
	
	private static bool FindAPath(List<Node> nodes, Vector3 destination, float height){
		Vector3 heading = Vector3.right;
		if (CheckDestination(destination, heading, height))
			return foundPath;
			
		currentDirection = (Direction)nodes[index].NewDirection();
		if (currentDirection == Direction.none || index > 7){
			Debug.Log("Current Direction " + currentDirection);
			index--;
			return foundPath;
		}
		
		bool hit1Test = false, hit2Test = false, hit3Test = false;
		RaycastHit hit, hit2, hit3;
		int mask = ClimbableMask;
		float distance = 9999;
		float x, y, z;
		float zOffset = .4f;
		x = nodes[index].curr.x;
		y = nodes[index].curr.y;
		z = nodes[index].curr.z;
		switch(currentDirection){
			case Direction.left: heading = Vector3.left; mask = ClimbableMask | ImpassableMask; break;	
			case Direction.right: heading = Vector3.right; mask = ClimbableMask | ImpassableMask; break;	
			case Direction.up: heading = Vector3.up; mask = LadderTopMask; y += height; break;	
			case Direction.down: heading = Vector3.down; mask = GroundMask | LadderTopMask; y-= height*2; break;	
		}
		
		if (Physics.Raycast(new Vector3(x,y,z), heading, out hit, Mathf.Infinity, mask)) {
			Debug.Log("Hit1 " + hit.transform.tag);
			hit1Test = true;
		}
		
		if (Physics.Raycast(new Vector3(x,y,z+zOffset), heading, out hit2, Mathf.Infinity, mask)) {
			Debug.Log("Hit2 " + hit2.transform.tag);
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
		
		if ((!hit1Test && !hit2Test && !hit3Test)){
			FindAPath(nodes, destination, height);
		} else{
			if (currentDirection == Direction.left || currentDirection == Direction.right){
				if (CheckGround(nodes[index].curr, hit.point, heading, height))
					return foundPath;
			}
			index++;
			nodes[index] = HitInfo.CheckHit((int)currentDirection, hit, destination, nodes[index-1], height);
			FindAPath(nodes, destination, height);
		}
		return foundPath;
	}
	
	private static bool CheckGround(Vector3 start, Vector3 end, Vector3 heading, float height){
		float distance = Mathf.Abs(start.x - end.x);
		RaycastHit hit;
		if (nodes[index].hitClimbable)
			start += heading;
		Debug.DrawLine(new Vector3(start.x,start.y-height,start.z), new Vector3(start.x + distance,start.y-height,start.z), Color.red, 20);
		if (Physics.Raycast(new Vector3(start.x,start.y-height,start.z), heading, out hit, distance)){//, mask)){
			return true;
		}
		
		return false;
	}
	
	private static bool CheckDestination(Vector3 destination, Vector3 heading, float height){
		if (nodes[index].curr.y + height < destination.y || nodes[index].curr.y - height > destination.y)
			return false;
		
		RaycastHit debugHit;
		int mask = GroundMask | ImpassableMask;
		if (foundPath || !Physics.Linecast(nodes[index].curr, destination, out debugHit, mask)){
			if (currentDirection == Direction.down || currentDirection == Direction.up || currentDirection == Direction.none){
				currentDirection = Direction.left;
				
				if (nodes[index].curr.x > destination.x)
					heading = Vector3.left;
				if (CheckGround(nodes[index].curr, destination, heading, height))
					return false;
			}else{
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