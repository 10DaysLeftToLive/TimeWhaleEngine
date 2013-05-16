using UnityEngine;
using System.Collections;

public class WayPoints : MonoBehaviour {
	
	public enum Age {
		young = 0,
		middle = 1,
		old = 2
	};

	public GameObject LeftWayPoint;
	public GameObject RightWayPoint;
	public GameObject UpWayPoint;
	public GameObject DownWayPoint;
	
	public float leftDistance;
	public float rightDistance;
	public float upDistance;
	public float downDistance;
	
	public int id;
	public Age pointAge;
	
	private bool setupWayPoints = false;
	private bool initialized = false;
	
	private Vector3 floorPosition;
	
	void Start () {
		floorPosition = this.transform.position;
		floorPosition.y -= this.collider.bounds.size.y/2;
		SetDistance();
	}
	
	void Update (){
		if (this.id == 0 && !initialized){
			Graph.Initialize();	
		}
		if ((this.id == 0)&& !setupWayPoints && initialized && pointAge == Age.young){
			Graph.StartGraph(this.gameObject);
			WayPointPath.Initialize();
			setupWayPoints = true;
		}
		initialized = true;
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
	
	private void SetDistance(){
		Vector3 pos = this.transform.position;
		if (CheckLeft()) leftDistance = Vector3.Distance(pos, LeftWayPoint.transform.position);
		if (CheckRight()) rightDistance = Vector3.Distance(pos, RightWayPoint.transform.position);
		if (CheckUp()) upDistance = Vector3.Distance(pos, UpWayPoint.transform.position);
		if (CheckDown()) downDistance = Vector3.Distance(pos, DownWayPoint.transform.position);
	}
	
}
