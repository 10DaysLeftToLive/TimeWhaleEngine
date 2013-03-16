using UnityEngine;
using System.Collections;

public class Mom : NPC {
	string whatToSay = "Stay safe and remember, don't go into the forest!";
	
	protected override EmotionState GetInitEmotionState(){
		return (new MomIntroEmotionState(this));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new Schedule(this);
		
		/* Note this is hard coded. This is an example of how scheduling works now
		Vector3 currentPos = transform.position;
		
		currentPos.x = currentPos.x - 5;
		
		TimeTask standAroundForBit = new TimeTask(2, new IdleState(this));
		
		Task walkLeft = new Task(new MoveThenDoState(this, currentPos, new MarkTaskDone(this)));
		
		currentPos.x = currentPos.x + 10;
		
		Task walkRight = new Task(new MoveThenDoState(this, currentPos, new MarkTaskDone(this)));
		
		schedule.Add(standAroundForBit);
		schedule.Add(walkLeft);
		schedule.Add(walkRight);
		
		*/
		Task standAround = new Task(new IdleState(this));
		
		schedule.Add(standAround);
		
		return (schedule);
	}
	
	protected override void LeftButtonCallback(string choice){
		Debug.Log(this.name + " left callback for choice " + choice);
		// TODO? this is for a chat dialoge
		EventManager.instance.RiseOnNPCInteractionEvent(new NPCChoiceInteraction(this.gameObject, choice));
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
	
	public class MomIntroEmotionState : EmotionState{
		public MomIntroEmotionState(NPC toControl) : base(toControl, "Where is your sister?"){
			_choices.Add(new Choice("Tell on", "She's in big trouble! I'll deal with your sister..."));
			_choices.Add(new Choice("Lie to", "Ok, well make sure she's okay..."));
		}
		
		public override void ReactToItemInteraction(string npc, string item){
			
		}
		
		public override void ReactToChoiceInteraction(string npc, string choice){
			if (npc == "Mom"){
			Debug.Log("mom is choice reacting to " + npc + " making choice " + choice);
				switch (choice){
				case "Tell on": Debug.Log("Told on"); 
					_choices.Clear();
					this._textToSay = "Thank you for watching out for your sister!";
					break;
				case "Lie to": Debug.Log("Lied to"); 
					_choices.Clear();
					this._textToSay = "Keep an eye out on your sister!";
					break;
				default: break;
				}
			}
		}
		
		public override void ReactToEnviromentInteraction(string npc, string enviromentAction){
			
		}
	}
}

