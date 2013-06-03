using UnityEngine;
using System.Collections;

/// <summary>
/// CarpenterMiddle specific scripting values
/// </summary>
public class CarpenterMiddle : NPC {	
	protected override void Init() {
		id = NPCIDs.CARPENTER;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		Reaction gaveFlagReaction = new Reaction();
		gaveFlagReaction.AddAction(new NPCAllRemoveScheduleAction(FlagStrings.gaveFishingRodToCarpenterSon));
		gaveFlagReaction.AddAction(new NPCAddScheduleAction(this, angryAtSonFishing));
		gaveFlagReaction.AddAction(new NPCAddScheduleAction(this, afterAngryAtSonFishing));
		flagReactions.Add(FlagStrings.gaveFishingRodToCarpenterSon, gaveFlagReaction);
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "This all started when he was a child, if only I had raised him better."));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}
	
	private Schedule openningWaitingSchedule;
	NPCConvoSchedule angryAtSonFishing;
	Schedule afterAngryAtSonFishing;

	protected override void SetUpSchedules(){
		openningWaitingSchedule = new Schedule(this, Schedule.priorityEnum.DoNow);
		openningWaitingSchedule.Add(new TimeTask(30, new WaitTillPlayerCloseState(this, ref player)));
		this.AddSchedule(openningWaitingSchedule);
		
//CONVERSATION SCHEDULE BUG!!!! PLEASE FIX		
		NPCConvoSchedule angryAtSonDefault =  new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.CarpenterSonMiddle), new MiddleCarpenterToSonDefaultScriptedConvo(), Schedule.priorityEnum.High); // CHANGE THIS CONVERSATION TO THE ONE WE WANT TO USE!
		angryAtSonDefault.SetCanNotInteractWithPlayer();
		angryAtSonDefault.AddFlagGroup(FlagStrings.gaveFishingRodToCarpenterSon);
		this.AddSchedule(angryAtSonDefault);

		angryAtSonFishing =  new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.CarpenterSonMiddle), new MiddleCarpenterToSonFishingScriptedConvo(), Schedule.priorityEnum.High); // CHANGE THIS CONVERSATION TO THE ONE WE WANT TO USE!
		angryAtSonFishing.SetCanNotInteractWithPlayer();		
//CONVERSATION SCHEDULE BUG!!!! PLEASE FIX		

		afterAngryAtSonFishing = new Schedule(this, Schedule.priorityEnum.Medium);
		Task setOffStormOffFlag = (new TimeTask(1.0f,new IdleState(this)));
		setOffStormOffFlag.AddFlagToSet(FlagStrings.carpenterSonStormOff);
		
		afterAngryAtSonFishing.Add(new TimeTask(1.0f, new IdleState(this)));
		afterAngryAtSonFishing.Add(setOffStormOffFlag);
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
