using UnityEngine;
using System.Collections;

public class WalkToState : AbstractGoToState {
	public WalkToState(Character toControl, Vector3 goal) : base(toControl, goal){}
	
	// If we are walking and we reach our goal then we should return to idle
	protected override void OnGoalReached(){
		Debug.Log("While walking " + character.name + " reached goal. Returning to idle");
		character.EnterState(new IdleState(character));
	}
}
