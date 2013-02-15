using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path {
	
	Vector3[] path;
	int [] direction;
	int index;
	
	public Path(){}
	
	public Path(int size, Vector3[] paths, int[] dir){
		index = 0;
		path = new Vector3[size];
		direction = new int[size];
		for (int i = 0; i < size; i++){
			this.path[i] = paths[i];
			direction[i] = dir[i];
		}
		index++;
	}
	
	public void Print(){

		for (int i = 0; i < path.Length; i++){
			Debug.Log("Point " + i + " at " + path[i] + " heading " + direction[i]);
			//Debug.Log(i + "  " + path.Length);
		}
	}
	
	public Vector3 GetPoint(){
		return path[index];	
	}
	
	public int GetDirection(){
		return direction[index];
	}
	
	public bool NextNode(){
		index++;
		return index < path.Length;	
	}
}
