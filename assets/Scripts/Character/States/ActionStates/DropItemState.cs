using UnityEngine;
using System.Collections;

public class DropItemState : AbstractState {	
	public DropItemState(Character toControl) : base(toControl){
	}
	
	public override void Update(){
		Debug.Log(character.name + ": DropItemState Update");
		
		character.Inventory.DropItem(character.GetFeet());
		
		character.EnterState(new IdleState(character));
	}
	
	public override void OnEnter(){
		Debug.Log(character.name + ": DropItemState Enter to pickup ");
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": DropItemState Exit");
	}
}