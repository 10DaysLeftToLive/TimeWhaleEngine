using UnityEngine;
using System.Collections;

public class IdleState : AbstractState {
	public IdleState(Character toControl) : base(toControl){ 
	}
	
	public override void Update(){
	}
	
	public override void OnEnter(){
		character.PlayAnimation(Strings.animation_stand);
		Debug.Log(character.name + ": IdleState Enter");
	}
	
	public override void Pause() {
		
	}
	
	public override void Resume() {
		
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": IdleState Exit");
		_isComplete = true;
	}
}