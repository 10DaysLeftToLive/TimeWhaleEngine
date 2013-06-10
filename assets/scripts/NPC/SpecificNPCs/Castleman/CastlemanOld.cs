using UnityEngine;
using System.Collections;

/// <summary>
/// CastlemanOld specific scripting values
/// </summary>
public class CastlemanOld : NPC {
	protected override void Init() {
		id = NPCIDs.CASTLE_MAN;
		base.Init();
		this.SetCharacterPortrait(StringsNPC.Crazy);
	}
	
	protected override void SetFlagReactions(){
		Reaction castleMarriage = new Reaction();
		castleMarriage.AddAction(new NPCCallbackSetStringAction(MoveForMarriage, this, "castle"));
		flagReactions.Add(FlagStrings.CastleMarriage, castleMarriage);
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Hey... Want to be friends...?"));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}

	protected override void SetUpSchedules(){
		
	}
	
	protected void MoveForMarriage(NPC npc, string text){
		if (text == "castle"){
			this.transform.position = new Vector3(1,0+LevelManager.levelYOffSetFromCenter*2, this.transform.position.z);
		}
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
