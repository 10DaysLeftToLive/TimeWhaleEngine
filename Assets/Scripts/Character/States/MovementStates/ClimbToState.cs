using UnityEngine;
using System.Collections;

public class ClimbToState : AbstractGoToState {
	public ClimbToState(Character toControl, Vector3 goal) : base(toControl, goal){}
	
	// If we are climbing and we reach our goal then we should go to idle while climbing
	protected override void OnGoalReached(){
		Debug.Log("While climbing " + character.name + " reached goal. Returning to climb idle");
		character.EnterState(new ClimbIdleState(character));
	}
}
