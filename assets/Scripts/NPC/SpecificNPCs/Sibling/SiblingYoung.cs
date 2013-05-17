using UnityEngine;
using System.Collections;

/// <summary>
/// Sibling young specific scripting values
/// </summary>
public class SiblingYoung : NPC {
	protected override void Init() {
		id = NPCIDs.SIBLING;
		base.Init();
	}
	//
	#region Set Flag Reactions
	public string currentPassiveText;
	//private bool initScheduleTriggered = false; //if the race is initiated via chatBox, it begins a new schedule
	
	protected override void SetFlagReactions() {
		#region Race To Carpenter House
		Reaction raceToCarpenterHouse = new Reaction();
		raceToCarpenterHouse.AddAction(new NPCEmotionUpdateAction(this, new InitialEmotionState(this, "Careful, the Carpenter is really mean!")));
		raceToCarpenterHouse.AddAction(new ShowOneOffChatAction(this, "These are our neighbors!", 2f));
		raceToCarpenterHouse.AddAction(new NPCAddScheduleAction(this, carpenterRaceSchedule));
		flagReactions.Add(FlagStrings.RunToCarpenter, raceToCarpenterHouse);
		
		Reaction nothing = new Reaction();
		flagReactions.Add(FlagStrings.StartedRace, nothing);
		#endregion
		#region Race To Beach House
		Reaction raceToBeachHouse = new Reaction();
		raceToBeachHouse.AddAction(new NPCEmotionUpdateAction(this, new InitialEmotionState(this, "The beach is beautiful, isn't it?")));
		raceToBeachHouse.AddAction(new ShowOneOffChatAction(this, "Let's go to the beach!", 2f));
		raceToBeachHouse.AddAction(new NPCAddScheduleAction(this, walkToBeach));
		flagReactions.Add(FlagStrings.RunToBeach, raceToBeachHouse);
		
		//Reaction nothingTwo = new Reaction();
		//flagReactions.Add(FlagStrings.StartedRace, nothingTwo);
		#endregion
		#region Race To Farmer House
		Reaction raceToFarmerHouse = new Reaction();
		raceToFarmerHouse.AddAction(new NPCEmotionUpdateAction(this, new InitialEmotionState(this, "Careful, the Farmer is really mean!")));
		raceToFarmerHouse.AddAction(new ShowOneOffChatAction(this, "Follow me!", 2f));
		raceToFarmerHouse.AddAction(new NPCAddScheduleAction(this, walkToFarmerHouse));
		flagReactions.Add(FlagStrings.RunToFarmer, raceToFarmerHouse);
		
		//Reaction nothingTwo = new Reaction();
		//flagReactions.Add(FlagStrings.StartedRace, nothingTwo);
		#endregion
		#region Race To Market House
		Reaction raceToMarket = new Reaction();
		raceToMarket.AddAction(new NPCEmotionUpdateAction(this, new InitialEmotionState(this, "Let's trade something!")));
		raceToMarket.AddAction(new ShowOneOffChatAction(this, "Let's go to the market!", 2f));
		raceToMarket.AddAction(new NPCAddScheduleAction(this, walkToMarket));
		flagReactions.Add(FlagStrings.RunToMarket, raceToMarket);
		
		//Reaction nothingTwo = new Reaction();
		//flagReactions.Add(FlagStrings.StartedRace, nothingTwo);
		#endregion	
		#region Race To Windmill
		Reaction raceToWindmill = new Reaction();
		raceToWindmill.AddAction(new NPCEmotionUpdateAction(this, new InitialEmotionState(this, "Aww, no music today.")));
		raceToWindmill.AddAction(new ShowOneOffChatAction(this, "Want to here some music? Over here!", 2f));
		raceToWindmill.AddAction(new NPCAddScheduleAction(this, walkToWindmill));
		flagReactions.Add(FlagStrings.RunToWindmill, raceToWindmill);
		
		//Reaction nothingTwo = new Reaction();
		//flagReactions.Add(FlagStrings.StartedRace, nothingTwo);
		#endregion
		#region Race To Reflection Tree
		Reaction raceToReflectionTree = new Reaction();
		raceToReflectionTree.AddAction(new NPCEmotionUpdateAction(this, new InitialEmotionState(this, "This is the best place on the island! I always come up here!")));
		raceToReflectionTree.AddAction(new ShowOneOffChatAction(this, "I'm going to beat you!", 2f));
		raceToReflectionTree.AddAction(new NPCAddScheduleAction(this, walkToReflectionTree));
		flagReactions.Add(FlagStrings.RunToReflectionTree, raceToReflectionTree);
		
		//Reaction nothingTwo = new Reaction();
		//flagReactions.Add(FlagStrings.StartedRace, nothingTwo);
		#endregion
		#region Race To Home
		Reaction raceToHome = new Reaction();
		raceToHome.AddAction(new NPCEmotionUpdateAction(this, new InitialEmotionState(this, "Time to go home!")));
		raceToHome.AddAction(new ShowOneOffChatAction(this, "Race you home!", 2f));
		raceToHome.AddAction(new NPCAddScheduleAction(this, walkToHome));
		flagReactions.Add(FlagStrings.RunToHome, raceToHome);
		
		//Reaction nothingTwo = new Reaction();
		//flagReactions.Add(FlagStrings.StartedRace, nothingTwo);
		#endregion
		#region Get in Trouble
		Reaction getInTrouble = new Reaction();
		getInTrouble.AddAction(new NPCEmotionUpdateAction(this, new InitialEmotionState(this, "Opps, I forgot mom told us not to go up there.")));
		getInTrouble.AddAction(new ShowOneOffChatAction(this, "Uh Oh!!!", 2f));
		//getInTrouble.AddAction(new NPCAddScheduleAction(this, walkToHome));
		flagReactions.Add(FlagStrings.PostSiblingExplore, getInTrouble);
		
		//Reaction nothingTwo = new Reaction();
		//flagReactions.Add(FlagStrings.StartedRace, nothingTwo);
		#endregion
		
	}
	#endregion
	protected override EmotionState GetInitEmotionState() {
		// change portrait picture
		return (new InitialEmotionState(this, "Hey there! Are you ready to race!?"));
	}

