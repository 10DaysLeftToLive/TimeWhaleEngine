using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sister : NPC {	
	protected override EmotionState GetInitEmotionState(){
		//EmotionState warningState = new EmotionState(this, "Stay safe and remember, don't go into the forest!");
		return (new IntroEmotionState(this));
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
		Vector3 currentPos = transform.position;
		currentPos.x = currentPos.x + 50;
		Task walkRight = new Task(new MoveThenDoState(this, currentPos, new MarkTaskDone(this)));
		
		TimeTask standAroundForBit = new TimeTask(2, new IdleState(this));
		//Task standAround = new Task(new IdleState(this));
		
		schedule.Add(standAroundForBit);
		schedule.Add(walkRight);
		
		return(schedule);
	}
	
	protected override void LeftButtonCallback(string choice){
		Debug.Log(this.name + " left callback(" + choice + ")");
		EventManager.instance.RiseOnNPCInteractionEvent(new NPCChoiceInteraction(this.gameObject, choice));
		// TODO? this is for a chat dialoge
	}
	
	protected override void RightButtonCallback(){
		Debug.Log(this.name + " right callback");
		GameObject item = player.Inventory.GetItem();
		DoReaction(item);
	}
	
	protected override void DoReaction(GameObject itemToReactTo){
		/*if (itemToReactTo != null){
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
		}*/
	}
	
	public class IntroEmotionState : EmotionState{
		public IntroEmotionState(NPC toControl) : base(toControl, "Help me push this rock, I want to see what is on the other side!"){
			_acceptableItems.Add("Plushie");
			_acceptableItems.Add("Frisbee");
		}
		
		public bool toldOn = false;
		
		public override void ReactToItemInteraction(string npc, GameObject item){
			if (item != null && npc == "Sibling"){
				Debug.Log(npc + " is reacting to: ");
				switch (item.name){
					case "Plushie":
					Debug.Log("NPC: " +npc + " Item: " +item.name + "  in sister");
						_npcInState.UpdateChat("Thanks, you're super cool! Hey let's play later!");
						this._textToSay = "Let's play later!";
						break;
					default:
						break;
				}
			}
			if (item != null && npc == "Mom"){
				switch(item.name){
				case "Plushie":
					if (toldOn == false){
						this._textToSay = "Why'd you give the plushie to mom first?";
					}
					break;
				default:
					break;
				}
			}
		}
		
		public override void ReactToChoiceInteraction(string npc, string choice){
			if (npc == "Mom"){
			Debug.Log("sister is choice reacting to " + npc + " making choice " + choice);
				switch (choice){
				case "Tell on": Debug.Log("Told on"); 
					this._textToSay = "Go away. You told mom, I don't talk to traitors!";
					toldOn = true;
					_acceptableItems.Clear();
					break;
				default: break;
				}
			}
		}
		
		public override void ReactToEnviromentInteraction(string npc, string enviromentAction){
			
		}
	}
}
