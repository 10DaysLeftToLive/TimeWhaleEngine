using UnityEngine;
using System.Collections;

public class CarpenterSonMiddleTest : NPC {
	string whatToSay;
	
	protected override void SetFlagReactions(){
		Reaction testReaction = new Reaction();
		testReaction.AddAction(new NPCCallbackAction(TestCallback));
		flagReactions.Add("Eat pie", testReaction);
	}
			
	private void TestCallback(){
		Debug.Log("Doing npc test callback");			
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new TestEmotionState(this));
		
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
	
	public class TestEmotionState : EmotionState {
		public TestEmotionState(NPC npcToControl) : base(npcToControl, "This is a test"){
			Reaction testReaction = new Reaction();
			testReaction.AddAction(new UpdateNPCDispositionAction(npcToControl, 5));
			testReaction.AddAction(new NPCCallbackAction(TestCallback));
			
			Reaction highTestReaction = new Reaction();
			highTestReaction.AddAction(new UpdateNPCDispositionAction(npcToControl, -5));
			highTestReaction.AddAction(new NPCCallbackAction(TestCallbackHigh));
			
			Reaction lowTestReaction = new Reaction();
			lowTestReaction.AddAction(new NPCCallbackAction(TestCallbackLow));
			
			DispositionDependentReaction testOne = new DispositionDependentReaction(testReaction);
			testOne.SetHighReaction(highTestReaction);
			testOne.SetLowReaction(lowTestReaction);
			
			_allChoiceReactions.Add(new Choice("Test 1", "This was a test 1 reaction"), testOne);
		}
		
		public void TestCallback(){
			Debug.Log("Doing a callback");	
		}
		
		public void TestCallbackHigh(){
			Debug.Log("Doing a high callback");	
		}
		
		public void TestCallbackLow(){
			Debug.Log("Doing a low callback");	
		}
	}
	
	#region Carpenter Path
		#region Low disposition state (doesn't like you)
		[System.Serializable]
		public class CarpenterSonMiddleLowDispositionEmotionState : EmotionState{
			public CarpenterSonMiddleLowDispositionEmotionState(NPC toControl) : base(toControl, "Hey!  We should play later!"){}
			// Pass the previous dialogue
			public CarpenterSonMiddleLowDispositionEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){}
			
			public override void UpdateEmotionState(){
				if (_npcInState.GetDisposition() >= NPC.DISPOSITION_HIGH){
					//_npcInState.currentEmotion = new CarpenterSonMiddleHighDispositionEmotionState(_npcInState);	
				}
				else if (_npcInState.GetDisposition() > NPC.DISPOSITION_LOW){
					//_npcInState.currentEmotion = new CarpenterSonMiddleMediumDispositionEmotionState(_npcInState);
				}
			}
		}
		#endregion
		
		
		#region Medium disposition state
		[System.Serializable]
		public class CarpenterSonMiddleMediumDispositionEmotionState : EmotionState{
			public CarpenterSonMiddleMediumDispositionEmotionState(NPC toControl) : base(toControl, "Hey!  We should play later!"){}
			// Pass the previous dialogue
			public CarpenterSonMiddleMediumDispositionEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){}
			
			public override void UpdateEmotionState(){
				if (_npcInState.GetDisposition() >= NPC.DISPOSITION_HIGH){
					//_npcInState.currentEmotion = new CarpenterSonMiddleHighDispositionEmotionState(_npcInState);	
				}
				else if (_npcInState.GetDisposition() <= NPC.DISPOSITION_LOW){
					//_npcInState.currentEmotion = new CarpenterSonMiddleLowDispositionEmotionState(_npcInState);
				}
			}
		}
		#endregion
		
		
		#region High disposition state
		[System.Serializable]
		public class CarpenterSonMiddleHighDispositionEmotionState : EmotionState{
			public CarpenterSonMiddleHighDispositionEmotionState(NPC toControl) : base(toControl, "Hey!  We should play later!"){}
			// Pass the previous dialogue
			public CarpenterSonMiddleHighDispositionEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){}
			
			public override void UpdateEmotionState(){
				if (_npcInState.GetDisposition() < NPC.DISPOSITION_HIGH){
					//_npcInState.currentEmotion = new CarpenterSonMiddleMediumDispositionEmotionState(_npcInState);	
				}
				else if (_npcInState.GetDisposition() <= NPC.DISPOSITION_LOW){
					//_npcInState.currentEmotion = new CarpenterSonMiddleLowDispositionEmotionState(_npcInState);
				}
			}
		}
		#endregion
	#endregion
	
	#region Fisherman Path
		#region Low disposition state (doesn't like you)
		[System.Serializable]
		public class CarpenterSonFishermanMiddleLowDispositionEmotionState : EmotionState{
			public CarpenterSonFishermanMiddleLowDispositionEmotionState(NPC toControl) : base(toControl, "Hey!  We should play later!"){}
			// Pass the previous dialogue
			public CarpenterSonFishermanMiddleLowDispositionEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){}
			
			public override void UpdateEmotionState(){
				if (_npcInState.GetDisposition() >= NPC.DISPOSITION_HIGH){
					//_npcInState.currentEmotion = new CarpenterSonMiddleHighDispositionEmotionState(_npcInState);	
				}
				else if (_npcInState.GetDisposition() > NPC.DISPOSITION_LOW){
					//_npcInState.currentEmotion = new CarpenterSonMiddleMediumDispositionEmotionState(_npcInState);
				}
			}
		}
		#endregion
		
		
		#region Medium disposition state
		[System.Serializable]
		public class CarpenterSonFishermanMiddleMediumDispositionEmotionState : EmotionState{
			public CarpenterSonFishermanMiddleMediumDispositionEmotionState(NPC toControl) : base(toControl, "Hey!  We should play later!"){}
			// Pass the previous dialogue
			public CarpenterSonFishermanMiddleMediumDispositionEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){}
			
			public override void UpdateEmotionState(){
				if (_npcInState.GetDisposition() >= NPC.DISPOSITION_HIGH){
					//_npcInState.currentEmotion = new CarpenterSonFishermanMiddleHighDispositionEmotionState(_npcInState);	
				}
				else if (_npcInState.GetDisposition() <= NPC.DISPOSITION_LOW){
					//_npcInState.currentEmotion = new CarpenterSonFishermanMiddleLowDispositionEmotionState(_npcInState);
				}
			}
		}
		#endregion
	
	
		#region High disposition state
		[System.Serializable]
		public class CarpenterSonFishermanMiddleHighDispositionEmotionState : EmotionState{
			public CarpenterSonFishermanMiddleHighDispositionEmotionState(NPC toControl) : base(toControl, "Hey!  We should play later!"){}
			// Pass the previous dialogue
			public CarpenterSonFishermanMiddleHighDispositionEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){}
			
			public override void UpdateEmotionState(){
				if (_npcInState.GetDisposition() < NPC.DISPOSITION_HIGH){
					//_npcInState.currentEmotion = new CarpenterSonFishermanMiddleMediumDispositionEmotionState(_npcInState);	
				}
				else if (_npcInState.GetDisposition() <= NPC.DISPOSITION_LOW){
					//_npcInState.currentEmotion = new CarpenterSonFishermanMiddleLowDispositionEmotionState(_npcInState);
				}
			}
		}
		#endregion
	#endregion
	// Put special state cases here
}


