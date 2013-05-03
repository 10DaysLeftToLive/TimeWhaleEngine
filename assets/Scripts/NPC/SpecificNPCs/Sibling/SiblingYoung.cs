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

	#region Set Flag Reactions
	public string currentPassiveText;
	protected override void SetFlagReactions() {
		//Reaction frogCrushing = new Reaction();
		//frogCrushing.AddAction(new ShowOneOffChatAction(this, "OmG yOu KiLleD dAt fROg!1!"));
		//frogCrushing.AddAction(new UpdateDefaultTextAction(this, "I can't belive you did that."));
		//flagReactions.Add(FlagStrings.CrushFrog, frogCrushing);
		#region Home To Bridge
		Reaction homeToBridge = new Reaction();
		//raceTime.AddAction(new ShowOneOffChatAction(this, "Hurry up! We don't have all day!!"));
		homeToBridge.AddAction(new ShowOneOffChatAction(this, "Go!!!"));
		homeToBridge.AddAction(new UpdateDefaultTextAction(this, "I heard there's a secret waterway under this bridge :)"));
		homeToBridge.AddAction(new NPCAddScheduleAction(this, runToBridge));
		flagReactions.Add(FlagStrings.RunToBridge, homeToBridge);
		#endregion	
		#region Bridge To Carpenter
		Reaction bridgeToCarpenter = new Reaction();
		//bridgeToCarpenter.AddAction(new ShowOneOffChatAction(this, currentPassiveText));
		//bridgeToCarpenter.AddAction(new NPCCallbackSetstringAction(setPassiveText, this));
		bridgeToCarpenter.AddAction(new UpdateDefaultTextAction(this, "Careful, the Carpenter is mean!"));
		bridgeToCarpenter.AddAction(new NPCAddScheduleAction(this, runToCarpenter));
		flagReactions.Add(FlagStrings.RunToCarpenter, bridgeToCarpenter);
		#endregion
		#region Carpenter To Beach
		Reaction carpenterToBeach = new Reaction();
		carpenterToBeach.AddAction(new UpdateDefaultTextAction(this, "The Ocean is beautiful~"));
		carpenterToBeach.AddAction(new NPCAddScheduleAction(this, runToBeach));
		flagReactions.Add(FlagStrings.RunToBeach, carpenterToBeach);
		#endregion
		#region Beach To Farmer
		Reaction beachToFarmer = new Reaction();
		beachToFarmer.AddAction(new UpdateDefaultTextAction(this, "Ms. Farmer is mean. /n but Mr. Farmer is nice!"));
		beachToFarmer.AddAction(new NPCAddScheduleAction(this, runToFarmer));
		flagReactions.Add(FlagStrings.RunToFarmer, beachToFarmer);
		#endregion
		#region Farmer to Lighthouse
		Reaction farmerToLighthouse = new Reaction();
		farmerToLighthouse.AddAction(new UpdateDefaultTextAction(this, "The lighthouse girl knows some good stories! There's one about a Sea Captain"));
		farmerToLighthouse.AddAction(new NPCAddScheduleAction(this, runToLighthouse));
		flagReactions.Add(FlagStrings.RunToLighthouse, farmerToLighthouse);
		#endregion
		#region Lighthouse to Market
		Reaction lighthouseToMarket = new Reaction();
		lighthouseToMarket.AddAction(new UpdateDefaultTextAction(this, "I really want a new toy. Could you get me one? "));
		lighthouseToMarket.AddAction(new NPCAddScheduleAction(this, runToMarket));
		flagReactions.Add(FlagStrings.RunToMarket, lighthouseToMarket);
		#endregion
		#region Market to Reflection Tree
		Reaction marketToReflectionTree = new Reaction();
		marketToReflectionTree.AddAction(new UpdateDefaultTextAction(this, "Soo peaceful~ I don't know why mom doesn't let us come here."));
		marketToReflectionTree.AddAction(new NPCAddScheduleAction(this, runToReflectionTree));
		flagReactions.Add(FlagStrings.RunToReflectionTree, marketToReflectionTree);
		#endregion
		#region Reflection Tree to Home
		Reaction reflectionTreeToHome = new Reaction();
		reflectionTreeToHome.AddAction(new UpdateDefaultTextAction(this, "Ready for the last sprint?"));
		reflectionTreeToHome.AddAction(new NPCAddScheduleAction(this, runToHome));
		flagReactions.Add(FlagStrings.RunToHome, reflectionTreeToHome);
		#endregion
		
		Reaction firstTimeMotherTalks = new Reaction();
		//FirstTimeMotherTalks.AddAction(new ShowOneOffChatAction(this, "Let's go!", 5));
		firstTimeMotherTalks.AddAction(new ShowOneOffChatAction(this, "Let's go!"));
		firstTimeMotherTalks.AddAction(new NPCAddScheduleAction(this, runToCarpenter));
		//FirstTimeMotherTalks.AddAction(new ShowOneOffChatAction(this, "C'mon, let's race!!!"));
		flagReactions.Add(FlagStrings.SiblingExplore, firstTimeMotherTalks); 
	}
	#endregion
	protected override EmotionState GetInitEmotionState() {
		// change portrait picture
		return (new InitialEmotionState(this, "Hey there! Are you ready to race!?"));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new Schedule(this);
		
		Task initialSchedule = new Task(new IdleState(this));
		schedule.Add(initialSchedule);
		//TimeTask exploringNearHome = new TimeTask(10, new IdleState(this));
		//Task moveToBridge = new Task(new MoveThenDoState(this, new Vector3(5, .2f, .3f), new MarkTaskDone(this)));
		//schedule.Add(exploringNearHome);
		//schedule.Add(moveToBridge);
		return (schedule);
	}
	
	private Schedule runToBeach;
	private Schedule runToBridge;
	private Schedule runToCarpenter;
	private Schedule runToFarmer;
	private Schedule runToLighthouse;
	private Schedule runToMarket;
	private Schedule runToReflectionTree;
	private Schedule runToHome;
	#region Set Up Sechedules
	protected override void SetUpSchedules() {
		/*
		runToCarpenter = new Schedule(this, Schedule.priorityEnum.High);
		runToCarpenter.Add(new TimeTask(2, new IdleState(this)));
		//runToCarpenter.Add(new Task(new MoveThenDoState(this, NPCManager.instance.getNPC(StringsNPC.CarpenterYoung).transform.position, new MarkTaskDone(this))));
		runToCarpenter.Add(new Task(new MoveThenDoState(this, new Vector3 (5, .2f, .3f), new MarkTaskDone(this))));
		runToCarpenter.Add (new TimeTask(1f, new IdleState(this)));
		runToCarpenter.Add(new Task(new MoveThenDoState(this, new Vector3 (4, .2f, .3f), new MarkTaskDone(this))));
		runToCarpenter.Add (new TimeTask(2f, new IdleState(this)));
		runToCarpenter.Add(new Task(new MoveThenDoState(this, new Vector3 (10, .2f, .3f), new MarkTaskDone(this))));
	
		//adding the ability to set flags would be nice
		//adding the ability to set emotion States on would be nice. (actions)
		runToCarpenter.SetCanChat(false); 
		*/
		
		runToBridge = new Schedule(this, Schedule.priorityEnum.High);
		runToBridge.Add(new TimeTask(.75f, new IdleState(this)));
		runToBridge.Add(new Task(new MoveThenDoState(this, new Vector3 (6, .2f, .3f), new MarkTaskDone(this))));
		runToBridge.Add(new TimeTask(.5f, new IdleState(this)));
		runToBridge.Add(new Task(new MoveThenDoState(this, new Vector3 (4, .2f, .3f), new MarkTaskDone(this))));
		runToBridge.Add(new TimeTask(.5f, new IdleState(this)));
		runToBridge.Add(new Task(new MoveThenDoState(this, new Vector3 (11, .2f, .3f), new MarkTaskDone(this))));
		runToBridge.Add(new Task(new MoveThenDoState(this, new Vector3 (10.90f, .2f, .3f), new MarkTaskDone(this))));
		
		runToCarpenter = new Schedule(this, Schedule.priorityEnum.High);
		//Some interactions with Carpenter
		runToCarpenter.Add(new TimeTask(.25f, new IdleState(this)));
		runToCarpenter.Add(new Task(new MoveThenDoState(this, new Vector3 (28, .2f, .3f), new MarkTaskDone(this))));
		
		runToBeach = new Schedule(this, Schedule.priorityEnum.High);
		runToBeach.Add(new TimeTask(.25f, new IdleState(this)));
		runToBeach.Add(new Task(new MoveThenDoState(this, new Vector3 (59, .2f, .3f), new MarkTaskDone(this))));
		//wait for player to reach, or wait 10 seconds
		runToBeach.Add(new TimeTask(2f, new IdleState(this)));
		runToBeach.Add(new Task(new MoveThenDoState(this, new Vector3 (66, -7.6f, .3f), new MarkTaskDone(this))));
		runToBeach.Add(new Task(new MoveThenDoState(this, new Vector3 (70, -8f, .3f), new MarkTaskDone(this))));
		//PIER (79,-5.1)
		runToFarmer = new Schedule(this, Schedule.priorityEnum.High);
		runToFarmer.Add(new TimeTask(.25f, new IdleState(this)));
		runToFarmer.Add(new Task(new MoveThenDoState(this, new Vector3 (66, -7.6f, .3f), new MarkTaskDone(this))));
		runToFarmer.Add(new TimeTask(.25f, new IdleState(this)));
		runToFarmer.Add(new Task(new MoveThenDoState(this, new Vector3 (59, .2f, .3f), new MarkTaskDone(this))));
		runToFarmer.Add(new TimeTask(2f, new IdleState(this)));
		runToFarmer.Add(new Task(new MoveThenDoState(this, new Vector3 (47, -1f, .3f), new MarkTaskDone(this))));
		runToFarmer.Add(new TimeTask(2f, new IdleState(this)));
		runToFarmer.Add(new Task(new MoveThenDoState(this, new Vector3 (53, 5f, .3f), new MarkTaskDone(this))));
		runToFarmer.Add(new TimeTask(1f, new IdleState(this)));
		runToFarmer.Add(new Task(new MoveThenDoState(this, new Vector3 (50, 10f, .3f), new MarkTaskDone(this))));
		runToFarmer.Add(new TimeTask(1f, new IdleState(this)));
		runToFarmer.Add(new Task(new MoveThenDoState(this, new Vector3 (55, 16f, .3f), new MarkTaskDone(this))));
		runToFarmer.Add(new TimeTask(1f, new IdleState(this)));
		runToFarmer.Add(new Task(new MoveThenDoState(this, new Vector3 (63, 16f, .3f), new MarkTaskDone(this))));
		
		runToLighthouse = new Schedule(this, Schedule.priorityEnum.High);
		runToLighthouse.Add(new TimeTask(.25f, new IdleState(this)));
		runToLighthouse.Add(new Task(new MoveThenDoState(this, new Vector3 (70, 16f, .3f), new MarkTaskDone(this))));
			
		//runToLighthouse.Add(new Task(new MoveThenDoState(this, new Vector3 (28, .2f, .3f), new MarkTaskDone(this))));
		
		runToMarket = new Schedule(this, Schedule.priorityEnum.High);
		runToMarket.Add(new TimeTask(.25f, new IdleState(this)));
		runToMarket.Add(new Task(new MoveThenDoState(this, new Vector3 (63, 16f, .3f), new MarkTaskDone(this))));
		runToMarket.Add(new TimeTask(1f, new IdleState(this)));
		runToMarket.Add(new Task(new MoveThenDoState(this, new Vector3 (55, 16f, .3f), new MarkTaskDone(this))));
		runToMarket.Add(new TimeTask(1.25f, new IdleState(this)));
		runToMarket.Add(new Task(new MoveThenDoState(this, new Vector3 (50, 10f, .3f), new MarkTaskDone(this))));
		runToMarket.Add(new TimeTask(1f, new IdleState(this)));
		runToMarket.Add(new Task(new MoveThenDoState(this, new Vector3 (37, 10f, .3f), new MarkTaskDone(this))));
		runToMarket.Add(new TimeTask(.5f, new IdleState(this)));
		runToMarket.Add(new Task(new MoveThenDoState(this, new Vector3 (40, 10f, .3f), new MarkTaskDone(this))));
		runToMarket.Add(new TimeTask(.5f, new IdleState(this)));
		runToMarket.Add(new Task(new MoveThenDoState(this, new Vector3 (27, 10f, .3f), new MarkTaskDone(this))));
		runToMarket.Add(new Task(new MoveThenDoState(this, new Vector3 (27.05f, 10f, .3f), new MarkTaskDone(this))));
		
		runToReflectionTree = new Schedule(this, Schedule.priorityEnum.High);
		runToReflectionTree.Add(new TimeTask(.25f, new IdleState(this)));
		runToReflectionTree.Add(new Task(new MoveThenDoState(this, new Vector3 (-40, 16f, .3f), new MarkTaskDone(this))));
		runToReflectionTree.Add(new TimeTask(2f, new IdleState(this)));
		runToReflectionTree.Add(new Task(new MoveThenDoState(this, new Vector3 (-49, 18f, .3f), new MarkTaskDone(this))));
		runToReflectionTree.Add(new TimeTask(2f, new IdleState(this)));
		runToReflectionTree.Add(new Task(new MoveThenDoState(this, new Vector3 (-41, 25f, .3f), new MarkTaskDone(this))));
		runToReflectionTree.Add(new TimeTask(.5f, new IdleState(this)));
		runToReflectionTree.Add(new Task(new MoveThenDoState(this, new Vector3 (-48, 30f, .3f), new MarkTaskDone(this))));
		runToReflectionTree.Add(new TimeTask(.1f, new IdleState(this)));
		runToReflectionTree.Add(new Task(new MoveThenDoState(this, new Vector3 (-66, 29.5f, .3f), new MarkTaskDone(this))));
		
		runToHome = new Schedule(this, Schedule.priorityEnum.High);
		runToHome.Add(new TimeTask(.25f, new IdleState(this)));
		runToHome.Add(new Task(new MoveThenDoState(this, new Vector3 (-48, 30f, .3f), new MarkTaskDone(this))));
		runToHome.Add(new TimeTask(2f, new IdleState(this)));
		runToHome.Add(new Task(new MoveThenDoState(this, new Vector3 (-41, 25f, .3f), new MarkTaskDone(this))));
		runToHome.Add(new TimeTask(1f, new IdleState(this)));
		runToHome.Add(new Task(new MoveThenDoState(this, new Vector3 (-49, 18f, .3f), new MarkTaskDone(this))));
		runToHome.Add(new TimeTask(1f, new IdleState(this)));
		runToHome.Add(new Task(new MoveThenDoState(this, new Vector3 (-40, 16f, .3f), new MarkTaskDone(this))));
		runToHome.Add(new TimeTask(1.5f, new IdleState(this)));
		runToHome.Add(new Task(new MoveThenDoState(this, new Vector3 (-18, 6f, .3f), new MarkTaskDone(this))));
		runToHome.Add(new TimeTask(1f, new IdleState(this)));
		runToHome.Add(new Task(new MoveThenDoState(this, new Vector3 (-9, -1.5f, .3f), new MarkTaskDone(this))));
		runToHome.Add(new TimeTask(.5f, new IdleState(this)));
		runToHome.Add(new Task(new MoveThenDoState(this, new Vector3 (-7, -1.5f, .3f), new MarkTaskDone(this))));
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
			string str_readyToRace = "Goooooo!!!!";
		
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
			string str_readyToRace = "Goooooo!!!!";
		
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
			string str_readyToRace = "Goooooo!!!!";
		
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
			string str_readyToRace = "Goooooo!!!!";
		
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
			string str_readyToRace = "Goooooo!!!!";
			
			public WalkToReflectionTreeState(NPC toControl) : base(toControl, " ") {
				activateWalkToHomeState = new Reaction();
				activateWalkToHomeState.AddAction(new NPCEmotionUpdateAction(toControl, new WalkToHomeState(toControl)));
				activateWalkToHomeState.AddAction(new NPCCallbackAction(updatePassiveText));
				activateWalkToHomeState.AddAction(new ShowOneOffChatAction(toControl, "Race you home!!! I'm going to win!"));
				_allChoiceReactions.Add(new Choice("Home", "Good Choice~"), new DispositionDependentReaction(activateWalkToHomeState));
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
			Reaction activatePostSiblingExplore;
			Reaction choiceTwoReaction;
			string str_readyToRace = "Goooooo!!!!";	
		
			public WalkToHomeState(NPC toControl) : base(toControl, " ") {
				activatePostSiblingExplore = new Reaction();
				//activatePostSiblingExplore.AddAction(new NPCEmotionUpdateAction(toControl, new ERROR(toControl)));
				activatePostSiblingExplore.AddAction(new NPCCallbackAction(updatePassiveText));
				activatePostSiblingExplore.AddAction(new ShowOneOffChatAction(toControl, "You're the best! Thanks for playing with me!"));
				_allChoiceReactions.Add(new Choice("Home", "Good Choice~"), new DispositionDependentReaction(activatePostSiblingExplore));
			}
		
			public void updatePassiveText() {
				if (!flagSet) {	
					FlagManager.instance.SetFlag(FlagStrings.PostSiblingExplore);
					flagSet = true;
				}
			}
		}
		#endregion
	#endregion	
}