using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node {
	public bool goneLeft, goneRight, goneUp, goneDown, hitClimbable;
	public Vector3 curr, dest;
	public int past, lastDir;
	public GameObject climbableIn;

	public enum Type{
		WalkTo,
		ClimbTo,
		StairClimbTo,
		UnSet
	}
	
	private class NodeDirection{
		Type _type;
		bool _hasTraveledOn;
		
		public NodeDirection(){
			_type = Type.UnSet;
			_hasTraveledOn = false;
		}
		
		public NodeDirection(Type type){
			_type = type;
			_hasTraveledOn = true;
		}
		
		public void Mark(){
			_hasTraveledOn = true;
		}
		
		public bool HasTraveled(){
			return (_hasTraveledOn);
		}
		
		public void SetType(Type type){
			_type = type;
		}
		
		public override string ToString(){
			return (_type + ": " + _hasTraveledOn);
		}
	}
	
	public Node(){}
	
	public Node(int past, Vector3 curr, Vector3 dest){
		goneLeft = false;
		goneRight = false;
		goneUp = false;
		goneDown = false;
		hitClimbable = false;
		climbableIn = null;
		this.past = past;
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
	
	public Node(int past, Vector3 curr, Vector3 dest, bool hit){
		goneLeft = false;
		goneRight = false;
		goneUp = false;
		goneDown = false;
		hitClimbable = hit;
		climbableIn = climbableIn;
		this.past = past;
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
	
	public Node(int past, Vector3 curr, Vector3 dest, bool hit, GameObject climbable){
		climbableIn = climbable;
		
		goneLeft = false;
		goneRight = false;
		goneUp = false;
		goneDown = false;
		hitClimbable = hit;
		this.past = past;
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
			lastDir = 0;
		} else if (curr.x < dest.x && !goneRight){//go right
			goneRight = true;
			lastDir = 1;
		}else if (curr.y > dest.y && !goneDown && hitClimbable){//go down
			goneDown = true;
			lastDir = 3;
		}else if (curr.y < dest.y && !goneUp && hitClimbable){//go up
			goneUp = true;
			lastDir = 2;
		}else if (!goneLeft){
			goneLeft = true;
			lastDir = 0;
		}else if (!goneRight){
			goneRight = true;
			lastDir = 1;
		}else if (!goneUp && hitClimbable == true){
			goneUp = true;
			lastDir = 2;
		}else if (!goneDown && hitClimbable){
			goneDown = true;
			lastDir = 3;
		}else if (past == 5 && !goneDown){
			goneDown = true;
			lastDir = 3;
		}else{	
			return 4;	
		}
		
		return lastDir;
	}
	
	public Vector3 GetPos(){
		return curr;	
	}
	
	public override string ToString(){
		return (curr.ToString() + " L = " + goneLeft + " R = " + goneRight + " U = " + goneUp + " D = " + goneDown + " hitClimbable = " + hitClimbable);
	}
}
