using UnityEngine;
using System.Collections;

/// <summary>
/// CarpenterMiddle specific scripting values
/// </summary>
public class CarpenterMiddle : NPC {	
	protected override void Init() {
		id = NPCIDs.CARPENTER;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "This all started when he was a child, if only I had raised him better."));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}

	protected override void SetUpSchedules(){
		NPCConvoSchedule angryAtSon =  new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.CarpenterSonMiddle), new MiddleCarpenterToSonDefaultScriptedConvo(), Schedule.priorityEnum.High); // CHANGE THIS CONVERSATION TO THE ONE WE WANT TO USE!
		angryAtSon.SetCanNotInteractWithPlayer();
		this.AddSchedule(angryAtSon);
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
	
	
		public StormOffEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			Schedule sched = new Schedule(_npcInState, Schedule.priorityEnum.High);
			// put move stuff here
				// sched.Add(new Task(new MoveState())
			_npcInState.AddSchedule(sched);
		}
		
		public override void UpdateEmotionState(){
			
		}
	
	}
	#endregion
	#endregion
}
