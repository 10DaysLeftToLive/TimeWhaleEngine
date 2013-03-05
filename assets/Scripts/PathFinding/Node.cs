using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node {
	public bool goneLeft, goneRight, goneUp, goneDown, hitClimbable;
	public Vector3 curr, dest;
	public int past, lastDir;
	
	private static float MINDIF = .5f; // Buffer room for difs of positions
	public Vector3 _position;	
	private Dictionary<PathFinding.Direction, NodeDirection> links = new Dictionary<PathFinding.Direction, NodeDirection>();
	
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
	
	public Node(){
		SetLinks();
	}
	
	public Node(Vector3 position){
		_position = position;
		SetLinks();
	}
	
	public Node(Vector3 position, PathFinding.Direction previousDirection, Type wayToGoTo){
		_position = position;
		MarkAndSet(previousDirection, wayToGoTo);
		SetLinks();
	}
	
	public void MarkAndSet(PathFinding.Direction direction, Type movementType){
		links[direction].Mark();
		links[direction].SetType(movementType);
	}
	
	public bool IsDeadEnd(){
		return (HasGoneIn(PathFinding.Direction.left) &&
			    HasGoneIn(PathFinding.Direction.right) &&
				HasGoneIn(PathFinding.Direction.up) &&
				HasGoneIn(PathFinding.Direction.down));
	}
	
	public bool HasGoneIn(PathFinding.Direction direction){
		return (links[direction].HasTraveled());
	}
	
	public void LinkToNode(Node node){
		Debug.Log("-----Linking------");
		Debug.Log(this);
		Debug.Log(node);
		
		if (IsAbove(node)){
			if (IsToLeft(node) || IsToRight(node)){
				MarkAndSet(PathFinding.Direction.up, Type.StairClimbTo);
				node.MarkAndSet(PathFinding.Direction.down, Type.StairClimbTo);
			} else {
				MarkAndSet(PathFinding.Direction.up, Type.ClimbTo);
				node.MarkAndSet(PathFinding.Direction.down, Type.StairClimbTo);
			}
		} else if (IsBelow(node)){
			if (IsToLeft(node) || IsToRight(node)){
				MarkAndSet(PathFinding.Direction.down, Type.StairClimbTo);
				node.MarkAndSet(PathFinding.Direction.up, Type.StairClimbTo);
			} else {
				MarkAndSet(PathFinding.Direction.down, Type.ClimbTo);
				node.MarkAndSet(PathFinding.Direction.up, Type.StairClimbTo);
			}
		} else {
			if (IsToLeft(node)){
				MarkAndSet(PathFinding.Direction.left, Type.WalkTo);
				node.MarkAndSet(PathFinding.Direction.right, Type.WalkTo);
			} else {
				MarkAndSet(PathFinding.Direction.right, Type.WalkTo);
				node.MarkAndSet(PathFinding.Direction.left, Type.WalkTo);
			}
		}
		
		Debug.Log("After linking node is set to " + this);
	}
	
	private bool IsToLeft(Node node){
		float dif = Utils.CalcDifference(node._position.x, _position.x);
		return ((Utils.Within(dif, MINDIF) ? false : dif < 0));
	}
	
	private bool IsToRight(Node node){
		float dif = Utils.CalcDifference(node._position.x, _position.x);
		return ((Utils.Within(dif, MINDIF) ? false : dif > 0));
	}
	
	private bool IsAbove(Node node){
		float dif = Utils.CalcDifference(node._position.y, _position.y);
		return ((Utils.Within(dif, MINDIF) ? false : dif < 0));
	}
	
	private bool IsBelow(Node node){
		float dif = Utils.CalcDifference(node._position.y, _position.y);
		return ((Utils.Within(dif, MINDIF) ? false : dif > 0));
	}
	
	private void SetLinks(){
		links[PathFinding.Direction.left] = new NodeDirection();
		links[PathFinding.Direction.right] = new NodeDirection();
		links[PathFinding.Direction.up] = new NodeDirection();
		links[PathFinding.Direction.down] = new NodeDirection();
	}
	
	public Node(int past, Vector3 curr, Vector3 dest){
		goneLeft = false;
		goneRight = false;
		goneUp = false;
		goneDown = false;
		hitClimbable = false;
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
		string result = "";
		result += " L:" + links[PathFinding.Direction.left];
		result += " R:" + links[PathFinding.Direction.right];
		result += " U:" + links[PathFinding.Direction.up];
		result += " D:" + links[PathFinding.Direction.down];
		
		return (_position + "  " +  result);
	}
}
