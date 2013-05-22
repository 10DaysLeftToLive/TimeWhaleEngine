using UnityEngine;
using System.Collections;

/// <summary>
/// MusicianYoung specific scripting values
/// </summary>
public class MusicianYoung : NPC {
	protected override void Init() {
		id = NPCIDs.MUSICIAN;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		Choice MuteResponseChoice = new Choice("Is your son mute?", "Oh no!  He's just very shy!");
		Reaction MuteResponseReaction = new Reaction();
		MuteResponseReaction.AddAction(new NPCRemoveChoiceAction(this, MuteResponseChoice));
		MuteResponseReaction.AddAction(new UpdateCurrentTextAction(this, "Oh no!  He's  just very shy!"));
		Reaction IsHeMuteReaction = new Reaction();
		IsHeMuteReaction.AddAction(new NPCAddChoiceAction(this, MuteResponseChoice, new DispositionDependentReaction(MuteResponseReaction)));
		flagReactions.Add(FlagStrings.MusicianCommentOnSon, IsHeMuteReaction);
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Hi there! I'm sorry I can't play you a tune, my strings broke this morning."));
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
	
	
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, "Hi!  I'm the musician and that's my son over there.  We're new to the island."){
			
		
		}
		
		public override void UpdateEmotionState(){
			
		}
	
	}
	#endregion
	#endregion
}
