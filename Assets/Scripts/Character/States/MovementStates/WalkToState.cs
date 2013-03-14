using UnityEngine;
using System.Collections;

public class WalkToState : AbstractGoToState {
	public WalkToState(Character toControl) : base(toControl, Strings.animation_walk){}
	
	// If we are walking and we reach our goal then we should return to idle
	public override void OnGoalReached(){
		Debug.Log("While walking " + character.name + " reached goal. Returning to idle");
		character.PlayAnimation(Strings.animation_stand);
		character.EnterState(new IdleState(character));
	}
	
	public override void OnStuck(){
		character.PlayAnimation(Strings.animation_stand);
		character.EnterState(new IdleState(character));
	}
}
