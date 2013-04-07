using UnityEngine;
using System.Collections;

public class MomYoung : NPC {
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
	
	protected override void ReactToTriggerCollision(EventManager EM, TriggerCollisionArgs triggerCollided){
		
	}
	
	public class MomIntroEmotionState : EmotionState{
		public MomIntroEmotionState(NPC toControl) : base(toControl, "Where is your sister?"){
			_choices.Add(new Choice("Tell on", "She's in big trouble! I'll deal with your sister..."));
			_choices.Add(new Choice("Lie to", "Ok, well make sure she's okay..."));
			_acceptableItems.Add("Apple");
			_acceptableItems.Add("Apple[Carpenter]");
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
					case "Apple[Carpenter]":
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
					_npcInState.UpdateChatButton();
					_choices.Clear();
					break;
				case "Lie to": Debug.Log("Lied to"); 
					this._textToSay = "Keep an eye out on your sister!";
					_acceptableItems.Add("Plushie");
					_npcInState.UpdateChatButton();
					_choices.Clear();
					break;
				default: break;
				}
			}
		}
		
		public override void ReactToEnviromentInteraction(string npc, string enviromentAction){
			
		}
	}
	
	// Low disposition state (doesn't like you)
	[System.Serializable]
	public class LowDispositionEmotionState : EmotionState{
		
		public LowDispositionEmotionState(NPC toControl) : base(toControl, "Where is your sister?"){
			
		}
		
		// Pass the previous dialogue
		public LowDispositionEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
		}
		
		public override void ReactToItemInteraction(string npc, GameObject item){
			
		}
		
		public override void ReactToChoiceInteraction(string npc, string choice){
			
		}
		
		public override void ReactToEnviromentInteraction(string npc, string enviromentAction){
			
		}
		
		public override void ReactToItemPickedUp(GameObject item){
			
		}
		
		public override void UpdateEmotionState(){
			if (_npcInState.GetDisposition() >= NPC.DISPOSITION_HIGH){
				_npcInState.currentEmotion = new HighDispositionEmotionState(_npcInState);	
			}
			else if (_npcInState.GetDisposition() > NPC.DISPOSITION_LOW){
				_npcInState.currentEmotion = new MediumDispositionEmotionState(_npcInState);
			}
		}
	}
	
	// Medium disposition state
	[System.Serializable]
	public class MediumDispositionEmotionState : EmotionState{
		
		public MediumDispositionEmotionState(NPC toControl) : base(toControl, "Where is your sister?"){
			
		}
		
		// Pass the previous dialogue
		public MediumDispositionEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
		}
		
		public override void ReactToItemInteraction(string npc, GameObject item){
			
		}
		
		public override void ReactToChoiceInteraction(string npc, string choice){
			
		}
		
		public override void ReactToEnviromentInteraction(string npc, string enviromentAction){
			
		}
		
		public override void ReactToItemPickedUp(GameObject item){
			
		}
		
		public override void UpdateEmotionState(){
			if (_npcInState.GetDisposition() >= NPC.DISPOSITION_HIGH){
				_npcInState.currentEmotion = new HighDispositionEmotionState(_npcInState);	
			}
			else if (_npcInState.GetDisposition() <= NPC.DISPOSITION_LOW){
				_npcInState.currentEmotion = new LowDispositionEmotionState(_npcInState);
			}
		}
	}
	
	// High disposition state
	[System.Serializable]
	public class HighDispositionEmotionState : EmotionState{
		
		public HighDispositionEmotionState(NPC toControl) : base(toControl, "Where is your sister?"){
			
		}
		
		// Pass the previous dialogue
		public HighDispositionEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
		}
		
		public override void ReactToItemInteraction(string npc, GameObject item){
			
		}
		
		public override void ReactToChoiceInteraction(string npc, string choice){
			
		}
		
		public override void ReactToEnviromentInteraction(string npc, string enviromentAction){
			
		}
		
		public override void ReactToItemPickedUp(GameObject item){
			
		}
		
		public override void UpdateEmotionState(){
			if (_npcInState.GetDisposition() < NPC.DISPOSITION_HIGH){
				_npcInState.currentEmotion = new MediumDispositionEmotionState(_npcInState);	
			}
			else if (_npcInState.GetDisposition() <= NPC.DISPOSITION_LOW){
				_npcInState.currentEmotion = new LowDispositionEmotionState(_npcInState);
			}
		}
	}
}

