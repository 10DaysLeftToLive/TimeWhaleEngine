using UnityEngine;
using System.Collections;

public class SiblingOldToReflectionTree : Schedule {
	public SiblingOldToReflectionTree (NPC toManage) : base (toManage) {
		schedulePriority = (int)priorityEnum.Medium;			
	}
	
	protected override void Init() {
//Begin heading toward Fortuneteller saying: "Let's go!"	
		Add(new TimeTask(7f, new IdleState(_toManage)));  	

//Wait for player at bottom of stairs in case they aren't able to keep up, then continue		
		Add(new Task(new MoveThenDoState(_toManage, new Vector3(32.5f, (LevelManager.levelYOffSetFromCenter*2) + 7.8f, 0), new MarkTaskDone(_toManage))));
		//Add(new Task(new MoveThenDoState(_toManage, new Vector3(32.8f, (LevelManager.levelYOffSetFromCenter*2) + 7.8f, 0), new MarkTaskDone(_toManage))));
		Add(new TimeTask(10f,new WaitTillPlayerCloseState(_toManage, ref _toManage.player, 3f))); 

//Greet Fortuneteller with: Hey! Thanks again!		
		Task siblingAtFortuneteller = (new Task(new MoveThenDoState(_toManage, new Vector3(27.5f, (LevelManager.levelYOffSetFromCenter*2) + 7.8f, 0), new MarkTaskDone(_toManage))));
		siblingAtFortuneteller.AddFlagToSet(FlagStrings.oldSiblingReflectionTreeFortuneteller);
		Add(siblingAtFortuneteller);
		Add(new TimeTask(1.5f, new IdleState(_toManage))); 
		
Add(new Task(new MoveThenDoState(_toManage, new Vector3(10.4f, (LevelManager.levelYOffSetFromCenter*2) + 7.8f, 0), new MarkTaskDone(_toManage))));
//Add(new Task(new MoveThenDoState(_toManage, new Vector3(32.8f, (LevelManager.levelYOffSetFromCenter*2) + 7.8f, 0), new MarkTaskDone(_toManage))));
Add(new TimeTask(10f,new WaitTillPlayerCloseState(_toManage, ref _toManage.player, 3f))); 

		
		Task siblingAtCastleman = (new Task(new MoveThenDoState(_toManage, new Vector3(9.4f, (LevelManager.levelYOffSetFromCenter*2) + 7.8f, 0), new MarkTaskDone(_toManage))));
		siblingAtCastleman.AddFlagToSet(FlagStrings.oldSiblingReflectionTreeCastleman);
		Add(siblingAtCastleman);
		Add(new TimeTask(3f, new IdleState(_toManage)));  

		Task siblingAtReflectionPond = (new Task(new MoveThenDoState(_toManage, new Vector3(-33f, (LevelManager.levelYOffSetFromCenter*2) + 19.5f, 0), new MarkTaskDone(_toManage))));
		siblingAtReflectionPond.AddFlagToSet(FlagStrings.oldSiblingReflectionTreePond);
		Add(siblingAtReflectionPond);
		Add(new TimeTask(6.5f, new IdleState(_toManage))); 
		
		Task siblingAtReflectionTreePartOne = (new Task(new MoveThenDoState(_toManage, new Vector3(-38f, (LevelManager.levelYOffSetFromCenter*2) + 19.5f, 0), new MarkTaskDone(_toManage))));
		siblingAtReflectionTreePartOne.AddFlagToSet(FlagStrings.oldSiblingReflectionTreePartOne);
		Add(siblingAtReflectionTreePartOne);
		Add(new TimeTask(1.5f, new IdleState(_toManage))); 
		Add(new Task(new MoveThenDoState(_toManage, new Vector3(-37f, (LevelManager.levelYOffSetFromCenter*2) + 19.5f, 0), new MarkTaskDone(_toManage))));
		Add(new TimeTask(8f, new IdleState(_toManage))); 
		
		Task siblingAtReflectionTreePartTwo = (new Task(new MoveThenDoState(_toManage, new Vector3(-40f, (LevelManager.levelYOffSetFromCenter*2) + 19.5f, 0), new MarkTaskDone(_toManage))));
		siblingAtReflectionTreePartTwo.AddFlagToSet(FlagStrings.oldSiblingReflectionTreePartTwo);
		Add(siblingAtReflectionTreePartTwo);
		Add(new TimeTask(13f, new IdleState(_toManage))); 
		
		Task siblingAtReflectionTreePartThree = (new Task(new MoveThenDoState(_toManage, new Vector3(-43f, (LevelManager.levelYOffSetFromCenter*2) + 19.5f, 0), new MarkTaskDone(_toManage))));
		siblingAtReflectionTreePartThree.AddFlagToSet(FlagStrings.oldSiblingReflectionTreePartThree);
		Add(siblingAtReflectionTreePartThree);
		Add(new TimeTask(6.5f, new IdleState(_toManage)));
/*		
		Task siblingAtReflectionTreePartFour = (new TimeTask(.05f, new IdleState(_toManage)));
		siblingAtReflectionTreePartFour.AddFlagToSet(FlagStrings.oldSiblingReflectionTreePartFour);
		Add(siblingAtReflectionTreePartFour);
		Add(new TimeTask(21f, new IdleState(_toManage)));
*/
		
//Change into some other state		
	}
}
