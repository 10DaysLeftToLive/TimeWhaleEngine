using UnityEngine;
using System.Collections;

public class IdleState : AbstractState {
	public IdleState(Character toControl) : base(toControl){ }
	
	public override void Update(){
	}
	
	public override void OnEnter(){
		Debug.Log(character.name + ": IdleState Enter");
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": IdleState Exit");
	}
}