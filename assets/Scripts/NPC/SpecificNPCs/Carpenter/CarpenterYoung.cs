using UnityEngine;
using System.Collections;

/// <summary>
/// Carpenter young specific scripting values
/// </summary>
public class CarpenterYoung : NPC {	
	protected override void Init() {
		id = NPCIDs.CARPENTER;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "||||This boy..."));
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
	
		Choice giveToolsChoice;
		Reaction giveToolsReaction;
		NPC carpenterRef;
		
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			carpenterRef = toControl;
			giveToolsChoice = new Choice("Give Tools.", "Thanks for finding my tools instead of my lazy son.  Honestly he sometimes I think he doesn't care about our great legacy.");
			giveToolsReaction = new Reaction();
			giveToolsReaction.AddAction(new NPCCallbackAction(GiveToolsToCarpenter));
		
			giveToolsReaction.AddAction(new ShowOneOffChatAction(NPCManager.instance.getNPC(StringsNPC.CarpenterYoung), 
				 "Now if you are to actually start becoming a great carpenter like my father and his before " +
				 "him then you need to start practicing on your own. Why don't you start with a treehouse?"));	
			
			_allChoiceReactions.Add(giveToolsChoice,new DispositionDependentReaction(giveToolsReaction));
		
		}
		
		public override void UpdateEmotionState(){
			
		}
	
		private void GiveToolsToCarpenter(){
			_allChoiceReactions.Remove(giveToolsChoice);
			GUIManager.Instance.RefreshInteraction();
			carpenterRef.SetCharacterPortrait(StringsNPC.Happy);
			SetDefaultText("Thank you for them tools, you rascal!");
			
			//Need to walk away and come back later to check on son
		
		
		}
	}
	#endregion
	#endregion
}
