using UnityEngine;
using System.Collections;

/// <summary>
/// Mother young specific scripting values
/// </summary>
public class MotherYoung : NPC {
	protected override void Init() {
		id = NPCIDs.MOTHER;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		//Reaction frogCrushing = new Reaction();
		//frogCrushing.AddAction(new ShowOneOffChatAction(this, "Gross! I'm out of here."));
		//frogCrushing.AddAction(new NPCAddScheduleAction(this, runToCarpenter));
		//flagReactions.Add(FlagStrings.CrushFrog, frogCrushing); 
		
		Reaction enterHappy = new Reaction();
		enterHappy.AddAction(new ShowOneOffChatAction(this, "I think there's a good spot over here!"));
		enterHappy.AddAction(new NPCAddScheduleAction(this, moveMotherHappyState));
		flagReactions.Add (FlagStrings.EnterHappyState, enterHappy);
		
		Reaction moveHome = new Reaction();
		moveHome.AddAction(new ShowOneOffChatAction(this, "Let's go back to the house."));
		moveHome.AddAction(new NPCAddScheduleAction(this, moveHomeSchedule));
		flagReactions.Add (FlagStrings.MoveHome, moveHome);
		
		Reaction gaveApple = new Reaction();
		gaveApple.AddAction(new ShowOneOffChatAction(this, "What a delicious looking apple!"));
		gaveApple.AddAction(new NPCAddScheduleAction(this, gaveAppleSchedule));
		flagReactions.Add (FlagStrings.GaveApple, gaveApple);
		
		Reaction giveSeeds = new Reaction();
		giveSeeds.AddAction(new ShowOneOffChatAction(this, "Here are the leftover seeds."));
		giveSeeds.AddAction(new NPCAddScheduleAction(this, giveSeedsSchedule));
		giveSeeds.AddAction(new NPCEmotionUpdateAction(this, new PostInitialEmotionState(this, "You're such a great help! Do you have more time to help me?")));
		flagReactions.Add (FlagStrings.GiveSeeds, giveSeeds);
		
		Reaction postRace = new Reaction();
		postRace.AddAction(new ShowOneOffChatAction(this, "Get over here you two!!"));
		postRace.AddAction(new NPCAddScheduleAction(this, postRaceSchedule));
		postRace.AddAction(new NPCEmotionUpdateAction(this, new DummyState(this)));
		flagReactions.Add (FlagStrings.PostSiblingExplore, postRace);
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Morning! I think we're out of apples, could you go get one for me?"));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}
	
	private Schedule gaveAppleSchedule;
	private Schedule giveSeedsSchedule;
	private Schedule moveMotherHappyState;
	private Schedule postRaceSchedule;
	private Schedule moveHomeSchedule;

	
	protected override void SetUpSchedules(){
		
		
		gaveAppleSchedule = new Schedule(this, Schedule.priorityEnum.DoNow);
		gaveAppleSchedule.Add (new TimeTask(1, new IdleState(this)));
		gaveAppleSchedule.Add(new Task(new MoveThenDoState(this, new Vector3(4f, -1f, -.5f), new MarkTaskDone(this))));
		
		giveSeedsSchedule = new Schedule(this, Schedule.priorityEnum.DoNow);
		giveSeedsSchedule.Add (new TimeTask(1, new IdleState(this)));
		giveSeedsSchedule.Add(new Task(new MoveThenDoState(this, new Vector3(-5f, -1f, -.5f), new MarkTaskDone(this))));
		
		moveMotherHappyState = new Schedule(this, Schedule.priorityEnum.DoNow);
		moveMotherHappyState.Add (new TimeTask(1.5f, new IdleState(this)));
		moveMotherHappyState.Add(new Task(new MoveThenDoState(this, new Vector3(-20f, -1f, -.5f), new MarkTaskDone(this))));
		
		postRaceSchedule = new Schedule(this, Schedule.priorityEnum.DoNow);
		postRaceSchedule.Add(new Task(new IdleState(this)));
		
		moveHomeSchedule = new Schedule(this, Schedule.priorityEnum.DoNow);
		moveHomeSchedule.Add(new TimeTask(.5f, new IdleState(this)));
		moveHomeSchedule.Add(new Task(new MoveThenDoState(this, new Vector3(0, -1f,.3f), new MarkTaskDone(this))));
		moveHomeSchedule.Add(new Task(new MoveThenDoState(this, new Vector3(-.9f, -1f,.3f), new MarkTaskDone(this))));
	}
	
	#region EmotionStates
		#region Initial Emotion State
		private class InitialEmotionState : EmotionState {
			Reaction enterAppleState; 
			Reaction otherState;
	
			public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue) {
				enterAppleState = new Reaction();
				//otherState = new Reaction();
				
				//otherState.AddAction(new NPCGiveItemAction(toControl, "apple"));
				//_allChoiceReactions.Add(new Choice ("Spawn Apple", "Spawning Apple, Clink, Clink, Clink. Buhjunk."), new DispositionDependentReaction(otherState));
			
				enterAppleState.AddAction(new NPCEmotionUpdateAction(toControl, new GaveAppleState(toControl," ")));
				//enterAppleState.AddAction(new NPCCallbackAction(UpdateText)); // ACTIVATING GaveApple Flag CREATES AN ERROR (From OneOffChat I believe)
				enterAppleState.AddAction(new NPCTakeItemAction(toControl));
				_allItemReactions.Add("apple",  new DispositionDependentReaction(enterAppleState));
			
				otherState = new Reaction();
				otherState.AddAction(new NPCEmotionUpdateAction(toControl, new PostInitialEmotionState(toControl,"You're such a great help! Do you have more time to help me?")));
				//otherState.AddAction(new ShowOneOffChatAction(toControl, "Let's go back to the house."));
			}	
		
		public void UpdateText() {
			FlagManager.instance.SetFlag(FlagStrings.GaveApple);
		}
	}
	
