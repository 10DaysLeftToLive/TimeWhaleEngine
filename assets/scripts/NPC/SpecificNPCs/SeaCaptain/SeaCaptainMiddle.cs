using UnityEngine;
using System.Collections;

/// <summary>
/// SeaCaptainMiddle specific scripting values
/// </summary>
public class SeaCaptainMiddle : NPC {	
	protected override void Init() {
		id = NPCIDs.SEA_CAPTAIN;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		#region Fishing rod stolen
		Reaction fishingRodStolen = new Reaction();
		fishingRodStolen.AddAction(new NPCEmotionUpdateAction(this, new InitialEmotionState(this, "Don't forget to bring my fishing rod back.")));
		fishingRodStolen.AddAction(new ShowOneOffChatAction(this, "Make sure to bring that back in one piece", 2f));
		flagReactions.Add(FlagStrings.StolenFishingRod, fishingRodStolen);
		#endregion
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Ooooy! Good winds we got fer sailing. A pity me ship is in disrepair, but in the meantime there's treasure to be found!"));
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
