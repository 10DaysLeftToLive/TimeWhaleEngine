using UnityEngine;
using System.Collections;

/// <summary>
/// Carpenter Son young specific scripting values
/// </summary>
public class CarpenterSonYoung : NPC {
	protected override void Init() {
		id = NPCIDs.CARPENTER_SON;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "|||| Where in the world are those tools?"));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}

	protected override void SetUpSchedules(){
		
	}
	
	
	#region EmotionStates
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState{
	
		Choice giveToolsChoice = new Choice("Give Tools.", "|||| Thanks for helpping me out broseidon!");
		Reaction giveToolsReaction = new Reaction();
		
		Choice makeFishingRodChoice = new Choice("Make Fishing Rod.", "|||| Chyeah, I'm going for it G Skillet. Fish-E-I-AH");
		Reaction makeFishingRodReaction = new Reaction();
		
		Choice makeTreeHouseChoice = new Choice("Make TreeHouse.", "|||| Chu be right broski, I maka ze treehouz");
		Reaction makeTreeHouseReaction = new Reaction();
	
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			giveToolsReaction.AddAction(new NPCCallbackAction(GiveToolsToCarpenterSon));
		
			giveToolsReaction.AddAction(new ShowOneOffChatAction(NPCManager.instance.getNPC(StringsNPC.CarpenterYoung), 
				"Oh good you found my old tools! " +
				"Now if  you are to actually start becoming a great carpenter like my father and his before him then you need to start practicing on your own. " +
				"Why don't you start with a treehouse?"));	
			
			giveToolsReaction.AddAction(new NPCGiveItemAction(NPCManager.instance.getNPC(StringsNPC.CarpenterYoung),"apple"));
			
			_allChoiceReactions.Add(giveToolsChoice,new DispositionDependentReaction(giveToolsReaction));
		
		}
		
		public override void UpdateEmotionState(){
			
		}
	
		private void GiveToolsToCarpenterSon(){
			_allChoiceReactions.Remove(giveToolsChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Man I wish I could build a fishing rod. Everyone in the town has one...but I'm not good enough yet.");
			
			makeFishingRodReaction.AddAction(new NPCCallbackAction(TellToMakeFishingRod));
			makeTreeHouseReaction.AddAction(new NPCCallbackAction(TellToMakeTreeHouse));
			
			_allChoiceReactions.Add(makeFishingRodChoice, new DispositionDependentReaction(makeFishingRodReaction));
			_allChoiceReactions.Add(makeTreeHouseChoice, new DispositionDependentReaction(makeTreeHouseReaction));
			
			//Need carpenter to come back, to start scriptd schedule chat
			
		}
				
		private void TellToMakeFishingRod(){
			_allChoiceReactions.Remove(makeFishingRodChoice);
			_allChoiceReactions.Remove (makeTreeHouseChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("|||| MO FISH MO PROBLEMS");
		}
			
		private void TellToMakeTreeHouse(){
			_allChoiceReactions.Remove(makeFishingRodChoice);
			_allChoiceReactions.Remove (makeTreeHouseChoice);	
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("|||| MO TREEHOUZ MO PROBLEMS");
		}
	}
	#endregion
	#endregion
}
