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
	
	public AbstractGoToState(Character toControl){
		character = toControl;
	}
	
	public void Move(Vector3 moveDelta){
		Vector3 curentPosisiton = character.transform.position;
		character.transform.position = curentPosisiton + moveDelta;
	}
	
	public abstract void OnGoalReached();
	public abstract void OnStuck();
}
