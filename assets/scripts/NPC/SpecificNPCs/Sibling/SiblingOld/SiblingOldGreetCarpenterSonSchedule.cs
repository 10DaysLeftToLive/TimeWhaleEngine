using UnityEngine;
using System.Collections;

public class SiblingOldGreetCarpenterSonSchedule : Schedule {
	private static readonly float Y_COORDINATE = -1.735313f + (LevelManager.levelYOffSetFromCenter*2);	
	public SiblingOldGreetCarpenterSonSchedule (NPC toManage) : base (toManage) {
		schedulePriority = (int)priorityEnum.Medium;			
	}
	protected override void Init() {
			Add(new TimeTask(4f, new IdleState(_toManage)));
//Display passive chat: Sometimes I wonder what Mom's garden would have been like.
			Task siblingOldGreetingCarpenterOldTaskPartOne = (new Task(new MoveThenDoState(_toManage, new Vector3(12f, Y_COORDINATE, .3f), new MarkTaskDone(_toManage)))); 
			siblingOldGreetingCarpenterOldTaskPartOne.AddFlagToSet(FlagStrings.siblingOldGreetCarpenterSonOldPartOneFlag);
			Add(siblingOldGreetingCarpenterOldTaskPartOne);
//Add(new Task(new MoveThenDoState(_toManage, new Vector3(38f, Y_COORDINATE, .3f), new MarkTaskDone(_toManage)))); 
//Add(new TimeTask(.3f, new IdleState(_toManage)));

//Move to base of beach stairs		
//Add(new Task(new MoveThenDoState(_toManage, new Vector3(50f, Y_COORDINATE, .3f), new MarkTaskDone(_toManage)))); 
//Add(new TimeTask(2f, new IdleState(_toManage)));
	}
}
