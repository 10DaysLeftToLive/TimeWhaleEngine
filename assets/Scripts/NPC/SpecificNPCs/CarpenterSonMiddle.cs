using UnityEngine;
using System.Collections;

public class CarpenterSonMiddle : NPC {
	string whatToSay;
	
	protected override void Init() {
		base.Init();
		animationData = GetComponent<SmoothMoves.BoneAnimation>();
	}
	protected override EmotionState GetInitEmotionState(){
		if (this.GetDisposition() >= NPC.DISPOSITION_HIGH){
			return (new CarpenterSonMiddleHighDispositionEmotionState(this));	
		}
		else if (this.GetDisposition() > NPC.DISPOSITION_LOW){
			return (new CarpenterSonMiddleMediumDispositionEmotionState(this));
		} else {
			return (new CarpenterSonMiddleLowDispositionEmotionState(this));
		}
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new Schedule(this);

		Task standAround = new Task(new IdleState(this));
		
		schedule.Add(standAround);
		
		return (schedule);
	}
	
	//********************* Carpenter ******************************//
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
	
	//****************************** Fisherman *****************************//
	// Low disposition state (doesn't like you)
	[System.Serializable]
	public class CarpenterSonFishermanMiddleLowDispositionEmotionState : EmotionState{
		
		public CarpenterSonFishermanMiddleLowDispositionEmotionState(NPC toControl) : base(toControl, "Hey!  We should play later!"){
			
		}
		
		// Pass the previous dialogue
		public CarpenterSonFishermanMiddleLowDispositionEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
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
	public class CarpenterSonFishermanMiddleMediumDispositionEmotionState : EmotionState{
		
		public CarpenterSonFishermanMiddleMediumDispositionEmotionState(NPC toControl) : base(toControl, "Hey!  We should play later!"){
			
		}
		
		// Pass the previous dialogue
		public CarpenterSonFishermanMiddleMediumDispositionEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
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
				_npcInState.currentEmotion = new CarpenterSonFishermanMiddleHighDispositionEmotionState(_npcInState);	
			}
			else if (_npcInState.GetDisposition() <= NPC.DISPOSITION_LOW){
				_npcInState.currentEmotion = new CarpenterSonFishermanMiddleLowDispositionEmotionState(_npcInState);
			}
		}
	}
	
	// High disposition state
	[System.Serializable]
	public class CarpenterSonFishermanMiddleHighDispositionEmotionState : EmotionState{
		
		public CarpenterSonFishermanMiddleHighDispositionEmotionState(NPC toControl) : base(toControl, "Hey!  We should play later!"){
			
		}
		
		// Pass the previous dialogue
		public CarpenterSonFishermanMiddleHighDispositionEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
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
				_npcInState.currentEmotion = new CarpenterSonFishermanMiddleMediumDispositionEmotionState(_npcInState);	
			}
			else if (_npcInState.GetDisposition() <= NPC.DISPOSITION_LOW){
				_npcInState.currentEmotion = new CarpenterSonFishermanMiddleLowDispositionEmotionState(_npcInState);
			}
		}
	}
	
	// Put special state cases here
}


