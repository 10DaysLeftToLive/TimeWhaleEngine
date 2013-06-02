using UnityEngine;
using System.Collections;

/// <summary>
/// Carpenter son old specific scripting values
/// </summary>
public class CarpenterSonOld : NPC {
	protected override void Init() {
		id = NPCIDs.CARPENTER_SON;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		#region Greet Old Sibling
		Reaction introductionToSiblingOld = new Reaction();
		introductionToSiblingOld.AddAction(new UpdateDefaultTextAction(this, "Best wind in years today, and with my father with me I'm sure I'll get my best catch yet."));
		introductionToSiblingOld.AddAction(new NPCAddScheduleAction(this, greetSiblingOldSchedule));
		flagReactions.Add(FlagStrings.siblingOldReachedCarpenterSonFlag, introductionToSiblingOld);
		
		Reaction greetSiblingPartOne = new Reaction();
		ShowMultipartChatAction greetSiblingPartOneChat = new ShowMultipartChatAction(this);
		greetSiblingPartOneChat.AddChat("Fine.. Fine..", 2f);
		greetSiblingPartOneChat.AddChat("Work as usual..", 2f);
		greetSiblingPartOneChat.AddChat("Brought an apple you say?", 2f);
		greetSiblingPartOne.AddAction(greetSiblingPartOneChat);
		greetSiblingPartOne.AddAction(new NPCEmotionUpdateAction(this, new WantAppleState(this,"Got an apple you say?")));
		flagReactions.Add(FlagStrings.oldCarpenterGreetSiblingPartOneFlag, greetSiblingPartOne);
		#endregion
		
		Reaction reconcileReaction = new Reaction();
		reconcileReaction.AddAction(new NPCTeleportToAction(this, MapLocations.BaseOfPierOld));
		reconcileReaction.AddAction(new UpdateDefaultTextAction(this, "Best wind in years today, and with my father with me I'm sure I'll get my best catch yet."));
		flagReactions.Add(FlagStrings.carpenterSonReconcile, reconcileReaction);
		
		
	}
	 	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "My back aches, my arms are tired and I'm too tired. I wish I never got into this lousy carpentry business."));
	}
	
	protected override Schedule GetSchedule() {
		//Schedule schedule = new DefaultSchedule(this);
		//return (schedule);
		
		Schedule schedule = new Schedule(this, Schedule.priorityEnum.Low); 
		schedule.Add(new TimeTask(120f, new IdleState(this)));
		return (schedule);
		
	}

	#region elder Schedules
	private Schedule greetSiblingOldSchedule;
	#endregion
	protected override void SetUpSchedules() {
		greetSiblingOldSchedule = (new CarpenterSonOldGreetSibllingSchedule(this));
		
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
	private class WantAppleState : EmotionState {
		Reaction uh = new Reaction();
		public WantAppleState(NPC toControl, string currentDialogue) : base (toControl, currentDialogue) {
			uh.AddAction(new NPCCallbackAction(Uhh));
			uh.AddAction(new NPCEmotionUpdateAction(toControl, new GaveAppleState(toControl, "hey!")));
			_allItemReactions.Add("apple", new DispositionDependentReaction(uh));
			_allChoiceReactions.Add(new Choice ("I don't", "Ohh ok.."), new DispositionDependentReaction(uh));
		}
		public void Uhh() {
			//nothing happens.
		}
	}
		
	private class GaveAppleState : EmotionState {
		public GaveAppleState(NPC toControl, string currentDialogue) : base (toControl, currentDialogue) {
			//_allItemReactions.Add("apple", 
			
		}
	}					
	#endregion
}
