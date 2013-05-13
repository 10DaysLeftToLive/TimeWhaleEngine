using UnityEngine;
using System.Collections;

/*
 * TouchInput.cs
 * 	Responible for reading touch input and calling correct events in parent class
 * 
 */

public class TouchInput : InputType {
	#region Fields
	private int[] fingerDown = new int[ 2 ];
	private Vector2[] fingerDownPosition = new Vector2[ 2 ];
	private int[] fingerDownFrame = new int[ 2 ];
	private float firstTouchTime;
	private int touchCount;
	private Vector3 deltaSinceDown;
	#endregion
	
	public TouchInput() : base(){}
	
	
	private int i;
    private Touch touch;
    private Touch[] theseTouches = Input.touches;
    
    private Touch touch0 = new Touch();
    private Touch touch1 = new Touch();
    private bool gotTouch0 = false;
    private bool gotTouch1 = false;   
	public override void HandleInput(){
		touchCount = Input.touchCount;
	    if ( touchCount == 0 ){
	        ResetControlState();
	    } else{
	        i = 0;
	        touch = new Touch();
	        theseTouches = Input.touches;
	        
	        Touch touch0 = new Touch();
	        Touch touch1 = new Touch();
	        gotTouch0 = false;
	        gotTouch1 = false;          
	        
	        // Check if we got the first finger down
	        if (currentState == ControlState.WaitingForFirstInput){
	            for (i = 0; i < touchCount; i++){
	                touch = theseTouches[ i ];
	
	                if (touch.phase != TouchPhase.Ended &&
	                    touch.phase != TouchPhase.Canceled ){
	                    currentState = ControlState.WaitingForSecondTouch;
	                    firstTouchTime = Time.time;
	                    fingerDown[ 0 ] = touch.fingerId;
	                    fingerDownPosition[ 0 ] = touch.position;
	                    fingerDownFrame[ 0 ] = Time.frameCount;
	                    break; // break out of the rest of the checks for efficiency
	                }
	            }
	        }
	        
	        // Wait to see if a second finger touches down. Otherwise, we will
	        // register this as a tap                                   
	        if ( currentState == ControlState.WaitingForSecondTouch ){
	            for ( i = 0; i < touchCount; i++ ){
	                touch = theseTouches[ i ];
	
	                if ( touch.phase != TouchPhase.Canceled ){
	                    if ( touchCount >= 2 && touch.fingerId != fingerDown[ 0 ] ){
	                        // If we got a second finger, then let's see what kind of 
	                        // movement occurs
	                        currentState = ControlState.WaitingForMovement;
	                        fingerDown[ 1 ] = touch.fingerId;
	                        fingerDownPosition[ 1 ] = touch.position;
	                        fingerDownFrame[ 1 ] = Time.frameCount;                                         
	                        break;
	                    } else if ( touchCount == 1 ) {
	                        deltaSinceDown = touch.position - fingerDownPosition[ 0 ];
	                        
	                        // if we are looking at the right finger
	                        if (touch.fingerId == fingerDown[ 0 ]) {
		                        // Either the finger is held down long enough to count
		                        // as a move or it is lifted, which is also a tap. 
		                        if (Time.time > firstTouchTime + minimumTimeUntilMove || 
		                            touch.phase == TouchPhase.Ended){
		                            SingleClickEvent(touch.position);
		                            currentState = ControlState.WaitingForNoInput;
		                            break;
		                        } else if (DragMovementDetected(deltaSinceDown)){ // else if the single touch has moved more than the minimum amount we take it to be a drag
		                        	currentState = ControlState.DragingCamera;
		                        	break;
		                        } else {
									currentState = ControlState.HoldingClick;
								}
		                    }                                           
	                    }
	                }
	            }
	        }
	        
	        // Now that we have two fingers down, let's see what kind of gesture is made                    
	        if ( currentState == ControlState.WaitingForMovement ) { 
	            // See if we still have both fingers    
	            for ( i = 0; i < touchCount; i++ ) {
	                touch = theseTouches[ i ];
	
	                if ( touch.phase == TouchPhase.Began ){
	                    if ( touch.fingerId == fingerDown[ 0 ] && 
	                    	 fingerDownFrame[ 0 ] == Time.frameCount )
	                        // We need to grab the first touch if this
	                        // is all in the same frame, so the control 
	                        // state doesn't reset.
	                        touch0 = touch;
	                        gotTouch0 = true;
	                } else if ( touch.fingerId != fingerDown[ 0 ] && 
	                			touch.fingerId != fingerDown[ 1 ] ){
	                    // We still have two fingers, but the second
	                    // finger was lifted and touched down again
	                    fingerDown[ 1 ] = touch.fingerId;
	                    touch1 = touch;
	                    gotTouch1 = true;
	                }
	                
	                if ( touch.phase == TouchPhase.Moved || 
	            	 touch.phase == TouchPhase.Stationary || 
	                 touch.phase == TouchPhase.Ended ) {
		                if ( touch.fingerId == fingerDown[ 0 ] ){
		                    touch0 = touch;
		                    gotTouch0 = true;
		                } else if ( touch.fingerId == fingerDown[ 1 ] ) {
		                    touch1 = touch;
		                    gotTouch1 = true;
		                }
		            }
	            }
	            
	            if ( gotTouch0 ){
		            if ( gotTouch1 ){
		                Vector2 originalVector = fingerDownPosition[ 1 ] - fingerDownPosition[ 0 ];
		                Vector3 currentVector = touch1.position - touch0.position;
		                
		                // If we are zooming
		                if ( currentState == ControlState.WaitingForMovement ){
		                    var deltaDistance = originalVector.magnitude - currentVector.magnitude;
		                    if ( Mathf.Abs( deltaDistance ) > zoomEpsilon ){
		                        // The distance between fingers has changed enough
		                        currentState = ControlState.ZoomingCamera;
		                    }
		                }               
		            }
		        } else {
		            // A finger was lifted, so let's just wait until we have no fingers
		            // before we reset to the origin state
		            currentState = ControlState.WaitingForNoInput;
		        }
	        }
	        
	        if (currentState == ControlState.DragingCamera){
	        	touch = theseTouches[ 0 ];
	        	
	        	if (touch.phase == TouchPhase.Ended){
	        		currentState = ControlState.WaitingForFirstInput;
	        	} else {
		       		deltaSinceDown = touch.position - fingerDownPosition[ 0 ];
		       		fingerDownPosition[ 0 ] = touch.position;
		       		// need to do negative in order to give the feeling of pushing the world underneath your finger
		       		DragEvent(deltaSinceDown);
		        }
	        }
	        
	        // Now that we are zooming the camera, let's keep
		    // feeding those changes until we no longer have two fingers
		    if ( currentState == ControlState.ZoomingCamera ){
		        for ( i = 0; i < touchCount; i++ ){
		            touch = theseTouches[ i ];
		
		            if ( touch.phase == TouchPhase.Moved || 
		            	 touch.phase == TouchPhase.Stationary || 
		            	 touch.phase == TouchPhase.Ended ){
		                if ( touch.fingerId == fingerDown[ 0 ] ){
		                    touch0 = touch;
		                    gotTouch0 = true;
		                } else if ( touch.fingerId == fingerDown[ 1 ] ){
		                    touch1 = touch;
		                    gotTouch1 = true;
		                }
		            }
		        }
		        
		        if ( gotTouch0 ){
		            if ( gotTouch1 ){
		                DetermineZoomingInOrOut( touch0, touch1 );
		            }
		        } else {
		            // A finger was lifted, so let's just wait until we have no fingers
		            // before we reset to the origin state
		            currentState = ControlState.WaitingForNoInput;
		        }
		    } 
			
			if (currentState == ControlState.HoldingClick){
				touch = theseTouches[ 0 ];
	        	
	        	if (touch.phase == TouchPhase.Ended){
	        		currentState = ControlState.WaitingForFirstInput;
					OnHoldRelease();
	        	} else {
					OnHoldClick(touch.position);
		        }
			}
    	}    
	}
	
	public override void ResetControlState() {
		currentState = ControlState.WaitingForFirstInput;	
		fingerDown[ 0 ] = -1;
		fingerDown[ 1 ] = -1;
	}
	
	protected override void DragEvent(Vector2 inputChangeSinceLastTick) {
		base.DragEvent(inputChangeSinceLastTick);
	}
	
	// calculates the distance between touches to determine if the gesture is to zoom in or out
	private void DetermineZoomingInOrOut(Touch touch0, Touch touch1){
		float touchDistance = ( touch1.position - touch0.position ).magnitude;
	    float lastTouchDistance = ( ( touch1.position - touch1.deltaPosition ) - ( touch0.position - touch0.deltaPosition ) ).magnitude;
	    float deltaPinch = touchDistance - lastTouchDistance; // calculate the change in distance between the fingers
	
		// if the change is negative then the fingers are closer together indicating to zoom in
		if (deltaPinch < 0) {
			ZoomEvent(ZOOM_IN);
		} else if (deltaPinch > 0) {
		 	ZoomEvent(ZOOM_OUT);
		}
	}
}
