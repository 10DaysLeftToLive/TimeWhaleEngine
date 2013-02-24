using UnityEngine;
using System.Collections;

/*
 *  This state will be active when the user clicks on something to interact with.
 *  We will then move to it and perform the specific action on it.
 */
public class MoveThenDoState : MoveState {
	private State _toDoWhenDone;
	
	public MoveThenDoState(Character toControl, Vector3 goal, State toDoWhenDone) : base(toControl, goal){
		_toDoWhenDone = toDoWhenDone;
	}
	
	public void OnGoalReached(){
		Debug.Log(character.name + " has reached the goal. Swithcing to what to do when done.");
		character.EnterState(_toDoWhenDone);
	}
}