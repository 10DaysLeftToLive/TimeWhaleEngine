using UnityEngine;
using System.Collections;

/// <summary>
/// CarpenterSonMiddle specific scripting values
/// </summary>
public class CarpenterSonMiddle : NPC {
	StormOffEmotionState stormoffState;
	Date dateState;
	BlankEmotionState initialState;
	BecomeACarpenter carpenterState;
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
	Schedule WhittleStuff;
	
	protected override void SetFlagReactions(){
////		Reaction stoodUpLG = new Reaction();
////		stoodUpLG.AddAction(new NPCEmotionUpdateAction(this, StoodUpState));
////		flagReactions.Add(FlagStrings.CarpenterNoShow, stoodUpLG);
//		

		startingPosition = transform.position;
		startingPosition.y += LevelManager.levelYOffSetFromCenter;
		//Schedule for the default path
		#region DefaultSection
		Reaction independentStormOffReaction = new Reaction();
		independentStormOffReaction.AddAction(new NPCAddScheduleAction(this, moveToWindmill));
		independentStormOffReaction.AddAction(new NPCEmotionUpdateAction(this, new StormOffToWindmill(this, 
			"I knew I was going to screw up somewhere. I left my specialized tools someplace.")));
		flagReactions.Add (FlagStrings.carpenterSonTalkWithFatherMorning, independentStormOffReaction);
		#endregion 
		#region FishSection
		//Moves the Son back to start if need be. Then triggers a flag to start the conversation with the carpenter
		Reaction MovePiecesForFishing =  new Reaction();
		//MovePiecesForFishing.AddAction
		MovePiecesForFishing.AddAction(new NPCTeleportToAction(this, startingPosition));
		MovePiecesForFishing.AddAction(new NPCCallbackAction(AcquireFishingPole));
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
		
		#region Making the Harp
		Reaction StartMakingHarp = new Reaction();
		StartMakingHarp.AddAction(new NPCAddScheduleAction(this, WhittleStuff));
		flagReactions.Add (FlagStrings.carpenterSonWhittleMiddleAge, StartMakingHarp);
		
		Reaction MakingTheHarpDone = new Reaction();
		MakingTheHarpDone.AddAction(new NPCGiveItemAction(this, StringsItem.Harp));
		flagReactions.Add (FlagStrings.carpenterSonMakesFatherProud, MakingTheHarpDone);
		#endregion
		
		Reaction DoNothing = new Reaction();
		DoNothing.AddAction(new NPCEmotionUpdateAction(this, carpenterState));
		DoNothing.AddAction(new NPCAddScheduleAction(this, DoNothingSchedule));
		flagReactions.Add(FlagStrings.IntroConvoCarpentry, DoNothing);
		
		Reaction EndOfDayConvo = new Reaction();
		EndOfDayConvo.AddAction(new NPCEmotionUpdateAction(this, new BlankEmotionState(this, "It's nice to relax after a long day.")));
		EndOfDayConvo.AddAction(new NPCAddScheduleAction(this, AfterConversationCarpentery));
		flagReactions.Add(FlagStrings.CarpenterReturnedHome, EndOfDayConvo);
		#endregion
		
		Reaction dating = new Reaction();
		dating.AddAction(new NPCCallbackAction(setFlagDateForMe));
		dating.AddAction(new NPCCallbackSetStringAction(FlagToNPC, this, "date"));
		flagReactions.Add(FlagStrings.CarpenterDate, dating);
		
		Reaction moveToDate = new Reaction();
		moveToDate.AddAction(new NPCAddScheduleAction(this, moveToBeach));
		flagReactions.Add(FlagStrings.CarpenterDating, moveToDate);
		
		Reaction endOfDate = new Reaction();
		endOfDate.AddAction(new NPCCallbackAction(dateOver));
		endOfDate.AddAction(new NPCAddScheduleAction(this, moveBack));
		flagReactions.Add(FlagStrings.EndOfDate, endOfDate);
		
		//chat stuff
		Reaction carpenterDateOne = new Reaction();
		ShowMultipartChatAction carpenterDateOneDialogue = new ShowMultipartChatAction(this);
		carpenterDateOneDialogue.AddChat("*Out of Breath* At long last I get to try and court you my fair lady!", 5f);
		carpenterDateOne.AddAction(carpenterDateOneDialogue);
		flagReactions.Add(FarmerFamilyFlagStrings.GirlCarpenterDateOne, carpenterDateOne);
		
		
		Reaction carpenterDateTwo = new Reaction();
		ShowMultipartChatAction carpenterDateTwoDialogue = new ShowMultipartChatAction(this);
		carpenterDateTwoDialogue.AddChat("Endearing? My dear woman, I always speak this way!", 3f);
		carpenterDateTwo.AddAction(carpenterDateTwoDialogue);
		flagReactions.Add(FarmerFamilyFlagStrings.GirlCarpenterDateThree, carpenterDateTwo);
		
		
		Reaction carpenterDateThree = new Reaction();
		ShowMultipartChatAction carpenterDateThreeDialogue = new ShowMultipartChatAction(this);
		carpenterDateThreeDialogue.AddChat("You remember me!", 2f);
		carpenterDateThree.AddAction(carpenterDateThreeDialogue);
		flagReactions.Add(FarmerFamilyFlagStrings.GirlCarpenterDateFive, carpenterDateThree);
		
		
		Reaction carpenterDateFour = new Reaction();
		Reaction carpenterDateFive = new Reaction();
		Reaction carpenterDateSix = new Reaction();
	}
	