	protected override Schedule GetSchedule(){
		Schedule schedule = new Schedule(this, Schedule.priorityEnum.DoNow);
		Task initialSchedule = new TimeTask(1f, new IdleState(this));
		schedule.Add(initialSchedule);
		//TimeTask exploringNearHome = new TimeTask(10, new IdleState(this));
		//Task moveToBridge = new Task(new MoveThenDoState(this, new Vector3(5, .2f, .3f), new MarkTaskDone(this)));
		//schedule.Add(exploringNearHome);
		//schedule.Add(moveToBridge);
		return (schedule);
	}
	
	private Schedule carpenterRaceSchedule;
	private Schedule walkToCarpenter;
	private Schedule walkToBeach;
	private Schedule walkToFarmerHouse;
	private Schedule walkToMarket;
	private Schedule walkToWindmill;
	private Schedule walkToReflectionTree;
	private Schedule walkToHome;
	
	#region Set Up Sechedules
	protected override void SetUpSchedules() {
		Schedule test = (new YoungRunIslandToCarpenterScript(this));
		scheduleStack.Add(test);
		
		// Changing an emotion state 
		//carpenterRaceSchedule.Add(new TimeTask(10f, new ChangeEmotionState(this, new InitialEmotionState(this))));
		//walkToCarpenter = (new YoungRunIslandToCarpenterScript(this));
		
		/*
		walkToCarpenter = new Schedule(this, Schedule.priorityEnum.Low);
		walkToCarpenter.RemoveScheduleWithFlag(FlagStrings.StartedRace);
		walkToCarpenter.Add(new TimeTask(.1f, new IdleState(this))); //or self-triggering
		walkToCarpenter.Add(new Task(new MoveThenDoState(this, new Vector3 (12, .2f, .3f), new MarkTaskDone(this))));
		walkToCarpenter.Add(new TimeTask(.2f, new IdleState(this)));
		walkToCarpenter.Add(new Task(new MoveThenDoState(this, new Vector3 (11.8f, .2f, .3f), new MarkTaskDone(this)))); // at bridge
		walkToCarpenter.Add(new TimeTask(10f, new WaitTillPlayerCloseState(this, this.player)));
		Task reachCarpenterTask = new Task(new MoveThenDoState(this, new Vector3 (28, .2f, .3f), new MarkTaskDone(this))); // at carpenter
		//reachCarpenterTask.AddFlagToSet(FlagStrings.StartedRace);
		walkToCarpenter.Add(reachCarpenterTask);
		walkToCarpenter.Add(new TimeTask(2f, new IdleState(this)));
		walkToCarpenter.Add(new Task (new IdleState(this)));
		*/
		
		carpenterRaceSchedule = new Schedule(this, Schedule.priorityEnum.Medium); 
		carpenterRaceSchedule.Add(new TimeTask(1f, new IdleState(this)));
		carpenterRaceSchedule.Add(new Task(new MoveThenDoState(this, new Vector3 (27, .2f, .3f), new MarkTaskDone(this))));
		carpenterRaceSchedule.Add(new TimeTask(1f, new IdleState(this)));
		carpenterRaceSchedule.Add(new Task(new MoveThenDoState(this, new Vector3 (29, .2f, .3f), new MarkTaskDone(this))));
		carpenterRaceSchedule.Add(new TimeTask(.5f, new IdleState(this)));
		carpenterRaceSchedule.Add(new Task(new MoveThenDoState(this, new Vector3 (27, .2f, .3f), new MarkTaskDone(this))));
		carpenterRaceSchedule.Add(new TimeTask(.5f, new IdleState(this)));
		Task setOffBeachFlag = (new Task(new MoveThenDoState(this, new Vector3 (29, .2f, .3f), new MarkTaskDone(this))));
		setOffBeachFlag.AddFlagToSet(FlagStrings.RunToBeach);
		carpenterRaceSchedule.Add(setOffBeachFlag);
		carpenterRaceSchedule.Add(new TimeTask(.2f, new IdleState(this)));
		
		walkToBeach = (new YoungRunIslandToBeachScript(this));
		walkToFarmerHouse = (new YoungRunIslandToFarmerScript(this));
		walkToMarket = (new YoungRunIslandToMarketScript(this));
		walkToWindmill = (new YoungRunIslandToWindmillScript(this));
		walkToReflectionTree = (new YoungRunIslandToReflectionTreeScript(this));
		walkToHome = (new YoungRunIslandToHomeScript(this));
		
		/*
		initialSchedule.Add(new TimeTask(.5f, new IdleState(this)));
		initialSchedule.Add(new Task(new MoveThenDoState(this, new Vector3 (6, .2f, .3f), new MarkTaskDone(this))));
			//Add (new TimeTask(5, new WaitTillPlayerCloseState(this));
		initialSchedule.Add(new TimeTask(.5f, new IdleState(this)));
		initialSchedule.Add(new Task(new MoveThenDoState(this, new Vector3 (4, .2f, .3f), new MarkTaskDone(this))));
		initialSchedule.Add(new TimeTask(.5f, new IdleState(this)));
		initialSchedule.Add(new Task(new MoveThenDoState(this, new Vector3 (11, .2f, .3f), new MarkTaskDone(this))));
		initialSchedule.Add(new Task(new MoveThenDoState(this, new Vector3 (10.90f, .2f, .3f), new MarkTaskDone(this)))); // at bridge
		initialSchedule.Add(new TimeTask(10f, new WaitTillPlayerCloseState(this, this.player)));
		scheduleStack.Add (initialSchedule);
		*/
		
	}
	#endregion
	
