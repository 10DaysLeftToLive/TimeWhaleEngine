using UnityEngine;
using System.Collections;

public class GrabMoveState : MoveState {
	
	public GrabMoveState(Character toControl, Vector3 goal) : base(toControl, goal) {}
	
	public override void OnStuck(){
		Debug.Log("Got stuck while pushing.");
		character.EnterState(new GrabIdleState(character));
	}
}
