using UnityEngine;
using System.Collections;

/// <summary>
/// LighthouseGirl Old specific scripting values
/// </summary>
public class LighthouseGirlOld : NPC {	
	protected override void SetFlagReactions(){
		Reaction castleMarriage = new Reaction();
		castleMarriage.AddAction(new NPCCallbackSetStringAction(MoveForMarriage, this, "castle"));
		//flagReactions.Add(FlagStrings.CastleMarriage, castleMarriage);
		flagReactions.Add(FlagStrings.MarryingCarpenter, castleMarriage);
		
		Reaction carpenterMarriage = new Reaction();
		carpenterMarriage.AddAction(new NPCCallbackSetStringAction(MoveForMarriage, this, "carpenter"));
		flagReactions.Add(FlagStrings.CastleMarriage, carpenterMarriage);
	}
	
	protected override EmotionState GetInitEmotionState(){
		//return (new InitialEmotionState(this, "|||| When did I get this old?"));
		return (new InitialEmotionState(this, "I wish I was never a bride."));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}

	protected override void SetUpSchedules(){
		
	}
	
	protected void MoveForMarriage(NPC npc, string text){
		if (text == "castle"){
			this.transform.position = new Vector3(0,0+LevelManager.levelYOffSetFromCenter*2, this.transform.position.z);
		}
		if (text == "carpenter"){
			this.transform.position = new Vector3(0,0+LevelManager.levelYOffSetFromCenter*2, this.transform.position.z);
		}
	}
	
	
	#region EmotionStates
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState{
	
	
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
		
		}
	}
	#endregion
	#endregion
}
