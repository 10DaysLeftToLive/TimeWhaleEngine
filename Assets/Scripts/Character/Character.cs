using UnityEngine;
using System.Collections;
using SmoothMoves;

public abstract class Character : PauseObject {
	private State currentState;
	
	void Start () {
		currentState = new IdleState(this);
		EnterState(new IdleState(this));
		Init();
	}
	
	protected abstract void Init();
	
	protected override void UpdateObject(){
		currentState.Update();
	}
	
	public void EnterState(State newState){		
		currentState.OnExit(); // Exit the current state
		newState.OnEnter(); // Enter the new state
		currentState = newState; // Update the current state
	}
	
	public bool AnimationPlaying(){
		return (true); //TODO
	}
	
	public void ChangeAnimation(string newAnimation){
		Debug.Log("Chaning animation to " + newAnimation);
		// TODO
	}
}
