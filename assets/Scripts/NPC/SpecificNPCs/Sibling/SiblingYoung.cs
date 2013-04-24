using UnityEngine;
using System.Collections;

/// <summary>
/// Sibling young specific scripting values
/// </summary>
public class SiblingYoung : NPC {
	protected override void Init() {
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		Reaction frogCrushing = new Reaction();
		frogCrushing.AddAction(new ShowOneOffChatAction(this, "OmG yOu KiLleD dAt fROg!1!"));
		frogCrushing.AddAction(new UpdateDefaultTextAction(this, "I can't belive you did that."));
		flagReactions.Add(FlagStrings.CrushFrog, frogCrushing); 
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Hai"));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new Schedule(this);
		
		Task standAround = new Task(new IdleState(this));
		
		schedule.Add(standAround);
		
		return (schedule);
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
