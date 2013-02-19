using UnityEngine;
using System.Collections;

public class MoveState : AbstractState {
	public MoveState(Character toControl) : base(toControl){}
	
	public override void Update(){
		Debug.Log(character.name + ": MoveState Update");
	}
	
	public override void OnEnter(){
		Debug.Log(character.name + ": MoveState Enter");
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": MoveState Exit");
	}
}
