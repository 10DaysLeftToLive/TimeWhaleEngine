using UnityEngine;
using System.Collections;

public class PickUpItemState : AbstractState {
	GameObject _toPickUp;
	
	public PickUpItemState(Character toControl, GameObject toPickUp) : base(toControl){
		_toPickUp = toPickUp;
	}
	
	public override void Update(){
		Debug.Log(character.name + ": PickUpItemState Update");
		
		character.Inventory.PickUpObject(_toPickUp);
		
		character.EnterState(new IdleState(character));
	}
	
	public override void OnEnter(){
		Debug.Log(character.name + ": PickUpItemState Enter to pickup " + _toPickUp.name);
		//TODO Grab item
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": PickUpItemState Exit");
	}
}