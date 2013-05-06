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
		//enterHappy.AddAction(new NPCAddScheduleAction(this, runToCarpenter));
		flagReactions.Add (FlagStrings.EnterHappyState, enterHappy);
		
		Reaction moveHome = new Reaction();
		moveHome.AddAction(new ShowOneOffChatAction(this, "Let's go back to the house."));
		moveHome.AddAction(new NPCAddScheduleAction(this, runToCarpenter));
		flagReactions.Add (FlagStrings.MoveHome, moveHome);
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Morning! Do you have some time to help?"));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}
	
	private Schedule runToCarpenter;
	private Schedule moveMotherHappyState;
	
	protected override void SetUpSchedules(){
		runToCarpenter = new Schedule(this, Schedule.priorityEnum.DoNow);
		runToCarpenter.Add(new TimeTask(3, new IdleState(this)));
		runToCarpenter.Add(new Task(new MoveThenDoState(this, new Vector3(0, -1f,.3f), new MarkTaskDone(this))));
		
		moveMotherHappyState = new Schedule(this, Schedule.priorityEnum.DoNow);
		moveMotherHappyState.Add (new TimeTask(2, new IdleState(this)));
		//moveMotherHappyState.Add(new Task(new MoveThenDoState(this, PathFinding.GetPathToNode(this.transform.position, "waypoint001", this.transform.localScale.y/2), new MarkTaskDone(this))));
		moveMotherHappyState.Add(new Task(new MoveThenDoState(this, new Vector3(-20f, -1f, -.5f), new MarkTaskDone(this))));
	
	
	}
	
	#region EmotionStates
		#region Initial Emotion State
		private class InitialEmotionState : EmotionState{
			int numberOfTimesBuggedMother;
			string text1 = "Ok, come back when you have some time! Remember, don't go near the cliffs!";
			
			Choice firstTimeBusy;
			Reaction changeDefaultText;
			Reaction enterMadState;
			Reaction enterHappyState;
		
			public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
				
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
			
// Add auto close GUIBox
			}
			
			public void UpdateText() {
				numberOfTimesBuggedMother++;
				Debug.Log(numberOfTimesBuggedMother);
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
			Action displayAnger = new ShowOneOffChatAction(_npcInState, "That isn't how you treat your mother!");
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
		Reaction changeFlag;
		
		public PlantTreeState(NPC toControl) : base(toControl, "I raised you well :')"){
			//evokeUpdatePosition = new NPCCallbackAction(UpdatePosition);
			//evokeUpdatePosition.Perform();
			changeFlag = new Reaction();
			changeFlag.AddAction(new NPCCallbackAction(UpdatePosition));
			goBackHome = new Choice("See ya!", "Looks like you had fun!");
			_allChoiceReactions.Add(goBackHome, new DispositionDependentReaction(changeFlag));
			
			//_allItemReactions.Add ("apple", 11);
			//.AddAction(new UpdateCurrentTextAction(npcInState, "Why thank you!"));
		}
		
		public void UpdatePosition() {
			if (!flagSet) {
				FlagManager.instance.SetFlag(FlagStrings.MoveHome);
				flagSet = true;
				SetDefaultText("Good work! I love you =]");
				_allChoiceReactions.Remove(goBackHome);
				GUIManager.Instance.RefreshInteraction();
			}
			
			
			//Schedule moveMyMomma = new Schedule(_npcInState, Schedule.priorityEnum.High);
			//moveMyMomma.Add(new Task(new MoveThenDoState(_npcInState, new Vector3(10, -1f,.3f), new MarkTaskDone(_npcInState))));
			//_npcInState.AddSchedule(moveMyMomma);
		}
	}
	
		#endregion
		#region HappyEmotionState
	private class HappyEmotionState : EmotionState {
		
		bool flagSet = false;
		Choice testing;
		
		Reaction changeFlag;
		Reaction postSeed;
		
		public HappyEmotionState(NPC toControl) : base(toControl, "You're my best friend! =]"){
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
