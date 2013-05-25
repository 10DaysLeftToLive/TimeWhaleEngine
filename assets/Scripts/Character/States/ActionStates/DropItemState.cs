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
		((Player) character).Inventory.DropItem(character.GetFeet());
		
		character.EnterState(new MarkTaskDone(character));
	}
	
	public override void OnEnter(){
		DebugManager.instance.Log(character.name + ": DropItemState Enter to pickup", "State", character.name);
	}
	
	public override void OnExit(){
		DebugManager.instance.Log(character.name + ": DropItemState Exit", "State", character.name);
	}
}