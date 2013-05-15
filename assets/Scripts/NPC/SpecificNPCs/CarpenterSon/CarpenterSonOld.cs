using UnityEngine;
using System.Collections;

/// <summary>
/// Carpenter son old specific scripting values
/// </summary>
public class CarpenterSonOld : NPC {
	protected override void Init() {
		id = NPCIDs.CARPENTER_SON;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		Reaction reconcileReaction = new Reaction();
		reconcileReaction.AddAction(new NPCTeleportToAction(this, MapLocations.BaseOfPierOld));
		reconcileReaction.AddAction(new UpdateDefaultTextAction(this, "Best wind in years today, and with my father with me I'm sure I'll get my best catch yet."));
		flagReactions.Add(FlagStrings.carpenterSonReconcile, reconcileReaction);
	}
	 	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "My back aches, my arms are tired and I'm too tired. I wish I never got into this lousy carpentry business."));
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
