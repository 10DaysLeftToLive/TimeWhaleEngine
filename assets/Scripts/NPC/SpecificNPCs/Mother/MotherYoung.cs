using UnityEngine;
using System.Collections;

/// <summary>
/// Mother young specific scripting values
/// </summary>
public class MotherYoung : NPC {
	protected override void Init() {
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		Reaction frogCrushing = new Reaction();
		frogCrushing.AddAction(new ShowOneOffChatAction(this, "Gross! I'm out of here."));
		frogCrushing.AddAction(new NPCAddScheduleAction(this, runToCarpenter));
		flagReactions.Add(FlagStrings.CrushFrog, frogCrushing); 
		
		Reaction enterHappy = new Reaction();
		enterHappy.AddAction(new ShowOneOffChatAction(this, "I think there's a good spot over here!"));
		enterHappy.AddAction(new NPCAddScheduleAction(this, moveMotherHappyState));
		//enterHappy.AddAction(new NPCAddScheduleAction(this, runToCarpenter));
		flagReactions.Add (FlagStrings.EnterHappyState, enterHappy);
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Good morning! Are you busy?"));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}
	
	private Schedule runToCarpenter;
	private Schedule moveMotherHappyState;
	
	protected override void SetUpSchedules(){
		runToCarpenter = new Schedule(this, Schedule.priorityEnum.High);
		runToCarpenter.Add(new TimeTask(3, new IdleState(this)));
		runToCarpenter.Add(new Task(new MoveThenDoState(this, new Vector3(10, -1f,.3f), new MarkTaskDone(this))));
		runToCarpenter.SetCanChat(true);
		
		moveMotherHappyState = new Schedule(this, Schedule.priorityEnum.DoNow);
		moveMotherHappyState.Add (new TimeTask(2, new IdleState(this)));
		//moveMotherHappyState.Add(new Task(new MoveThenDoState(this, PathFinding.GetPathToNode(this.transform.position, "waypoint001", this.transform.localScale.y/2), new MarkTaskDone(this))));
		moveMotherHappyState.Add(new Task(new MoveThenDoState(this, new Vector3(-25f, -1f, 0), new MarkTaskDone(this))));
		
	}
	
	#region EmotionStates
		#region Initial Emotion State
		private class InitialEmotionState : EmotionState{
			int numberOfTimesBuggedMother;
			string text1 = "Ok, come back when you have some time!";
			
			Choice firstTimeBusy;
			Reaction changeDefaultText;
			Reaction enterMadState;
			Reaction enterHappyState;
		
			public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
				enterMadState = new Reaction();
				enterHappyState = new Reaction();
			
				numberOfTimesBuggedMother = 0;
				changeDefaultText = new Reaction();
				
				enterMadState.AddAction(new NPCEmotionUpdateAction(toControl, new MadEmotionState(toControl)));
				enterHappyState.AddAction(new NPCEmotionUpdateAction(toControl, new HappyEmotionState(toControl)));
				changeDefaultText.AddAction(new NPCCallbackAction(UpdateText));
				firstTimeBusy = new Choice("Busy!", text1);
				_allChoiceReactions.Add((firstTimeBusy), new DispositionDependentReaction(changeDefaultText));		
				_allChoiceReactions.Add(new Choice("Nope!", "Good boy"), new DispositionDependentReaction(enterHappyState));
			}
			
			public void UpdateText() {
				numberOfTimesBuggedMother++;
				Debug.Log(numberOfTimesBuggedMother);
				if (numberOfTimesBuggedMother == 1) {
					SetDefaultText("Are you free now?");
					text1 = "Alright, I need your help, so come back soon.";
					FlagManager.instance.SetFlag(FlagStrings.SiblingExplore);
				}
			
				if (numberOfTimesBuggedMother == 2) {
					SetDefaultText("Back again? Have any time now?");
					text1 = "This isn't a game, I really do need help.";
				}
			
				if (numberOfTimesBuggedMother == 3) {
					SetDefaultText("Good you're back, the seeds are over there.");
					text1 = "If you're busy, stop bugging me.";
					_allChoiceReactions.Remove(firstTimeBusy);
					firstTimeBusy = new Choice ("Busy!!", text1);
					_allChoiceReactions.Add((firstTimeBusy), new DispositionDependentReaction(changeDefaultText));
					GUIManager.Instance.RefreshInteraction();
				
					_allChoiceReactions.Remove(firstTimeBusy);
					_allChoiceReactions.Add((firstTimeBusy), new DispositionDependentReaction(enterMadState));
				}
			
				if (numberOfTimesBuggedMother >= 1 && numberOfTimesBuggedMother <= 2) { 
					_allChoiceReactions.Remove(firstTimeBusy);
					firstTimeBusy = new Choice ("Busy!!", text1);
					_allChoiceReactions.Add((firstTimeBusy), new DispositionDependentReaction(changeDefaultText));
					GUIManager.Instance.RefreshInteraction();
				}
			}
		
			public override void UpdateEmotionState(){
				
			}
		}
		#endregion
	
	private class MadEmotionState : EmotionState {
		public MadEmotionState(NPC toControl) : base(toControl, "You're useless, go away!"){
		}
		
		public override void UpdateEmotionState(){
			
		}
	}
	
	private class PlantTreeState : EmotionState {
		bool flagSet = false;
		
		public PlantTreeState(NPC toControl) : base(toControl, "I raised you well :')"){
			
		}
		
		public void UpdatePosition() {
			Schedule moveMyMomma = new Schedule(_npcInState, Schedule.priorityEnum.High);
			moveMyMomma.Add(new Task(new MoveThenDoState(_npcInState, new Vector3(10, -1f,.3f), new MarkTaskDone(_npcInState))));
			_npcInState.AddSchedule(moveMyMomma);
		}
	}
	
	private class HappyEmotionState : EmotionState {
		
		bool flagSet = false;
		Choice testing;
		
		Reaction changeFlag;
		Reaction postSeed;
		
		public HappyEmotionState(NPC toControl) : base(toControl, "You're my best friend! =]"){
			changeFlag = new Reaction();
			postSeed = new Reaction();
			testing = new Choice ("TRANSFORM", "Follow Me!!");
			postSeed.AddAction(new NPCEmotionUpdateAction(toControl, new PlantTreeState(toControl)));
			changeFlag.AddAction(new NPCCallbackAction(UpdateFlag));
			_allChoiceReactions.Add((testing), new DispositionDependentReaction(changeFlag));
		}
		
		public void UpdateFlag() {
			
			if (!flagSet) {
				FlagManager.instance.SetFlag(FlagStrings.EnterHappyState);	
				flagSet = true;
				_allChoiceReactions.Remove(testing);
				SetDefaultText("Yay, you planted the seed!");
				testing = new Choice ("BaBoom!!", "transform to postSeed");
				_allChoiceReactions.Add((testing), new DispositionDependentReaction(postSeed));
				
			}
		}
	}
	#endregion
}
