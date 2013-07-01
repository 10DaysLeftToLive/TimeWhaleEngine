using UnityEngine;
using System.Collections;

/// <summary>
/// SiblingMiddle specific scripting values
/// </summary>
public class SiblingMiddle : Sibling {
	protected override void SetFlagReactions(){
		
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Hey! Looking good! Want to go somewhere?"));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		Task initialSchedule = new TimeTask(.25f , new IdleState(this));
		Task moveToBridge = new Task(new MoveThenDoState(this, new Vector3(5, .2f + LevelManager.levelYOffSetFromCenter, .3f), new MarkTaskDone(this)));
		schedule.Add(initialSchedule);
		schedule.Add (moveToBridge);
		return (schedule);
	}

	protected override void SetUpSchedules(){
		//Schedule initialSchedule = new Schedule(this);
		//initialSchedule.Add(new Task(new MoveThenDoState(this, new Vector3 (6, .2f, .3f), new MarkTaskDone(this))));
		//Task moveToBridge = new Task(new MoveThenDoState(this, new Vector3(5, .2f, .3f), new MarkTaskDone(this)));
		//scheduleStack.Add (moveToBridge);
	}
	
	
	#region EmotionStates
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState{
		Reaction gaveRose;
		Reaction gavePendant;
	
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			gaveRose = new Reaction();
			gavePendant = new Reaction();
			
			gaveRose.AddAction(new UpdateCurrentTextAction(toControl, "My bedroom window when I was little had this bed of peach colored roses, they made my room smell wonderful in the summers."));
			_allItemReactions.Add("apple",  new DispositionDependentReaction(gaveRose)); // change item to rose
		
		}
	}
	#endregion
	#endregion
}
