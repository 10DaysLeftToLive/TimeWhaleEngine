using UnityEngine;
using System.Collections;

public class Mom : NPC {
	string whatToSay;
	
	protected override EmotionState GetInitEmotionState(){
		EmotionState warningState = new EmotionState("Stay safe and remember, don't go into the forest!");
		return (warningState);
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new Schedule(this);
		
		// Note this is hard coded. This is an example of how scheduling works now
		Vector3 currentPos = transform.position;
		
		currentPos.x = currentPos.x - 5;
		
		TimeTask standAroundForBit = new TimeTask(2, new IdleState(this));
		
		Task walkLeft = new Task(new MoveThenDoState(this, currentPos, new MarkTaskDone(this)));
		
		currentPos.x = currentPos.x + 10;
		
		Task walkRight = new Task(new MoveThenDoState(this, currentPos, new MarkTaskDone(this)));
		
		Task standAround = new Task(new IdleState(this));
		
		schedule.Add(standAroundForBit);
		schedule.Add(walkLeft);
		schedule.Add(walkRight);
		schedule.Add(standAround);
		
		return (schedule);
	}
	
	protected override void LeftButtonCallback(string choice){
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

