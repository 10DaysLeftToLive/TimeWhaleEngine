using UnityEngine;
using System.Collections;

public class CarpenterYoung : NPC {
	string whatToSay;
	
	protected override void Init() {
		base.Init();
		animationData = GetComponent<SmoothMoves.BoneAnimation>();
	}
	protected override EmotionState GetInitEmotionState(){
		if (this.GetDisposition() >= NPC.DISPOSITION_HIGH){
			return (new CarpenterYoungHighDispositionEmotionState(this));	
		}
		else if (this.GetDisposition() > NPC.DISPOSITION_LOW){
			return (new CarpenterYoungMediumDispositionEmotionState(this));
		} else {
			return (new CarpenterYoungLowDispositionEmotionState(this));
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
	
	// Low disposition state (doesn't like you)
	[System.Serializable]
	public class CarpenterYoungLowDispositionEmotionState : EmotionState{
		public string toolboxResponse = "Thanks for finding these for me. You know my father gave me these as my first set of tools...Now I'm going to pass these onto my son!";
		public string toolboxDialogue = "I'm excited for what my son will build with his tools!";
		public string toolboxGivenToSonDialogue = "Oh nice, you found my old tools. Now my son can start on that tree house.";
		public string fishingRodGivenToSonDialogue = "Well great, now I'll be too busy fishing my son out of the river to get any work done.";
		public int toolboxDispositionChange = 3;
		public int fishingRodGivenToSonDispositionChange = -2;
		
		public CarpenterYoungLowDispositionEmotionState(NPC toControl) : base(toControl, "[Make angry?] You can play with my son when he finishes building his treehouse. Now where did I place my old tools?"){
			_acceptableItems.Add("ToolBox");
		}
		
		// Pass the previous dialogue
		public CarpenterYoungLowDispositionEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			_acceptableItems.Add("ToolBox");
		}
		
		public override void ReactToItemInteraction(string npc, GameObject item){
			if (item != null && npc == "Carpenter[SWITCH_SPRITES]"){
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
			
			// React to item interactions with the Carpenter's son
			if (item != null && npc == "CarpenterSon[SWITCH_SPRITES]"){
				switch (item.name){
					case "ToolBox":
						// Change default dialogue
						this._textToSay = toolboxGivenToSonDialogue;
						
						// Set new disposition
						_npcInState.UpdateDisposition(toolboxDispositionChange);
						break;
					case "FishingRod":
						// Change default dialogue
						this._textToSay = fishingRodGivenToSonDialogue;
						
						// Set new disposition
						_npcInState.UpdateDisposition(fishingRodGivenToSonDispositionChange);
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
				_npcInState.currentEmotion = new CarpenterYoungAppleStolenEmotionState(_npcInState);	
			}
		}
		
		public override void UpdateEmotionState(){
			if (_npcInState.GetDisposition() >= NPC.DISPOSITION_HIGH){
				_npcInState.currentEmotion = new CarpenterYoungHighDispositionEmotionState(_npcInState);	
			}
			else if (_npcInState.GetDisposition() > NPC.DISPOSITION_LOW){
				_npcInState.currentEmotion = new CarpenterYoungMediumDispositionEmotionState(_npcInState);
			}
		}
	}
	
	// Medium disposition state
	[System.Serializable]
	public class CarpenterYoungMediumDispositionEmotionState : EmotionState{
		public string toolboxResponse = "Thanks for finding these for me. You know my father gave me these as my first set of tools...Now I'm going to pass these onto my son!";
		public string toolboxDialogue = "I'm excited for what my son will build with his tools!";
		public string fishingRodResponse = "Ah, I guess there isn't too much harm in it. I'll take him fishing later, but the tree house comes first.";
		public string fishingRodDialogue = "I wish he had built the fishing rod himself...";
		public string toolboxGivenToSonDialogue = "Oh nice, you found my old tools. Now my son can start on that tree house.";
		public string fishingRodGivenToSonDialogue = "Well great, now I'll be too busy fishing my son out of the river to get any work done.";
		public int toolboxDispositionChange = 3;
		public int fishingRodDispositionChange = 0;
		public int fishingRodGivenToSonDispositionChange = -1;
		
		public CarpenterYoungMediumDispositionEmotionState(NPC toControl) : base(toControl, "You can play with my son when he finishes building his treehouse. Now where did I place my old tools?"){
			_acceptableItems.Add("ToolBox");
			_acceptableItems.Add("FishingRod");
		}
		
		// Pass the previous dialogue
		public CarpenterYoungMediumDispositionEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			_acceptableItems.Add("ToolBox");
		}
		
		public override void ReactToItemInteraction(string npc, GameObject item){
			if (item != null && npc == "Carpenter[SWITCH_SPRITES]"){
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
					
						// Tree house built
					
						// Set new disposition
						_npcInState.UpdateDisposition(fishingRodDispositionChange);
						break;
					default:
						// Give a default response about the item, but do not accept the item
						_npcInState.UpdateChat(DefaultDialogueYoung.getDialogue(Strings.DialogueAmbivalent));
						break;
				}
			}
			
			// React to item interactions with the Carpenter's son
			if (item != null && npc == "CarpenterSon[SWITCH_SPRITES]"){
				switch (item.name){
					case "ToolBox":
						// Change default dialogue
						this._textToSay = toolboxGivenToSonDialogue;
						
						// Set new disposition
						_npcInState.UpdateDisposition(toolboxDispositionChange);
						break;
					case "FishingRod":
						// Change default dialogue
						this._textToSay = fishingRodGivenToSonDialogue;
						
						// Set new disposition
						_npcInState.UpdateDisposition(fishingRodGivenToSonDispositionChange);
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
			if (_npcInState.GetDisposition() >= NPC.DISPOSITION_HIGH){
				_npcInState.currentEmotion = new CarpenterYoungHighDispositionEmotionState(_npcInState);	
			}
			else if (_npcInState.GetDisposition() <= NPC.DISPOSITION_LOW){
				_npcInState.currentEmotion = new CarpenterYoungLowDispositionEmotionState(_npcInState);
			}
		}
	}
	
	// High disposition state
	[System.Serializable]
	public class CarpenterYoungHighDispositionEmotionState : EmotionState{
		public string toolboxResponse = "Thanks for finding these for me. You know my father gave me these as my first set of tools...Now I'm going to pass these onto my son!";
		public string toolboxDialogue = "I'm excited for what my son will build with his tools!";
		public string fishingRodResponse = "Ah, I guess there isn't too much harm in it. I'll take him fishing later, but the tree house comes first.";
		public string fishingRodDialogue = "I wish he had built the fishing rod himself...";
		public string toolboxGivenToSonDialogue = "Oh nice, you found my old tools. Now my son can start on that tree house.";
		public string fishingRodGivenToSonDialogue = "Well great, now I'll be too busy fishing my son out of the river to get any work done.";
		public int toolboxDispositionChange = 3;
		public int fishingRodDispositionChange = 0;
		public int fishingRodGivenToSonDispositionChange = -1;
		
		public CarpenterYoungHighDispositionEmotionState(NPC toControl) : base(toControl, "You can play with my son when he finishes building his treehouse. Now where did I place my old tools?"){
			_acceptableItems.Add("ToolBox");
			_acceptableItems.Add("FishingRod");
		}
		
		// Pass the previous dialogue
		public CarpenterYoungHighDispositionEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			_acceptableItems.Add("ToolBox");
		}
		
		public override void ReactToItemInteraction(string npc, GameObject item){
			if (item != null && npc == "Carpenter[SWITCH_SPRITES]"){
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
					
						// Tree house built
					
						// Set new disposition
						_npcInState.UpdateDisposition(fishingRodDispositionChange);
						break;
					default:
						// Give a default response about the item, but do not accept the item
						_npcInState.UpdateChat(DefaultDialogueYoung.getDialogue(Strings.DialogueAmbivalent));
						break;
				}
			}
			
			// React to item interactions with the Carpenter's son
			if (item != null && npc == "CarpenterSon[SWITCH_SPRITES]"){
				switch (item.name){
					case "ToolBox":
						// Change default dialogue
						this._textToSay = toolboxGivenToSonDialogue;
						
						// Set new disposition
						_npcInState.UpdateDisposition(toolboxDispositionChange);
						break;
					case "FishingRod":
						// Change default dialogue
						this._textToSay = fishingRodGivenToSonDialogue;
						
						// Set new disposition
						_npcInState.UpdateDisposition(fishingRodGivenToSonDispositionChange);
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
				_npcInState.currentEmotion = new CarpenterYoungMediumDispositionEmotionState(_npcInState);	
			}
			else if (_npcInState.GetDisposition() <= NPC.DISPOSITION_LOW){
				_npcInState.currentEmotion = new CarpenterYoungLowDispositionEmotionState(_npcInState);
			}
		}
	}
	
	// Put special state cases here
	public class CarpenterYoungAppleStolenEmotionState : EmotionState{
		public string appleStolenResponse = "That is my apple. Give it back!";
		public string appleStolenDialogue = "Don't get anywhere near our apple tree again, thief!";
		public string appleReturnedResponse = "Thanks.  I'm sure it was just a harmless mistake.";
		public string otherAppleReturnedResponse = "Thanks for at least replacing the apply you stole.";
		public string toolboxResponse = "Thanks for finding these for me. You know my father gave me these as my first set of tools...Now I'm going to pass these onto my son!";
		public string toolboxDialogue = "I'm excited for what my son will build with his tools!";
		public string fishingRodGivenToSonDialogue = "Don't get anywhere near our apple tree again thief, and stop trying to distract my son!";
		public int appleStolenDispositionChange = -3;
		public int appleReturnedDispositionChange = 3;
		public int toolboxDispositionChange = 3;
		public int fishingRodGivenToSonDispositionChange = -3;
		
		public CarpenterYoungAppleStolenEmotionState(NPC toControl) : base(toControl, "Don't get anywhere near our apple tree again, thief!"){
			_acceptableItems.Add("ToolBox");
			_acceptableItems.Add("Apple");
			_acceptableItems.Add("Apple[Carpenter]");
			
			// toggle chat
			//_npcInState.ToggleChat();
			_npcInState.OpenChat();
			
			// Update dialogue
			_npcInState.UpdateChat(appleStolenResponse);
			
			// Update the disposition for stealing the apple
			_npcInState.UpdateDisposition(appleStolenDispositionChange);
		}
		
		public override void ReactToItemInteraction(string npc, GameObject item){
			if (item != null && npc == "Carpenter[SWITCH_SPRITES]"){
				switch (item.name){
					case "ToolBox":
						// Update chat
						_npcInState.UpdateChat(toolboxResponse);
						this._textToSay = toolboxDialogue;
					
						// Tree house built
					
						// Set new disposition
						_npcInState.UpdateDisposition(toolboxDispositionChange);
						break;
					case "Apple[Carpenter]":
						// Update chat
						_npcInState.UpdateChat(appleReturnedResponse);
					
						// Tree house built
					
						// Set new disposition
						_npcInState.UpdateDisposition(appleReturnedDispositionChange);
					
						// Return to the correct emotion state
						ReturnToDispositionState();
						break;
					case "Apple":
						// Update chat
						_npcInState.UpdateChat(otherAppleReturnedResponse);
					
						// Tree house built
					
						// Set new disposition
						_npcInState.UpdateDisposition(appleReturnedDispositionChange);
					
						// Return to the correct emotion state
						ReturnToDispositionState();
						break;
					default:
						// Give a default response about the item, but do not accept the item
						_npcInState.UpdateChat(DefaultDialogueYoung.getDialogue(Strings.DialogueAmbivalent));
						break;
				}
			}
			
			// React to item interactions with the Carpenter's son
			if (item != null && npc == "CarpenterSon[SWITCH_SPRITES]"){
				switch (item.name){
					case "ToolBox":
						// Set new disposition
						_npcInState.UpdateDisposition(toolboxDispositionChange);
					
						// Return to the correct emotion state
						ReturnToDispositionState();
						break;
					case "FishingRod":
						// Change default dialogue
						this._textToSay = fishingRodGivenToSonDialogue;
						
						// Set new disposition
						_npcInState.UpdateDisposition(fishingRodGivenToSonDispositionChange);
						break;
					default:
						break;
				}
			}
			
			// Check if disposition is high enough for the carpenter to give you the apple and return to the correct emotion state
			UpdateEmotionState();
		}
		
		public override void ReactToChoiceInteraction(string npc, string choice){
			
		}
		
		public override void ReactToEnviromentInteraction(string npc, string enviromentAction){
			
		}
		
		public override void ReactToItemPickedUp(GameObject item){
			
		}
		
		public override void UpdateEmotionState(){
			if (_npcInState.GetDisposition() >= NPC.DISPOSITION_HIGH){
				_npcInState.currentEmotion = new CarpenterYoungHighDispositionEmotionState(_npcInState);	
			}
		}
		
		public void ReturnToDispositionState() {
			if (_npcInState.GetDisposition() >= NPC.DISPOSITION_HIGH){
				_npcInState.currentEmotion = new CarpenterYoungHighDispositionEmotionState(_npcInState);	
			}
			else if (_npcInState.GetDisposition() > NPC.DISPOSITION_LOW){
				_npcInState.currentEmotion = new CarpenterYoungMediumDispositionEmotionState(_npcInState);
			} else {
				_npcInState.currentEmotion = new CarpenterYoungLowDispositionEmotionState(_npcInState);
			}
		}
	}
}


