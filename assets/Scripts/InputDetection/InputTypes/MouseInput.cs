using UnityEngine;
using System.Collections;

/*
 * MouseInput.cs
 * 	Responible for reading mouse input and calling correct events in parent class
 * 
 */

public class MouseInput : InputType {
	#region Fields
	private Vector3 clickPosition;
	private Vector3 deltaSinceDown;
	#endregion
	
	public MouseInput() : base(){}
	
	public override void HandleInput(){
		if (Input.GetAxis("Mouse ScrollWheel") > 0){
			ZoomEvent(ZOOM_IN);
		} else if (Input.GetAxis("Mouse ScrollWheel") < 0){
			ZoomEvent(ZOOM_OUT);
		}
	
		// if the user has not clicked then keep cheking for a click
		if (currentState == ControlState.WaitingForFirstInput){
			// if a click occurs then start waiting for movement
			if (Input.GetKey(KeyCode.Mouse0)) {
				currentState = ControlState.WaitingForNoInput;
				clickPosition = Input.mousePosition;
			}
		}
		
		if (currentState == ControlState.WaitingForNoInput){
			deltaSinceDown = Input.mousePosition - clickPosition;
			// if the mouse has moved over the threshold then consider it a drag
			if (DragMovementDetected(deltaSinceDown)) {
				currentState = ControlState.DragingCamera;
			} else if (!Input.GetKey(KeyCode.Mouse0)){ // if the mouse has been released or held for the minimum duration then count it as a click
				SingleClickEvent(Input.mousePosition);
				currentState = ControlState.WaitingForFirstInput;
			} else {
				currentState = ControlState.HoldingClick;
			}
		}
		
		if (currentState == ControlState.HoldingClick){
			if (!Input.GetKey(KeyCode.Mouse0)){
				currentState = ControlState.WaitingForFirstInput;
				OnHoldRelease();
			} else {
				OnHoldClick(Input.mousePosition);
			}
		}
		
		if (currentState == ControlState.DragingCamera){
			deltaSinceDown = Input.mousePosition - clickPosition;
			clickPosition = Input.mousePosition;
			
			// if the mouse is still down keep dragging the camera
			if (Input.GetKey(KeyCode.Mouse0)){
				DragEvent(deltaSinceDown);
			} else {
				currentState = ControlState.WaitingForFirstInput;
			}
		}
	}
	
	public override void ResetControlState() {
		currentState = ControlState.WaitingForFirstInput;
	}
}
