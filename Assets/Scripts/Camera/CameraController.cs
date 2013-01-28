using UnityEngine;
using System.Collections;

	/*
CameraController.js
This script allows for panning the camera via drag and zooming
Attach this script to the camera.
*/

public class CameraController : MonoBehaviour {
	// Variables for altering the camera's movement
	static public float speedOfZoom = 100f;	
	static public float closestZoomDistance = 4f;	
	static public float farthestZoomDistnace = 15f;
	static public float zoomingIncrement = .5f;
	
	static private Camera thisCamera;
	
	static public float zOffsetRelativeToPlayer = - 10;
		
	void Start () {
		thisCamera = Camera.main;
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
}
