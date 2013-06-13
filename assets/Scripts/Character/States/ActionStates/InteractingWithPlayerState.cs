using UnityEngine;
using System.Collections;

public class InteractingWithPlayerState : AbstractState {
	private string _animationToPlay;
	
	public InteractingWithPlayerState(Character toControl) : base(toControl){
		_animationToPlay = Strings.animation_stand;
	}
	
	public InteractingWithPlayerState(Character toControl, string animationToPlay) : base(toControl){
		_animationToPlay = animationToPlay;
	}
	
	public override void Update(){
		
	}
	
	public override void OnEnter(){
		character.PlayAnimation(_animationToPlay);
	}
	
	public override void OnExit(){
		((NPC) character).chatingWithPlayer = false;
	}
}