using UnityEngine;
using System.Collections;

public class FishState : AbstractState {
	public FishState(Character toControl) : base(toControl){ 
	}
	
	public override void Update(){
	}
	
	public override void OnEnter(){
		character.LookRight();
		character.PlayAnimation(Strings.animation_fish);
	}
	
	public override void OnExit(){
	}
}