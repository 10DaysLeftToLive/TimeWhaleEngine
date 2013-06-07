using UnityEngine;
using System.Collections;

/// <summary>
/// CarpenterSonMiddle specific scripting values
/// </summary>
public class CarpenterSonMiddle : NPC {
	StormOffEmotionState stormoffState;
	Date dateState;
	InitialEmotionState initialState;
	Vector3 startingPosition;
	bool castlemanDateSuccess = false;
	bool dateForMe = false;
	bool successfulDate = false;
	
	Schedule stormOffSchedule, moveToBeach, moveBack, moveToWindmill;
	NPCConvoSchedule dateWithLG;
	
	NPCConvoSchedule reportedDidHardWorkToFather, reportedDidNoWorkToFather;
	
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
		
		Reaction independentStormOffReaction = new Reaction();
		independentStormOffReaction.AddAction(new NPCAddScheduleAction(this, moveToWindmill));
		independentStormOffReaction.AddAction(new NPCEmotionUpdateAction(this, new StormOffToWindmill(this, 
			"I knew I was going to screw up somewhere. I left my specialized tools someplace")));
		flagReactions.Add (FlagStrings.carpenterSonIndependent, independentStormOffReaction);
		
		Reaction becomesACarpenter = new Reaction();
		becomesACarpenter.AddAction(new NPCEmotionUpdateAction(this, new BecomeACarpenter(this, "Hey there man, I'm a bit busy right now.")));
		flagReactions.Add(FlagStrings.carpenterSonTalkWithFatherMorning, becomesACarpenter);
		
//		Reaction stormOffReaction = new Reaction();
//		stormOffReaction.AddAction(new NPCEmotionUpdateAction(this, stormoffState));
//		stormOffReaction.AddAction(new NPCAddScheduleAction(this, stormOffSchedule));
//		flagReactions.Add(FlagStrings.carpenterSonIndependent, stormOffReaction);
		
	}
	
	protected override EmotionState GetInitEmotionState() {
		startingPosition = transform.position;
		startingPosition.y += LevelManager.levelYOffSetFromCenter;
		return (new InitialEmotionState(this, "One Second, I am talking to my father"));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}
	
	
	//Schedule IdleSchedule;

	protected override void SetUpSchedules(){
		
		moveBack = new Schedule(this, Schedule.priorityEnum.High);
		moveBack.Add(new Task(new MoveThenDoState(this, startingPosition, new MarkTaskDone(this))));
		
		//SetepDefaultPathSchedules();
		//SetepFishingPathSChedules();
		//SetupCarpentryPathSchedules();
	}
	
	private void SetupFishingPathSchedules() {
		stormOffSchedule = new Schedule(this,Schedule.priorityEnum.DoNow);
		stormOffSchedule.Add(new Task(new MoveState(this, MapLocations.BaseOfPierMiddle)));
		stormOffSchedule.Add(new TimeTask(2.0f, new IdleState(this))); //Will replace with working on windmill
		stormOffSchedule.Add(new Task(new MoveThenDoState(this, MapLocations.BaseOfPierMiddle, new MarkTaskDone(this))));
		
	}
	
	private void SetepDefaultPathSchedules() {
		moveToWindmill = new Schedule(this, Schedule.priorityEnum.DoNow);
		moveToWindmill.Add (new Task(new MoveState(this, MapLocations.WindmillMiddle)));
		moveToWindmill.Add (new TimeTask(2.0f, new IdleState(this)));
		moveToWindmill.Add (new Task(new MoveThenDoState(this, MapLocations.WindmillMiddle, new MarkTaskDone(this))));
		
		TimeTask finishWindmill = new TimeTask(50.0f, new IdleState(this));
		moveToWindmill.Add(finishWindmill);
	}
	
	private void SetupCarpentryPathSchedules() {
//		dateWithLG =  new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.LighthouseGirlMiddle),
//			new MiddleCastleManToLighthouseGirl(), Schedule.priorityEnum.DoConvo); 
//		dateWithLG.SetCanNotInteractWithPlayer();
		
		reportedDidHardWorkToFather = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.CarpenterMiddle), null);
		reportedDidHardWorkToFather.SetCanNotInteractWithPlayer();
		
		reportedDidNoWorkToFather = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.CarpenterMiddle), null);
		reportedDidNoWorkToFather.SetCanNotInteractWithPlayer();
		
		
		
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
	private class InitialEmotionState : EmotionState {
		
		
		
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
		}
				
		
		
		public override void UpdateEmotionState(){
			
		}
	
	}
	#endregion
	#region Storm off To The Beach Emotion State
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
	
	private class BecomeACarpenter : EmotionState {
		
		Choice curiousAboutMood = new Choice("What are you up to?", 
			"Well, I thought I'd make a present for my Dad, I thought I'd make him a rocking chair");
		Choice presentForDad = new Choice("Can I help?", "Yeah Sure, I'll need some wood.  Can you get it from the beach");
		
		
		Reaction curiousAboutMoodReaction = new Reaction();
		Reaction assistGettingWood = new Reaction();
		Reaction helpAppreciated = new Reaction();
		
		public BecomeACarpenter(NPC toControl, string currentDialogue) : base(toControl, currentDialogue) {
			
			curiousAboutMoodReaction.AddAction(new NPCCallbackAction(selectCuriousMoodChoice));
			
			assistGettingWood.AddAction(new NPCCallbackAction(helpCarpenterSon));
			assistGettingWood.AddAction(new UpdateDefaultTextAction(toControl, "Would you like to help make me a present for my Dad?"));
			
			helpAppreciated.AddAction(new NPCTakeItemAction(toControl));
			helpAppreciated.AddAction(new NPCCallbackAction(removeAllOtherReactions));
			helpAppreciated.AddAction(new UpdateCurrentTextAction(toControl, "Thanks!"));
			helpAppreciated.AddAction(new UpdateDefaultTextAction(toControl, "Come back later and I should have the rocking chair done!"));
			helpAppreciated.AddAction(new SetOffFlagAction(FlagStrings.carpenterSonMakesFatherProud));
			_allChoiceReactions.Add (curiousAboutMood, new DispositionDependentReaction(curiousAboutMoodReaction));
			//TODO: Replace Toolbox with piece of wood
			_allItemReactions.Add(StringsItem.Toolbox, new DispositionDependentReaction(helpAppreciated));
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
	}
	
	#endregion
	
	#endregion
}
