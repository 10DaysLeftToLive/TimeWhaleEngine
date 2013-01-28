/*
CameraController.js
This script allows for panning the camera via drag and zooming
Attach this script to the camera.
*/

#pragma strict

// Variables for altering the camera's movement
static public var speedOfZoom: float = 100;	
static public var closestZoomDistance: float = 4;	
static public var farthestZoomDistnace: float = 15;
static public var zoomingIncrement: float = .5;

static private var thisCamera: Camera;

static public var zOffsetRelativeToPlayer : float = - 10;
	
function Start () {
	thisCamera = Camera.main;
}

// The function uses the difference in the mouse's position between frames
// to determine which way to drag the camera, and moves the camera in that direction.
static public function Drag(currentInputPos: Vector2){
	thisCamera.transform.Translate(new Vector3(currentInputPos.x, currentInputPos.y, 0), Space.World);
}

// This function is used to zoom the camera in and out.
// Assumes the camera is at a 45 degree angle towards the terrain.
static function Zoom(isZoomingIn: boolean){
	if (isZoomingIn && CanZoomIn()){
		ZoomIn();
	} else if (!isZoomingIn && CanZoomOut()) {
		ZoomOut();
	}
}

private static function CanZoomIn() : boolean{
	return (thisCamera.orthographicSize > closestZoomDistance);
}

private static function CanZoomOut() : boolean{
	return (thisCamera.orthographicSize < farthestZoomDistnace);
}

private static function ZoomIn(){
	thisCamera.orthographicSize -= zoomingIncrement;
}

private static function ZoomOut(){
	thisCamera.orthographicSize += zoomingIncrement;
}