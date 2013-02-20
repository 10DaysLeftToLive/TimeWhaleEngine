using UnityEngine;
using System.Collections;

/*
 * AbstractGoToState.cs
 * 	Base abstract class for moving the character to a point.
 * 	The way they should move and the animation to play will be completed in the children.
 */
public abstract class AbstractGoToState : GoToState {
	protected Character character; // The character that is in this state
	protected float speed = 5f;
	
	public AbstractGoToState(Character toControl, Vector3 goal){
		character = toControl;
	}
	
	public void Update(){
		
	}
	
	protected abstract void OnGoalReached();
	void OnStuck(){
		// This will happen when a path has been found and the character is moving on it,
		// but something has gotten in the way.
		Debug.Log("Something got in the way when moving.");
	}
}
