using UnityEngine;
using System.Collections;

public class SiblingOldWalkToCarpenterSchedule : Schedule {
	private static readonly float Y_COORDINATE = -1.735313f + (LevelManager.levelYOffSetFromCenter*2);
	public SiblingOldWalkToCarpenterSchedule (NPC toManage) : base (toManage) {
		schedulePriority = (int)priorityEnum.High;			
	}
	protected override void Init() {
			//AddFlagGroup(FlagStrings.oldSiblingFinishedAtCarpenterHouseFlag);
			
//Move to the center of the bridge, wait 2 seconds
			Add(new TimeTask(.05f, new IdleState(_toManage)));
//Display passive chat: Sometimes I wonder what Mom's garden would have been like.

		
//			Task activateStoryIntroPartOne = (new Task(new MoveThenDoState(_toManage, new Vector3(14f, Y_COORDINATE, .3f), new MarkTaskDone(_toManage)))); 
//			activateStoryIntroPartOne.AddFlagToSet(FlagStrings.oldSiblingIntroStoryOnePartOneFlag);
//			Add(activateStoryIntroPartOne);

		
//Wait for 4 hard seconds, then till player is near or if 6 more seconds pass
//			Add(new TimeTask(3.5f, new IdleState(_toManage)));

//Move inbetween bridge and Carpenter's House, wait 2 second		
//			Add(new Task(new MoveThenDoState(_toManage, new Vector3(24f, 98.4f, .3f), new MarkTaskDone(_toManage))));
//Display passive chat:	These trees aren't as beautiful as her vision.
//			Task activateStoryIntroPartTwo = (new Task(new MoveThenDoState(_toManage, new Vector3(22f, Y_COORDINATE, .3f), new MarkTaskDone(_toManage)))); 
//			activateStoryIntroPartTwo.AddFlagToSet(FlagStrings.oldSiblingIntroStoryOnePartTwoFlag);
//			Add(activateStoryIntroPartTwo);
//Wait for 4 hard seconds, then till player is near or if 6 more seconds pass		
//			Add(new TimeTask(3f, new IdleState(_toManage)));
//			Add(new TimeTask(10f, new WaitTillPlayerCloseState(_toManage, _toManage.player)));
//Move to Carpenter's House
			Add(new Task(new MoveThenDoState(_toManage, new Vector3(30f, Y_COORDINATE, .3f), new MarkTaskDone(_toManage))));
//Display passive chat: Hey! Doing well today?			
			Task siblingOldReachedCarpenterSonTask = new Task(new MoveThenDoState(_toManage, new Vector3 (32f, Y_COORDINATE, .3f), new MarkTaskDone(_toManage))); // at top staircase
			siblingOldReachedCarpenterSonTask.AddFlagToSet(FlagStrings.siblingOldReachedCarpenterSonFlag);
			Add(siblingOldReachedCarpenterSonTask);
			Add(new TimeTask(12.5f, new IdleState(_toManage)));
		
			Task siblingChatWithCarpenterPartOne = new Task(new MoveThenDoState(_toManage, new Vector3 (32f, Y_COORDINATE, .3f), new MarkTaskDone(_toManage))); // at top staircase
			siblingChatWithCarpenterPartOne.AddFlagToSet(FlagStrings.siblingOldGreetCarpenterSonOldPartOneFlag);
			Add(siblingChatWithCarpenterPartOne);
		
			Add(new TimeTask(6f, new IdleState(_toManage)));
	}
}
