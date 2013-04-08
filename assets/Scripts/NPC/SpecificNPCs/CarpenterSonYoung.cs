using UnityEngine;
using System.Collections;

public class CarpenterSonYoung : NPC {
	string whatToSay;
	
	protected override void Init() {
		base.Init();
		animationData = GetComponent<SmoothMoves.BoneAnimation>();
	}
	protected override EmotionState GetInitEmotionState(){
		if (this.GetDisposition() >= NPC.DISPOSITION_HIGH){
			return (new HighDispositionEmotionState(this));	
		}
		else if (this.GetDisposition() > NPC.DISPOSITION_LOW){
			return (new MediumDispositionEmotionState(this));
		} else {
			return (new LowDispositionEmotionState(this));
		}
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new Schedule(this);

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
		
	}
	
	protected override void ReactToTriggerCollision(EventManager EM, TriggerCollisionArgs triggerCollided){
		
	}
	
	// Low disposition state (doesn't like you)
	[System.Serializable]
	public class LowDispositionEmotionState : EmotionState{
		public string toolboxResponse = "Thanks for the tools, now I can build my treehouse.";
		public string toolboxDialogue = "Thanks for those tools!";
		public string toolboxGivenToFatherDialogue = "Why did you give him that?  Now, he's gonna make me build stuff instead of letting me go fishing!";
		public string fishingRodResponse = "Thanks!  Fishing is gonna be so much fun!  I'm glad  you're my best friend.";
		public string fishingRodDialogue = "Thanks now I can fish!";
		public string fishingRodGivenToFatherDialogue = "";
		public string appleStolenDialogue = "My dad said you stole our apple! You're not my friend anymore!";
		public int toolboxDispositionChange = 3;
		public int fishingRodGivenToFatherDispositionChange = -2;
		public int fishingRodDispositionChange = 3;
		public int appleStolenDispositionChange = -3;
		
		public LowDispositionEmotionState(NPC toControl) : base(toControl, "Hey!  We should play later!"){
			_acceptableItems.Add("ToolBox");
			_acceptableItems.Add("FishingRod");
		}
		
		// Pass the previous dialogue
		public LowDispositionEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			_acceptableItems.Add("ToolBox");
			_acceptableItems.Add("FishingRod");
		}
		
		public override void ReactToItemInteraction(string npc, GameObject item){
			if (item != null && npc == "CarpenterSon[SWITCH_SPRITES]"){
				switch (item.name){
					case "ToolBox":
						// Update chat
						_npcInState.UpdateChat(toolboxResponse);
						this._textToSay = toolboxDialogue;
					
						// Tree house built
					
						// Set new disposition
						_npcInState.UpdateDisposition(toolboxDispositionChange);
						break;
					default:
						// Give a default response about the item, but do not accept the item
						_npcInState.UpdateChat(DefaultDialogueYoung.getDialogue(Strings.DialogueAngry));
						break;
				}
			}
			
			// React to item interactions with the Carpenter
			if (item != null && npc == "Carpenter[SWITCH_SPRITES]"){
				switch (item.name){
					case "ToolBox":
						// Change default dialogue
						this._textToSay = toolboxGivenToFatherDialogue;
						
						// Set new disposition
						_npcInState.UpdateDisposition(toolboxDispositionChange);
						break;
					case "FishingRod":
						// Change default dialogue
						this._textToSay = fishingRodGivenToFatherDialogue;
						
						// Set new disposition
						_npcInState.UpdateDisposition(fishingRodGivenToFatherDispositionChange);
						break;
					default:
						break;
				}
			}
			
			// Check if the disposition has changed to a different emotion state
			UpdateEmotionState();
		}
		
		public override void ReactToChoiceInteraction(string npc, string choice){
			
		}
		
		public override void ReactToEnviromentInteraction(string npc, string enviromentAction){
			
		}
		
		public override void ReactToItemPickedUp(GameObject item){
			if(item.name == "Apple[Carpenter]"){	
				// If Carpenter does NOT let you take the apple
					// Change default dialogue
					this._textToSay = appleStolenDialogue;
					
					// Set new disposition
					_npcInState.UpdateDisposition(appleStolenDispositionChange);
			}
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
		public string toolboxResponse = "Thanks for the tools, now I can build my treehouse.";
		public string toolboxDialogue = "Thanks for those tools!";
		public string toolboxGivenToFatherDialogue = "Why did you give him that?  Now, he's gonna make me build stuff instead of letting me go fishing!";
		public string fishingRodResponse = "Thanks!  Fishing is gonna be so much fun!  I'm glad  you're my best friend.";
		public string fishingRodDialogue = "Thanks now I can fish!";
		public string fishingRodGivenToFatherDialogue = "";
		public string appleStolenDialogue = "My dad said you stole our apple! You're not my friend anymore!";
		public int toolboxDispositionChange = 3;
		public int fishingRodGivenToFatherDispositionChange = -2;
		public int fishingRodDispositionChange = 3;
		public int appleStolenDispositionChange = -3;
		
		public MediumDispositionEmotionState(NPC toControl) : base(toControl, "Hey!  We should play later!"){
			_acceptableItems.Add("ToolBox");
			_acceptableItems.Add("FishingRod");
		}
		
		// Pass the previous dialogue
		public MediumDispositionEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			_acceptableItems.Add("ToolBox");
			_acceptableItems.Add("FishingRod");
		}
		
