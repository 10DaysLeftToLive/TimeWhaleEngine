using UnityEngine;
using System.Collections;

/// <summary>
/// CarpenterSonMiddle specific scripting values
/// </summary>
public class CarpenterSonMiddle : NPC {
	StormOffEmotionState stormoffState;
	Date dateState;
	BlankEmotionState initialState;
	Vector3 startingPosition;
	bool castlemanDateSuccess = false;
	bool dateForMe = false;
	bool successfulDate = false;
	
	Schedule stormOffSchedule, moveToBeach, moveBack, moveToWindmill;
	Schedule MoveToPierToFish, AfterSeaCaptainTalk, TeleportToStartConvo;
	NPCConvoSchedule dateWithLG;
	
	NPCConvoSchedule reportedDidHardWorkToFather, reportedDidNoWorkToFather;
	Schedule EndState;
	Schedule StartCarpentry;
	Schedule DoNothingSchedule;
	Schedule AfterConversationCarpentery;
	protected override void Init() {
		id = NPCIDs.CARPENTER_SON;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
//		Reaction waitingForDate = new Reaction();
//		waitingForDate.AddAction(new NPCEmotionUpdateAction(this, dateState));
//		flagReactions.Add(FlagStrings.WaitingForDate, waitingForDate);
//		
//		Reaction gotTheGirl = new Reaction();
//		gotTheGirl.AddAction(new NPCEmotionUpdateAction(this, initialState)); //change state after successfuldate
//		flagReactions.Add(FlagStrings.PostDatingCarpenter, gotTheGirl);
//		
//		Reaction iBeDating = new Reaction();
//		iBeDating.AddAction(new NPCCallbackAction(setFlagDateForMe));
//		flagReactions.Add(FlagStrings.CarpenterDate, iBeDating);
//		
//		Reaction endOfDate = new Reaction();
//		endOfDate.AddAction(new NPCCallbackAction(dateOver));
//		endOfDate.AddAction(new NPCAddScheduleAction(this, moveBack));
//		flagReactions.Add(FlagStrings.EndOfDate, endOfDate);
//		
//		
////		Reaction stoodUpLG = new Reaction();
////		stoodUpLG.AddAction(new NPCEmotionUpdateAction(this, StoodUpState));
////		flagReactions.Add(FlagStrings.CarpenterNoShow, stoodUpLG);
//		
//		Reaction moveToDate = new Reaction();
//		moveToDate.AddAction(new NPCAddScheduleAction(this, dateWithLG));
//		flagReactions.Add(FlagStrings.CarpenterDating, moveToDate);
		
		startingPosition = transform.position;
		startingPosition.y += LevelManager.levelYOffSetFromCenter;
		//Schedule for the default path
		#region DefaultSection
		Reaction independentStormOffReaction = new Reaction();
		independentStormOffReaction.AddAction(new NPCAddScheduleAction(this, moveToWindmill));
		independentStormOffReaction.AddAction(new NPCEmotionUpdateAction(this, new StormOffToWindmill(this, 
			"I knew I was going to screw up somewhere. I left my specialized tools someplace")));
		flagReactions.Add (FlagStrings.carpenterSonTalkWithFatherMorning, independentStormOffReaction);
		#endregion 
		#region FishSection
		//Moves the Son back to start if need be. Then triggers a flag to start the conversation with the carpenter
		Reaction MovePiecesForFishing =  new Reaction();
		//MovePiecesForFishing.AddAction
		MovePiecesForFishing.AddAction(new NPCTeleportToAction(this, startingPosition));
		MovePiecesForFishing.AddAction(new NPCAddScheduleAction(this, TeleportToStartConvo));
		flagReactions.Add(FlagStrings.carpenterSonEncouragedFishing, MovePiecesForFishing);
		
		//Moves the carpenter son to the pier.
		Reaction StartFishing = new Reaction();
		StartFishing.AddAction(new NPCAddScheduleAction(this, MoveToPierToFish));
		StartFishing.AddAction(new NPCEmotionUpdateAction(this, new StormOffEmotionState(this, "I need to work on the windmill.")));
		flagReactions.Add(FlagStrings.CarpenterSonMovesToTheBeach, StartFishing);
		
		Reaction StartWhittling = new Reaction();
		StartWhittling.AddAction(new NPCAddScheduleAction(this, AfterSeaCaptainTalk));
		flagReactions.Add(FlagStrings.AfterConversationAboutBuildingShip, StartWhittling);
		
		Reaction EndReaction = new Reaction();
		EndReaction.AddAction(new NPCAddScheduleAction(this, EndState));
		flagReactions.Add(FlagStrings.AfterProudOfSonConversation, EndReaction);
		/*Reaction StartTalkingToSeaCaptain = new Reaction();
		//Add in NPCConvoSchedule
		StartTalkingToSeaCaptain.AddAction(new NPCAddScheduleAction(this, AfterSeaCaptainTalk));
		flagReactions.Add(FlagStrings.StartConversationWithSeaCaptainAboutBuildingShip, StartTalkingToSeaCaptain);*/
		#endregion
		#region Carpentry Section
		Reaction MovePiecesForCarpentry = new Reaction();
		MovePiecesForCarpentry.AddAction(new NPCTeleportToAction(this, startingPosition));
		MovePiecesForCarpentry.AddAction(new NPCAddScheduleAction(this, StartCarpentry));
		flagReactions.Add(FlagStrings.carpenterSonEncouragedCarpentry, MovePiecesForCarpentry);
			
		Reaction DoNothing = new Reaction();
		DoNothing.AddAction(new NPCEmotionUpdateAction(this, new BecomeACarpenter(this, "")));
		DoNothing.AddAction(new NPCAddScheduleAction(this, DoNothingSchedule));
		flagReactions.Add(FlagStrings.IntroConvoCarpentry, DoNothing);
		
		Reaction EndOfDayConvo = new Reaction();
		EndOfDayConvo.AddAction(new NPCEmotionUpdateAction(this, new BlankEmotionState(this, "It's nice to relax after a long day.")));
		EndOfDayConvo.AddAction(new NPCAddScheduleAction(this, AfterConversationCarpentery));
		flagReactions.Add(FlagStrings.CarpenterReturnedHome, EndOfDayConvo);
		#endregion

		
	}
	
