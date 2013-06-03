using UnityEngine;
using System.Collections;

public class CarpenterSonOldGreetSibllingSchedule : Schedule {
	
	public CarpenterSonOldGreetSibllingSchedule (NPC toManage) : base (toManage) {
		schedulePriority = (int)priorityEnum.Medium;			
	}
	protected override void Init() {
			
//Wait 7 seconds for Sibling to finish greeting
		Add(new TimeTask(6f, new IdleState(_toManage)));
//Disply passive chat:
		Task respondToSiblingPartOne = (new Task(new MoveThenDoState(_toManage, new Vector3(_toManage.transform.position.x, -1.735313f + (LevelManager.levelYOffSetFromCenter*2), -.5f), new MarkTaskDone(_toManage)))); 
		respondToSiblingPartOne.AddFlagToSet(FlagStrings.oldCarpenterGreetSiblingPartOneFlag);
		Add(respondToSiblingPartOne);
		
		//Add(new Task(new MoveThenDoState(_toManage, new Vector3(14f, 98.4f, .3f), new MarkTaskDone(_toManage)))); 
		//Add(new TimeTask(2.5f, new IdleState(_toManage)));

//Wait for 4 hard seconds, then till player is near or if 6 more seconds pass
		//Add(new TimeTask(3f, new IdleState(_toManage)));
		Add(new TimeTask(4f, new WaitTillPlayerCloseState(_toManage, ref _toManage.player)));
		//Add(new TimeTask(3f, new IdleState(_toManage)));
	}
}
