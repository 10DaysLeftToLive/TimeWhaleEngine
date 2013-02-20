using UnityEngine;
using System.Collections;

public class PickUpItemState : AbstractState {
	public PickUpItemState(Character toControl) : base(toControl){}
	
	public override void Update(){
		Debug.Log(character.name + ": PickUpItemState Update");
	}
	
	public override void OnEnter(){
		Debug.Log(character.name + ": PickUpItemState Enter");
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": PickUpItemState Exit");
	}
}