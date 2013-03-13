using UnityEngine;
using System.Collections;

/*
 * MarkTaskDone.cs
 * 	Used with npc scheduling to mark that an npc completed its' task
 * 	Do not use with the player
 */
public class MarkTaskDone : AbstractState {
	public MarkTaskDone(NPC toControl) : base(toControl){
	}
	
	public override void Update(){
	}
	
	public override void OnEnter(){
		Debug.Log(character.name + ": MarkTaskDone Enter");
		((NPC)character).NextTask();
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": MarkTaskDone Exit");
	}
}