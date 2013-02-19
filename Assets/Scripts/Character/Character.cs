using UnityEngine;
using System.Collections;
using SmoothMoves;

public class Character : PauseObject {
	private State currentState;
	
	void Start () {
		currentState = new IdleState(this);
		EnterState(new IdleState(this));
		EventManager.instance.mOnClickOnObjectAwayFromPlayerEvent += new EventManager.mOnClickOnObjectAwayFromPlayerDelegate (OnClickToMove);
		EventManager.instance.mOnClickNoObjectEvent += new EventManager.mOnClickedNoObjectDelegate (OnClickToMove);
	}
	
	// We want to be able to switch to move at any state when the player clicks
	// Sudden transisitions will be handled by the state's OnExit()
	private void OnClickToMove (EventManager EM, ClickPositionArgs e){
		Debug.Log("OnClickToMove");
		Vector3 pos = Camera.main.ScreenToWorldPoint(e.position);
		pos.z = this.transform.position.z;
		
		EnterState(new MoveState(this, pos));
    }
	
	protected override void UpdateObject(){
		currentState.Update();
	}
	
	public void EnterState(State newState){		
		currentState.OnExit(); // Exit the current state
		newState.OnEnter(); // Enter the new state
		currentState = newState; // Update the current state
	}
}
