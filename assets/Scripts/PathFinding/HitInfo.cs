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
		Debug.Log("CheckHit");
		Debug.Log("hitPos = " + hitPos);
		Debug.Log("hit = " + hit.transform.position);
		Debug.Log("dest = " + dest);
		Debug.Log("lastNode = " + lastNode);
		Debug.Log("dir = " + dir);
		Debug.Log("Tag = " + hit.transform.tag);
		
		if (hit.transform.tag == Strings.tag_Climbable){
			hitPos.x = hit.transform.position.x;
			Node nodes = new Node(dir, hitPos, dest, true, hit.transform.gameObject);
			return nodes;
		} else if (hit.transform.tag == Strings.tag_Mechanics){
			hitPos.x = hit.transform.position.x;
			Node nodes = new Node(dir, hitPos, dest, false);
			return nodes;			
		} else if (hit.transform.tag == Strings.tag_LadderTop){
			float objHeight = Mathf.Abs(hit.point.y - hit.transform.position.y)*2;
			hitPos.x = hit.transform.position.x;
			hitPos.y += height + objHeight;
			Node nodes = new Node(dir, hitPos, dest, true);
			return nodes;
		} else if (hit.transform.tag == Strings.tag_Ground){
			if (dir == 3 && lastNode.hitClimbable) { // down
				Debug.Log("point.y is " + hitPos.y + " height is " + height/2);
				hitPos.y += height;
				Node nodes = new Node(dir, hitPos, dest);
				return nodes;
			}
		}
		
		return node;
	}
}
