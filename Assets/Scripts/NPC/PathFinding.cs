using UnityEngine;
using System.Collections;

public class PathFinding{
	
	public int foundPath = 0; // 0 - finding path, 1 - no path, 2 - path found
	
	private enum Direction {left, right, up, down, none, fall}; // 0,1,2,3,4
	private Direction currentDirection;
	private float testTimer;
	private Vector3 currentPos, destination;
	
	private bool findPath = false;
	private NodeDirections[] nodeHistory = new NodeDirections[10];
	private Vector3[] nodePoints;
	private bool hit, hitClimbable, upTest;
	private RaycastHit testHit;
	private int nodeIndex;

	
	public PathFinding(){
	}

	public void Update () {
		if (testTimer > 1){
			findPath = false;
			foundPath = 1;
		}
		if (findPath){
			testTimer += Time.deltaTime;
			FindAPath();	
		}
	}

	public void StartPath(Vector3 startPos, Vector3 destination){
		currentDirection = Direction.none;
		findPath = true;
		this. destination = destination;
		currentPos = startPos;
		nodeIndex = 0;
		int mask = (1 << 8);
		CreateCube(startPos);
		if (Physics.Raycast(new Vector3(startPos.x, startPos.y, startPos.z-2), Vector3.forward, out testHit, Mathf.Infinity, mask)){
			if (testHit.transform.tag == Strings.tag_Climbable) {
				nodeHistory[0].hitClimbable = true;
				Debug.Log(testHit.transform.tag);
			}
		}
		currentDirection = (Direction)nodeHistory[nodeIndex-1].NewDirection();
		testTimer = 0;
		FindAPath();
	}
	
	public void FindAPath(){
			HitTest();
			
			if (upTest){
				Vector3 hitPos = new Vector3(testHit.point.x, testHit.point.y -.3f, testHit.point.z);
				Debug.Log("hit up  " + currentDirection + " at " + testHit.point + "  node = " + nodeIndex);
				CreateCube(hitPos);
				nodeHistory[nodeIndex-1].goneUp = true;
				currentDirection = (Direction)nodeHistory[nodeIndex-1].NewDirection();
			}else if (hit){
				Vector3 hitPos = testHit.transform.position;
				if (testHit.transform.tag == Strings.tag_Climbable){ // Ladder
					Debug.Log("hit ladder  " + currentDirection + " at " + testHit.point + "  node = " + nodeIndex);
					CreateCube(new Vector3 (hitPos.x, currentPos.y, currentPos.z));
					currentDirection = (Direction)nodeHistory[nodeIndex-1].NewDirection();
				}else if (testHit.transform.tag == Strings.tag_Ground){ // Ground
					Debug.Log("hit ground  " + currentDirection + " at " + testHit.point + "  node = " + nodeIndex);
					CreateCube(new Vector3 (currentPos.x, hitPos.y + 1f, currentPos.z));
					currentDirection = (Direction)nodeHistory[nodeIndex-1].NewDirection();
				}else if (testHit.transform.tag == Strings.tag_Block){ // Block
					Debug.Log("hit wall at " + testHit.point + "  node = " + nodeIndex);
					if (nodeIndex > 0){
						currentDirection = (Direction)nodeHistory[nodeIndex-1].NewDirection();
					}
					if (nodeIndex < 0 ){
						findPath = false;
					}
					if (currentDirection == Direction.none) {
						RemoveNode();
					}
					if (nodeIndex == 0 ){
						findPath = false;
						Debug.Log("no path found");
					}		
				}else if (testHit.transform.tag == Strings.tag_Destination){ // Destination
					Debug.Log("found a path!");
					findPath = false;
					CreateCube(new Vector3 (hitPos.x, hitPos.y-.4f, currentPos.z));
				}else if (nodeIndex == 0) {
					findPath = false;
					foundPath = 1;
					Debug.Log ("nothing found");
				}
				
			}
			
		if (currentDirection == Direction.none && nodeIndex > 0)
			RemoveNode();
			
		
		if (nodeIndex > 7){
			nodeIndex = 0;	
			currentDirection = (Direction)nodeHistory[nodeIndex].NewDirection();
		}
		
		if (nodeIndex < 0){
			foundPath = 1;
			findPath = false;
		}
	
	}
	
