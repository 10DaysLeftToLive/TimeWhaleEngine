using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sister : NPC {	
	protected override EmotionState GetInitEmotionState(){
		EmotionState warningState = new EmotionState("Stay safe and remember, don't go into the forest!");
		return (warningState);
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new Schedule(this);
		
		Task standAround = new Task(new IdleState(this));
		
		schedule.Add(standAround);
		
		return(schedule);
	}
	
	protected override void LeftButtonCallback(string choice){
		Debug.Log(this.name + " left callback(" + choice + ")");
		EventManager.instance.RiseOnNPCInteractionEvent(new NPCItemInteraction(this.gameObject, player.Inventory.GetItem().name));
		// TODO? this is for a chat dialoge
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
				case "Plushie":
					break;
				case "Frisbee":
					break;
				default:
					break;
			}
			player.Inventory.DisableHeldItem();
		}
	}
	
	private class IntroEmotionState : EmotionState{
		IntroEmotionState() : base("Help me push this rock, I want to see what is on the other side!"){
			List<Choice> _choices = new List<Choice>(); // no choices for this state
			List<string> _acceptableItems = new List<string>();
			_acceptableItems.Add("Plushie");
			_acceptableItems.Add("Frisbee");
		}
		
		public override void ReactToItemInteraction(string npc, string item){
			
		}
		
		public override void ReactToChoiceInteraction(string npc, string choice){
			
		}
		
		public override void ReactToEnviromentInteraction(string npc, string enviromentAction){
			
		}
	}
}
