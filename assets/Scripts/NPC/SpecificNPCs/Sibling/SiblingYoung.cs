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
	protected override void SetFlagReactions() {

		#region Home To Bridge
/*
		Reaction homeToBridge = new Reaction();
		//raceTime.AddAction(new ShowOneOffChatAction(this, "Hurry up! We don't have all day!!"));
		homeToBridge.AddAction(new ShowOneOffChatAction(this, "Go!!!"));
		homeToBridge.AddAction(new UpdateDefaultTextAction(this, "I heard there's a secret waterway under this bridge :)"));
		homeToBridge.AddAction(new NPCAddScheduleAction(this, initialSchedule));
		flagReactions.Add(FlagStrings.RunToBridge, homeToBridge);
*/
		#endregion			
	}
	#endregion
	protected override EmotionState GetInitEmotionState() {
		// change portrait picture
		return (new InitialEmotionState(this, "Hey there! Are you ready to race!?"));
	}

	protected override Schedule GetSchedule(){
		Schedule schedule = new Schedule(this);
		Task initialSchedule = new TimeTask(.25f , new IdleState(this));
		schedule.Add(initialSchedule);
		
		//TimeTask exploringNearHome = new TimeTask(10, new IdleState(this));
		//Task moveToBridge = new Task(new MoveThenDoState(this, new Vector3(5, .2f, .3f), new MarkTaskDone(this)));
		//schedule.Add(exploringNearHome);
		//schedule.Add(moveToBridge);
		return (schedule);
	}
	
	//private Schedule initialSchedule;
	
	#region Set Up Sechedules
	protected override void SetUpSchedules() {
		 //initialSchedule = new Schedule(this);
		
		scheduleStack.Add (new YoungRunIslandScript(this));
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
			//Choice firstTimeBusy;
			Reaction activateWalkToBridgeState;
			Reaction changeDefaultText;
			string str_readyToRace = "Go!!!";
		
			public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
				activateWalkToBridgeState = new Reaction();
				//changeDefaultText = new Reaction();
				   //enterMadState.AddAction(new NPCEmotionUpdateAction(toControl, new MadEmotionState(toControl)));
				activateWalkToBridgeState.AddAction(new NPCEmotionUpdateAction(toControl, new WalkToBridgeState(toControl)));
				//changeDefaultText.AddAction(new NPCCallbackAction(UpdateText));
				//firstTimeBusy = new Choice("You're On!", str_readyToRace);	
				activateWalkToBridgeState.AddAction(new NPCCallbackAction(updateFlag));
				_allChoiceReactions.Add(new Choice("You're On!", str_readyToRace), new DispositionDependentReaction(activateWalkToBridgeState));		
				_allChoiceReactions.Add(new Choice("Hold on.", str_readyToRace), new DispositionDependentReaction(activateWalkToBridgeState));

// use to make item reactions
			
				//_allItemReactions
			}
		
			public void updateFlag() {
				if (!flagSet) {
					FlagManager.instance.SetFlag(FlagStrings.RunToBridge);
					flagSet = true;
				}
			}
		}
	
		#endregion
		#region Walk To Bridge State
		//WTB === Walk to Bridge
		private class WalkToBridgeState : EmotionState {
		
			bool flagSet = false;	
			Reaction activateWalkToCarpenterState;
			Reaction choiceTwoReaction;
			string str_readyToRace = "Goooooo!!!!";
			
			public WalkToBridgeState(NPC toControl) : base(toControl, "He's mean.") {
				activateWalkToCarpenterState = new Reaction();
				activateWalkToCarpenterState.AddAction(new NPCEmotionUpdateAction(toControl, new WalkToCarpenterState(toControl)));
				activateWalkToCarpenterState.AddAction(new NPCCallbackAction(updatePassiveText));
				activateWalkToCarpenterState.AddAction(new ShowOneOffChatAction(toControl, "Hurry up Slowpoke!"));
				_allChoiceReactions.Add(new Choice("Really?", "What? Where?"), new DispositionDependentReaction(activateWalkToCarpenterState));	
			
				choiceTwoReaction = new Reaction();
				choiceTwoReaction.AddAction(new NPCEmotionUpdateAction(toControl, new WalkToCarpenterState(toControl)));
				choiceTwoReaction.AddAction(new NPCCallbackAction(updatePassiveText));
				choiceTwoReaction.AddAction(new ShowOneOffChatAction(toControl, "Out of breath already? We have a long ways to go!"));
				_allChoiceReactions.Add(new Choice("*Huff*", str_readyToRace), new DispositionDependentReaction(choiceTwoReaction));
			}
		
			public void updatePassiveText() {
				if (!flagSet) {	
					FlagManager.instance.SetFlag(FlagStrings.RunToCarpenter);
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
			
			public WalkToCarpenterState(NPC toControl) : base(toControl, " <(0.0)>") {
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