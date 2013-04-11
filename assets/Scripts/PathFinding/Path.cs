using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path {
	
	Vector3[] path;
	Vector3[] vectorDirection;
	//int [] direction;
	int index;
	
	public Path(){}
	
	public Path(int size, Vector3[] paths){
		index = 0;
		path = new Vector3[size];
		vectorDirection = new Vector3[size];
		//direction = new int[size];
		for (int i = 0; i < size; i++){
			this.path[i] = paths[i];
			//direction[i] = dir[i];
			if (i > 0 && path[i-1].x - path[i].x < 0){
				vectorDirection[i] = new Vector3(1,0,0);
				if (path[i-1] != path[i]){
					vectorDirection[i].y = (path[i].y - path[i-1].y)/(Mathf.Abs(path[i-1].x - path[i].x));
					vectorDirection[i] = vectorDirection[i].normalized;
				}
			}else if(i > 0 && path[i-1].x - path[i].x > 0){
				vectorDirection[i] = new Vector3(-1,0,0);
				if (path[i-1] != path[i]){
					vectorDirection[i].y -= (path[i-1].y - path[i].y)/(Mathf.Abs(path[i-1].x - path[i].x));
					vectorDirection[i] = vectorDirection[i].normalized;	
				}
			}			
		}
		index++;
		//Print();
	}
	
	public void Print(){

		for (int i = 0; i < path.Length; i++){
			Debug.Log("Point " + i + " at " + path[i] + " vector heading " + vectorDirection[i]);
		}
	}
	
	public Vector3 GetPoint(){
		return path[index];	
	}
	
	public Vector3 GetVectorDirection(){
		return vectorDirection[index];	
	}
	
	/*public int GetDirection(){
		return direction[index];
	}*/
	
	public bool NextNode(){
		index++;
		return index < path.Length;	
	}
}
