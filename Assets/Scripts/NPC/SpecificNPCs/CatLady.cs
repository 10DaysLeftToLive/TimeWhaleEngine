using UnityEngine;
using System.Collections;

public class CatLady : NPC {
	protected override void ReactToItemInteraction(string npc, string item){
		Debug.Log(name + " is reacting to " + npc + " getting " + item);
	}
	
	protected override void ReactToChoiceInteraction(string npc, string choice){
		Debug.Log(name + " is reacting to player making choice " + choice + " with " + npc);
	}
	
	protected override string GetWhatToSay(){
		return (this.name + " says hai! I am feeling " + npcDisposition);
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new Schedule(this);
		
		Task standAround = new Task(new IdleState(this));
		
		schedule.Add(standAround);
		
		return (schedule);
	}
	
	protected override void LeftButtonCallback(){
		Debug.Log(this.name + " left callback");
		// TODO? this is for a chat dialoge
		EventManager.instance.RiseOnNPCInteractionEvent(new NPCItemInteraction(this.gameObject, player.Inventory.GetItem().name));
		
	}
	
	protected override void RightButtonCallback(){
		Debug.Log(this.name + " right callback");
		GameObject item = player.Inventory.GetItem();
		DoReaction(item);
	}
	
	protected override void DoReaction(GameObject itemToReactTo){
		if (itemToReactTo != null){
			Debug.Log(name + " is reacting to: " + itemToReactTo.name);
			switch (itemToReactTo.tag){
				case "GoldenGear":
					UpdateDisposition(10);
					break;
				default:
					break;
			}
			player.Inventory.DisableHeldItem();
		}
	}
}
