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
	
	protected override void SetFlagReactions(){
		Reaction frogCrushing = new Reaction();
		frogCrushing.AddAction(new ShowOneOffChatAction(this, "OmG yOu KiLleD dAt fROg!1!"));
		frogCrushing.AddAction(new UpdateDefaultTextAction(this, "I can't belive you did that."));
		flagReactions.Add(FlagStrings.CrushFrog, frogCrushing);
		
		Reaction FirstTimeMotherTalks = new Reaction();
		//FirstTimeMotherTalks.AddAction(new ShowOneOffChatAction(this, "Let's go!", 5));
		FirstTimeMotherTalks.AddAction(new ShowOneOffChatAction(this, "Let's go!"));
		FirstTimeMotherTalks.AddAction(new NPCAddScheduleAction(this, runToCarpenter));
		//FirstTimeMotherTalks.AddAction(new ShowOneOffChatAction(this, "C'mon, let's race!!!"));
		flagReactions.Add(FlagStrings.SiblingExplore, FirstTimeMotherTalks); 
		
		Reaction raceTime = new Reaction();
		raceTime.AddAction(new ShowOneOffChatAction(this, "Hey, let's race!!"));
		flagReactions.Add(FlagStrings.RaceTime, raceTime);
		
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Hey there ;}"));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new Schedule(this);
		
		Task standAround = new Task(new IdleState(this));
		
		schedule.Add(standAround);
		
		return (schedule);
	}
	
	private Schedule runToCarpenter;
	protected override void SetUpSchedules(){
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