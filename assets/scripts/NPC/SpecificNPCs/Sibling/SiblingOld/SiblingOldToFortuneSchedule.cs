using UnityEngine;
using System.Collections;

public class SiblingOldToFortunetellerSchedule : Schedule {
	private static readonly float Y_COORDINATE = -1.735313f + (LevelManager.levelYOffSetFromCenter*2);	
	public SiblingOldToFortunetellerSchedule (NPC toManage) : base (toManage) {
		schedulePriority = (int)priorityEnum.Medium;			
	}
	
	protected override void Init() {
//Wait for 5 seconds as Carpenter finishes his chat. Then say: Um | I'm going to the fortuneteller. So I'll pass | Have fun though | See ya later!		
			Add(new TimeTask(5f, new IdleState(_toManage)));
			Task siblingOldGoToFortuneTellerIntroTask = (new Task(new MoveThenDoState(_toManage, new Vector3(30f, _toManage.transform.position.y + (LevelManager.levelYOffSetFromCenter*2), 0), new MarkTaskDone(_toManage)))); 
			siblingOldGoToFortuneTellerIntroTask.AddFlagToSet(FlagStrings.siblingOldGoToFortuneTellerIntro);
			Add(siblingOldGoToFortuneTellerIntroTask);
			Add(new TimeTask(4.25f, new IdleState(_toManage)));
		
//Sibling begins conversation: I'm here for my fortune! Are you ready?		
			Task siblingOldToFortunetellerPartOne = (new Task(new MoveThenDoState(_toManage, new Vector3(27f, 7.8f + (LevelManager.levelYOffSetFromCenter*2), 0), new MarkTaskDone(_toManage)))); 
			siblingOldToFortunetellerPartOne.AddFlagToSet(FlagStrings.siblingOldTalkToFortunePartOne);
			Add(siblingOldToFortunetellerPartOne);
			Add(new TimeTask(4.5f, new IdleState(_toManage))); //Sibling
			
//Fortuneteller says: Patience.
			Task fortunetellerToSiblingPartOne = (new TimeTask(.05f, new IdleState(_toManage))); 
			fortunetellerToSiblingPartOne.AddFlagToSet(FlagStrings.FortunetellerTalkToSiblingOldPartOne);
			Add(fortunetellerToSiblingPartOne);
			Add(new TimeTask(2f, new IdleState(_toManage))); //Sibling
	
//Sibling says: ... Now?		
			Task siblingOldToFortunetellerPartTwo = (new TimeTask(.05f, new IdleState(_toManage))); 
			siblingOldToFortunetellerPartTwo.AddFlagToSet(FlagStrings.siblingOldTalkToFortunePartTwo);
			Add(siblingOldToFortunetellerPartTwo);
			Add(new TimeTask(4f, new IdleState(_toManage))); //Sibling

//Fortuneteller says: Patience!!
			Task fortunetellerToSiblingPartTwo = (new TimeTask(.05f, new IdleState(_toManage))); 
			fortunetellerToSiblingPartTwo.AddFlagToSet(FlagStrings.FortunetellerTalkToSiblingOldPartTwo);
			Add(fortunetellerToSiblingPartTwo);
			Add(new TimeTask(3f, new IdleState(_toManage))); //Sibling
		
//Sibling says: . . .  Now?		
			Task siblingOldToFortunetellerPartThree = (new TimeTask(.05f, new IdleState(_toManage))); 
			siblingOldToFortunetellerPartThree.AddFlagToSet(FlagStrings.siblingOldTalkToFortunePartThree);
			Add(siblingOldToFortunetellerPartThree);
			Add(new TimeTask(5.5f, new IdleState(_toManage))); //Sibling
		
//Fortuneteller says: . . . !! | ... | .. Ok. | Let's get this over with.
			Task fortunetellerToSiblingPartThree = (new TimeTask(.05f, new IdleState(_toManage))); 
			fortunetellerToSiblingPartThree.AddFlagToSet(FlagStrings.FortunetellerTalkToSiblingOldPartThree);
			Add(fortunetellerToSiblingPartThree);
			Add(new TimeTask(6f, new IdleState(_toManage))); //Sibling
		
		
		
///////////////////////////////////////////////////////////////////////////////////////////////////////////////		
			Task goToFarmerArea = (new TimeTask(.05f, new IdleState(_toManage))); 
			goToFarmerArea.AddFlagToSet(FlagStrings.oldSiblingGoToFarmerArea);
			Add(goToFarmerArea);
			Add(new TimeTask(2f, new IdleState(_toManage))); //Sibling
			Add(new Task(new MoveThenDoState(_toManage, new Vector3(48f, (LevelManager.levelYOffSetFromCenter*2) + 15, 0), new MarkTaskDone(_toManage))));
			Add(new TimeTask(60f, new WaitTillPlayerCloseState(_toManage, ref _toManage.player, 3f))); //Sibling
			// 
			//TALK ABOUT FARMER FAMILY BRIEFLY
			//
			//Task goToFarmerArea = (new TimeTask(.05f, new IdleState(_toManage))); 
			//goToFarmerArea.AddFlagToSet(FlagStrings.oldSiblingGoToFarmerArea);
			//Add(goToFarmerArea);
			//
			//
			//Add(new Task(new MoveThenDoState(_toManage, new Vector3(19f, (LevelManager.levelYOffSetFromCenter*2) + 7, 0), new MarkTaskDone(_toManage))));
			//Add(new TimeTask(30f, new WaitTillPlayerCloseState(_toManage, ref _toManage.player, 3f))); //Sibling
		
			//ShowOneOffChat("Don't look, he's crazy.");
		
			// go to reflection tree
			//Add(new Task(new MoveThenDoState(_toManage, new Vector3(89f, (LevelManager.levelYOffSetFromCenter*2) + 17, 0), new MarkTaskDone(_toManage))));
			// TALK ABOUT GOING BACK, ENCOURAGE THE ACTION AT THE TREE --> PASS THE PLAYER OFF TO SIBLING MIDDLE (SHOULD BE LOCATED AT THE TREE, DISTRESSED AT MOM's DEATHBED)
	}
}
