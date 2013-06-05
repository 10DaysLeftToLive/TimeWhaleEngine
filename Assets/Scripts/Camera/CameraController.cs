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
	static private float farthestZoomDistance = 8f;
	static private float zoomingIncrement = .25f; //.2f; was soso //.075f; was original
	static private float maxYOffset = 1f;
	static private float minYOffset = 1.3f;
	
	static public float currentYOffsetRelativeToTarget = 1f;
	static public float zOffsetFromTarget = -5f;
	
	static private Camera thisCamera;
	static private Player player;
	#endregion
	
	public void Start () {
		try{
			thisCamera = Camera.main;
			player = FindObjectOfType(typeof(Player)) as Player;
			thisCamera.orthographicSize = getStartZoom();
		}catch{
			Debug.LogWarning("Main Camera or Player not found");	
		}
	}
	
	void Update () {
		if(thisCamera != null)	
			MoveCameraToTarget();
	}
	
	// This function is used to zoom the camera in and out.
	// Assumes the camera is at a 45 degree angle towards the terrain.
	static public void Zoom(bool isZoomingIn){
		if (isZoomingIn && CanZoomOut()){
			ZoomOut();
		} else if (!isZoomingIn && CanZoomIn()) {	
			ZoomIn();
		}
		currentYOffsetRelativeToTarget = CalcOffset();
	}
	
	static private bool CanZoomIn(){
		return (thisCamera.orthographicSize > closestZoomDistance);
	}
	
	static private bool CanZoomOut(){
		return (thisCamera.orthographicSize < farthestZoomDistance);
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
	
	private float getStartZoom() {
			return (farthestZoomDistance + closestZoomDistance) / 2;
	}
	
	/// <summary>
	/// Calculates the offset in the y direction by linear interpolation
	/// </summary>
	static private float CalcOffset(){
		return ((minYOffset + (maxYOffset - minYOffset)*((thisCamera.orthographicSize - closestZoomDistance)/(farthestZoomDistance-closestZoomDistance))));
	}
	
}