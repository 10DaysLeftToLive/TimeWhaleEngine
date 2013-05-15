// Back and forth Dialogue with CarpenterSonYoung
// If interrupted, tells you he's busy right now, then continues
	// when finished, Carpenter Son walks away
	// Carpenter grunts about his son, first the tools are gone and now he starts talking about doing something other than Carpentry
	// Talks about disciplining his son, how could he not want to be a carpenter?
	
using UnityEngine;
using System.Collections;

public class CarpenterYoung : NPC {	
	protected override void Init() {
		id = NPCIDs.CARPENTER;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Sorry, my son can't play with you today. He's coming with me to learn the family trade."));
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
	/*
		
		bool choiceSureBool = true;
		bool choiceBusyBool = true;
		Choice giveToolsChoiceSure;
		Choice giveToolsChoiceBusy;
		int SureCounter = 0;
		int BusyCounter = 0;
		Reaction giveToolsReaction;
		Reaction changeTextReactionSure;
		Reaction changeTextReactionBusy;
		
		public string _carpenterText = "Alright, come back later if you still want to help";
		
		NPC carpenterRef;	*/	
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			/*carpenterRef = toControl;
			
			giveToolsChoiceSure = new Choice("Sure", "Alright, go find my tools then.");
			giveToolsChoiceBusy = new Choice("Busy", _carpenterText);
			giveToolsReaction = new Reaction();
			giveToolsReaction.AddAction(new NPCGiveItemAction(toControl, "apple"));
			giveToolsReaction.AddAction(new NPCCallbackAction(GiveToolsToCarpenter));
			giveToolsReaction.AddAction(new NPCTakeItemAction(toControl));
			
			changeTextReactionSure = new Reaction();
			changeTextReactionSure.AddAction(new NPCCallbackAction(updateTextSure));
			
			changeTextReactionBusy = new Reaction();
			changeTextReactionBusy.AddAction(new NPCCallbackAction(updateTextBusy));
			
			
			_allChoiceReactions.Add(giveToolsChoiceSure,new DispositionDependentReaction(changeTextReactionSure));
			_allChoiceReactions.Add(giveToolsChoiceBusy,new DispositionDependentReaction(changeTextReactionBusy));
			_allItemReactions.Add("toolbox", new DispositionDependentReaction(giveToolsReaction));*/
		}
/*
		private void GiveToolsToCarpenter() {
			
			if (choiceSureBool) {
				_allChoiceReactions.Remove(giveToolsChoiceSure);
			}
			
			if (choiceBusyBool) {
				_allChoiceReactions.Remove(giveToolsChoiceBusy);
			}
			SetDefaultText("Thank you for 'em tools, you rascal!");
			GUIManager.Instance.RefreshInteraction();
			carpenterRef.SetCharacterPortrait(StringsNPC.Happy);
			SetDefaultText("Thank you for them tools, you rascal!");
			
			//Need to walk away and come back later to check on son
		}
		
		private void updateTextSure() {
			SureCounter++;
			if (SureCounter == 1) {
				_allChoiceReactions.Remove(giveToolsChoiceSure);
				_allChoiceReactions.Remove(giveToolsChoiceBusy);
				GUIManager.Instance.RefreshInteraction();
				SetOnCloseInteractionReaction(new DispositionDependentReaction(changeTextReactionSure));
			}
			if (SureCounter == 3) {
				SetDefaultText("Any luck? They should be around here somewhere.");
				SetOnCloseInteractionReaction(new DispositionDependentReaction(changeTextReactionSure));
				
			}
			if (SureCounter == 4) {
				SetDefaultText("Go ask my son where he put 'em. He's the one who lost them.");
				SetOnCloseInteractionReaction(new DispositionDependentReaction(changeTextReactionSure));
				
			}
			if (SureCounter == 5) {
				SetDefaultText("I know you're helping, but I don't got all day neighbor kid.");
				SetOnCloseInteractionReaction(new DispositionDependentReaction(changeTextReactionSure));		
			}
			if (SureCounter == 6) {
				SetDefaultText("Guess I better start looking for the tools myself. Thanks for trying.");
				SetOnCloseInteractionReaction(new DispositionDependentReaction(changeTextReactionSure));		
			}	
			
			if (SureCounter == 7) {
				SetDefaultText("See ya later.");
				SetOnCloseInteractionReaction(new DispositionDependentReaction(changeTextReactionSure));		
			}
		}
		
		private void updateTextBusy() {
			BusyCounter++;
			if (BusyCounter == 1) {
				_carpenterText = "You're no help.";
				SetDefaultText("Back again? Going to help this time?");
			}
			if (BusyCounter == 2) {
				_carpenterText = "...";
				SetDefaultText("How about now neighbor kid? Or are you messing with me?");	
			}
			if (BusyCounter == 3) {
				_carpenterText = "I see how it is neighbor kid.";
				SetDefaultText("This a joke to you?");	
			}
			
			if (BusyCounter == 4) {
				_allChoiceReactions.Remove(giveToolsChoiceSure);
				_allChoiceReactions.Remove(giveToolsChoiceBusy);
				GUIManager.Instance.RefreshInteraction();
				SetDefaultText("Get lost.");	
			}
			
			if (BusyCounter >= 1 && BusyCounter <= 3) {
				_allChoiceReactions.Remove(giveToolsChoiceSure);
				_allChoiceReactions.Remove(giveToolsChoiceBusy);
				GUIManager.Instance.RefreshInteraction();
				giveToolsChoiceBusy = new Choice ("Busy", _carpenterText);
				_allChoiceReactions.Add (giveToolsChoiceBusy, new DispositionDependentReaction(changeTextReactionBusy));
				_allChoiceReactions.Add (giveToolsChoiceSure, new DispositionDependentReaction(changeTextReactionSure));
				//SetDefaultText(_carpenterText);
			}
		}	*/
	}
		#endregion
	#endregion
}