	#region EmotionStates
		#region Initial Emotion State
		private class InitialEmotionState : EmotionState {
			bool flagSet = false;	
			Reaction activateWalkToBridgeState;
			Reaction changeDefaultText;
			string str_readyToRace = "Go!!!";
		
			public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
				activateWalkToBridgeState = new Reaction();
				//changeDefaultText = new Reaction();		
				//changeDefaultText.AddAction(new NPCCallbackAction(UpdateText));
				
				activateWalkToBridgeState.AddAction(new NPCEmotionUpdateAction(toControl, new WalkToBridgeState(toControl)));	
				activateWalkToBridgeState.AddAction(new NPCCallbackAction(updateFlag));
				_allChoiceReactions.Add(new Choice("Ready to keep going?", str_readyToRace), new DispositionDependentReaction(activateWalkToBridgeState));		
				_allChoiceReactions.Add(new Choice("Hold on.", str_readyToRace), new DispositionDependentReaction(activateWalkToBridgeState));
			}
		
			public void updateFlag() {
				if (!flagSet) {
					FlagManager.instance.SetFlag(FlagStrings.RunToCarpenter);
					FlagManager.instance.SetFlag(FlagStrings.StartedRace);
					flagSet = true;
					SetDefaultText("I'm going to beat you!!!");
				}
			}
		}
		#endregion
	
		#region Initial Emotion State
		private class NearCarpenterState : EmotionState {
			bool flagSet = false;	
			Reaction activateWalkToBridgeState;
			Reaction changeDefaultText;
			string str_readyToRace = "Go!!!";
		
			public NearCarpenterState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
				SetDefaultText("These are our neighbors!");
				activateWalkToBridgeState = new Reaction();
				//changeDefaultText = new Reaction();
				//changeDefaultText.AddAction(new NPCCallbackAction(UpdateText));
				
				//activateWalkToBridgeState.AddAction(new NPCEmotionUpdateAction(toControl, new WalkToBridgeState(toControl)));	
				//activateWalkToBridgeState.AddAction(new NPCCallbackAction(updateFlag));
				_allChoiceReactions.Add(new Choice("You're On!", str_readyToRace), new DispositionDependentReaction(activateWalkToBridgeState));		
				_allChoiceReactions.Add(new Choice("Hold on.", str_readyToRace), new DispositionDependentReaction(activateWalkToBridgeState));
			}
		
			public void updateFlag() {
				if (!flagSet) {
					//FlagManager.instance.SetFlag(FlagStrings.RunToBridge);
					//flagSet = true;
					//SetDefaultText("I'm going to beat you!!!");
				}
			}
		}
		#endregion
	
	#region Toy Quest
		#region Want Toy State 
		private class WantToyState : EmotionState {	
			Reaction dollReaction = new Reaction();
			Reaction trainReaction = new Reaction();
			Reaction swordReaction = new Reaction();
			string defaultText = "I want a new toy?! All these old one are boring. See if your friend the carpenter's son can make one.";	
		
			public WantToyState(NPC toControl, string currentDialogue) : base(toControl, "These toys are boring. Could you ask our neighbors to make me a new one?") {
				dollReaction.AddAction(new NPCEmotionUpdateAction(toControl, new GaveDollState(toControl)));
				//dollReaction.AddAction(new NPCTakeItemAction(toControl));
				//dollReaction.AddAction(new NPCEmotionUpdateAction(toControl, new gaveDollState(toControl)));
				
				trainReaction.AddAction(new NPCEmotionUpdateAction(toControl, new GaveTrainState(toControl)));
				trainReaction.AddAction(new NPCTakeItemAction(toControl));
				//trainReaction.AddAction(new NPCEmotionUpdateAction(toControl, new gaveDollState(toControl)));
				
				swordReaction.AddAction(new NPCEmotionUpdateAction(toControl, new GaveSwordState(toControl)));
				swordReaction.AddAction(new NPCTakeItemAction(toControl));
				//swordReaction.AddAction(new NPCEmotionUpdateAction(toControl, new gaveDollState(toControl)));
			
				//activateWalkToBridgeState.AddAction(new NPCCallbackAction(updateFlag));
				_allItemReactions.Add("doll",new DispositionDependentReaction(dollReaction));
				_allItemReactions.Add("train",new DispositionDependentReaction(trainReaction));
				_allItemReactions.Add("sword",new DispositionDependentReaction(swordReaction));
				
			}
		}
		#endregion
		#region Want Toy State 
		private class GaveDollState : EmotionState {	
			Reaction dollReaction = new Reaction();
			
			public GaveDollState(NPC toControl) : base(toControl, "Ugh! A doll. You must think I'm a baby!") {
				//dollReaction.AddAction(new NPCEmotionUpdateAction(toControl, new GaveDollState(toControl)));
				//dollReaction.AddAction(new NPCTakeItemAction(toControl));
				//dollReaction.AddAction(new NPCEmotionUpdateAction(toControl, new gaveDollState(toControl)));
			}
		}
		#endregion
		#region Want Toy State 
		private class GaveTrainState : EmotionState {	
			Reaction trainReaction = new Reaction();
			
			public GaveTrainState(NPC toControl) : base(toControl, "Thanks, you're the best!") {
				//trainReaction.AddAction(new NPCEmotionUpdateAction(toControl, new GaveDollState(toControl)));
				//trainReaction.AddAction(new NPCTakeItemAction(toControl));
				//trainReaction.AddAction(new NPCEmotionUpdateAction(toControl, new gaveDollState(toControl)));

			}
		}
		#endregion
		#region Want Toy State 
		private class GaveSwordState : EmotionState {	
			bool flagSet = false;
			Reaction swordReaction = new Reaction();
			
			public GaveSwordState(NPC toControl) : base(toControl, "Wow! This is the best toy ever!") {
				//swordReaction.AddAction(new NPCEmotionUpdateAction(toControl, new GaveDollState(toControl)));
				//swordReaction.AddAction(new NPCTakeItemAction(toControl));
				swordReaction.AddAction(new NPCCallbackAction(SetMotherSwordFlag));
			}
			
			public void SetMotherSwordFlag() { // activates mother's reaction to sibling to the sword if she is within a certain radius
				if 	(!flagSet) {
					//FlagManager.instance.SetFlag(FlagStrings.)
					flagSet = true;
				}
			}
			//Hey! What have I told you about playing with dangerous toys? Toy Weapons?
		
		}
		#endregion
	#endregion
	#region ReflectionTree
		//WTB === Walk to Bridge
		private class ReflectionTreeState : EmotionState {	
			bool flagSet = false;
			Reaction walkToTreeReaction = new Reaction();
			Reaction walkHomeReaction = new Reaction();
			
			public ReflectionTreeState(NPC toControl) : base(toControl, "I'm going to beat you!") {//He's mean.") {
				walkToTreeReaction = new Reaction();
				//walkToTreeReaction.AddAction(new NPCEmotionUpdateAction(toControl, new WalkToCarpenterState(toControl)));
				_allChoiceReactions.Add(new Choice("Let's go!", "Yay! First one to the top wins!"), new DispositionDependentReaction(walkToTreeReaction));
				_allChoiceReactions.Add(new Choice("No Way!", "You're never any fun.. You've ruined the race. Let's go back home."), new DispositionDependentReaction(walkHomeReaction));
				walkHomeReaction.AddAction(new NPCCallbackAction(SetMotherAfterRace));
			}
			public void SetMotherAfterRace() { // lets the mother know to ask where you've been when you get back
				if (!flagSet) {
					//FlagManager.instance.SetFlag(FlagStrings.);
					flagSet = true;
				}
			}
		}
		#endregion
	
	#region Walk To Bridge State
	private class WalkToBridgeState : EmotionState {
		
			bool flagSet = false;	
			Reaction activateWalkToCarpenterState;
			Reaction choiceTwoReaction;
			string str_readyToRace = "Goooooo!!!!";
			
			public WalkToBridgeState(NPC toControl) : base(toControl, "I'm going to beat you!") {//He's mean.") {
				activateWalkToCarpenterState = new Reaction();
				activateWalkToCarpenterState.AddAction(new NPCEmotionUpdateAction(toControl, new WalkToCarpenterState(toControl)));
				activateWalkToCarpenterState.AddAction(new NPCCallbackAction(updatePassiveText));
				activateWalkToCarpenterState.AddAction(new ShowOneOffChatAction(toControl, "Hurry up Slowpoke!"));
				//_allChoiceReactions.Add(new Choice("Really?", "What? Where?"), new DispositionDependentReaction(activateWalkToCarpenterState));	
			
				choiceTwoReaction = new Reaction();
				choiceTwoReaction.AddAction(new NPCEmotionUpdateAction(toControl, new WalkToCarpenterState(toControl)));
				choiceTwoReaction.AddAction(new NPCCallbackAction(updatePassiveText));
				choiceTwoReaction.AddAction(new ShowOneOffChatAction(toControl, "Out of breath already? We have a long ways to go!"));
				//_allChoiceReactions.Add(new Choice("*Huff*", str_readyToRace), new DispositionDependentReaction(choiceTwoReaction));
			}
		
			public void updatePassiveText() {
				if (!flagSet) {	
					//FlagManager.instance.SetFlag(FlagStrings.RunToCarpenter);
					flagSet = true;
				}
			}
		}	
		#endregion
		#region Walk To Carpenter State
		private class WalkToCarpenterState : EmotionState{
		
			bool flagSet = false;	
			Reaction activateWalkToBeachState;
			Reaction choiceTwoReaction;
			string str_readyToRace = "Goooooo!!!!";
			
			public WalkToCarpenterState(NPC toControl) : base(toControl, " ") {
				activateWalkToBeachState = new Reaction();
				activateWalkToBeachState.AddAction(new NPCEmotionUpdateAction(toControl, new WalkToBeachState(toControl)));
				activateWalkToBeachState.AddAction(new NPCCallbackAction(updatePassiveText));
				activateWalkToBeachState.AddAction(new ShowOneOffChatAction(toControl, "Hehehe, You're silly! Hurry up! :)"));
				_allChoiceReactions.Add(new Choice(">:[", "Mm hmm!"), new DispositionDependentReaction(activateWalkToBeachState));	
			
				choiceTwoReaction = new Reaction();
				choiceTwoReaction.AddAction(new NPCEmotionUpdateAction(toControl, new WalkToBeachState(toControl)));
				choiceTwoReaction.AddAction(new NPCCallbackAction(updatePassiveText));
				choiceTwoReaction.AddAction(new ShowOneOffChatAction(toControl, "He's behind us! Run!!!"));
				_allChoiceReactions.Add(new Choice("...", str_readyToRace), new DispositionDependentReaction(choiceTwoReaction));
			}
			
			public void updatePassiveText() {
				if (!flagSet) {	
					FlagManager.instance.SetFlag(FlagStrings.RunToBeach);
					flagSet = true;
				}
			}
		}
		#endregion
		#region Walk To Beach State
		private class WalkToBeachState : EmotionState {
			
			bool flagSet = false;	
			Reaction activateWalkToBeachState;
			Reaction choiceTwoReaction;
			Reaction choiceThreeReaction;
			//string str_readyToRace = "Goooooo!!!!";
		
			public WalkToBeachState(NPC toControl) : base(toControl, "The Ocean is beautiful~") {
				//GUIManager.Instance.RefreshInteraction(); <--DON'T DO THIS IN CONSTRUCTOR
			
//edit later to stay near the beach for a while before moving on --> the sibling can tell you a story			
				activateWalkToBeachState = new Reaction();
				activateWalkToBeachState.AddAction(new NPCEmotionUpdateAction(toControl, new WalkToFarmerState(toControl)));
				activateWalkToBeachState.AddAction(new NPCCallbackAction(updatePassiveText));
				activateWalkToBeachState.AddAction(new ShowOneOffChatAction(toControl, "Hehehe, You're silly! Hurry up! :)"));
				_allChoiceReactions.Add(new Choice("Ocean", "Good Choice~"), new DispositionDependentReaction(activateWalkToBeachState));	
//edit later to go to the edge of the pier			
				choiceTwoReaction = new Reaction();
				choiceTwoReaction.AddAction(new NPCEmotionUpdateAction(toControl, new WalkToFarmerState(toControl)));
				choiceTwoReaction.AddAction(new NPCCallbackAction(updatePassiveText));
				choiceTwoReaction.AddAction(new ShowOneOffChatAction(toControl, "I love the pier, let's go!!"));
				_allChoiceReactions.Add(new Choice("Pier", "I like to look off the pier. The world out there is bigger than we can imagine."), new DispositionDependentReaction(choiceTwoReaction));

				choiceThreeReaction = new Reaction();
				choiceThreeReaction.AddAction(new NPCEmotionUpdateAction(toControl, new WalkToFarmerState(toControl)));
				choiceThreeReaction.AddAction(new NPCCallbackAction(updatePassiveText));
				choiceThreeReaction.AddAction(new ShowOneOffChatAction(toControl, "It's steep, be careful! I'm going to beat you!"));
				_allChoiceReactions.Add(new Choice("Cliffside", "Up we go!"), new DispositionDependentReaction(choiceThreeReaction));
			}
		
			public void updatePassiveText() {
				if (!flagSet) {	
					FlagManager.instance.SetFlag(FlagStrings.RunToFarmer);
					flagSet = true;
				}
			}
		}
		#endregion
		#region Walk To Farmer State
		private class WalkToFarmerState : EmotionState {
			
			bool flagSet = false;	
			Reaction activateWalkToLighthouseState;
			//Reaction choiceTwoReaction;
			//string str_readyToRace = "Goooooo!!!!";
		
			public WalkToFarmerState(NPC toControl) : base(toControl, " ") {
				activateWalkToLighthouseState = new Reaction();
				activateWalkToLighthouseState.AddAction(new NPCEmotionUpdateAction(toControl, new WalkToLighthouseState(toControl)));
				activateWalkToLighthouseState.AddAction(new NPCCallbackAction(updatePassiveText));
				activateWalkToLighthouseState.AddAction(new ShowOneOffChatAction(toControl, "Let's go to the lighthouse!"));
				_allChoiceReactions.Add(new Choice("Lighthouse!", "Good Choice~"), new DispositionDependentReaction(activateWalkToLighthouseState));	
			}
		
				public void updatePassiveText() {
				if (!flagSet) {	
					FlagManager.instance.SetFlag(FlagStrings.RunToLighthouse);
					flagSet = true;
				}
			}
			
		}
		#endregion	
		#region Walk To Lighthouse State	
		private class WalkToLighthouseState : EmotionState {
		
			bool flagSet = false;	
			Reaction activateWalkToMarketState;
			//Reaction choiceTwoReaction;
			//string str_readyToRace = "Goooooo!!!!";
		
			public WalkToLighthouseState(NPC toControl) : base(toControl, " ") {
				activateWalkToMarketState = new Reaction();
				activateWalkToMarketState.AddAction(new NPCEmotionUpdateAction(toControl, new WalkToMarketState(toControl)));
				activateWalkToMarketState.AddAction(new NPCCallbackAction(updatePassiveText));
				activateWalkToMarketState.AddAction(new ShowOneOffChatAction(toControl, "Shopping time!"));
				_allChoiceReactions.Add(new Choice("Market", "Good Choice~"), new DispositionDependentReaction(activateWalkToMarketState));	
			}
		
			public void updatePassiveText() {
				if (!flagSet) {	
					FlagManager.instance.SetFlag(FlagStrings.RunToMarket);
					flagSet = true;
				}
			}
			
		}
		#endregion
		#region Walk To Market State
		private class WalkToMarketState : EmotionState {
		
			bool flagSet = false;	
			Reaction activateWalkToReflectionTreeState;
			//Reaction choiceTwoReaction;
			//string str_readyToRace = "Goooooo!!!!";
		
			public WalkToMarketState(NPC toControl) : base(toControl, " ") {
				activateWalkToReflectionTreeState = new Reaction();
				activateWalkToReflectionTreeState.AddAction(new NPCEmotionUpdateAction(toControl, new WalkToReflectionTreeState(toControl)));
				activateWalkToReflectionTreeState.AddAction(new NPCCallbackAction(updatePassiveText));
				activateWalkToReflectionTreeState.AddAction(new ShowOneOffChatAction(toControl, "This is the best place on the island!!"));
				_allChoiceReactions.Add(new Choice("ReflectionTree", "Good Choice~"), new DispositionDependentReaction(activateWalkToReflectionTreeState));
			}
		
			public void updatePassiveText() {
				if (!flagSet) {	
					//FlagManager.instance.SetFlag(FlagStrings.RunToWindmill);
					FlagManager.instance.SetFlag(FlagStrings.RunToReflectionTree);
					flagSet = true;
				}
			}
			
		}
		#endregion
		#region Walk To Reflection Tree State
		private class WalkToReflectionTreeState : EmotionState {
		
			bool flagSet = false;	
			Reaction activateWalkToHomeState;
			Reaction choiceTwoReaction;
			//string str_readyToRace = "Goooooo!!!!";
			
			public WalkToReflectionTreeState(NPC toControl) : base(toControl, " ") {
				activateWalkToHomeState = new Reaction();
				activateWalkToHomeState.AddAction(new NPCEmotionUpdateAction(toControl, new WalkToHomeState(toControl)));
				activateWalkToHomeState.AddAction(new NPCCallbackAction(updatePassiveText));
				activateWalkToHomeState.AddAction(new ShowOneOffChatAction(toControl, "Race you home!!! I'm going to win!"));
				_allChoiceReactions.Add(new Choice("Home", "Ready for the last sprint?"), new DispositionDependentReaction(activateWalkToHomeState));
			}
			
			public void updatePassiveText() {
				if (!flagSet) {	
					FlagManager.instance.SetFlag(FlagStrings.RunToHome);
					flagSet = true;
				}
			}
		}
		#endregion
		#region Walk To Home State
		private class WalkToHomeState : EmotionState {
		
			bool flagSet = false;	
			Choice choice;
			Reaction activatePostSiblingExplore;
			Reaction choiceTwoReaction;
			//string str_readyToRace = "Goooooo!!!!";	
		
			public WalkToHomeState(NPC toControl) : base(toControl, "Good Race!") {
				
				activatePostSiblingExplore = new Reaction();
				choiceTwoReaction = new Reaction();
				choice = new Choice("Mm hmm!", "Should we tell mom about our adventure?");
				//activatePostSiblingExplore.AddAction(new NPCEmotionUpdateAction(toControl, new ERROR(toControl)));
				activatePostSiblingExplore.AddAction(new ShowOneOffChatAction(toControl, "You're the best! Thanks for playing with me!"));
				activatePostSiblingExplore.AddAction(new NPCCallbackOnNPCAction(updatePassiveText, toControl));
				//activatePostSiblingExplore.AddAction(new NPCEmotionUpdateAction(toControl, new MotherUpsetState(toControl)));	
				choiceTwoReaction.AddAction(new NPCCallbackAction(changeLocation));
				choiceTwoReaction.AddAction(new NPCEmotionUpdateAction(toControl, new MotherUpsetState(toControl)));
				_allChoiceReactions.Add(choice, new DispositionDependentReaction(activatePostSiblingExplore));
			}
		
			public void updatePassiveText(NPC toControl) {
				if (!flagSet) {	
					//FlagManager.instance.SetFlag(FlagStrings.PostSiblingExplore);
					//flagSet = true;
					//activatePostSiblingExplore.AddAction(new ShowOneOffChatAction(toControl, "You're the best! Thanks for playing with me!"));
					
				//DOESN'T WORK =(
					//Schedule testSchedule = new Schedule(toControl, Schedule.priorityEnum.DoNow);
					//testSchedule.Add(new TimeTask(.5f, new IdleState(toControl)));
					//testSchedule.Add(new Task(new MoveThenDoState(toControl, new Vector3 (10, -1.5f, .3f), new MarkTaskDone(toControl))));	
					_allChoiceReactions.Remove(choice);
					SetDefaultText ("Should we tell mom about our adventure?");
					choice = new Choice("Sure!", "Yay! Let's go!");
					_allChoiceReactions.Add(choice, new DispositionDependentReaction(choiceTwoReaction));
					_allChoiceReactions.Add(new Choice ("Don't!","Too bad, I'm going to tell her anyways!"), new DispositionDependentReaction(choiceTwoReaction));
					GUIManager.Instance.RefreshInteraction();
				}
			}
		
			public void changeLocation() {
				FlagManager.instance.SetFlag(FlagStrings.PostSiblingExplore);
			}
		}
		#endregion
		#region Mother Upset State 
		private class MotherUpsetState : EmotionState {
		
			//bool flagSet = false;	
			Choice choice;
			Reaction activatePostSiblingExplore;
			Reaction choiceTwoReaction;
			//string str_readyToRace = "Goooooo!!!!";	
		
			public MotherUpsetState(NPC toControl) : base(toControl, " ") {
				activatePostSiblingExplore = new Reaction();
				SetDefaultText("Opps.. I got us in trouble.");	
				activatePostSiblingExplore.AddAction(new NPCCallbackOnNPCAction(talkToMother,toControl));
				_allChoiceReactions.Add(new Choice("Mom!", "fdsfds"), new DispositionDependentReaction(activatePostSiblingExplore));
			}
		
			public void talkToMother(NPC toControl) {
				toControl.AddSchedule(new NPCConvoSchedule(toControl, NPCManager.instance.getNPC(StringsNPC.MomYoung), new ExampleNPCConverastion(),Schedule.priorityEnum.DoNow));	
			}
		}
		#endregion
	#endregion	
}