	protected override EmotionState GetInitEmotionState() {
		carpenterState = new BecomeACarpenter(this, "");
		return (new BlankEmotionState(this, "One second, I am talking to someone"));
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
		moveToWindmill.Add (new Task(new MoveThenMarkDoneState(this, MapLocations.WindmillMiddle, "Somber Walk", 2f)));
		moveToWindmill.Add (new TimeTask(100f, new AbstractAnimationState(this, "Hammer", false, false)));
		moveToWindmill.Add (new Task(new MoveThenMarkDoneState(this, startingPosition)));
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
		MoveToPierToFish.Add(new TimeTask(100f, new AbstractAnimationState(this, "Fishing", true, true)));
		Task SetOffConversationWithSeaCaptain = new TimeTask(0f, new IdleState(this));
		SetOffConversationWithSeaCaptain.AddFlagToSet(FlagStrings.StartConversationWithSeaCaptainAboutBuildingShip);
		MoveToPierToFish.Add(SetOffConversationWithSeaCaptain);
		
		AfterSeaCaptainTalk = new Schedule (this, Schedule.priorityEnum.DoNow);
		AfterSeaCaptainTalk.Add(new Task(new MoveThenMarkDoneState(this, MapLocations.MiddleOfBeachMiddle)));
		Task SetOffAfterSeaCaptain = new TimeTask(100f, new AbstractAnimationState(this, "Whittle"));
		SetOffAfterSeaCaptain.AddFlagToSet(FlagStrings.StartProudOfSonConversation);
		Task CarpenterSonTransitionToFisherman = new Task(new IdleState(this));
		CarpenterSonTransitionToFisherman.AddFlagToSet(FlagStrings.carpenterSonBecomesFisherman);
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
		
		WhittleStuff = new Schedule(this, Schedule.priorityEnum.High);
		TimeTask WhittlingAHarp = new TimeTask(30f, new AbstractAnimationState(this, "Whittle"));
		WhittlingAHarp.AddFlagToSet(FlagStrings.BuiltRockingChairTalk);
		WhittleStuff.Add(WhittlingAHarp);
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
		
		#region carpenterDate
		moveBack = new Schedule(this, Schedule.priorityEnum.High);
		moveBack.Add(new Task(new MoveThenDoState(this, startingPosition, new MarkTaskDone(this))));
		
		moveToBeach = new Schedule(this, Schedule.priorityEnum.DoNow);
		moveToBeach.Add(new Task(new MoveThenDoState(this, new Vector3(MapLocations.MiddleOfBeachMiddle.x+1.5f, MapLocations.MiddleOfBeachMiddle.y, MapLocations.MiddleOfBeachMiddle.z), new MarkTaskDone(this))));
		Task reachedBeach = new TimeTask(.1f,new IdleState(this));
		reachedBeach.AddFlagToSet(FarmerFamilyFlagStrings.GirlCarpenterDateOne);
		moveToBeach.Add(reachedBeach);
		moveToBeach.Add(new TimeTask(5.3f, new IdleState(this)));
		
		Task reachedBeachTwo = new TimeTask(.1f,new IdleState(this));
		reachedBeachTwo.AddFlagToSet(FarmerFamilyFlagStrings.GirlCarpenterDateTwo);
		moveToBeach.Add(reachedBeachTwo);
		moveToBeach.Add(new TimeTask(7.3f, new IdleState(this)));
		
		Task reachedBeachThree = new TimeTask(.1f,new IdleState(this));
		reachedBeachThree.AddFlagToSet(FarmerFamilyFlagStrings.GirlCarpenterDateThree);
		moveToBeach.Add(reachedBeachThree);
		moveToBeach.Add(new TimeTask(3.3f, new IdleState(this)));
		
		
		Task reachedBeachFour = new TimeTask(.1f,new IdleState(this));
		reachedBeachFour.AddFlagToSet(FarmerFamilyFlagStrings.GirlCarpenterDateFour);
		moveToBeach.Add(reachedBeachFour);
		moveToBeach.Add(new TimeTask(6.3f, new IdleState(this)));
		
		Task reachedBeachFive = new TimeTask(.1f,new IdleState(this));
		reachedBeachFive.AddFlagToSet(FarmerFamilyFlagStrings.GirlCarpenterDateFive);
		moveToBeach.Add(reachedBeachFive);
		moveToBeach.Add(new TimeTask(2.3f, new IdleState(this)));
		
		Task reachedBeachSix = new TimeTask(.1f,new IdleState(this));
		reachedBeachSix.AddFlagToSet(FarmerFamilyFlagStrings.GirlCarpenterDateSix);
		moveToBeach.Add(reachedBeachSix);
		moveToBeach.Add(new TimeTask(6f, new IdleState(this)));
		
		Task reachedBeachEnd = new TimeTask(.1f,new IdleState(this));
		reachedBeachEnd.AddFlagToSet(FlagStrings.EndOfDate);
		moveToBeach.Add(reachedBeachEnd);
		moveToBeach.Add(new TimeTask(3f, new IdleState(this)));
		moveToBeach.SetCanInteract(false);
		#endregion
	}
	
