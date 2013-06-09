using UnityEngine;
using System.Collections;

/// <summary>
/// Pick up item state. Will grab a given item and put it into the player's invetory
/// Will also set out a flag with the name of the item being picked up
/// </summary>
public class PickUpItemState : PlayAnimationThenDoState {
	GameObject _toPickUp;

	public PickUpItemState(Character toControl, GameObject toPickUp) : base(toControl, Strings.animation_pickup) {
		_toPickUp = toPickUp;
	}
	
	public override void Update() {
		if (character is NPC){
			character.EnterState(new MarkTaskDone(character));
		}
		base.Update();
	}
	
	public override void OnEnter() {
		base.OnEnter();
		//character.PlayAnimation(Strings.animation_pickup);;
		DebugManager.instance.Log(character.name + 
			": PickUpItemState Enter to pickup " + _toPickUp.name, "State", character.name);
	}
	
	public override void OnExit() {
		DebugManager.instance.Log(character.name + ": PickUpItemState Exit", "State", character.name);
		FlagManager.instance.SetFlag(_toPickUp.name);
		((Player) character).Inventory.PickUpObject(_toPickUp);
		// Shoot off event for having picked up item
		EventManager.instance.RiseOnPlayerPickupEvent(new PickUpStateArgs(_toPickUp));
	}
}