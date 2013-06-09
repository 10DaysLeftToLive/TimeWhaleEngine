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
	bool builtChair = false;
	
	//private bool followDefaultSchedule = true;
	private Schedule openningWaitingSchedule;
	//Carpenter Son Fishing Branch
	NPCConvoSchedule angryAtSonFishing, seaCaptainCarpenterSonTalk, angryAtSonWantingToFish;
	//Carpenter Son Default/Independent Branch
	NPCConvoSchedule angryAtSonBeingIndependent, endOfDayTalk;
	//Carpenter Son Carpenter Branch
	NPCConvoSchedule happyAtSonForBeingCarpenter, talkToSonAfterWhittle, talkToSonWithoutWhittle;
	NPCConvoSchedule ConversationAboutRockingChair, ConversationAboutNotDoingAnything;
	Schedule afterAngryAtSonFishing, afterAngryAtSonIndependent, afterHappyForSonBeingACarpenter;
	Schedule AfterAngryAtSonFishing;
	Schedule MoveToCliffSide;
	Schedule AfterAcceptFishing;
	Schedule WorkOnWindmill;
	NPCConvoSchedule AcceptFishing;
	protected override void SetFlagReactions() {		
		//This is where the Initial Base Conversation Happens.
		Reaction carpenterSonBecomesIndependent = new Reaction();
		carpenterSonBecomesIndependent.AddAction(new NPCAddScheduleAction(this, angryAtSonBeingIndependent));
		carpenterSonBecomesIndependent.AddAction(new NPCAddScheduleAction(this, afterAngryAtSonIndependent));
		flagReactions.Add(FlagStrings.CarpenterAndCarpenterSonIndependantConversation, carpenterSonBecomesIndependent);
		#region Fishing
		Reaction StartFishingChain =  new Reaction();
		StartFishingChain.AddAction(new NPCAddScheduleAction(this, angryAtSonFishing));
		StartFishingChain.AddAction(new NPCAddScheduleAction(this, AfterAngryAtSonFishing));
		flagReactions.Add(FlagStrings.FishingConversation, StartFishingChain);
		
		Reaction MoveToCliff = new Reaction();
		MoveToCliff.AddAction(new NPCAddScheduleAction(this, MoveToCliffSide));
		flagReactions.Add(FlagStrings.AfterConversationAboutBuildingShip, MoveToCliff);
		
		Reaction FishingGo = new Reaction();
		FishingGo.AddAction(new ShowOneOffChatAction(this, "What's he doing? Is that carpentry!?"));
		FishingGo.AddAction(new NPCAddScheduleAction(this, AcceptFishing));
		FishingGo.AddAction(new NPCAddScheduleAction(this, AfterAcceptFishing));
		flagReactions.Add(FlagStrings.StartProudOfSonConversation , FishingGo);
		#endregion
		#region Carpentry
		Reaction HaveConversation = new Reaction();
		HaveConversation.AddAction(new NPCAddScheduleAction(this, happyAtSonForBeingCarpenter));
		HaveConversation.AddAction(new NPCAddScheduleAction(this, WorkOnWindmill));
		flagReactions.Add(FlagStrings.IntroConvoCarpentry, HaveConversation);
		
		
		Reaction setBuiltStuff = new Reaction();
		setBuiltStuff.AddAction(new NPCCallbackAction(SettingConversation));
		flagReactions.Add(FlagStrings.BuiltStuffForDad, setBuiltStuff);
		
		Reaction ReturnHomeReaction = new Reaction();
		ReturnHomeReaction.AddAction(new NPCCallbackAction(PrepareReturnedHomeConversation));
		flagReactions.Add(FlagStrings.CarpenterReturnedHome, ReturnHomeReaction);
		
		Reaction PerformConversationAboutNothing = new Reaction();
		PerformConversationAboutNothing.AddAction(new NPCAddScheduleAction(this, ConversationAboutNotDoingAnything));
		PerformConversationAboutNothing.AddAction(new NPCEmotionUpdateAction(this, new InitialEmotionState(this, "")));
		flagReactions.Add(FlagStrings.DidntBuildRockingChairConversation, PerformConversationAboutNothing);
		
		Reaction BuiltRockingChair = new Reaction();
		BuiltRockingChair.AddAction(new NPCAddScheduleAction(this, ConversationAboutRockingChair));
		BuiltRockingChair.AddAction(new NPCEmotionUpdateAction(this, new InitialEmotionState(this, "")));
		flagReactions.Add(FlagStrings.BuiltRockingChairTalk, BuiltRockingChair);
		#endregion
		/*Reaction carpenterSonBecomesFishermen = new Reaction();
		carpenterSonBecomesFishermen.AddAction (new NPCAddScheduleAction(this, angryAtSonFishing));
		//carpenterSonBecomesFishermen.AddAction (new NPCAddScheduleAction(this, afterAngryAtSonFishing));
		flagReactions.Add (FlagStrings.FishingConversation, carpenterSonBecomesFishermen);*/
		
		/*Reaction carpenterSonBecomesCarpenter = new Reaction();
		carpenterSonBecomesCarpenter.AddAction(new NPCAddScheduleAction(this, happyAtSonForBeingCarpenter));
		carpenterSonBecomesCarpenter.AddAction(new NPCAddScheduleAction(this, afterHappyForSonBeingACarpenter));
		carpenterSonBecomesCarpenter.AddAction(new NPCAddScheduleAction(this, talkToSonAfterWhittle));
		flagReactions.Add(FlagStrings.gaveToolsToCarpenterOrSon, carpenterSonBecomesCarpenter);*/
	}
	protected void PrepareReturnedHomeConversation(){
		if(builtChair == true){
			FlagManager.instance.SetFlag(FlagStrings.BuiltRockingChairTalk);
		}
		else{
			FlagManager.instance.SetFlag(FlagStrings.DidntBuildRockingChairConversation);
		}
	}
	protected void SettingConversation(){
			builtChair = true;
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
		Vector3 startingPosition = transform.position;
		startingPosition.y += LevelManager.levelYOffSetFromCenter;
		
		openningWaitingSchedule = new Schedule(this, Schedule.priorityEnum.Low);
		openningWaitingSchedule.Add(new TimeTask(5, new WaitTillPlayerCloseState(this, ref player)));
		Task nullFlag =  new TimeTask(0f, new IdleState(this));
		nullFlag.AddFlagToSet(FlagStrings.CarpenterAndCarpenterSonIndependantConversation);
		openningWaitingSchedule.Add(nullFlag);
		openningWaitingSchedule.AddFlagGroup("DefaultPath");
		
		angryAtSonBeingIndependent = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.CarpenterSonMiddle), new MiddleCarpenterToSonDefaultScriptedConvo(), Schedule.priorityEnum.Medium); // CHANGE THIS CONVERSATION TO THE ONE WE WANT TO USE!
		angryAtSonBeingIndependent.SetCanNotInteractWithPlayer();
		//this.AddSchedule(openningWaitingSchedule);
		
		afterAngryAtSonIndependent = new Schedule(this, Schedule.priorityEnum.Low);
		Task SetMoveToWindmillFlag = new TimeTask(0f, new IdleState(this));
		SetMoveToWindmillFlag.AddFlagToSet(FlagStrings.carpenterSonTalkWithFatherMorning);
		afterAngryAtSonIndependent.Add(SetMoveToWindmillFlag);
		
		#region Fishing
		angryAtSonFishing =  new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.CarpenterSonMiddle), new MiddleCarpenterToSonFishingScriptedConvo(), Schedule.priorityEnum.DoNow); // CHANGE THIS CONVERSATION TO THE ONE WE WANT TO USE!
		angryAtSonFishing.SetCanNotInteractWithPlayer();
		
		AfterAngryAtSonFishing = new Schedule(this, Schedule.priorityEnum.High);
		Task TriggerAfterFishingConvo =  new TimeTask(0f, new IdleState(this));
		TriggerAfterFishingConvo.AddFlagToSet(FlagStrings.CarpenterSonMovesToTheBeach);
		AfterAngryAtSonFishing.Add(TriggerAfterFishingConvo);
		
		MoveToCliffSide = new Schedule(this, Schedule.priorityEnum.High);
		MoveToCliffSide.Add(new Task(new MoveThenMarkDoneState(this, MapLocations.BeachCliffMiddle)));
		
		AcceptFishing = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.CarpenterSonMiddle), new MiddleCarpenterToCarpenterSonAcceptingFishing(), Schedule.priorityEnum.DoNow); // CHANGE THIS CONVERSATION TO THE ONE WE WANT TO USE!
		AcceptFishing.SetCanNotInteractWithPlayer();
		
		AfterAcceptFishing = new Schedule(this, Schedule.priorityEnum.High);
		Task flagTask = new TimeTask(0f, new IdleState(this));
		flagTask.AddFlagToSet(FlagStrings.AfterProudOfSonConversation);
		AfterAcceptFishing.Add(flagTask);
		AfterAcceptFishing.Add(new Task (new MoveThenMarkDoneState(this, startingPosition)));
		AfterAcceptFishing.Add(new TimeTask(10000f, new IdleState(this)));
		#endregion
		#region Carpentry
		happyAtSonForBeingCarpenter = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.CarpenterSonMiddle), 
			new MiddleCarpenterToSonCarpentryScriptedConvo(), Schedule.priorityEnum.DoNow);
		happyAtSonForBeingCarpenter.SetCanNotInteractWithPlayer();
		
		WorkOnWindmill = new Schedule(this, Schedule.priorityEnum.High);
		WorkOnWindmill.Add(new Task(new MoveThenMarkDoneState(this, MapLocations.WindmillMiddle)));
		WorkOnWindmill.Add(new TimeTask(250f, new IdleState(this)));
		WorkOnWindmill.Add(new Task(new MoveThenMarkDoneState(this, startingPosition)));
		Task setCarpentryFlag = new TimeTask(0f, new IdleState(this));
		setCarpentryFlag.AddFlagToSet(FlagStrings.CarpenterReturnedHome);
		WorkOnWindmill.Add(setCarpentryFlag);
		
		ConversationAboutRockingChair = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.CarpenterSonMiddle), 
			new GiveRockingChairScript(), Schedule.priorityEnum.DoNow);
		ConversationAboutRockingChair.SetCanNotInteractWithPlayer();
		
		ConversationAboutNotDoingAnything = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.CarpenterSonMiddle), 
			new GiveNothingScript(), Schedule.priorityEnum.DoNow);
		ConversationAboutNotDoingAnything.SetCanNotInteractWithPlayer();
		
		#endregion
//CONVERSATION SCHEDULE BUG!!!! PLEASE FIX		
//CONVERSATION SCHEDULE BUG!!!! PLEASE FIX		
	}
	
	
	#region CarpentryBranch
	
	private void SetupCarpentrySchedules() {
		SetupPrimaryCarpentrySchedules();
		SetupPassiveCarpentrySchedules();
	}
	
	private void SetupPrimaryCarpentrySchedules() {

		
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
