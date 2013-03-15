using UnityEngine;
using System.Collections;

/*
 * InputType.cs
 * Base class for input into game. These functions will be called at the appropriat time be a derived class.
 */


public abstract class InputType  {
	// The input detection is based off of these states
	protected enum ControlState {
		WaitingForFirstInput, // there are no fingers down/ mouse down
		WaitingForSecondTouch, // there is one finger down and we are waiting to see if user presses down another for 2 touch actions *note this will only occur on mobile
		WaitingForMovement, // the user has pressed 2 fingers down and we are waiting to see what gesture is performed
		DragingCamera, // the user has one finger down/clicked and is moving across the screen greater than some minimum amount
		ZoomingCamera, // The state that occurs when the user move two fingers in opposite directions to zoom in and out or if using mouse when using the wheel
		WaitingForNoInput // The final state where the user's input has been performed but the user is still touching the screen/ clicking
	};
	
	public CameraController camera;
	
	// General input system settings to be altered as seen fit
	protected float minimumTimeUntilMove = .25f; // the time in seconds that we will wait for the user to move before we interprate as a tap
	protected float minimumMovementDistance = 5; // the amount of posisiton change in a single touch gesture/click before it is considered a drag
	protected bool zoomEnabled = true; 
	protected float zoomEpsilon = 10;
	
	// some variables to represent zooming in/out
	protected bool ZOOM_IN  = true;
	protected bool ZOOM_OUT = false;
	
	protected ControlState currentState;
	protected AgeTransitionShader shader;
	protected LevelManager levelManager;
	
	public InputType(){
		camera = Camera.main.GetComponent<CameraController>();
		shader = GameObject.Find("FadePlane").GetComponent<AgeTransitionShader>();
		levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		ResetControlState();
	}
	
	#region abstract functions
	public abstract void HandleInput();
	
	public abstract void ResetControlState();
	#endregion
	
	#region InputEvents
	// ------------ These functions will be called when the given event occurs, put any code to be perform on the event in here 
	protected virtual void DragEvent(Vector2 inputChangeSinceLastTick){
		if (inputChangeSinceLastTick.y < 0) {
			if (levelManager.CanAgeTransition(Strings.ButtonAgeShiftUp)) {
				shader.DoFade(Strings.ButtonAgeShiftUp);
			}
		}
		else if (inputChangeSinceLastTick.y > 0) {
			if (levelManager.CanAgeTransition(Strings.ButtonAgeShiftDown)) {
				shader.DoFade (Strings.ButtonAgeShiftDown);
			}
		}
	}
	
	// called when a click/tap occurs
	protected void SingleClickEvent(Vector2 inputScreenPos){	
		DelegateClickForObjects(inputScreenPos);
	}
	
	// Tell the camera to zoom in or out
	protected void ZoomEvent(bool isZoomingIn){
		CameraController.Zoom(isZoomingIn);
	}	
	#endregion	
	
	#region Notifications
	protected void NotifyNoObjectClickedOn(Vector2 inputScreenPos){
		EventManager.instance.RiseOnClickedNoObjectEvent(new ClickPositionArgs(inputScreenPos));
	}
	
	protected void NotifyObjectClickedOn(Vector2 inputScreenPos){
		EventManager.instance.RiseOnClickEvent(new ClickPositionArgs(inputScreenPos));
	}
	#endregion
	
	private void DelegateClickForObjects(Vector2 inputScreenPos){
		Ray ray = Camera.main.ScreenPointToRay (inputScreenPos);
		
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit, 15) && !hit.transform.CompareTag("Untagged")) {
			NotifyObjectClickedOn(inputScreenPos);
		} else {
			NotifyNoObjectClickedOn(inputScreenPos);
		}
	}
	
	// will detect if the change in input position since the last tick is enough to be accepted as a drag
	protected bool DragMovementDetected(Vector2 movementChange){
		// if the x or y is greater than the minimum amount to be considered a drag then return true
		if (Mathf.Abs(movementChange.x) > minimumMovementDistance || Mathf.Abs(movementChange.y) > minimumMovementDistance){
			return (true);
		} else {
			return (false);
		}
	}
}