	private void HitTest(){
		upTest = false;
		hitClimbable = false;
		hit = true;
		Vector3 heading = Vector3.right;
		bool hit1Test = false, hit2Test = false, hit3Test = false;
		int mask = (1 << 8) | (1 << 9) | (1 << 10); // Ladder, Ground, Impassable
		RaycastHit hit1, hit2, hit3;
		float distance = 9999;
		float x, y, z;
		float zOffset = .4f;
		x = currentPos.x;
		y = currentPos.y;
		z = currentPos.z;
		switch(currentDirection){
			case Direction.left: heading = Vector3.left; mask = (1 << 8) | (1 << 10); break;	
			case Direction.right: heading = Vector3.right; mask = (1 << 8) | (1 << 10); break;	
			case Direction.up: heading = Vector3.down; mask = (1 << 8) | (1 << 10); break;	
			case Direction.down: heading = Vector3.down; mask = (1 << 9) | (1 << 10); break;	
		}
		
		//Debug.Log ("Testing Path to " + currentDirection + " from pos " + currentPos);
		if (currentDirection != Direction.up){ 
			if (Physics.Raycast(new Vector3(x,y,z), heading, out hit1, Mathf.Infinity, mask)) {
				//Debug.Log("hit1 " +  hit1.transform.tag + "   " + hit1.transform.position);
				hit1Test = true;
			}
			
			if (Physics.Raycast(new Vector3(x,y,z+zOffset), heading, out hit2, Mathf.Infinity, mask)) {
				//Debug.Log("hit2 " +  hit2.transform.tag + "  " + hit2.transform.position);
				hit2Test = true;
			}
			
			if (Physics.Raycast(new Vector3(x,y,z-zOffset), heading, out hit3, Mathf.Infinity, mask)) {
				//Debug.Log("hit3 " +  hit3.transform.tag + "  " + hit3.transform.position);
				hit3Test = true;
			}
				
			//get shortest hit distance
			if (hit1Test){
				distance = hit1.distance; 
				testHit = hit1;
			}
			if (hit2Test && hit2.distance < distance || distance == 9999){
				distance = hit2.distance;
				testHit = hit2;
			}
			if (hit3Test && hit3.distance < distance || distance == 9999){
				distance = hit3.distance;
				testHit = hit3;
			}
			
			// check for pitfalls
			if ((currentDirection == Direction.right || currentDirection == Direction.left) && distance != 9999){
				mask = (1 << 9);
				for (int i = 1; i < distance; i++){
					if (Physics.Raycast(new Vector3(x,y,z)+heading*i, Vector3.down, out hit1, Mathf.Infinity)) {
						//Debug.Log("hit1 " +  hit1.transform.tag + "   " + hit1.point);
						if (hit1.distance > 1){ 
							//Debug.Log("traveled far " + hit1.distance);
							currentDirection = Direction.fall;
							//Debug.Log("hit pit " + currentDirection + " at " + new Vector3 (hit1.point.x, currentPos.y, currentPos.z) + "  node = " + nodeIndex);
							CreateCube(new Vector3 (hit1.point.x, currentPos.y, currentPos.z));
							currentDirection = (Direction)nodeHistory[nodeIndex-1].NewDirection();
							hit = false;
							return;
						}
					}
				}
			}
			if ((!hit1Test && !hit2Test && !hit3Test)){
				Debug.Log("no hit going " + currentDirection);
				if (nodeIndex > 0)
				currentDirection = (Direction)nodeHistory[nodeIndex-1].NewDirection();
				hit = false;
				return;
			}
			if (testHit.transform.tag == Strings.tag_Climbable) 
				hitClimbable = true;
		}else if (currentDirection == Direction.up){
			y+= 7;
			if (Physics.Raycast(new Vector3(x,y,z), heading, out testHit, mask)) {
				Debug.Log("hit " +  testHit.transform.tag + "  " + testHit.point);
				if (testHit.point.y > currentPos.y){
				upTest = true;
				hit = false;
				distance = 5;
				}
			}
		}
		if (currentDirection == Direction.none && nodeIndex > 1){
			RemoveNode();
		}
		
		if (distance == 9999){
			hit = false;
			if (nodeIndex > 0)
				currentDirection = (Direction)nodeHistory[nodeIndex-1].NewDirection();
		}
	}
	
