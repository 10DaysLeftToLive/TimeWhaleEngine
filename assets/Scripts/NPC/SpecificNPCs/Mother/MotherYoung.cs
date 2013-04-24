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
		return (new InitialEmotionState(this, "Don't you step on that frog."));
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
		string text1 = "I can't stop you but what I can do is use a very particular set of skills. Skills I have acquired over a very long career. Skills that make me a nightmare for people like you.";
			public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
				Reaction changeDefaultText = new Reaction();
				changeDefaultText.AddAction(new NPCEmotionUpdateAction(toControl, new MadEmotionState(toControl)));
				_allChoiceReactions.Add(new Choice("Watch me", text1), new DispositionDependentReaction(changeDefaultText));
				_allChoiceReactions.Add(new Choice("Okay Mom", "Good boy"), new DispositionDependentReaction(new Reaction()));
			}
			
			public override void UpdateEmotionState(){
				
			}
		}
		#endregion
	
	private class MadEmotionState : EmotionState {
		public MadEmotionState(NPC toControl) : base(toControl, "I can't believe you would talk to me like that."){
		}
		
		public override void UpdateEmotionState(){
			
		}
	}
	#endregion
}
