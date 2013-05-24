using UnityEngine;
using System.Collections;

/// <summary>
/// FortuneTellerOld specific scripting values
/// </summary>
public class FortuneTellerOld : NPC {
	protected override void Init() {
		id = NPCIDs.FORTUNE_TELLER;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		#region Race To Carpenter House
		/*
		Reaction raceToCarpenterHouse = new Reaction();
		raceToCarpenterHouse.AddAction(new NPCEmotionUpdateAction(this, new InitialEmotionState(this, "Careful, the Carpenter is really mean!")));
		raceToCarpenterHouse.AddAction(new ShowOneOffChatAction(this, "These are our neighbors!", 2f));
		raceToCarpenterHouse.AddAction(new NPCAddScheduleAction(this, carpenterRaceSchedule));
		flagReactions.Add(FlagStrings.RunToCarpenter, raceToCarpenterHouse);
		*/
		#endregion
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Have the choices in your past affected your present and influenced your future?"));
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
