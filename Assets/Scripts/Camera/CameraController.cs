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
	static private float closestZoomDistance = 2.45f;	
	static private float farthestZoomDistnace = 4f;
	static private float zoomingIncrement = .075f;
	static private float maxYOffset = 1f;
	static private float minYOffset = 1.3f;
	
	static public float currentYOffsetRelativeToTarget = 1f;
	static private float zOffsetFromTarget = -5f;
	
	static private Camera thisCamera;
	static private Player player;
	#endregion
	
	public void Start () {
		thisCamera = Camera.main;
		player = FindObjectOfType(typeof(Player)) as Player;
		thisCamera.orthographicSize = farthestZoomDistnace;
	}
	
	void Update () {
		MoveCameraToTarget();
	}
	
	// This function is used to zoom the camera in and out.
	// Assumes the camera is at a 45 degree angle towards the terrain.
	static public void Zoom(bool isZoomingIn){
		if (isZoomingIn && CanZoomIn()){
			ZoomIn();
		} else if (!isZoomingIn && CanZoomOut()) {
			ZoomOut();
		}
		currentYOffsetRelativeToTarget = CalcOffset();
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
	
	private Vector3 newCameraPosition;
	private void MoveCameraToTarget() {
		newCameraPosition = player.transform.position;
		newCameraPosition.y += currentYOffsetRelativeToTarget;
		newCameraPosition.z += zOffsetFromTarget;
		thisCamera.transform.position = newCameraPosition;
	}
	
	/// <summary>
	/// Calculates the offset in the y direction by linear interpolation
	/// </summary>
	static private float CalcOffset(){
		return (minYOffset + (maxYOffset - minYOffset)*((thisCamera.orthographicSize - closestZoomDistance)/(farthestZoomDistnace-closestZoomDistance)));
	}
	
}