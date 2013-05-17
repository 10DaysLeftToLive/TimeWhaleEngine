using UnityEngine;
using System.Collections;

/// <summary>
/// FortuneTellerMiddle specific scripting values
/// </summary>
public class FortuneTellerMiddle : NPC {
	protected override void Init() {
		id = NPCIDs.FORTUNE_TELLER;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		Reaction appleReaction = new Reaction();
		//appleReaction.AddAction()
			
		/*Reaction enterHappy = new Reaction();
		enterHappy.AddAction(new ShowOneOffChatAction(this, "I think there's a good spot over here!"));
		enterHappy.AddAction(new NPCAddScheduleAction(this, moveMotherHappyState));
		flagReactions.Add (FlagStrings.EnterHappyState, enterHappy);*/
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Have an item for me to appraise?"));
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
	
	
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
		
		}
		
		public override void UpdateEmotionState(){
			
		}
	
	}
	#endregion
	#endregion
}
