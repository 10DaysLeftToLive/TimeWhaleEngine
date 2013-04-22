using UnityEngine;
using System.Collections;

/*
 * MarkTaskDone.cs
 * 	Used with npc scheduling to mark that an npc completed its' task
 * 	Do not use with the player
 */
public class MarkTaskDone : AbstractState {
	public MarkTaskDone(Character toControl) : base(toControl){
	}
	
	public override void Update(){
	}
	
	public override void OnEnter(){
		Debug.Log(character.name + ": MarkTaskDone Enter");
		if (character is NPC) {
			Debug.Log("Was an npc");
			((NPC)character).NextTask();
		} else {
			character.EnterState(new IdleState(character));
		}
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": MarkTaskDone Exit");
	}
}