using UnityEngine;
using System.Collections;

/// <summary>
/// MusicianMiddle specific scripting values
/// </summary>
public class MusicianMiddle : NPC {
	protected override void Init() {
		id = NPCIDs.MUSICIAN;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "I am very disapointed by my son's actions, would you be a dear and make sure he stays out of trouble?"));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}

	protected override void SetUpSchedules() {
		
	}
	
	
	#region EmotionStates
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState{
	
		Choice reassureMusician = new Choice("Sure, I will look after him.", "Thanks!  I appreciate your consideration.");
		Choice distressMusician = new Choice("He is your kid, watch over him yourself.", "How Rude!  Leave me alone then");
		
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
		
		}
		
		public override void UpdateEmotionState(){
			
		}
	
	}
	#endregion
	#endregion
}
