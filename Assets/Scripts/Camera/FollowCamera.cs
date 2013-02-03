using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
	
	public Transform cameraTarget;
	
	public float yOffsetRelativeToTarget = 3;
	public float zOffsetRelativeToTarget = - 10;
	
	// Use this for initialization
	void Start () {
		MoveCameraToTarget();
	}
	
	// Update is called once per frame
	void Update () {
		MoveCameraToTarget();
	}
	
	void MoveCameraToTarget(){
		Vector3 newCameraPosition = cameraTarget.position;
		newCameraPosition.y += yOffsetRelativeToTarget;
		newCameraPosition.z += zOffsetRelativeToTarget;
		transform.position = newCameraPosition;
	}
}
