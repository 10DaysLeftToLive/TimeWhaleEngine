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
	
	private bool followDefaultSchedule = true;
	private Schedule openningWaitingSchedule;
	//Carpenter Son Fishing Branch
	NPCConvoSchedule angryAtSonFishing, seaCaptainCarpenterSonTalk, angryAtSonWantingToFish;
	//Carpenter Son Default/Independent Branch
	NPCConvoSchedule angryAtSonBeingIndependent, endOfDayTalk;
	//Carpenter Son Carpenter Branch
	NPCConvoSchedule happyAtSonForBeingCarpenter, talkToSonAfterWhittle, talkToSonWithoutWhittle;
	
	Schedule afterAngryAtSonFishing, afterAngryAtSonIndependent, afterHappyForSonBeingACarpenter;
	protected override void SetFlagReactions() {		
		//This is where the Initial Base Conversation Happens.
		Reaction carpenterSonBecomesIndependent = new Reaction();
		carpenterSonBecomesIndependent.AddAction(new NPCAddScheduleAction(this, angryAtSonBeingIndependent));
		carpenterSonBecomesIndependent.AddAction(new NPCAddScheduleAction(this, afterAngryAtSonIndependent));
		flagReactions.Add(FlagStrings.CarpenterAndCarpenterSonIndependantConversation, carpenterSonBecomesIndependent);
		
		Reaction carpenterSonBecomesFishermen = new Reaction();
		carpenterSonBecomesFishermen.AddAction (new NPCAddScheduleAction(this, angryAtSonFishing));
		carpenterSonBecomesFishermen.AddAction (new NPCAddScheduleAction(this, afterAngryAtSonFishing));
		flagReactions.Add (FlagStrings.carpenterSonMakesFishingRod, carpenterSonBecomesFishermen);
		
		Reaction carpenterSonBecomesCarpenter = new Reaction();
		carpenterSonBecomesCarpenter.AddAction(new NPCAddScheduleAction(this, happyAtSonForBeingCarpenter));
		carpenterSonBecomesCarpenter.AddAction(new NPCAddScheduleAction(this, afterHappyForSonBeingACarpenter));
		carpenterSonBecomesCarpenter.AddAction(new NPCAddScheduleAction(this, talkToSonAfterWhittle));
		flagReactions.Add(FlagStrings.gaveToolsToCarpenterOrSon, carpenterSonBecomesCarpenter);
	}
	
	/*protected void IndepdentTalkActionDone() {
		if (afterAngryAtSonIndependent.IsComplete()) {
			FlagManager.instance.SetFlag(FlagStrings.carpenterSonIndependent);
		}
	}*/
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "This all started when he was a child, if only I had raised him better."));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (openningWaitingSchedule);
	}
	
	

	protected override void SetUpSchedules() {
		openningWaitingSchedule = new Schedule(this, Schedule.priorityEnum.DoNow);
		openningWaitingSchedule.Add(new TimeTask(300, new WaitTillPlayerCloseState(this, ref player)));
		Task nullFlag =  new TimeTask(0f, new IdleState(this));
		nullFlag.AddFlagToSet(FlagStrings.CarpenterAndCarpenterSonIndependantConversation);
		openningWaitingSchedule.Add(nullFlag);
		openningWaitingSchedule.AddFlagGroup("DefaultPath");
		//this.AddSchedule(openningWaitingSchedule);
		
		afterAngryAtSonIndependent = new Schedule(this, Schedule.priorityEnum.DoNow);
		Task SetMoveToWindmillFlag = new TimeTask(0f, new IdleState(this));
		SetMoveToWindmillFlag.AddFlagToSet(FlagStrings.carpenterSonTalkWithFatherMorning);
		afterAngryAtSonIndependent.Add(SetMoveToWindmillFlag);
		
		
//CONVERSATION SCHEDULE BUG!!!! PLEASE FIX		
		SetupCarpentrySchedules();		
		SetupDefualtSchedules();
		SeteupPrimaryDefaultSchedules();
//CONVERSATION SCHEDULE BUG!!!! PLEASE FIX		
	}

	#region Independent Branch
	private void SetupDefualtSchedules() {
		SetupPrimaryCarpentrySchedules();
		SetupPassiveDefaultSchedules();
	}
	
	private void SeteupPrimaryDefaultSchedules() {
		angryAtSonBeingIndependent = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.CarpenterSonMiddle), new MiddleCarpenterToSonDefaultScriptedConvo(), Schedule.priorityEnum.DoConvo); // CHANGE THIS CONVERSATION TO THE ONE WE WANT TO USE!
		angryAtSonBeingIndependent.SetCanNotInteractWithPlayer();
		//angryAtSonBeingIndependent.SetFlagOnComplete(FlagStrings.carpenterSonTalkWithFatherMorning);
		//AddSchedule(angryAtSonBeingIndependent);
	}
	
	private void SetupPassiveDefaultSchedules() {
	}
	#endregion
	
	#region Fisherman Branch
	private void SetupFishingSchedules() {
		angryAtSonFishing =  new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.CarpenterSonMiddle), new MiddleCarpenterToSonFishingScriptedConvo(), Schedule.priorityEnum.High); // CHANGE THIS CONVERSATION TO THE ONE WE WANT TO USE!
		angryAtSonFishing.SetCanNotInteractWithPlayer();
		
		afterAngryAtSonFishing = new Schedule(this, Schedule.priorityEnum.Medium);
		Task setOffStormOffFlag = new TimeTask(2.0f,new IdleState(this));
		setOffStormOffFlag.AddFlagToSet(FlagStrings.carpenterSonTalkWithFatherMorning);
		
		afterAngryAtSonFishing.Add(setOffStormOffFlag);
		this.AddSchedule(afterAngryAtSonFishing);
	}
	#endregion
	
	#region CarpentryBranch
	
	private void SetupCarpentrySchedules() {
		SetupPrimaryCarpentrySchedules();
		SetupPassiveCarpentrySchedules();
	}
	
	private void SetupPrimaryCarpentrySchedules() {
		happyAtSonForBeingCarpenter = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.CarpenterSonMiddle), 
			new MiddleCarpenterToSonCarpentryScriptedConvo(), Schedule.priorityEnum.High);
		happyAtSonForBeingCarpenter.SetCanNotInteractWithPlayer();
		happyAtSonForBeingCarpenter.SetFlagOnComplete(FlagStrings.carpenterSonTalkWithFatherMorning);
		
		afterHappyForSonBeingACarpenter = new Schedule(this, Schedule.priorityEnum.Medium);
		TimeTask stormOffToWindmill = new TimeTask(50f, new MoveState(this, MapLocations.WindmillMiddle));
		afterHappyForSonBeingACarpenter.Add(stormOffToWindmill);
		
		talkToSonAfterWhittle = new NPCConvoSchedule(this, 
			NPCManager.instance.getNPC(StringsNPC.CarpenterSonMiddle), new MiddleCarpenterToSonCarpentryScriptedConvo(), Schedule.priorityEnum.Low);
		talkToSonAfterWhittle.SetCanNotInteractWithPlayer();
		
		
//		AddSchedule(happyAtSonForBeingCarpenter);
//		AddSchedule(afterHappyForSonBeingACarpenter);
//		AddSchedule(talkToSonAfterWhittle);
		
		talkToSonWithoutWhittle = new NPCConvoSchedule(this,
			NPCManager.instance.getNPC(StringsNPC.CarpenterSonMiddle), new MiddleCarpenterToSonCarpentryScriptedConvo(), Schedule.priorityEnum.Low);
		talkToSonWithoutWhittle.SetCanNotInteractWithPlayer();
	}
	
	private void SetupPassiveCarpentrySchedules() {
		
	}
	#endregion
	
	
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
