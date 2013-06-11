using UnityEngine;
using System.Collections;

public class MenuTransition : TransitionEffect {
	public DragDirection directionToShow;
	public TitleMenu titleMenu;
	
	protected override void Init(){
		PlaceEmitter(emitter, cameraMain, directionToShow);
	}
	
	protected override void OnDragEvent(EventManager EM, DragArgs dragInformation) {
		Vector2 inputChangeSinceLastTick = dragInformation.dragMagnitude;
		if (inputChangeSinceLastTick.y > 0 &&
			inputChangeSinceLastTick.magnitude > minimumDragDistance &&
			directionToShow == DragDirection.Up) {
			DoFade();
		} else if (inputChangeSinceLastTick.y < 0 &&
			inputChangeSinceLastTick.magnitude > minimumDragDistance &&
			directionToShow == DragDirection.Down) {
			DoFade();
		} 
	}
	
	protected override void DoSwitchAction(){
		Debug.Log("Action");
		titleMenu.TransitionToMainMenu();
	}
}
