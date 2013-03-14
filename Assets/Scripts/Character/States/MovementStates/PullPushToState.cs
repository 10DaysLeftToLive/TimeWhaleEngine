using UnityEngine;
using System.Collections;

public class PullPushToState : WalkToState {
	public PullPushToState(Character toControl) : base(toControl){	}
	
	// If we are pushig/pulling and we reach our goal then we should go to idle while grabbing
	public override void OnGoalReached(){
		Debug.Log("While pushing/pulling " + character.name + " reached goal. Returning to grab idle");
		character.EnterState(new GrabIdleState(character));
	}
}
