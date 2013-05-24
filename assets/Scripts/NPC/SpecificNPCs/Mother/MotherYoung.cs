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
	#region SetFlagReactions	
	protected override void SetFlagReactions(){		
		Reaction enterHappy = new Reaction();
		enterHappy.AddAction(new ShowOneOffChatAction(this, "I think there's a good spot over here!"));
		enterHappy.AddAction(new NPCAddScheduleAction(this, moveMotherHappyState));
		flagReactions.Add (FlagStrings.EnterHappyState, enterHappy);
		
		Reaction moveHome = new Reaction();
		moveHome.AddAction(new ShowOneOffChatAction(this, "Let's go back to the house.", 3f));
		moveHome.AddAction(new NPCAddScheduleAction(this, moveHomeSchedule));
		flagReactions.Add (FlagStrings.MoveHome, moveHome);
		
		Reaction gaveApple = new Reaction();
		gaveApple.AddAction(new ShowOneOffChatAction(this, "What a delicious looking apple!"));
		gaveApple.AddAction(new NPCAddScheduleAction(this, gaveAppleSchedule));
		flagReactions.Add (FlagStrings.GaveApple, gaveApple);
		
		Reaction giveSeeds = new Reaction();
		giveSeeds.AddAction(new ShowOneOffChatAction(this, "Here are the leftover seeds.",4f));
		giveSeeds.AddAction(new NPCAddScheduleAction(this, giveSeedsSchedule));
		giveSeeds.AddAction(new NPCEmotionUpdateAction(this, new AskToGardenState(this, "You're such a great help! Do you have more time to help me?")));
		flagReactions.Add (FlagStrings.GiveSeeds, giveSeeds);
		
		Reaction postRace = new Reaction();
		postRace.AddAction(new ShowOneOffChatAction(this, "Get over here you two!!"));
		postRace.AddAction(new NPCAddScheduleAction(this, postRaceSchedule));
		//postRace.AddAction(new NPCEmotionUpdateAction(this, new DummyState(this)));
		flagReactions.Add (FlagStrings.PostSiblingExplore, postRace);
		
		Reaction enterMadMother = new Reaction();
		enterMadMother.AddAction(new ShowOneOffChatAction(this, "I can't believe you.", 2f));
		enterMadMother.AddAction(new NPCAddScheduleAction(this, enterMadSchedule));
		flagReactions.Add (FlagStrings.EnterMadState, enterMadMother);
		
		Reaction exitMadMother = new Reaction();
		exitMadMother.AddAction(new ShowOneOffChatAction(this, "I can't believe you.", 2f));
		exitMadMother.AddAction(new NPCAddScheduleAction(this, exitMadSchedule));
		flagReactions.Add (FlagStrings.ExitMadState, exitMadMother);
		
		Reaction moveToMusicianReaction = new Reaction();
		moveToMusicianReaction.AddAction(new ShowOneOffChatAction(this, "Follow me. They live up here.", 4f));
		moveToMusicianReaction.AddAction(new NPCAddScheduleAction(this, moveToMusicianSchedule));
		moveToMusicianReaction.AddAction(new NPCAddScheduleAction(this, new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.MusicianYoung), 
			new YoungFarmerMotherToFarmerFatherOpenningScriptedDialogue(),Schedule.priorityEnum.DoNow)));
		flagReactions.Add(FlagStrings.MoveToMusician, moveToMusicianReaction);
		
	}
	#endregion
	
	protected override EmotionState GetInitEmotionState(){
		//return (new InitialEmotionState(this, "Morning! I think we're out of apples, could you get one for me?"));
		return (new GaveAppleState(this, "Thanks! Time to bake the pie! In the meantime, could you plant this for me?"));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}
	
	private Schedule gaveAppleSchedule;
	private Schedule giveSeedsSchedule;
	private Schedule enterMadSchedule;
	private Schedule exitMadSchedule;
	private Schedule moveHomeSchedule;
	private Schedule moveMotherHappyState;
	private Schedule moveToMusicianSchedule;
	private Schedule postRaceSchedule;
	
	#region Set Up Sechedules
	protected override void SetUpSchedules(){
		gaveAppleSchedule = new Schedule(this, Schedule.priorityEnum.DoNow);
		gaveAppleSchedule.Add (new TimeTask(1, new IdleState(this)));
		gaveAppleSchedule.Add(new Task(new MoveThenDoState(this, new Vector3(4f, -1f, -.5f), new MarkTaskDone(this))));
		
		giveSeedsSchedule = new Schedule(this, Schedule.priorityEnum.DoNow);
		giveSeedsSchedule.Add (new TimeTask(1, new IdleState(this)));
		giveSeedsSchedule.Add(new Task(new MoveThenDoState(this, new Vector3(0f, -1f, -.5f), new MarkTaskDone(this))));
		
		moveMotherHappyState = new Schedule(this, Schedule.priorityEnum.DoNow);
		moveMotherHappyState.Add (new TimeTask(1.5f, new IdleState(this)));
		moveMotherHappyState.Add(new Task(new MoveThenDoState(this, new Vector3(-20f, -1f, -.5f), new MarkTaskDone(this))));
		
		postRaceSchedule = new Schedule(this, Schedule.priorityEnum.DoNow);
		postRaceSchedule.Add(new Task(new IdleState(this)));
		
		moveHomeSchedule = new Schedule(this, Schedule.priorityEnum.DoNow);
		moveHomeSchedule.Add(new TimeTask(.5f, new IdleState(this)));
		moveHomeSchedule.Add(new Task(new MoveThenDoState(this, new Vector3(0, -1f,.3f), new MarkTaskDone(this))));
		moveHomeSchedule.Add(new Task(new MoveThenDoState(this, new Vector3(-.9f, -1f,.3f), new MarkTaskDone(this))));
		moveHomeSchedule.SetCanChat(true);
		enterMadSchedule = new Schedule (this, Schedule.priorityEnum.DoNow);
		//madSchedule.SetCanNotInteractWithPlayer();
		enterMadSchedule.SetCanChat(false);
		enterMadSchedule.Add(new Task(new MoveThenDoState(this, new Vector3(-2, -1f,.3f), new MarkTaskDone(this))));
		enterMadSchedule.Add (new TimeTask(10f, new IdleState(this)));
		enterMadSchedule.SetCanChat(true);
		Task setNotAngryFlag = (new Task(new MoveThenDoState(this, new Vector3 (0, .2f, .3f), new MarkTaskDone(this))));
		setNotAngryFlag.AddFlagToSet(FlagStrings.ExitMadState);
		enterMadSchedule.Add(setNotAngryFlag);
		enterMadSchedule.Add (new TimeTask(.5f, new IdleState(this)));
		
		moveToMusicianSchedule = new Schedule(this, Schedule.priorityEnum.DoNow);
		moveToMusicianSchedule.Add(new TimeTask(2f, new IdleState(this)));//this.transform.position
		//moveToMusicianSchedule.Add(new Task(new MoveThenDoState(this, new Vector3(5f, -1f,.3f), new MarkTaskDone(this))));
		Task setFlag = (new Task(new MoveThenDoState(this, new Vector3 (5, -1f, .3f), new MarkTaskDone(this))));
		setFlag.AddFlagToSet(FlagStrings.FinishMusicianConvo);
		moveToMusicianSchedule.Add(setFlag);
		moveToMusicianSchedule.Add(new TimeTask(2f, new IdleState(this)));
		//moveToMusicianSchedule.Add(new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.MusicianYoung), 
		//	new YoungFarmerMotherToFarmerFatherOpenningScriptedDialogue(),Schedule.priorityEnum.High));
		//moveToMusicianSchedule.Add(new TimeTask(.5f, new IdleState(this)));
		
// ADD EXIT MAD SCHEDULE
		
	}
	#endregion
	#region EmotionStates
		#region Initial Emotion State
		private class InitialEmotionState : EmotionState {
			Reaction GaveAppleReaction = new Reaction(); 
			//Reaction otherState;
	
			public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue) {
				GaveAppleReaction = new Reaction();
				GaveAppleReaction.AddAction(new NPCEmotionUpdateAction(toControl, new GaveAppleState(toControl," ")));
				//enterAppleState.AddAction(new NPCCallbackAction(UpdateText)); // ACTIVATING GaveApple Flag CREATES AN ERROR (From OneOffChat I believe)
				GaveAppleReaction.AddAction(new NPCTakeItemAction(toControl));
				_allItemReactions.Add("apple",  new DispositionDependentReaction(GaveAppleReaction));
			}	
		
		public void UpdateText() {
			FlagManager.instance.SetFlag(FlagStrings.GaveApple);
		}
	}
	
		#endregion
	#region Gave Emotion State
		private class GaveAppleState : EmotionState {
			Reaction AskToGardenReaction = new Reaction(); 
			Reaction otherState;
		
			public GaveAppleState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue) {
				SetDefaultText("Thanks so much! Now I can start baking!");
				
				SetOnCloseInteractionReaction(new DispositionDependentReaction(AskToGardenReaction));
				AskToGardenReaction.AddAction(new NPCGiveItemAction(toControl, StringsItem.LilySeeds)); // supposed to drop apple seeds (brown baggy?)
				AskToGardenReaction.AddAction(new ShowOneOffChatAction(toControl, "Here are the leftover seeds.", 3f));	
				AskToGardenReaction.AddAction(new NPCEmotionUpdateAction(toControl, new AskToGardenState(toControl,"I'm going to get the pie ready. Plant these seeds for me, ok?")));
				//enterPostInitialEmotionState.AddAction(new NPCCallbackAction(UpdateText));	
			}	
		
		public void UpdateText() {
			//FlagManager.instance.SetFlag(FlagStrings.GiveSeeds);
			//_allChoiceReactions.Remove(giveToolsChoiceSure);
			SetOnOpenInteractionReaction(new DispositionDependentReaction(otherState));
			//GUIManager.Instance.RefreshInteraction();
			//SetDefaultText("Care you dance?");
		}
	}
		#endregion
	#region PostInitialEmotionState
		private class AskToGardenState : EmotionState {
			int numberOfTimesBuggedMother;
			string currentDefaultText = "Ok, come back when you have some time! Remember, don't go near the cliffs!";
			
			Choice firstTimeBusy;
			Choice okChoice = new Choice ("Ok!", "Great! Could you plant these in the backyard? I'll be right back.");
			Reaction changeDefaultText = new Reaction();
			Reaction enterMadReaction = new Reaction();
			Reaction enterHappyReaction = new Reaction();
		
			public AskToGardenState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue) {		
				numberOfTimesBuggedMother = 0;
				
				enterMadReaction.AddAction(new NPCEmotionUpdateAction(toControl, new MadEmotionState(toControl)));
				enterHappyReaction.AddAction(new NPCEmotionUpdateAction(toControl, new HappyEmotionState(toControl)));
				changeDefaultText.AddAction(new NPCCallbackAction(UpdateText));
				firstTimeBusy = new Choice("I'm Busy!", currentDefaultText);		
				_allChoiceReactions.Add(okChoice, new DispositionDependentReaction(enterHappyReaction));
				_allChoiceReactions.Add((firstTimeBusy), new DispositionDependentReaction(changeDefaultText));		
			}
