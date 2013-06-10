using UnityEngine;
using System.Collections;

public class SiblingOldToLighthouseScript : Schedule {
	public SiblingOldToLighthouseScript (NPC toManage) : base (toManage) {
		schedulePriority = (int)priorityEnum.Medium;			
	}
	
	protected override void Init() {
		Add(new Task(new MoveThenDoState(_toManage, new Vector3(48f, (LevelManager.levelYOffSetFromCenter*2) + 15, 0), new MarkTaskDone(_toManage))));
		Add(new TimeTask(60f, new WaitTillPlayerCloseState(_toManage, ref _toManage.player, 2f))); 	
		
		Task siblingAtLightHouseOne = (new TimeTask(.05f, new IdleState(_toManage))); 		
		siblingAtLightHouseOne.AddFlagToSet(FlagStrings.siblingOldTalkAboutFarmerOne);
		Add(siblingAtLightHouseOne);
		
		Add(new TimeTask(5.25f, new IdleState(_toManage))); 
		Add(new Task(new MoveThenDoState(_toManage, new Vector3(51f, (LevelManager.levelYOffSetFromCenter*2) + 15, 0), new MarkTaskDone(_toManage))));
		Add(new TimeTask(5.5f, new IdleState(_toManage))); 
		
		Task siblingAtLightHouseTwo = (new TimeTask(.05f, new IdleState(_toManage))); 		
		siblingAtLightHouseTwo.AddFlagToSet(FlagStrings.siblingOldTalkAboutFarmerTwo);
		Add(siblingAtLightHouseTwo);
		Add(new TimeTask(3.5f, new IdleState(_toManage))); 
		
		Task siblingAtLightHouseThree = (new TimeTask(.05f, new IdleState(_toManage))); 
		siblingAtLightHouseThree.AddFlagToSet(FlagStrings.siblingOldTalkAboutFarmerThree);
		Add(siblingAtLightHouseThree);
		Add(new TimeTask(6.5f, new IdleState(_toManage))); 
	}
}
