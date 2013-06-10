using UnityEngine;
using System.Collections;

public class SiblingOldWalkToCarpenterSchedule : Schedule {
	private static readonly float Y_COORDINATE = -1.735313f + (LevelManager.levelYOffSetFromCenter*2);
	public SiblingOldWalkToCarpenterSchedule (NPC toManage) : base (toManage) {
		schedulePriority = (int)priorityEnum.High;			
	}
	protected override void Init() {
		
			Add(new TimeTask(.05f, new IdleState(_toManage)));

			Task activateStoryIntroPartOne = (new Task(new MoveThenDoState(_toManage, new Vector3(14f, Y_COORDINATE, .3f), new MarkTaskDone(_toManage)))); 
			activateStoryIntroPartOne.AddFlagToSet(FlagStrings.oldSiblingIntroStoryOnePartOneFlag);
			Add(activateStoryIntroPartOne);

			Add(new TimeTask(3.5f, new IdleState(_toManage)));
		
			Add(new Task(new MoveThenDoState(_toManage, new Vector3(24f, Y_COORDINATE, .3f), new MarkTaskDone(_toManage))));

			//Task activateStoryIntroPartTwo = (new Task(new MoveThenDoState(_toManage, new Vector3(22f, Y_COORDINATE, .3f), new MarkTaskDone(_toManage)))); 
			//activateStoryIntroPartTwo.AddFlagToSet(FlagStrings.oldSiblingIntroStoryOnePartTwoFlag);
			//Add(activateStoryIntroPartTwo);
		
			//Add(new TimeTask(3f, new IdleState(_toManage)));
			Add(new TimeTask(10f, new WaitTillPlayerCloseState(_toManage, ref _toManage.player,2f)));

			Add(new Task(new MoveThenDoState(_toManage, new Vector3(30f, Y_COORDINATE, .3f), new MarkTaskDone(_toManage))));		
			
		Task siblingOldReachedCarpenterSonTask = new TimeTask(.05f, new IdleState(_toManage)); // at top staircase
			siblingOldReachedCarpenterSonTask.AddFlagToSet(FlagStrings.siblingOldReachedCarpenterSonFlag);
			Add(siblingOldReachedCarpenterSonTask);
			Add(new TimeTask(12.5f, new IdleState(_toManage)));
		
			Task siblingChatWithCarpenterPartOne = new Task(new MoveThenDoState(_toManage, new Vector3 (32f, Y_COORDINATE, .3f), new MarkTaskDone(_toManage))); // at top staircase
			siblingChatWithCarpenterPartOne.AddFlagToSet(FlagStrings.siblingOldGreetCarpenterSonOldPartOneFlag);
			Add(siblingChatWithCarpenterPartOne);
		
			Add(new TimeTask(6f, new IdleState(_toManage)));
	}
}
