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
		return (new InitialEmotionState(this, "Hai"));
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
			public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
				
			}
			
			public override void UpdateEmotionState(){
				
			}
		}
		#endregion
	#endregion
}
