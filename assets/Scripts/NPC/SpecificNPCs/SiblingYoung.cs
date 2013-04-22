using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SiblingYoung : NPC {	
	protected override EmotionState GetInitEmotionState(){
		//EmotionState warningState = new EmotionState(this, "Stay safe and remember, don't go into the forest!");
		return (new IntroEmotionState(this));
	}
	
	protected override void SetFlagReactions(){
		
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
	
	public class IntroEmotionState : EmotionState{
		public IntroEmotionState(NPC toControl) : base(toControl, "Help me push this rock, I want to see what is on the other side!"){
			//_acceptableItems.Add("Plushie");
			//_acceptableItems.Add("Frisbee");
		}
		
		public bool toldOn = false;
		
		public override void ReactToItemInteraction(string npc, GameObject item){
			if (item != null && npc == "Sibling"){
				Debug.Log(npc + " is reacting to: ");
				switch (item.name){
					case "Plushie":
					Debug.Log("NPC: " +npc + " Item: " +item.name + "  in sister");
						//_npcInState.UpdateChat("Thanks, you're super cool! Hey let's play later!");
						this._defaultTextToSay = "Let's play later!";
						break;
					default:
						break;
				}
			}
			if (item != null && npc == "Mom"){
				switch(item.name){
				case "Plushie":
					if (toldOn == false){
						this._defaultTextToSay = "Why'd you give the plushie to Mom first?";
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
					this._defaultTextToSay = "Go away! You told Mom, I don't talk to traitors!";
					toldOn = true;
					//_acceptableItems.Clear();
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
	public class CarpenterSonMiddleLowDispositionEmotionState : EmotionState{
		
		public CarpenterSonMiddleLowDispositionEmotionState(NPC toControl) : base(toControl, "Hey!  We should play later!"){
			
		}
		
		// Pass the previous dialogue
		public CarpenterSonMiddleLowDispositionEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
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
				_npcInState.currentEmotion = new CarpenterSonMiddleHighDispositionEmotionState(_npcInState);	
			}
			else if (_npcInState.GetDisposition() > NPC.DISPOSITION_LOW){
				_npcInState.currentEmotion = new CarpenterSonMiddleMediumDispositionEmotionState(_npcInState);
			}
		}
	}
	
	// Medium disposition state
	[System.Serializable]
	public class CarpenterSonMiddleMediumDispositionEmotionState : EmotionState{
		
		public CarpenterSonMiddleMediumDispositionEmotionState(NPC toControl) : base(toControl, "Hey!  We should play later!"){
			
		}
		
		// Pass the previous dialogue
		public CarpenterSonMiddleMediumDispositionEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
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
				_npcInState.currentEmotion = new CarpenterSonMiddleHighDispositionEmotionState(_npcInState);	
			}
			else if (_npcInState.GetDisposition() <= NPC.DISPOSITION_LOW){
				_npcInState.currentEmotion = new CarpenterSonMiddleLowDispositionEmotionState(_npcInState);
			}
		}
	}
	
	// High disposition state
	[System.Serializable]
	public class CarpenterSonMiddleHighDispositionEmotionState : EmotionState{
		
		public CarpenterSonMiddleHighDispositionEmotionState(NPC toControl) : base(toControl, "Hey!  We should play later!"){
			
		}
		
		// Pass the previous dialogue
		public CarpenterSonMiddleHighDispositionEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
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
				_npcInState.currentEmotion = new CarpenterSonMiddleMediumDispositionEmotionState(_npcInState);	
			}
			else if (_npcInState.GetDisposition() <= NPC.DISPOSITION_LOW){
				_npcInState.currentEmotion = new CarpenterSonMiddleLowDispositionEmotionState(_npcInState);
			}
		}
	}
}
