using UnityEngine;
using System.Collections;

public class FollowObjectState : MoveToObjectState {
	private bool isStanding = false;
	
	public FollowObjectState(Character toControl, GameObject toMoveTo) : base(toControl, toMoveTo){
    }
	
	public override void Update() {
		if (!Utils.InDistance(character.gameObject, _toMoveTo, DISTANCE_TO_STOP)) {
			if (isStanding) {
				character.PlayAnimation(Strings.animation_walk);
			}
			base.Update();
		} else {
			OnGoalReached();
		}
	}
	
	protected override void OnGoalReached(){
        character.PlayAnimation(Strings.animation_stand);
		isStanding = true;
    }
}