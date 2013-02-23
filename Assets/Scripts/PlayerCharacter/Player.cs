using UnityEngine;
using System.Collections;

public class Player : Character {
	protected override void Init(){
		EventManager.instance.mOnClickOnObjectAwayFromPlayerEvent += new EventManager.mOnClickOnObjectAwayFromPlayerDelegate (OnClickToMove);
		EventManager.instance.mOnClickNoObjectEvent += new EventManager.mOnClickedNoObjectDelegate (OnClickToMove);
	}
	
	// We want to be able to switch to move at any state when the player clicks
	private void OnClickToMove (EventManager EM, ClickPositionArgs e){
		Vector3 pos = Camera.main.ScreenToWorldPoint(e.position);
		pos.z = this.transform.position.z;
		
		// Will need to be changed with later refactoring
		if (currentState.GetType() == typeof(IdleState) || currentState.GetType() == typeof(ClimbIdleState)){ // if we are idled or climbing idled
			EnterState(new MoveState(this, pos)); // move normaly
		} else if (currentState.GetType() == typeof(GrabIdleState)){ // if we are attached to an object 
			EnterState(new GrabMoveState(this, pos));
		} else if (currentState.GetType() == typeof(MoveState)){
			EnterState(new MoveState(this, pos));
		} else if (currentState.GetType() == typeof(GrabMoveState)){
			EnterState(new GrabMoveState(this, pos));
		}
    }
}
