using UnityEngine;
using System.Collections;

public class PullPushToState : WalkToState {
	public PullPushToState(Character toControl, Vector3 goal) : base(toControl, goal){}
	
	// If we are climbing and we reach our goal then we should go to idle while climbing
	protected override void OnGoalReached(){
		Debug.Log("While pushing/pulling " + character.name + " reached goal. Returning to grab idle");
		character.EnterState(new ClimbIdleState(character));
	}
}
