using UnityEngine;
using System.Collections;

public class PickUpItemState : AbstractState {
	GameObject _toPickUp;
	
	public PickUpItemState(Character toControl, GameObject toPickUp) : base(toControl){
		_toPickUp = toPickUp;
	}
	
	public override void Update(){
		Debug.Log(character.name + ": PickUpItemState Update");
		Debug.Log ("Player transform: " + character.animationData.mLocalTransform);
		character.Inventory.PickUpObject(_toPickUp);
		
		Debug.Log("PickupItem does " + (character.Inventory.HasItem() ? "" : "not") + "have an item");
		
		character.EnterState(new IdleState(character));
	}
	
	public override void OnEnter(){
		Debug.Log(character.name + ": PickUpItemState Enter to pickup " + _toPickUp.name);
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": PickUpItemState Exit");
	}
}