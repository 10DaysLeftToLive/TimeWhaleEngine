using UnityEngine;
using System.Collections;

public class GameAgeTransition : TransitionEffect {
	public LevelManager levelManager;
	public Player playerCharacter;
	
	protected override void OnDragEvent(EventManager EM, DragArgs dragInformation) {
		Vector2 inputChangeSinceLastTick = dragInformation.dragMagnitude;
		if (inputChangeSinceLastTick.y > 0 &&
			inputChangeSinceLastTick.x == 0 && inputChangeSinceLastTick.magnitude > minimumDragDistance) {
			OnDragUp();
		} else if (inputChangeSinceLastTick.y < 0 &&
			inputChangeSinceLastTick.x == 0 && inputChangeSinceLastTick.magnitude > minimumDragDistance) {
			OnDragDown();
		}
	}
	
	protected virtual void OnDragDown() {
		if (CanShift() && levelManager.CanAgeTransitionDown()) {
			if (directionFacing != DragDirection.Down){
				ToggleEmitter();
			}
			DoFade();
		}
	}
	
	protected virtual void OnDragUp() {
		if (CanShift() && levelManager.CanAgeTransitionUp()) {
			if (directionFacing != DragDirection.Up){
				ToggleEmitter();
			}
			DoFade();
		}
	}
	
	protected override void DoSwitchAction(){
		if (directionFacing == DragDirection.Up){
			levelManager.ShiftUpAge();
		} else {
			levelManager.ShiftDownAge();
		}
	}
	
	private bool CanShift(){
		return (playerCharacter.State != typeof(MoveState) && !isChanging);
	}
}
