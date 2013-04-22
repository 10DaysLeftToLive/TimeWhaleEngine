using UnityEngine;
using System.Collections;

public class InteractingWithPlayerState : AbstractState {
	public InteractingWithPlayerState(Character toControl) : base(toControl){
	}
	
	public override void Update(){
		
	}
	
	public override void OnEnter(){
		Debug.Log(character.name + ": InteractingWithPlayerState Enter");
		character.PlayAnimation(Strings.animation_stand);
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": InteractingWithPlayerState Exit");
	}
}