	protected void AcquireFishingPole() {
		
	}
	
	protected void dateOver(){
		if (dateForMe)
			FlagManager.instance.SetFlag(FlagStrings.CarpenterNoShow);
	}
	
	
	protected void setFlagDateForMe(){
		dateForMe = true;
	}
	
	protected void FlagToNPC(NPC npc, string text){
		if (text == "date"){
			carpenterState.PassStringToEmotionState(text);	
		}
	}
	
	
	#region EmotionStates
	#region Initial Emotion State
	//EmotionState to be used for setting up non-choice dialogue.
	private class BlankEmotionState : EmotionState {
		
		
		
		public BlankEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
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
			
			youDontNeedHimReaction.AddAction(new UpdateDefaultTextAction(toControl, "Who needs his approval anyway?"));
			youDontNeedHimReaction.AddAction(new NPCCallbackAction(removeChoices));
			
			_allChoiceReactions.Add(reconcileWithFather, new DispositionDependentReaction(reconcileReaction));
			_allChoiceReactions.Add(youDontNeedHim, new DispositionDependentReaction(youDontNeedHimReaction));
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
				FlagManager.instance.SetFlag(FlagStrings.MarryingCarpenter);
			}
		}
		
	}
	#endregion
	
	#region Storm Off To WindMill
	//This is for when he storms off to work on windmill alone.
	//This is from the default path.
	private class StormOffToWindmill : EmotionState {
		
		#region Independent
		Choice askAboutToolBox = new Choice("Want me to get your toolbox?", "Thanks! could you please find it for me?");
		
		Reaction searchForToolBox = new Reaction();
		Reaction toolsFound = new Reaction();
		#endregion
		
		public StormOffToWindmill(NPC toControl, string currentDialogue) : base(toControl, currentDialogue) {
			searchForToolBox.AddAction(new UpdateDefaultTextAction(toControl, "Have you found my tools yet?"));
			searchForToolBox.AddAction(new NPCCallbackAction(removeChoices));
			
			toolsFound.AddAction(new NPCTakeItemAction(toControl));
			toolsFound.AddAction(new NPCCallbackAction(removeChoices));
			toolsFound.AddAction(new UpdateCurrentTextAction(toControl, "Awesome! Now I can finish my repairs on this windmill"));
			toolsFound.AddAction(new UpdateDefaultTextAction(toControl, "Thanks for getting the tools for me, now I can continue to work on this windmill."));
			//Makes it so you cannot click on Carpenter Middle
			
			_allChoiceReactions.Add(askAboutToolBox, new DispositionDependentReaction(searchForToolBox));
			_allItemReactions.Add(StringsItem.Toolbox, new DispositionDependentReaction(toolsFound));
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
		Choice curiousAboutMood = new Choice("What are you up to?", 
			"Well, I thought I'd make a present for my Dad, I thought I'd make him a Harp");
		Choice presentForDad = new Choice("Can I help?", "Yeah Sure, I'll need some wood.  Can you get it from the beach");
		Choice DateChoice = new Choice("You have a date!", "Really? This...this...this is the most beauteous day of my life! Hurry to the beach. I cannot tarry!");
		
		Reaction curiousAboutMoodReaction = new Reaction();
		Reaction assistGettingWood = new Reaction();
		Reaction helpAppreciated = new Reaction();
		Reaction DateReaction = new Reaction();
		
		bool flagSet = false;
		public BecomeACarpenter(NPC toControl, string currentDialogue) : base(toControl, "Hi there.  I'm a bit busy right now.") {
				
			curiousAboutMoodReaction.AddAction(new NPCCallbackAction(selectCuriousMoodChoice));
			
			assistGettingWood.AddAction(new UpdateDefaultTextAction(toControl, "Would you like to help make me a present for my dad?"));
			assistGettingWood.AddAction(new UpdateCurrentTextAction(toControl, "Yeah sure, I'll need some wood. Can you get it from the beach?"));
			assistGettingWood.AddAction(new NPCCallbackAction(removeAllOtherReactions));
			
			helpAppreciated.AddAction(new NPCTakeItemAction(toControl));
			helpAppreciated.AddAction(new NPCCallbackAction(removeAllOtherReactions));
			helpAppreciated.AddAction(new UpdateCurrentTextAction(toControl, "Thanks!"));
			helpAppreciated.AddAction(new UpdateDefaultTextAction(toControl, "Come back later and I should have the harp done!"));
			helpAppreciated.AddAction(new SetOffFlagAction(FlagStrings.carpenterSonWhittleMiddleAge));

			_allChoiceReactions.Add (curiousAboutMood, new DispositionDependentReaction(curiousAboutMoodReaction));
			_allItemReactions.Add(StringsItem.Whittle, new DispositionDependentReaction(helpAppreciated));
		}
		
		
			
		void selectCuriousMoodChoice() {
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
_npcInState.ChangeFacialExpression(StringsNPC.Default);
			_allChoiceReactions.Clear();
			_allChoiceReactions.Add (presentForDad, new DispositionDependentReaction(assistGettingWood));
			GUIManager.Instance.RefreshInteraction();
		}
			
		void helpCarpenterSon() {
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
_npcInState.ChangeFacialExpression(StringsNPC.Default);
			_allChoiceReactions.Remove(presentForDad);
			GUIManager.Instance.RefreshInteraction();
		}
		
		void removeAllOtherReactions() {
			_allChoiceReactions.Clear();
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void DateResponse(){
			_npcInState.SetCharacterPortrait(StringsNPC.Happy);
			_npcInState.ChangeFacialExpression(StringsNPC.Happy);
			if (!flagSet){
				_allChoiceReactions.Remove(DateChoice);
				GUIManager.Instance.CloseInteractionMenu();
				FlagManager.instance.SetFlag(FlagStrings.CarpenterDating);
				flagSet = true;
			}
		}
		
		public override void PassStringToEmotionState(string text){
			if (text == "date"){
				DateReaction.AddAction(new NPCCallbackAction(DateResponse));
				_allChoiceReactions.Add(DateChoice, new DispositionDependentReaction(DateReaction));
			}
		}
	}
	
	#endregion
	
	private class OtherCarpentryTasks : EmotionState {
		public OtherCarpentryTasks(NPC toControl, string currentDialogue) : base(toControl, currentDialogue) {
		}
		
		void removeChoices() {
			_allChoiceReactions.Clear();
			GUIManager.Instance.RefreshInteraction();
		}
	}
	
	#endregion
}
