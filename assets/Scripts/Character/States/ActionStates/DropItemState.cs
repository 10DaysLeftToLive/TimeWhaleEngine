using UnityEngine;
using System.Collections;

/*
 * DropItemState.cs
 * 	Called when the player should drop thier item
 */
public class DropItemState : PlayAnimationThenDoState {	
	public DropItemState(Character toControl) : base(toControl, Strings.animation_pickup){
	}
	
	public override void Update() {
		base.Update();
	}
	
	public override void OnEnter(){
		base.OnEnter();
		if (character is NPC){
			character.EnterState(new MarkTaskDone(character));
		}
		DebugManager.instance.Log(character.name + ": DropItemState Enter to pickup", "State", character.name);
	}
	
	public override void OnExit(){
		((Player) character).Inventory.DropItem(character.GetFeet());
		
		DebugManager.instance.Log(character.name + ": DropItemState Exit", "State", character.name);
	}
}