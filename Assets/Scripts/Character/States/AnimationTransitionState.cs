using UnityEngine;
using System.Collections;
using SmoothMoves;

public class AnimationTransitionState : AbstractState {
	private string _transistionAnimation;
	private State _nextState;
	
	public AnimationTransitionState(Character toControl, State currentState, State newState) : base(toControl){
		_transistionAnimation = GetTransitionBetween(currentState, newState);
		_nextState = newState;
	}
	
	public override void Update(){
		Debug.Log(character.name + ": AnimationTransitionState Update");
		while (character.AnimationPlaying()){
			Debug.Log("Transition playing not doing anything.");
		}
	}
	
	public override void OnEnter(){
		Debug.Log(character.name + ": AnimationTransitionState Enter");
		character.ChangeAnimation(_transistionAnimation);
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": AnimationTransitionState Exit");
	}
	
	private string GetTransitionBetween(State currentState, State newState){
		return StateAnimationTransitionManager.instance.GetTransitionAnimation(currentState, newState);
	}
}