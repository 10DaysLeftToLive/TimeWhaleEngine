using UnityEngine;
using System.Collections;

/*
 * DropItemState.cs
 * 	Called when the player should drop thier item
 */
public class DropItemState : AbstractState {	
	public DropItemState(Character toControl) : base(toControl){
	}
	
	public override void Update(){
		Debug.Log(character.name + ": DropItemState Update");
		
		character.Inventory.DropItem(character.GetFeet()); // TODO better implementation.
		
		character.EnterState(new IdleState(character));
	}
	
	public override void OnEnter(){
		Debug.Log(character.name + ": DropItemState Enter to pickup ");
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": DropItemState Exit");
	}
}