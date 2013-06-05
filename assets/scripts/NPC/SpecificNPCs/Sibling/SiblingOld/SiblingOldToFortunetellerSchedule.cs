using UnityEngine;
using System.Collections;

public class SiblingOldToFortunetellerSchedule : Schedule {
	public SiblingOldToFortunetellerSchedule (NPC toManage) : base (toManage) {
		schedulePriority = (int)priorityEnum.Medium;			
	}
	protected override void Init() {
		Add(new TimeTask(.25f, new IdleState(_toManage)));
//Move in front of Fortuneteller old
		Task siblingOldToFortunetellerPartOne = (new Task(new MoveThenDoState(_toManage, new Vector3(27f, 7.8f + (LevelManager.levelYOffSetFromCenter*2), 0), new MarkTaskDone(_toManage)))); 
		//siblingOldToFortunetellerPartOne.AddFlagToSet(FlagStrings.siblingOldGoToFortuneTellerIntro);
		Add(siblingOldToFortunetellerPartOne);
		Add(new TimeTask(5f, new IdleState(_toManage)));
	}
}
