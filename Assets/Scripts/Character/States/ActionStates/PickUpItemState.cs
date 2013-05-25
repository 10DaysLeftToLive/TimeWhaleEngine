using UnityEngine;
using System.Collections;

/// <summary>
/// Pick up item state. Will grab a given item and put it itno the player's invetory
/// Will also set out a flag with the name of the item being picked up
/// </summary>
public class PickUpItemState : AbstractState {
	GameObject _toPickUp;
	
	public PickUpItemState(Character toControl, GameObject toPickUp) : base(toControl){
		_toPickUp = toPickUp;
	}
	
	public override void Update(){
		if (character is NPC){
			character.EnterState(new MarkTaskDone(character));
		} else {
			character.EnterState(new IdleState(character));
			((Player) character).Inventory.PickUpObject(_toPickUp);
		}
	}
	
	public override void OnEnter(){
		DebugManager.instance.Log(character.name + ": PickUpItemState Enter to pickup " + _toPickUp.name, "State", character.name);
	}
	
	public override void OnExit(){
		DebugManager.instance.Log(character.name + ": PickUpItemState Exit", "State", character.name);
		FlagManager.instance.SetFlag(_toPickUp.name);
		// Shoot off event for having picked up item
		EventManager.instance.RiseOnPlayerPickupEvent(new PickUpStateArgs(_toPickUp));
	}
	
	private void OnPickUpItem(EventManager Em, PickUpItemState pickedUpItem){
		
	}
}