	protected override EmotionState GetInitEmotionState() {
		
		return (new BlankEmotionState(this, "One Second, I am talking to someone"));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}
	
	
	//Schedule IdleSchedule;

	protected override void SetUpSchedules(){
		startingPosition = transform.position;
		startingPosition.y += LevelManager.levelYOffSetFromCenter;
		#region PathOne
		//Schedule for the Default path
		moveToWindmill = new Schedule(this, Schedule.priorityEnum.Low);
		moveToWindmill.Add (new Task(new MoveThenMarkDoneState(this, MapLocations.WindmillMiddle, "Somber Walk", 0.000000000025f)));
		moveToWindmill.Add (new TimeTask(100f, new IdleState(this)));
		moveToWindmill.Add (new Task(new MoveThenMarkDoneState(this, this.gameObject.transform.position)));
		#endregion
		
		#region FishingPath
		TeleportToStartConvo =  new Schedule (this, Schedule.priorityEnum.DoNow);
		TeleportToStartConvo.Add(new TimeTask(300f, new WaitTillPlayerCloseState(this, ref player))); 
		//TeleportToStartConvo.Add(new Task (new MoveThenMarkDoneState(this, this.gameObject.transform.position)));
		Task StartFishingStuff = new TimeTask(0f, new IdleState(this));
		StartFishingStuff.AddFlagToSet(FlagStrings.FishingConversation);
		TeleportToStartConvo.Add(StartFishingStuff);
		
		MoveToPierToFish = new Schedule(this, Schedule.priorityEnum.DoNow);
		MoveToPierToFish.Add(new Task(new MoveThenMarkDoneState(this, MapLocations.BaseOfPierMiddle)));
		MoveToPierToFish.Add(new TimeTask(100f, new IdleState(this)));
		//Fishing stuffs
		Task SetOffConversationWithSeaCaptain = new TimeTask(0f, new IdleState(this));
		SetOffConversationWithSeaCaptain.AddFlagToSet(FlagStrings.StartConversationWithSeaCaptainAboutBuildingShip);
		MoveToPierToFish.Add(SetOffConversationWithSeaCaptain);
		
		AfterSeaCaptainTalk = new Schedule (this, Schedule.priorityEnum.DoNow);
		AfterSeaCaptainTalk.Add(new Task(new MoveThenMarkDoneState(this, MapLocations.MiddleOfBeachMiddle)));
		//Whittling Animation.
		Task SetOffAfterSeaCaptain = new TimeTask(100f, new IdleState(this));
		SetOffAfterSeaCaptain.AddFlagToSet(FlagStrings.StartProudOfSonConversation);
		AfterSeaCaptainTalk.Add(SetOffAfterSeaCaptain);
		
		EndState = new Schedule(this, Schedule.priorityEnum.High);
		EndState.Add(new TimeTask(10000f, new IdleState(this)));
		#endregion
		
		#region CarpentryPath
		StartCarpentry =  new Schedule (this, Schedule.priorityEnum.High);
		StartCarpentry.Add(new TimeTask(300f, new WaitTillPlayerCloseState(this, ref player))); 
		//TeleportToStartConvo.Add(new Task (new MoveThenMarkDoneState(this, this.gameObject.transform.position)));
		Task StartCarpentryStuff = new TimeTask(0f, new IdleState(this));
		StartCarpentryStuff.AddFlagToSet(FlagStrings.IntroConvoCarpentry);
		StartCarpentry.Add(StartCarpentryStuff);
		
		DoNothingSchedule =  new Schedule(this, Schedule.priorityEnum.High);
		DoNothingSchedule.Add(new TimeTask(10000f, new IdleState(this)));
		#endregion
		//Schedule for something
		stormOffSchedule = new Schedule(this,Schedule.priorityEnum.DoNow);
		stormOffSchedule.Add(new Task(new MoveThenMarkDoneState(this, MapLocations.BaseOfPierMiddle)));
		stormOffSchedule.Add(new TimeTask(2.0f, new IdleState(this))); //Will replace with working on windmill
		stormOffSchedule.Add(new Task(new MoveThenMarkDoneState(this, MapLocations.BaseOfPierMiddle)));
				
		
		AfterConversationCarpentery = new Schedule(this, Schedule.priorityEnum.High);
		AfterConversationCarpentery.Add(new TimeTask(10000f, new IdleState(this)));
		#region NPCConvoSchedules
		#endregion
	}
	
