using UnityEngine;
using System.Collections;

public class IdleState : AbstractState {
	public IdleState(Character toControl) : base(toControl){}
	
	public override void Update(){
		if (Input.GetButtonDown(Strings.ButtonAgeShiftDown)){
			character.EnterState(new MoveState(character));
		}
		Debug.Log(character.name + ": IdleState Update");
	}
	
	public override void OnEnter(){
		Debug.Log(character.name + ": IdleState Enter");
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": IdleState Exit");
	}
}