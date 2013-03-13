using UnityEngine;
using System.Collections;

public class Mom : NPC {
	string whatToSay;
	
	protected override void ReactToItemInteraction(string npc, string item){
		Debug.Log(name + " is reacting to " + npc + " getting " + item);
	}
	
	protected override void ReactToChoiceInteraction(string npc, string choice){
		Debug.Log(name + " is reacting to player making choice " + choice + " with " + npc);
	}
	
	protected override string GetWhatToSay(){
		return (whatToSay); // TODO maybe a better way of this?
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new Schedule(this);
		
		// Note this is hard coded. This is an example of how scheduling works now
		Vector3 currentPos = transform.position;
		
		currentPos.x = currentPos.x - 5;
		
		Task walkLeft = new Task(new MoveState(this, currentPos));
		
		currentPos.x = currentPos.x + 10;
		
		TimeTask walkRight = new TimeTask(6000, new MoveState(this, currentPos));
		
		Task standAround = new Task(new IdleState(this));
		
		schedule.Add(walkLeft);
		schedule.Add(walkRight);
		schedule.Add(standAround);
		
		return (schedule);
	}
	
	protected override void LeftButtonCallback(){
		Debug.Log(this.name + " left callback");
		// TODO? this is for a chat dialoge
		EventManager.instance.RiseOnNPCInteractionEvent(new NPCChoiceInteraction(this.gameObject, "Default"));
	}
	
	protected override void RightButtonCallback(){
		Debug.Log(this.name + " right callback");
		GameObject item = player.Inventory.GetItem();
		DoReaction(item);
	}
	
	protected override void DoReaction(GameObject itemToReactTo){
		if (itemToReactTo != null){
			Debug.Log(name + " is reacting to: " + itemToReactTo.name);
			switch (itemToReactTo.name){
				case "Plushie":
					UpdateChat("Thanks kid.");
					break;
				case "Apple":
					UpdateChat("Perfect! These will work wonderfully!");
					break;
				default:
					break;
			}
			player.Inventory.DisableHeldItem();
		}
	}
}