	protected void dateOver(){
		if (dateForMe)
			FlagManager.instance.SetFlag(FlagStrings.CarpenterNoShow);
	}
	
	
	protected void setFlagDateForMe(){
		dateForMe = true;
	}
	
	
	#region EmotionStates
	#region Initial Emotion State
	//EmotionState to be used for setting up non-choice dialogue.
	private class BlankEmotionState : EmotionState {
		
		
		
		public BlankEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
		}
				
		
		
		public override void UpdateEmotionState(){
			
		}
	
	}
	#endregion
	#region Storm off To The Beach Emotion State
	//This is for when the Carpenter Son runs off to the beach.
	private class StormOffEmotionState : EmotionState{
		Choice reconcileWithFather = new Choice("You should try to get along with your father.", "Yeah, if I just keep trying I'm sure my dad will accept me");
		Choice youDontNeedHim = new Choice("You don't need your dad's approval.", "Yeah, my dad doesn't deserve my respect.");
		
		Reaction reconcileReaction = new Reaction();
		Reaction youDontNeedHimReaction = new Reaction();
		
		public StormOffEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			reconcileReaction.AddAction(new SetOffFlagAction(FlagStrings.carpenterSonReconcile));
			reconcileReaction.AddAction(new UpdateDefaultTextAction(toControl, "I'll go talk to him soon."));
			reconcileReaction.AddAction(new NPCCallbackAction(removeChoices));
			
			youDontNeedHimReaction.AddAction(new UpdateDefaultTextAction(toControl, "Who needs his aproval anyway?"));
			youDontNeedHimReaction.AddAction(new NPCCallbackAction(removeChoices));
			
			_allChoiceReactions.Add(reconcileWithFather, new DispositionDependentReaction(reconcileReaction));
			_allChoiceReactions.Add(youDontNeedHim, new DispositionDependentReaction(youDontNeedHimReaction));
		}
		
		public override void UpdateEmotionState(){
			
		}
			
		void removeChoices() {
			_allChoiceReactions.Clear();
			GUIManager.Instance.RefreshInteraction();
		}
	
	}
	#endregion
	
	#region Date With LightHouse Girl
	//This is Eric's stuff for setting up a date with the LG.
	private class Date: EmotionState{
		Choice DateChoice = new Choice("You have a date!", "Really? This...this...this is the most beauteous day of my life! Hurry to the beach. I cannot tarry!");
		
		Reaction DateReaction = new Reaction();
		
		bool flagSet = false;
		public Date (NPC toControl, string currentDialogue):base (toControl, "You look like you have an urgent message."){
			_allChoiceReactions.Clear();
			
			DateReaction.AddAction(new NPCCallbackAction(DateResponse));
			_allChoiceReactions.Add(DateChoice, new DispositionDependentReaction(DateReaction));
		}
		
		public void DateResponse(){
			if (!flagSet){
				FlagManager.instance.SetFlag(FlagStrings.CarpenterDating);
			}
		}
		
	}
	#endregion
	
	#region Storm Off To WindMill
	//This is for when he storms off to work on windmill alone.
	//This is from the default path.
	private class StormOffToWindmill : EmotionState {
		
		Choice askAboutToolBox = new Choice("Want me to get your ToolBox?", "Thanks! could you please find them for me?");
		
		Reaction searchForToolBox = new Reaction();
		Reaction toolsFound = new Reaction();
		
		public StormOffToWindmill(NPC toControl, string currentDialogue) : base(toControl, currentDialogue) {
			searchForToolBox.AddAction(new UpdateDefaultTextAction(toControl, "Have you found my tools yet?"));
			searchForToolBox.AddAction(new NPCCallbackAction(removeChoices));
			
			toolsFound.AddAction(new NPCTakeItemAction(toControl));
			toolsFound.AddAction(new NPCCallbackAction(removeChoices));
			toolsFound.AddAction(new UpdateCurrentTextAction(toControl, "Awesome! Now I can finish my repairs on this.. Windmill"));
			toolsFound.AddAction(new UpdateDefaultTextAction(toControl, "Thanks for getting the tools for me, now I can continue to work on this... Windmill."));
			//Makes it so you cannot click on Carpenter Middle
			
			_allChoiceReactions.Add(askAboutToolBox, new DispositionDependentReaction(searchForToolBox));
			_allItemReactions.Add(StringsItem.Toolbox, new DispositionDependentReaction(toolsFound));
		}
		
		public override void UpdateEmotionState(){
			
		}
			
		void removeChoices() {
			_allChoiceReactions.Clear();
			GUIManager.Instance.RefreshInteraction();
		}
	}
	
	#endregion
	
	#region Become A Carpenter
	//State for when Carpenter's son becomes interested in being a carpenter.
	private class BecomeACarpenter : EmotionState {
		Reaction GetWoodReaction;
		Choice curiousAboutMood = new Choice("What are you up to?", 
			"Well, I thought I'd make a present for my Dad, I thought I'd make him a rocking chair");
		Choice presentForDad = new Choice("Can I help?", "Yeah Sure, I'll need some wood.  Can you get it from the beach");
		
		
		Reaction curiousAboutMoodReaction = new Reaction();
		Reaction assistGettingWood = new Reaction();
		Reaction helpAppreciated = new Reaction();
		
		public BecomeACarpenter(NPC toControl, string currentDialogue) : base(toControl, "Hi there.  I'm a bit busy right now.") {
			GetWoodReaction = new Reaction();
			GetWoodReaction.AddAction(new NPCCallbackAction(UpdateGetWoodReaction));
			GetWoodReaction.AddAction(new NPCTakeItemAction(toControl));
			//Change this to wood or whatever is the needed item.
			_allItemReactions.Add(StringsItem.Apple, new DispositionDependentReaction(GetWoodReaction));	
				
			curiousAboutMoodReaction.AddAction(new NPCCallbackAction(selectCuriousMoodChoice));
			
			assistGettingWood.AddAction(new NPCCallbackAction(helpCarpenterSon));
			assistGettingWood.AddAction(new UpdateDefaultTextAction(toControl, "Would you like to help make me a present for my Dad?"));
			
			helpAppreciated.AddAction(new NPCTakeItemAction(toControl));
			helpAppreciated.AddAction(new NPCCallbackAction(removeAllOtherReactions));
			helpAppreciated.AddAction(new UpdateCurrentTextAction(toControl, "Thanks!"));
			helpAppreciated.AddAction(new UpdateDefaultTextAction(toControl, "Come back later and I should have the rocking chair done!"));
			helpAppreciated.AddAction(new SetOffFlagAction(FlagStrings.carpenterSonMakesFatherProud));
			helpAppreciated.AddAction(new NPCCallbackAction(WhittleWood));
			_allChoiceReactions.Add (curiousAboutMood, new DispositionDependentReaction(curiousAboutMoodReaction));
			//TODO: Replace Toolbox with piece of wood
			_allItemReactions.Add(StringsItem.Toolbox, new DispositionDependentReaction(helpAppreciated));
		}
		public void UpdateGetWoodReaction(){
			FlagManager.instance.SetFlag(FlagStrings.BuiltStuffForDad);	
			SetDefaultText("Thank you so much for helping me!");
			GUIManager.Instance.RefreshInteraction();
		}
		public override void UpdateEmotionState(){
			
		}
			
		void selectCuriousMoodChoice() {
			_allChoiceReactions.Remove(curiousAboutMood);
			_allChoiceReactions.Add (presentForDad, new DispositionDependentReaction(assistGettingWood));
			GUIManager.Instance.RefreshInteraction();
		}
			
		void helpCarpenterSon() {
			_allChoiceReactions.Remove(presentForDad);
			GUIManager.Instance.RefreshInteraction();
		}
		
		void removeAllOtherReactions() {
			_allChoiceReactions.Clear();
			GUIManager.Instance.RefreshInteraction();
		}
		
		void WhittleWood() {
			_npcInState.animationData.Play("Whittle");
		}
	}
	
	#endregion
	
	#endregion
}
