using UnityEngine;
using System.Collections;

/*
CameraController.cs
This script allows for panning the camera via drag and zooming. It will also follow the player character.

Attach this script to the camera.
*/

public class CameraController : MonoBehaviour {
	#region Fields
	// Variables for altering the camera's movement
	static private float closestZoomDistance = 1f;	
	static private float farthestZoomDistnace = 15f;
	static private float zoomingIncrement = .5f;
	
	static private float yOffsetRelativeToTarget = 1;
	static private float zOffsetRelativeToPlayer = - 10;
	
	static private Camera thisCamera;
	static private PlayerController player;
	#endregion
	
	public void Start () {
		thisCamera = Camera.main;
		player = FindObjectOfType(typeof(PlayerController)) as PlayerController;	
	}
	
	public void Update () {
		MoveCameraToTarget();
	}
	
	// The function uses the difference in the mouse's position between frames
	// to determine which way to drag the camera, and moves the camera in that direction.
	static public void Drag(Vector2 currentInputPos){
		thisCamera.transform.Translate(new Vector3(currentInputPos.x, currentInputPos.y, zOffsetRelativeToPlayer), Space.World);
	}
	
	// This function is used to zoom the camera in and out.
	// Assumes the camera is at a 45 degree angle towards the terrain.
	static public void Zoom(bool isZoomingIn){
		if (isZoomingIn && CanZoomIn()){
			ZoomIn();
		} else if (!isZoomingIn && CanZoomOut()) {
			ZoomOut();
		}
	}
	
	static private bool CanZoomIn(){
		return (thisCamera.orthographicSize > closestZoomDistance);
	}
	
	static private bool CanZoomOut(){
		return (thisCamera.orthographicSize < farthestZoomDistnace);
	}
	
	static private void ZoomIn(){
		thisCamera.orthographicSize -= zoomingIncrement;
	}
	
	static private void ZoomOut(){
		thisCamera.orthographicSize += zoomingIncrement;
	}
	
	static private void MoveCameraToTarget(){
		Vector3 newCameraPosition = player.transform.position;
		newCameraPosition.y += yOffsetRelativeToTarget;
		newCameraPosition.z += zOffsetRelativeToPlayer;
		thisCamera.transform.position = newCameraPosition;
	}
}