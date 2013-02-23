using UnityEngine;
using System.Collections;
using SmoothMoves;

public abstract class Character : PauseObject {
	protected State currentState;
	private static float RIGHT = 1;
	private static float LEFT = -1;
	
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
	
	public void LookRight(){
		this.transform.localScale = new Vector3(RIGHT, 1, 1);
	}
	
	public void LookLeft(){
		this.transform.localScale = new Vector3(LEFT, 1, 1);
	}
	
	public void ForceChangeToState(State newState){
		// TODO need to enter the correct idle state the change to the new one.
		//EnterState 
	}
}
