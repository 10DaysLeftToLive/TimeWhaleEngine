using UnityEngine;
using System.Collections;

public class WayPoints : MonoBehaviour {

	public GameObject LeftWayPoint;
	public GameObject RightWayPoint;
	public GameObject UpWayPoint;
	public GameObject DownWayPoint;
	
	private Vector3 floorPosition;
	
	void Start () {
		floorPosition = this.transform.position;
		floorPosition.y -= this.collider.bounds.size.y/2;
	}
	
	public Vector3 GetFloorPosition(){
		return floorPosition;
	}
	
	public GameObject GetLeft(){
		return LeftWayPoint;
	}
	
	public GameObject GetRight(){
		return RightWayPoint;
	}
	
	public GameObject GetUp(){
		return UpWayPoint;
	}
	
	public GameObject GetDown(){
		return DownWayPoint;
	}
	
	public bool CheckLeft(){
		if (LeftWayPoint != null)
			return true;
		return false;
	}
	
	public bool CheckRight(){
		if (RightWayPoint != null)
			return true;
		return false;
	}
	
	public bool CheckUp(){
		if (UpWayPoint != null)
			return true;
		return false;
	}
	
	public bool CheckDown(){
		if (DownWayPoint != null)
			return true;
		return false;
	}
	
}
