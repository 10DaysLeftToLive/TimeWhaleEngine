using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
	
	public Transform cameraTarget;
	
	public float zOffsetRelativeToPlayer = - 10;
	
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
		newCameraPosition.z += zOffsetRelativeToPlayer;
		transform.position = newCameraPosition;
	}
}
