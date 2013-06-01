using UnityEngine;
using System.Collections;

/// <summary>
/// CarpenterSonMiddle specific scripting values
/// </summary>
public class CarpenterSonMiddle : NPC {
	StormOffEmotionState stormoffState;
	Date dateState;
	InitialEmotionState initialState;
	Vector3 startingPosition;
	bool castlemanDateSuccess = false;
	bool dateForMe = false;
	bool successfulDate = false;
	protected override void Init() {
		id = NPCIDs.CARPENTER_SON;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		Reaction waitingForDate = new Reaction();
		waitingForDate.AddAction(new NPCEmotionUpdateAction(this, dateState));
		flagReactions.Add(FlagStrings.WaitingForDate, waitingForDate);
		
		Reaction gotTheGirl = new Reaction();
		gotTheGirl.AddAction(new NPCEmotionUpdateAction(this, initialState)); //change state after successfuldate
		flagReactions.Add(FlagStrings.PostDatingCarpenter, gotTheGirl);
		
		Reaction iBeDating = new Reaction();
		iBeDating.AddAction(new NPCCallbackAction(setFlagDateForMe));
		flagReactions.Add(FlagStrings.CarpenterDate, iBeDating);
		
		Reaction endOfDate = new Reaction();
		endOfDate.AddAction(new NPCCallbackAction(dateOver));
		endOfDate.AddAction(new NPCAddScheduleAction(this, moveBack));
		flagReactions.Add(FlagStrings.EndOfDate, endOfDate);
		
		/*Reaction stoodUpLG = new Reaction();
		stoodUpLG.AddAction(new NPCEmotionUpdateAction(this, stoodUpState));
		flagReactions.Add(FlagStrings.CarpenterNoShow, stoodUpLG);*/
		
		Reaction moveToDate = new Reaction();
		moveToDate.AddAction(new NPCAddScheduleAction(this, dateWithLG));
		flagReactions.Add(FlagStrings.CarpenterDating, moveToDate);
		
		
		Reaction stormOffReaction = new Reaction();
		stormOffReaction.AddAction(new NPCEmotionUpdateAction(this, stormoffState));
		stormOffReaction.AddAction(new NPCAddScheduleAction(this, stormOffSchedule));
		flagReactions.Add(FlagStrings.carpenterSonStormOff, stormOffReaction);
		Reaction IdleReaction = new Reaction();
		//IdleReaction.AddAction(new NPCAddScheduleAction (this, ));
	}
	
	protected override EmotionState GetInitEmotionState(){
		dateState = new Date(this, "Date is over...shes alright, little wierd.");
		stormoffState = new StormOffEmotionState(this, "Why can my father never let up? He knows my dream is to fish but at every turn he stifles me and makes me want to just stop doing anything.");
		initialState = new InitialEmotionState(this, "");
		
		startingPosition = transform.position;
		startingPosition.y += LevelManager.levelYOffSetFromCenter;
		return (new InitialEmotionState(this, "Just leave me alone."));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}
	
	Schedule stormOffSchedule, moveToBeach, moveBack;
	NPCConvoSchedule dateWithLG;
	//Schedule IdleSchedule;

	protected override void SetUpSchedules(){
		
		moveBack = new Schedule(this, Schedule.priorityEnum.High);
		moveBack.Add(new Task(new MoveThenDoState(this, startingPosition, new MarkTaskDone(this))));
		
		dateWithLG =  new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.LighthouseGirlMiddle),
			new MiddleCastleManToLighthouseGirl(), Schedule.priorityEnum.DoConvo); 
		//dateWithLG.SetCanNotInteractWithPlayer();
		
		
		stormOffSchedule = new Schedule(this,Schedule.priorityEnum.DoNow);
		stormOffSchedule.Add(new Task(new MoveState(this, MapLocations.BaseOfPierMiddle)));
		stormOffSchedule.Add(new TimeTask(1.0f, new IdleState(this)));
		
		//IdleSchedule = new Schedule(this, Schedule.priorityEnum.High);
		//IdleSchedule.Add(new Task(new MoveState(this, transform.position.x - 5)));
		//IdleSchedule.Add(new TimeTask(5, new WaitState(this)));
		//IdleSchedule.Add(new Task(new MoveState(this, transform.position.x + 5)));
		//IdleSchedule.Add(new TimeTask(5, new WaitState(this)));
	}
	
	protected void dateOver(){
		if (dateForMe)
			FlagManager.instance.SetFlag(FlagStrings.CarpenterNoShow);
	}
	
	protected void setFlagDateForMe(){
		dateForMe = true;
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
	
	private class Date: EmotionState{
		Choice DateChoice = new Choice("You have a date!", "Really? This...this...this is the most beauteous day of my life! Hurry to the beach. I cannot tarry!");
		
		Reaction DateReaction = new Reaction();
		
		bool flagSet = false;
		public Date (NPC toControl, string currentDialogue):base (toControl, "You look like you have an urgent message."){
			_allChoiceReactions.Clear();
			
			DateReaction.AddAction(new NPCCallbackAction(DateResponse));
			_allChoiceReactions.Add(DateChoice, new DispositionDependentReaction(DateReaction));
		}
		
		public void DateResponse(){
			if (!flagSet){
				FlagManager.instance.SetFlag(FlagStrings.CarpenterDating);
			}
		}
		
	}
	#endregion
}
