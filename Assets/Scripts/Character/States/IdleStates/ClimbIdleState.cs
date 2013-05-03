using UnityEngine;
using System.Collections;

public class ClimbIdleState : IdleState {
	public ClimbIdleState(Character toControl) : base(toControl){}
	
	public override void Update(){
	}
	
	public override void OnEnter(){
		Debug.Log(character.name + ": ClimbIdleState Enter");
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": ClimbIdleState Exit");
	}
}