		public override void ReactToItemInteraction(string npc, GameObject item){
			if (item != null && npc == "CarpenterSon[SWITCH_SPRITES]"){
				switch (item.name){
					case "ToolBox":
						// Update chat
						_npcInState.UpdateChat(toolboxResponse);
						this._textToSay = toolboxDialogue;
					
						// Tree house built
					
						// Set new disposition
						_npcInState.UpdateDisposition(toolboxDispositionChange);
						break;
					case "FishingRod":
						// Update chat
						_npcInState.UpdateChat(fishingRodResponse);
						this._textToSay = fishingRodDialogue;
					
						// Make Fisherman
					
						// Set new disposition
						_npcInState.UpdateDisposition(fishingRodDispositionChange);
						break;
					default:
						// Give a default response about the item, but do not accept the item
						_npcInState.UpdateChat(DefaultDialogueYoung.getDialogue(Strings.DialogueAmbivalent));
						break;
				}
			}
			
			// React to item interactions with the Carpenter
			if (item != null && npc == "Carpenter[SWITCH_SPRITES]"){
				switch (item.name){
					case "ToolBox":
						// Change default dialogue
						this._textToSay = toolboxGivenToFatherDialogue;
						
						// Set new disposition
						_npcInState.UpdateDisposition(toolboxDispositionChange);
						break;
					case "FishingRod":
						// Change default dialogue
						this._textToSay = fishingRodGivenToFatherDialogue;
						
						// Set new disposition
						_npcInState.UpdateDisposition(fishingRodGivenToFatherDispositionChange);
						break;
					default:
						break;
				}
			}
			
			// Check if the disposition has changed to a different emotion state
			UpdateEmotionState();
		}
		
		public override void ReactToChoiceInteraction(string npc, string choice){
			
		}
		
		public override void ReactToEnviromentInteraction(string npc, string enviromentAction){
			
		}
		
		public override void ReactToItemPickedUp(GameObject item){
			if(item.name == "Apple[Carpenter]"){	
				// If Carpenter does NOT let you take the apple
					// Change default dialogue
					this._textToSay = appleStolenDialogue;
					
					// Set new disposition
					_npcInState.UpdateDisposition(appleStolenDispositionChange);
			}
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
		public string toolboxResponse = "Thanks for the tools, now I can build my treehouse.";
		public string toolboxDialogue = "Thanks for those tools!";
		public string toolboxGivenToFatherDialogue = "Why did you give him that?  Now, he's gonna make me build stuff instead of letting me go fishing!";
		public string fishingRodResponse = "Thanks!  Fishing is gonna be so much fun!  I'm glad  you're my best friend.";
		public string fishingRodDialogue = "Thanks now I can fish!";
		public string fishingRodGivenToFatherDialogue = "";
		public int toolboxDispositionChange = 3;
		public int fishingRodGivenToFatherDispositionChange = -2;
		public int fishingRodDispositionChange = 3;
		public int appleStolenDispositionChange = -3;
		
		public HighDispositionEmotionState(NPC toControl) : base(toControl, "Hey!  We should play later!"){
			_acceptableItems.Add("ToolBox");
			_acceptableItems.Add("FishingRod");
		}
		
		// Pass the previous dialogue
		public HighDispositionEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			_acceptableItems.Add("ToolBox");
			_acceptableItems.Add("FishingRod");
		}
		
		public override void ReactToItemInteraction(string npc, GameObject item){
			if (item != null && npc == "CarpenterSon[SWITCH_SPRITES]"){
				switch (item.name){
					case "ToolBox":
						// Update chat
						_npcInState.UpdateChat(toolboxResponse);
						this._textToSay = toolboxDialogue;
					
						// Tree house built
					
						// Set new disposition
						_npcInState.UpdateDisposition(toolboxDispositionChange);
						break;
					case "FishingRod":
						// Update chat
						_npcInState.UpdateChat(fishingRodResponse);
						this._textToSay = fishingRodDialogue;
					
						// Make Fisherman
					
						// Set new disposition
						_npcInState.UpdateDisposition(fishingRodDispositionChange);
						break;
					default:
						// Give a default response about the item, but do not accept the item
						_npcInState.UpdateChat(DefaultDialogueYoung.getDialogue(Strings.DialogueAmbivalent));
						break;
				}
			}
			
			// React to item interactions with the Carpenter
			if (item != null && npc == "Carpenter[SWITCH_SPRITES]"){
				switch (item.name){
					case "ToolBox":
						// Change default dialogue
						this._textToSay = toolboxGivenToFatherDialogue;
						
						// Set new disposition
						_npcInState.UpdateDisposition(toolboxDispositionChange);
						break;
					case "FishingRod":
						// Change default dialogue
						this._textToSay = fishingRodGivenToFatherDialogue;
						
						// Set new disposition
						_npcInState.UpdateDisposition(fishingRodGivenToFatherDispositionChange);
						break;
					default:
						break;
				}
			}
			
			// Check if the disposition has changed to a different emotion state
			UpdateEmotionState();
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
	
	// Put special state cases here
}


