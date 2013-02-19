using UnityEngine;
using System.Collections;
using SmoothMoves;

public class Character : PauseObject {
	private State currentState;
	
	void Start () {
		currentState = new IdleState(this);
		EnterState(new IdleState(this));
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
