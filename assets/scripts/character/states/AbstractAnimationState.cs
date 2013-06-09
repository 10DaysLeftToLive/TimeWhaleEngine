using UnityEngine;
using System.Collections;

public class AbstractAnimationState : AbstractState {
	protected string _animationToPlay;
	// Use this for initialization
	public AbstractAnimationState(Character toControl, string animation) : base(toControl){ 
		_animationToPlay = animation;
	}
	
	public override void Update(){
	}
	
	public override void OnEnter(){
		character.PlayAnimation(_animationToPlay);
	}
	
	public override void OnExit(){
	}
}
