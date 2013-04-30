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
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Good morning! Are you busy?"));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}
	
	private Schedule runToCarpenter;
	protected override void SetUpSchedules(){
		runToCarpenter = new Schedule(this, Schedule.priorityEnum.High);
		runToCarpenter.Add(new TimeTask(1, new IdleState(this)));
		runToCarpenter.Add(new Task(new MoveThenDoState(this, new Vector3(10, -1f,.3f), new MarkTaskDone(this))));
		runToCarpenter.SetCanChat(true);
	}
	
	#region EmotionStates
		#region Initial Emotion State
		private class InitialEmotionState : EmotionState{
			int numberOfTimesBuggedMother;
			string text1 = "Ok, come back when you have some time!";
			
			Choice firstTimeBusy;
			Reaction changeDefaultText;
			Reaction enterMadState;
		
			public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
				numberOfTimesBuggedMother = 0;
				changeDefaultText = new Reaction();
				enterMadState = new Reaction();
				enterMadState.AddAction(new NPCEmotionUpdateAction(toControl, new MadEmotionState(toControl)));
				changeDefaultText.AddAction(new NPCCallbackAction(UpdateText));
				firstTimeBusy = new Choice("Busy!", text1);
				_allChoiceReactions.Add((firstTimeBusy), new DispositionDependentReaction(changeDefaultText));		
				_allChoiceReactions.Add(new Choice("Nope!", "Good boy"), new DispositionDependentReaction(new Reaction()));
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
	#endregion
}