	private void CreateCube(Vector3 pos){
		if (nodeIndex > 1 && Vector3.Distance(pos, nodeHistory[nodeIndex-1].GetPos()) <= .3f && testHit.transform.tag != Strings.tag_Destination){
			Debug.Log("SAME POS");
			currentDirection = (Direction)nodeHistory[nodeIndex-1].NewDirection();
		}else {
			currentPos = pos;
			NodeDirections item = new NodeDirections((int)currentDirection,
				currentPos, destination, hitClimbable);
			nodeHistory[nodeIndex] = item;
			nodeIndex++;
			if (Mathf.Abs(pos.x - destination.x) <= .2f){
				HitDestination();
			}
		}
	}
	
	private void RemoveNode(){
		if (nodeIndex > 1){
			nodeIndex--;
			currentPos = nodeHistory[nodeIndex-1].GetPos();
			currentDirection = (Direction)nodeHistory[nodeIndex-1].NewDirection();
			Debug.Log("Back a node");
		}else{
			findPath = false;
		}
	}
	
	private void HitDestination(){
		nodePoints = new Vector3[nodeIndex];
		for (int i = 0; i < nodeIndex; i++){
			nodePoints[i] = nodeHistory[i].curr;
			//Debug.Log("point[" + i + "] at " + nodePoints[i]);
		}
		foundPath = 2;
	}
	
	public Vector3[] FoundPath(){
		return nodePoints;
	}	
}

public class NodeDirections{
	public bool goneLeft, goneRight, goneUp, goneDown, hitClimbable;
	public Vector3 curr, dest;
	public NodeDirections(int past, Vector3 curr, Vector3 dest, bool hit){
		goneLeft = false;
		goneRight = false;
		goneUp = false;
		goneDown = false;
		hitClimbable = hit;
		this.curr = curr;
		this.dest = dest;
		switch(past){
			case 0: goneRight = true; break; //cur dir = left
			case 1: goneLeft = true; break; //cur dir = right
			case 2: goneDown = true; break; //cur dir = up
			case 3: goneUp = true; break; //cur dir = down
			case 4: break;	
			case 5: goneRight = true;
					goneLeft = true;
					goneUp = true;
					break; 
		}
	}
	
	public int NewDirection(){
		if (curr.x > dest.x && !goneLeft){//go left
			goneLeft = true;
			//Debug.Log ("goneLeft is " + goneLeft + " goneRight is " + goneRight + " goneUp is " + goneUp + " goneDown is " + goneDown + " return 0");
			return 0;
		} else if (curr.x < dest.x && !goneRight){//go right
			goneRight = true;
			//Debug.Log ("goneLeft is " + goneLeft + " goneRight is " + goneRight + " goneUp is " + goneUp + " goneDown is " + goneDown + " return 1");
			return 1;
		}else if (curr.y > dest.y && !goneDown){//go down
			goneDown = true;
			//Debug.Log ("goneLeft is " + goneLeft + " goneRight is " + goneRight + " goneUp is " + goneUp + " goneDown is " + goneDown + " return 3");
			return 3;
		}else if (curr.y < dest.y && !goneUp && hitClimbable){//go up
			goneUp = true;
			//Debug.Log ("Left is " + goneLeft + " Right is " + goneRight + " Up is " + goneUp + " Down is " + goneDown + " return 2");
			//Debug.Log("hitClimbable status " + hitClimbable);
			return 2;
		}
		if (!goneLeft){
			goneLeft = true;
			return 0;
		}else if (!goneRight){
			goneRight = true;
			return 1;
		}else if (!goneUp && hitClimbable == true){
			goneUp = true;
			return 2;
		}else if (!goneDown){
			goneDown = true;
			return 3;
		}else{	
			//Debug.Log ("goneLeft is " + goneLeft + " goneRight is " + goneRight + " goneUp is " + goneUp + " goneDown is " + goneDown + " return 4");
			return 4;	
		}
	}
	
	public Vector3 GetPos(){
		return curr;	
	}
}