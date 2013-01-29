using UnityEngine;
using System.Collections;

public class MouseInput : InputType {
	private float firstClickTime;
	private Vector3 clickPosition;
	private Vector3 deltaSinceDown;
	
	public MouseInput() : base(){}
	
	public override void HandleInput(){
		if (Input.GetAxis("Mouse ScrollWheel") > 0){
			ZoomEvent(ZOOM_IN);
		} else if (Input.GetAxis("Mouse ScrollWheel") < 0){
			ZoomEvent(ZOOM_OUT);
		}
	
		// if the user has not clicked then keep cheking for a click
		if (state == ControlState.WaitingForFirstInput){
			// if a click occurs then start waiting for movement
			if (Input.GetKey(KeyCode.Mouse0)) {
				state = ControlState.WaitingForNoInput;
				firstClickTime = Time.time;
				clickPosition = Input.mousePosition;
			}
		}
		
		if (state == ControlState.WaitingForNoInput){
			deltaSinceDown = Input.mousePosition - clickPosition;
			// if the mouse has moved over the threshhold then consider it a drag
			if (DragMovementDetected(deltaSinceDown)) {
				state = ControlState.DragingCamera;
			} else if (!Input.GetKey(KeyCode.Mouse0)){ // if the mouse has been released or held for the minimum duration then count it as a click
				SingleClickEvent(Input.mousePosition);
				state = ControlState.WaitingForFirstInput;
			}
		}
		
		if (state == ControlState.DragingCamera){
			deltaSinceDown = Input.mousePosition - clickPosition;
			clickPosition = Input.mousePosition;
			
			// if the mouse is still down keep dragging the camera
			if (Input.GetKey(KeyCode.Mouse0)){
				DragEvent(deltaSinceDown);
			} else {
				state = ControlState.WaitingForFirstInput;
			}
		}
	}
	
	public override void ResetControlState() {
		state = ControlState.WaitingForFirstInput;
	}
}