//Framework for a responsive yes/no system
			
			public void UpdateText() {
				numberOfTimesBuggedMother++;
				if (numberOfTimesBuggedMother == 1) {
					SetDefaultText("Are you free now?");
					_allChoiceReactions.Remove(okChoice);
					okChoice = new Choice("I'm free!", "Great! Could you plant these in the backyard? I'll be right back.");
					_allChoiceReactions.Add(okChoice, new DispositionDependentReaction(enterHappyReaction));
					GUIManager.Instance.RefreshInteraction();
					currentDefaultText = "Alright, I need your help, so come back soon.";
				}
			
				if (numberOfTimesBuggedMother == 2) {
					SetDefaultText("Oh good, you're back!!");
					currentDefaultText = "This isn't a game, I really do need help.";
					//FlagManager.instance.SetFlag(FlagStrings.SiblingExplore);
				}
			
				if (numberOfTimesBuggedMother == 3) {
					SetDefaultText("Good you're back, the seeds are over there.");
					currentDefaultText = "If you're busy, stop bugging me.";
					_allChoiceReactions.Remove(firstTimeBusy);
					firstTimeBusy = new Choice ("I'm Busy!!", currentDefaultText);
					_allChoiceReactions.Add((firstTimeBusy), new DispositionDependentReaction(changeDefaultText));
					GUIManager.Instance.RefreshInteraction();
					
// FIX THIS LINE
					FlagManager.instance.SetFlag(FlagStrings.EnterMadState);
//					
					_allChoiceReactions.Remove(firstTimeBusy);
					_allChoiceReactions.Add((firstTimeBusy), new DispositionDependentReaction(enterMadReaction));
				}
			
				if (numberOfTimesBuggedMother >= 1 && numberOfTimesBuggedMother <= 2) { 
					_allChoiceReactions.Remove(firstTimeBusy);
					firstTimeBusy = new Choice ("Busy mom!!", currentDefaultText);
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
	
		}
	}
		#endregion
		#region HappyEmotionState
	private class HappyEmotionState : EmotionState {
		
		bool flagSet = false;
		Choice testing;
		
		Reaction changeFlagReaction = new Reaction();
		Reaction postSeedReaction = new Reaction();
		
		public HappyEmotionState(NPC toControl) : base(toControl, "Do you want me to show you where to plant them?"){;
			testing = new Choice ("Where?", "Follow me, There's a good spot over here.");
			postSeedReaction.AddAction(new NPCEmotionUpdateAction(toControl, new PlantTreeState(toControl)));
			//postSeed.AddAction(new NPCTakeItemAction(toControl));
			changeFlagReaction.AddAction(new NPCCallbackAction(UpdateFlag));
			_allChoiceReactions.Add((testing), new DispositionDependentReaction(changeFlagReaction));
			//GUIManager.Instance.RefreshInteraction();
		}
		
		public void UpdateFlag() {
			
			if (!flagSet) {
				//FlagManager.instance.SetFlag(FlagStrings.SiblingExplore);
				FlagManager.instance.SetFlag(FlagStrings.EnterHappyState);	
				flagSet = true;
				_allChoiceReactions.Remove(testing);
				SetDefaultText("Right here would be good!");
				testing = new Choice ("*plant seed*", "You looked like you were having fun with that!");
				_allChoiceReactions.Add((testing), new DispositionDependentReaction(postSeedReaction));
				GUIManager.Instance.RefreshInteraction();
			}
		}
	}
	#endregion
	#region PLantTreeState
	private class PlantTreeState : EmotionState {
		Action evokeUpdatePosition;
		bool flagSet = false;
		bool piePlaced = false;
		//Choice goBackHome = new Choice("Mm hmm!", "Here, I'm afraid money is a little scarce, but you might be able to exchange this pendant for some seeds.");
		Choice goBackHome = new Choice("Mm hmm!", "I think the pie should be done, let's go back to check!");
		Choice otherChoice;
		//Reaction changeFlag;
		Reaction otherReaction;
		
		public PlantTreeState(NPC toControl) : base(toControl, "I can't wait to see how beautiful this tree will be in a few years!"){
			//evokeUpdatePosition = new NPCCallbackAction(UpdatePosition);
			//evokeUpdatePosition.Perform();
			//changeFlag = new Reaction();
			otherReaction = new Reaction();

			//changeFlag.AddAction(new NPCCallbackAction(UpdatePosition));
			//SetOnCloseInteractionReaction(new DispositionDependentReaction(otherReaction));
			otherReaction.AddAction(new NPCCallbackOnNPCAction(InitiatePieQuest, toControl));
			otherReaction.AddAction(new NPCEmotionUpdateAction(toControl, new PieQuestState(toControl)));
			//otherReaction.AddAction(new NPCGiveItemAction(toControl,"apple")); //switch to pendant later
			_allChoiceReactions.Add(goBackHome, new DispositionDependentReaction(otherReaction));
		}
		public void InitiatePieQuest(NPC toControl) {
			if (!piePlaced) {
				//SetDefaultText("Here's the pie!");
				FlagManager.instance.SetFlag(FlagStrings.MoveHome);
				//activate flag to move mom back home.
				piePlaced = true;
				_allChoiceReactions.Remove(goBackHome);
				GUIManager.Instance.RefreshInteraction();
				GUIManager.Instance.CloseInteractionMenu();
			}
			//goBackHome = new Choice("Sure!", "Looks like you had fun!");
			//otherChoice = new Choice("(X.X)", "Shouldn't be able to see this initialization");
			//_allChoiceReactions.Add(goBackHome, new DispositionDependentReaction(changeFlag));
			//_allChoiceReactions.Add(otherChoice, new DispositionDependentReaction(changeFlag));
			
			//FlagManager.instance.SetFlag(FlagStrings.MoveHome);
			//SetDefaultText("Get the seeds, then I'll tell you the story");
			//_allChoiceReactions.Remove(goBackHome);
			//_allChoiceReactions.Remove(otherChoice);
			//GUIManager.Instance.RefreshInteraction();
			//_allItemReactions.Add("", new DispositionDependentReaction(otherReaction));
		}
	}
		
		/*
		public void UpdatePosition() {
			if (!flagSet) {
				FlagManager.instance.SetFlag(FlagStrings.MoveHome);
				flagSet = true;
				//SetDefaultText("If you go into the market and buy mommy some more seeds, I'll tell you a story when you get back.");
				_allChoiceReactions.Remove(goBackHome);
				//goBackHome = new Choice("Ok!", "The story is a secret till you come back with more seeds to plant.");
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
		*/
		
		#endregion
	private class PieQuestState : EmotionState {
		bool flagSet = false;
		//Choice goBackHome = new Choice("Mm hmm!", "Here, I'm afraid money is a little scarce, but you might be able to exchange this pendant for some seeds.");
		Choice goBackHome = new Choice("Mm hmm!", "I think the pie should be done, let's go back to check!");
		Reaction moveToMusicianReaction = new Reaction();
		
		public PieQuestState(NPC toControl) : base(toControl, "Let's bring the pie to our new neighbors! Follow me.") {
			
			moveToMusicianReaction.AddAction(new NPCCallbackAction(SetWalkToMusicianFlag));
			SetOnOpenInteractionReaction(new DispositionDependentReaction(moveToMusicianReaction));
			//evokeUpdatePosition = new NPCCallbackAction(UpdatePosition);
			//evokeUpdatePosition.Perform();
			//SetOnCloseInteractionReaction(new DispositionDependentReaction(otherReaction));
			//otherReaction.AddAction(new NPCCallbackOnNPCAction(InitiatePieQuest, toControl));
			//otherReaction.AddAction(new NPCGiveItemAction(toControl,"apple")); //switch to pendant later
			//_allChoiceReactions.Add(goBackHome, new DispositionDependentReaction(otherReaction));
		}
		
		public void SetWalkToMusicianFlag() {
			if (!flagSet) {
				FlagManager.instance.SetFlag(FlagStrings.MoveToMusician);
				flagSet = true;
			}
		}
}
	#endregion
}
