using UnityEngine;
using System;
using System.Collections;
using SmoothMoves;

/*
 *  Character.cs
 * 		The base class for both player and NPC
 * 		This should not be edited for specific characters put any specific code in the children classes.
 */
public abstract class Character : PauseObject {
	#region Fields	
	private float LEFT = -1;
	private float RIGHT = 1;
	private static bool PAUSED = true;
	private static bool UPPAUSED = false;
	
	public bool SpriteLookingLeft;
	protected State currentState;
	
	public BoneAnimation animationData;
	#endregion
	
	public State State {
		get {return currentState;}
	}
	
	protected override void Awake() {
		base.Awake();
		currentState = new IdleState(this);
	}
	
	void Start () {
		EnterState(new IdleState(this));
		InitializeSpriteLookDirections();
		Init();
	}
	
	protected void InitializeSpriteLookDirections() {
		if (SpriteLookingLeft) {
			LEFT = 1;
			RIGHT = -1;
		}
	}
	
	protected abstract void Init();
	protected virtual void CharacterUpdate(){}
	
	protected override void UpdateObject(){
		currentState.Update();
		CharacterUpdate();
	}
	
	protected override void OnPause(){
		ToggleAnimationPlaying(PAUSED);
	}
	
	protected override void OnResume(){
		ToggleAnimationPlaying(UPPAUSED);
	}
	
	private void ToggleAnimationPlaying(bool isPaused){
		//int speed = (isPaused ? 0 : 1);
		// TODO by Brent
	}
	
	public void EnterState(State newState){		
		currentState.OnExit(); // Exit the current state
		currentState = newState; // Update the current state
		newState.OnEnter(); // Enter the new state
	}
	
	public void LookRight(){
		this.transform.localScale = new Vector3(RIGHT, 1, 1);
	}
	
	public void LookLeft(){
		this.transform.localScale = new Vector3(LEFT, 1, 1);
	}
	
	public void ForceChangeToState(State newState) {
		// TODO need to enter the correct idle state the change to the new one.
		EnterState(newState);
	}
	
	public void PlayAnimation(string animation){
		
		if (animationData.GetClipCount() > 0) {
			try {
				animationData.Play(animation);
			} catch (ArgumentOutOfRangeException e){
				Debug.LogError("Animation data " + animation + " does not exist for " + name + "\n" + e.StackTrace);
			}
		}
	}
	
	public Vector3 GetFeet(){
		Vector3 feetPos = this.transform.position;
		feetPos.y = this.transform.position.y - this.collider.bounds.size.y/2;
		return (feetPos);
	}
}