using UnityEngine;
using System.Collections;

public class CarpenterSonOldToBeachScript : Schedule {

	public CarpenterSonOldToBeachScript (NPC toManage) : base (toManage) {
		schedulePriority = (int)priorityEnum.Medium;			
	}
	protected override void Init() {
			
//Wait 7 seconds for Sibling to finish greeting
		Add(new TimeTask(13f, new IdleState(_toManage)));
//Disply passive chat:
		Task GoToBeachPartOne = (new Task(new MoveThenDoState(_toManage, new Vector3(_toManage.transform.position.x, -1.735313f + (LevelManager.levelYOffSetFromCenter*2), 0f), new MarkTaskDone(_toManage)))); 
		GoToBeachPartOne.AddFlagToSet(FlagStrings.oldCarpenterGoToBeachPartOneFlag);
		Add(GoToBeachPartOne);
		
		Add(new TimeTask(4f, new IdleState(_toManage)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3(67f,(LevelManager.levelYOffSetFromCenter*2) - 5f, 0f), new MarkTaskDone(_toManage)))); 
//WaitTillPlayerCloseState(30f)
		Add(new TimeTask(2f, new IdleState(_toManage)));
		Task GoToBeachPartTwo = (new Task(new MoveThenDoState(_toManage, new Vector3(67f,(LevelManager.levelYOffSetFromCenter*2) - 5f, 0f), new MarkTaskDone(_toManage)))); 
		GoToBeachPartTwo.AddFlagToSet(FlagStrings.oldCarpenterGoToBeachPartTwoFlag);
		Add(GoToBeachPartTwo);
		
		Add(new TimeTask(7.5f, new IdleState(_toManage)));
		Task GoToBeachPartThree = (new Task(new MoveThenDoState(_toManage, new Vector3(69.5f,(LevelManager.levelYOffSetFromCenter*2) - 3f, 0f), new MarkTaskDone(_toManage)))); 
		GoToBeachPartThree.AddFlagToSet(FlagStrings.oldCarpenterGoToBeachPartThreeFlag);
		Add(GoToBeachPartThree);
/*		
		Add(new TimeTask(12f, new IdleState(_toManage)));
		Task GoToBeachPartFour = (new Task(new MoveThenDoState(_toManage, new Vector3(73.5f,(LevelManager.levelYOffSetFromCenter*2) - 3f, 0f), new MarkTaskDone(_toManage)))); 
		GoToBeachPartFour.AddFlagToSet(FlagStrings.oldCarpenterGoToBeachPartFourFlag);
		Add(GoToBeachPartFour);
		Add(new TimeTask(9f, new IdleState(_toManage)));
		
		Task GoToBeachPartFive = (new Task(new MoveThenDoState(_toManage, new Vector3(73.5f,(LevelManager.levelYOffSetFromCenter*2) - 3f, 0f), new MarkTaskDone(_toManage)))); 
		GoToBeachPartFive.AddFlagToSet(FlagStrings.oldCarpenterGoToBeachPartFiveFlag);
		Add(GoToBeachPartFive);
		
		Add(new TimeTask(9f, new IdleState(_toManage)));
*/
		Add(new Task(new IdleState(_toManage)));
	}
}
