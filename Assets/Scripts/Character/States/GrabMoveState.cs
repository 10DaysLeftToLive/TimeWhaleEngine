using UnityEngine;
using System.Collections;

public class GrabMoveState : MoveState {
	
	public GrabMoveState(Character toControl, Vector3 goal) : base(toControl, goal) {}
	
	public override void OnEnter(){
		base.OnEnter();
		speed = 3f;
	}
	
	protected override bool PathSearch(Vector3 pos, Vector3 hitPos, float height){
		return (PathFinding.StartPathWithGrabable(pos, hitPos, height, character.AttachedObject.gameObject));
	}
	
	public override void OnStuck(){
		Debug.Log("Got stuck while pushing.");
		character.EnterState(new GrabIdleState(character));
	}
}