		#endregion
	#region Gave Emotion State
		private class GaveAppleState : EmotionState {
			Reaction enterPostInitialEmotionState; 
			Reaction otherState;
		
			public GaveAppleState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue) {
				SetDefaultText("Thanks so much! Now I can start baking!");
				enterPostInitialEmotionState = new Reaction();
				
				enterPostInitialEmotionState.AddAction(new NPCGiveItemAction(toControl, "seeds")); // supposed to drop apple seeds (brown baggy?)
				enterPostInitialEmotionState.AddAction(new NPCCallbackAction(UpdateText));
				
				otherState = new Reaction();
				otherState.AddAction(new NPCEmotionUpdateAction(toControl, new PostInitialEmotionState(toControl,"You're such a great help! Do you have more time to help me?")));
				//otherState.AddAction(new ShowOneOffChatAction(toControl, "Let's go back to the house."));
			
				_allChoiceReactions.Add(new Choice ("Switch States", "You're such a great help! Do you have more time to help me?"), new DispositionDependentReaction(enterPostInitialEmotionState));	
			}	
		
		public void UpdateText() {
			FlagManager.instance.SetFlag(FlagStrings.GiveSeeds);
			//_allChoiceReactions.Remove(giveToolsChoiceSure);
			SetOnOpenInteractionReaction(new DispositionDependentReaction(otherState));
			//GUIManager.Instance.RefreshInteraction();
			//SetDefaultText("Care you dance?");
			
		}
	}
	
		#endregion
		
	#region PostInitialEmotionState
		private class PostInitialEmotionState : EmotionState {
			int numberOfTimesBuggedMother;
			string text1 = "Ok, come back when you have some time! Remember, don't go near the cliffs!";
			
			Choice firstTimeBusy;
			Reaction changeDefaultText;
			Reaction enterMadState;
			Reaction enterHappyState;
		
			public PostInitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
				changeDefaultText = new Reaction();
				enterMadState = new Reaction();
				enterHappyState = new Reaction();
			
				numberOfTimesBuggedMother = 0;
				
				enterMadState.AddAction(new NPCEmotionUpdateAction(toControl, new MadEmotionState(toControl)));
				enterHappyState.AddAction(new NPCEmotionUpdateAction(toControl, new HappyEmotionState(toControl)));
				changeDefaultText.AddAction(new NPCCallbackAction(UpdateText));
				firstTimeBusy = new Choice("I'm Busy!", text1);
				_allChoiceReactions.Add((firstTimeBusy), new DispositionDependentReaction(changeDefaultText));		
				_allChoiceReactions.Add(new Choice("I'm free!", "Great! Could you plant these in the backyard?"), new DispositionDependentReaction(enterHappyState));
	
			}
			
			public void UpdateText() {
				numberOfTimesBuggedMother++;
				//Debug.Log(numberOfTimesBuggedMother);
				if (numberOfTimesBuggedMother == 1) {
					SetDefaultText("Are you free now?");
					text1 = "Alright, I need your help, so come back soon.";
				}
			
				if (numberOfTimesBuggedMother == 2) {
					SetDefaultText("Oh good, you're back!!");
					text1 = "This isn't a game, I really do need help.";
					FlagManager.instance.SetFlag(FlagStrings.SiblingExplore);
				}
			
				if (numberOfTimesBuggedMother == 3) {
					SetDefaultText("Good you're back, the seeds are over there.");
					text1 = "If you're busy, stop bugging me.";
					_allChoiceReactions.Remove(firstTimeBusy);
					firstTimeBusy = new Choice ("I'm Busy!!", text1);
					_allChoiceReactions.Add((firstTimeBusy), new DispositionDependentReaction(changeDefaultText));
					GUIManager.Instance.RefreshInteraction();
				
					_allChoiceReactions.Remove(firstTimeBusy);
					_allChoiceReactions.Add((firstTimeBusy), new DispositionDependentReaction(enterMadState));
				}
			
				if (numberOfTimesBuggedMother >= 1 && numberOfTimesBuggedMother <= 2) { 
					_allChoiceReactions.Remove(firstTimeBusy);
					firstTimeBusy = new Choice ("Busy mom!!", text1);
					_allChoiceReactions.Add((firstTimeBusy), new DispositionDependentReaction(changeDefaultText));
					GUIManager.Instance.RefreshInteraction();
				}
			}
		}
	#endregion
	
		#region MadEmotionState
	private class MadEmotionState : EmotionState {
		Action evokeDisplayMad;
		
		public MadEmotionState(NPC toControl) : base(toControl, "I've had enough of your behavior today. Keep this up and you won't be getting dinner tonight."){
			evokeDisplayMad = new NPCCallbackAction(displayMad);
			evokeDisplayMad.Perform();
		}
		
		public void displayMad() {
			//Stall the mother for 15 seconds, then transition her back to a new state
			Schedule angryMom = new Schedule(_npcInState, Schedule.priorityEnum.DoNow);
			//Action displayAnger = new ShowOneOffChatAction(_npcInState, "That isn't how you treat your mother!");
			angryMom.Add(new TimeTask(15, new IdleState(_npcInState)));
//how can I transition back to another emotion state?			
			//angryMom.Add(new Task(
			//_npcInState.AddSchedule(angryMom);
		}
	}
		#endregion
		#region PLantTreeState
	private class PlantTreeState : EmotionState {
		Action evokeUpdatePosition;
		bool flagSet = false;
		Choice goBackHome;
		Choice otherChoice;
		Reaction changeFlag;
		Reaction otherReaction;
		
		public PlantTreeState(NPC toControl) : base(toControl, "I raised you well :')"){
			//evokeUpdatePosition = new NPCCallbackAction(UpdatePosition);
			//evokeUpdatePosition.Perform();
			changeFlag = new Reaction();
			otherReaction = new Reaction();
			changeFlag.AddAction(new NPCCallbackAction(UpdatePosition));
			otherReaction.AddAction(new NPCCallbackAction(initiateSeedQuest));
			goBackHome = new Choice("All Done!", "Looks like you had fun!");
			otherChoice = new Choice("(X.X)", "Shouldn't be able to see this initialization");;
			_allChoiceReactions.Add(goBackHome, new DispositionDependentReaction(changeFlag));
			//_allItemReactions.Add ("apple", 11);
			//.AddAction(new UpdateCurrentTextAction(npcInState, "Why thank you!"));
		}
		
		public void UpdatePosition() {
			if (!flagSet) {
				FlagManager.instance.SetFlag(FlagStrings.MoveHome);
				flagSet = true;
				SetDefaultText("If you go into the market and buy mommy some more seeds, I'll tell you a story when you get back.");
				_allChoiceReactions.Remove(goBackHome);
				goBackHome = new Choice("Ok!", "The story is a secret till you come back with more seeds to plant.");
				GUIManager.Instance.RefreshInteraction();
				
				otherChoice = new Choice("Not now", "Ok, well if you change your mind, I'll be here for a little while.");
				//otherReaction.Add();
				_allChoiceReactions.Add(goBackHome, new DispositionDependentReaction(otherReaction));
				_allChoiceReactions.Add(otherChoice, new DispositionDependentReaction(changeFlag));
				
			}
			//Schedule moveMyMomma = new Schedule(_npcInState, Schedule.priorityEnum.High);
			//moveMyMomma.Add(new Task(new MoveThenDoState(_npcInState, new Vector3(10, -1f,.3f), new MarkTaskDone(_npcInState))));
			//_npcInState.AddSchedule(moveMyMomma);
		}
		
		public void initiateSeedQuest() {
				//FlagManager.instance.SetFlag(FlagStrings.MoveHome);
				SetDefaultText("Get the seeds, then I'll tell you the story");
				_allChoiceReactions.Remove(goBackHome);
				_allChoiceReactions.Remove(otherChoice);
				GUIManager.Instance.RefreshInteraction();
				_allItemReactions.Add("", new DispositionDependentReaction(otherReaction));
		}
		
	}
	
		#endregion
	#region Dummy State
	public class DummyState : EmotionState {
		
		//bool flagSet = false;
		Choice testing;
		
		Reaction changeFlag;
		Reaction postSeed;
		
		public DummyState(NPC toControl) : base(toControl, "I'm a dummy"){
			SetDefaultText("I told you two not to go up there! ..");
		}
	}
		#endregion
	
		#region HappyEmotionState
	private class HappyEmotionState : EmotionState {
		
		bool flagSet = false;
		Choice testing;
		
		Reaction changeFlag;
		Reaction postSeed;
		
		public HappyEmotionState(NPC toControl) : base(toControl, "Go find somewhere to plant the seeds."){
			changeFlag = new Reaction();
			postSeed = new Reaction();
			testing = new Choice ("Where?", "Follow me, There's a good spot over here.");
			postSeed.AddAction(new NPCEmotionUpdateAction(toControl, new PlantTreeState(toControl)));
			changeFlag.AddAction(new NPCCallbackAction(UpdateFlag));
			_allChoiceReactions.Add((testing), new DispositionDependentReaction(changeFlag));
			//GUIManager.Instance.RefreshInteraction();
		}
		
		public void UpdateFlag() {
			
			if (!flagSet) {
				FlagManager.instance.SetFlag(FlagStrings.SiblingExplore);
				FlagManager.instance.SetFlag(FlagStrings.EnterHappyState);	
				flagSet = true;
				_allChoiceReactions.Remove(testing);
				SetDefaultText("Right here would be good!");
				testing = new Choice ("*plant seed*", "Beautiful! I can't wait to see how beautiful this tree will be in a few years!");
				_allChoiceReactions.Add((testing), new DispositionDependentReaction(postSeed));
				GUIManager.Instance.RefreshInteraction();
				
			}
		}
	}
		#endregion
	#endregion
}
