using UnityEngine;
using System.Collections;

public class WalkToState : AbstractGoToState {
	public WalkToState(Character toControl) : base(toControl){}
	
	// If we are walking and we reach our goal then we should return to idle
	public override void OnGoalReached(){
		Debug.Log("While walking " + character.name + " reached goal. Returning to idle");
		character.EnterState(new IdleState(character));
	}
	
	public override void OnStuck(){
		character.EnterState(new IdleState(character));
	}
}
