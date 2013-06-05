using UnityEngine;
using System.Collections;

public class SiblingOldGreetCarpenterSonSchedule : Schedule {
	private static readonly float Y_COORDINATE = -1.735313f + (LevelManager.levelYOffSetFromCenter*2);	
	public SiblingOldGreetCarpenterSonSchedule (NPC toManage) : base (toManage) {
		schedulePriority = (int)priorityEnum.Medium;			
	}
	
	protected override void Init() {
			Add(new TimeTask(5f, new IdleState(_toManage)));
//Display passive chat: Sometimes I wonder what Mom's garden would have been like.
			Task siblingOldGreetingCarpenterOldTaskPartOne = (new Task(new MoveThenDoState(_toManage, new Vector3(30f, _toManage.transform.position.y + (LevelManager.levelYOffSetFromCenter*2), 0), new MarkTaskDone(_toManage)))); 
			siblingOldGreetingCarpenterOldTaskPartOne.AddFlagToSet(FlagStrings.siblingOldGoToFortuneTellerIntro);
			Add(siblingOldGreetingCarpenterOldTaskPartOne);
			Add(new TimeTask(5.25f, new IdleState(_toManage)));
			Task siblingOldToFortunetellerPartOne = (new Task(new MoveThenDoState(_toManage, new Vector3(27f, 7.8f + (LevelManager.levelYOffSetFromCenter*2), 0), new MarkTaskDone(_toManage)))); 
		//siblingOldToFortunetellerPartOne.AddFlagToSet(FlagStrings.siblingOldGoToFortuneTellerIntro);
			Add(siblingOldToFortunetellerPartOne);
			Add(new TimeTask(.3f, new IdleState(_toManage)));

//Move to base of beach stairs		
//Add(new Task(new MoveThenDoState(_toManage, new Vector3(50f, Y_COORDINATE, .3f), new MarkTaskDone(_toManage)))); 
//Add(new TimeTask(2f, new IdleState(_toManage)));
	}
}