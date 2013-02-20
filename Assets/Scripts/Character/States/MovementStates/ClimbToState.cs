using UnityEngine;
using System.Collections;

public class ClimbToState : AbstractGoToState {
	public ClimbToState(Character toControl) : base(toControl){}
	
	// If we are climbing and we reach our goal then we should go to idle while climbing
	public override void OnGoalReached(){
		Debug.Log("While climbing " + character.name + " reached goal. Returning to climb idle");
		character.EnterState(new ClimbIdleState(character));
	}
}
