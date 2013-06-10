using UnityEngine;
using System.Collections;

public class WalkToState : AbstractGoToState {
	public WalkToState(Character toControl) : base(toControl, Strings.animation_walk) {}
	
	public WalkToState(Character toControl, string walkAnimation) : base(toControl, walkAnimation) {}
	
	// If we are walking and we reach our goal then we should return to idle
	public override void OnGoalReached() {
		if (character is NPC){
			character.PlayAnimation(Strings.animation_stand);
			character.EnterState(new MarkTaskDone(character));
		} else {
			character.EnterState(new IdleState(character));
		}
	}
	
	public override void OnStuck(){
		if (character is NPC){
			character.PlayAnimation(Strings.animation_stand);
			character.EnterState(new MarkTaskDone(character));
		} else {
			character.EnterState(new IdleState(character));
		}
	}
}
