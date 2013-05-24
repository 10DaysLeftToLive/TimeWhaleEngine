using UnityEngine;
using System.Collections;

public class InteractingWithPlayerState : AbstractState {
	public InteractingWithPlayerState(Character toControl) : base(toControl){
	}
	
	public override void Update(){
		
	}
	
	public override void OnEnter(){
		character.PlayAnimation(Strings.animation_stand);
	}
	
	public override void OnExit(){
		((NPC) character).chatingWithPlayer = false;
	}
}