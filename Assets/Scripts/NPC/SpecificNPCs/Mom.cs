using UnityEngine;
using System.Collections;

public class Mom : NPC {
	string whatToSay;
	
	protected override EmotionState GetInitEmotionState(){
		return (new MomIntroEmotionState(this));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new Schedule(this);
		
		/*
		Vector3 currentPos = transform.position;
		
		currentPos.x = currentPos.x - 5;
		
		TimeTask standAroundForBit = new TimeTask(2, new IdleState(this));
		
		Task walkLeft = new Task(new MoveThenDoState(this, currentPos, new MarkTaskDone(this)));
		
		currentPos.x = currentPos.x + 10;
		
		Task walkRight = new Task(new MoveThenDoState(this, currentPos, new MarkTaskDone(this)));
		
		schedule.Add(standAroundForBit);
		schedule.Add(walkLeft);
		schedule.Add(walkRight);
		
		Task standAround = new Task(new IdleState(this));
		
		schedule.Add(standAround);*/
		
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
		/*if (itemToReactTo != null){
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
		}*/
	}
	
	public class MomIntroEmotionState : EmotionState{
		public MomIntroEmotionState(NPC toControl) : base(toControl, "Where is your sister?"){
			_choices.Add(new Choice("Tell on", "She's in big trouble! I'll deal with your sister..."));
			_choices.Add(new Choice("Lie to", "Ok, well make sure she's okay..."));
			_acceptableItems.Add("Apple");
		}
		public bool hasToldOn = false;
		
		public override void ReactToItemInteraction(string npc, GameObject item){
			if (item != null && npc == "Mom"){
				Debug.Log(npc + " is reacting to: ");
				switch (item.name){
					case "Plushie":
					Debug.Log("NPC: " +npc + " Item: " +item.name + " in mom");
					if (hasToldOn){
							_npcInState.UpdateChat("*sigh*  This stupid plushie again?  Its always causing trouble!");
					}
					else{
						_npcInState.UpdateChat("Oh, your sister was looking for this, I'll give it to her.");
					}
							break;
					case "Apple":
						_npcInState.UpdateChat("Perfect! These will work wonderfully!");
						break;
					default:
						break;
				}
			}
		}
		
		public override void ReactToChoiceInteraction(string npc, string choice){
			if (npc == "Mom"){
			Debug.Log("mom is choice reacting to " + npc + " making choice " + choice);
				switch (choice){
				case "Tell on": Debug.Log("Told on"); 
					this._textToSay = "Thank you for watching out for your sister!";
					hasToldOn = true;
					_acceptableItems.Add("Plushie");
					//_npcInState.OpenChat();
					_choices.Clear();
					break;
				case "Lie to": Debug.Log("Lied to"); 
					this._textToSay = "Keep an eye out on your sister!";
					_acceptableItems.Add("Plushie");
					//_npcInState.OpenChat();
					_choices.Clear();
					break;
				default: break;
				}
			}
		}
		
		public override void ReactToEnviromentInteraction(string npc, string enviromentAction){
			
		}
	}
}

