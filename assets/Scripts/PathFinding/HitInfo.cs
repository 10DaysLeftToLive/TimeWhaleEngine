using UnityEngine;
using System.Collections;

public static class HitInfo {
	
	
	public static int FirstNode(){
		return 0;
	}
	
	public static Node CheckHit(int dir, RaycastHit hit, Vector3 dest, Node lastNode, float height){
		Node node = new Node();
		Vector3 hitPos = hit.point;
		hitPos.z = lastNode.curr.z;
		if (hit.transform.tag == Strings.tag_Climbable){
			hitPos.x = hit.transform.position.x;
			Node nodes = new Node(dir, hitPos, dest, true);
			return nodes;
		}else if (hit.transform.tag == Strings.tag_LadderTop){
			hitPos.x = hit.transform.position.x;
			hitPos.y += height;
			Node nodes = new Node(dir, hitPos, dest, true);
			return nodes;
		}else if (hit.transform.tag == Strings.tag_Ground){
			if (dir == 3 && lastNode.hitClimbable) { // down
				Node nodes = new Node(dir, hitPos, dest);
			}
		}
		
		return node;
		/*if (testHit.transform.tag == Strings.tag_Climbable){ // Ladder
			hitClimbable = true;	
			CreateCube(new Vector3 (testHit.transform.position.x, currentPos.y, currentPos.z));
			currentDirection = (Direction)nodeHistory[nodeIndex-1].NewDirection();
		}else if (testHit.transform.tag == Strings.tag_Ground){ // Ground
			CreateCube(new Vector3 (currentPos.x, hitPos.y + 1f, currentPos.z));
			currentDirection = (Direction)nodeHistory[nodeIndex-1].NewDirection();
		}else if (testHit.transform.tag == Strings.tag_LadderTop){
			hitClimbable = true;
			CreateCube(new Vector3 (currentPos.x, hitPos.y + 1.5f, currentPos.z));
			currentDirection = (Direction)nodeHistory[nodeIndex-1].NewDirection();
		}else if (testHit.transform.tag == Strings.tag_Block){ // Block
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
			findPath = false;
			CreateCube(new Vector3 (hitPos.x, hitPos.y, currentPos.z));
		}else if (nodeIndex == 0) {
			findPath = false;
			foundPath = 1;
		}	*/	
	}
	
}
