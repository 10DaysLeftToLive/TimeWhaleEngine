using UnityEngine;
using System.Collections;

/// <summary>
/// CarpenterSonMiddle specific scripting values
/// </summary>
public class CarpenterSonMiddle : NPC {
	protected override void Init() {
		id = NPCIDs.CARPENTER_SON;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		Reaction stormOffReaction = new Reaction();
		stormOffReaction.AddAction(new NPCEmotionUpdateAction(this, new StormOffEmotionState(this, "Why can my father never let up? He knows my dream is to fish but at every turn he stifles me and makes me want to just stop doing anything.")));
		stormOffReaction.AddAction(new NPCAddScheduleAction(this, stormOffSchedule));
		flagReactions.Add(FlagStrings.carpenterSonStormOff, stormOffReaction);
		Reaction IdleReaction = new Reaction();
		//IdleReaction.AddAction(new NPCAddScheduleAction (this, ));
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Just leave me alone."));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}
	
	Schedule stormOffSchedule;
	//Schedule IdleSchedule;

	protected override void SetUpSchedules(){
		stormOffSchedule = new Schedule(this,Schedule.priorityEnum.DoNow);
		stormOffSchedule.Add(new Task(new MoveState(this, MapLocations.BaseOfPierMiddle)));
		stormOffSchedule.Add(new TimeTask(1.0f, new IdleState(this)));
		
		//IdleSchedule = new Schedule(this, Schedule.priorityEnum.High);
		//IdleSchedule.Add(new Task(new MoveState(this, transform.position.x - 5)));
		//IdleSchedule.Add(new TimeTask(5, new WaitState(this)));
		//IdleSchedule.Add(new Task(new MoveState(this, transform.position.x + 5)));
		//IdleSchedule.Add(new TimeTask(5, new WaitState(this)));
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
	#region Storm off in anger Emotion State
	private class StormOffEmotionState : EmotionState{
	
		
		Choice reconcileWithFather = new Choice("You should try to get along with your father.", "Yeah, if I just keep trying I'm sure my dad will accept me");
		Choice youDontNeedHim = new Choice("You don't need your dad's approval.", "Yeah, my dad doesn't deserve my respect.");
		
		Reaction reconcileReaction = new Reaction();
		Reaction youDontNeedHimReaction = new Reaction();
		
		public StormOffEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			reconcileReaction.AddAction(new SetOffFlagAction(FlagStrings.carpenterSonReconcile));
			reconcileReaction.AddAction(new UpdateDefaultTextAction(toControl, "I'll go talk to him soon."));
			reconcileReaction.AddAction(new NPCCallbackAction(removeChoices));
			
			youDontNeedHimReaction.AddAction(new UpdateDefaultTextAction(toControl, "Who needs his aproval anyway?"));
			youDontNeedHimReaction.AddAction(new NPCCallbackAction(removeChoices));
			
			_allChoiceReactions.Add(reconcileWithFather, new DispositionDependentReaction(reconcileReaction));
			_allChoiceReactions.Add(youDontNeedHim, new DispositionDependentReaction(youDontNeedHimReaction));
		}
		
		public override void UpdateEmotionState(){
			
		}
			
		void removeChoices(){
			_allChoiceReactions.Clear();
			GUIManager.Instance.RefreshInteraction();
		}
	
	}
	#endregion
	#endregion
}
