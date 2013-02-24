using UnityEngine;
using System.Collections;

public class GrabIdleState : IdleState {
	public GrabIdleState(Character character) : base(character){}
	
	public override void Update(){
		Debug.Log(character.name + ": GrabIdleState Update");
	}
	
	public override void OnEnter(){
		Debug.Log(character.name + ": GrabIdleState Enter");
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": GrabIdleState Exit");
	}
}
