using UnityEngine;
using System.Collections;

public class MenuTransition : TransitionEffect {
	public DragDirection directionToShow;
	
	
	protected override void Init(){
		PlaceEmitter(emitter, cameraMain, directionToShow);
	}
	
	protected override void OnDragEvent(EventManager EM, DragArgs dragInformation) {
		Vector2 inputChangeSinceLastTick = dragInformation.dragMagnitude;
		if (inputChangeSinceLastTick.y > 0 &&
			inputChangeSinceLastTick.magnitude > minimumDragDistance) {
		} else if (inputChangeSinceLastTick.y < 0 &&
			inputChangeSinceLastTick.magnitude > minimumDragDistance) {
		} 
	}
}
