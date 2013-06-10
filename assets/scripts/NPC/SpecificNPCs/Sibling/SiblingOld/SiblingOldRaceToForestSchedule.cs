using UnityEngine;
using System.Collections;

public class SiblingOldRaceToForestSchedule : Schedule {
	
	public SiblingOldRaceToForestSchedule (NPC toManage) : base (toManage) {
		schedulePriority = (int)priorityEnum.High;			
	}
	protected override void Init() {
// Move to the right of the well near Player's house	
			Add(new TimeTask(.25f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, MapLocations.PlayerHouseWaterWellOld, new MarkTaskDone(_toManage)))); 
// Initiate Dialogue: Ready? | Set?, takes 3 seconds for dialogue to finish
			Task activateRacePartOne = (new Task(new MoveThenDoState(_toManage, MapLocations.PlayerHouseWaterWellOld, new MarkTaskDone(_toManage)))); 
			activateRacePartOne.AddFlagToSet(FlagStrings.siblingOldIntroRaceChatPartOneFlag);
			Add(activateRacePartOne);
			Add(new TimeTask(3f, new IdleState(_toManage)));	
		
// Initiate Dialogue: Go!! | Hurry up Slowpoke! | Hahahahaha, takes 1.6 seconds for dialogue to finish
			Task activateRacePartTwo = (new TimeTask(.05f, new IdleState(_toManage))); 
			activateRacePartTwo.AddFlagToSet(FlagStrings.siblingOldIntroRaceChatPartTwoFlag);
			Add(activateRacePartTwo);
			Add(new TimeTask(.1f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage,MapLocations.MiddleOfHauntedForestOld , new MarkTaskDone(_toManage)))); 
			Add(new TimeTask(1.5f, new IdleState(_toManage)));
		
// Move to the edge of the bottom forest area, Initiate Dialogue: Whew! Good Race! It's been a while, hasn't it?, wait for 8.5 seconds for dialogue to finish		
			Task activateRacePartThree = (new Task(new MoveThenDoState(_toManage, new Vector3(MapLocations.MiddleOfHauntedForestOld.x - 1.5f,MapLocations.MiddleOfHauntedForestOld.y, MapLocations.MiddleOfHauntedForestOld.z), new MarkTaskDone(_toManage)))); 
			activateRacePartThree.AddFlagToSet(FlagStrings.siblingOldIntroRaceChatPartThreeFlag);
			Add(activateRacePartThree);
			Add(new TimeTask(8.5f, new IdleState(_toManage)));
		
// Move to the middle of bottom forest area, Initiate Dialogue: Mom's garden isn't look so good | Too bad we didn't help before she passed. If only we could go back right?, wait for 9 seconds for dialogue to finish		
			Task activateRacePartFour = (new Task(new MoveThenDoState(_toManage, new Vector3(MapLocations.MiddleOfHauntedForestOld.x + 8f,MapLocations.MiddleOfHauntedForestOld.y, MapLocations.MiddleOfHauntedForestOld.z), new MarkTaskDone(_toManage)))); 
			activateRacePartFour.AddFlagToSet(FlagStrings.siblingOldIntroRaceChatPartFourFlag);
			Add(activateRacePartFour);
			Add(new TimeTask(9f, new IdleState(_toManage)));

// Move to the edge of the bottom forest area, Initiate Dialogue: Hey. | Want to go visit Carpy? 	
			Task activateRacePartFive = (new TimeTask(.05f, new IdleState(_toManage))); //(new Task(new MoveThenDoState(_toManage,new Vector3(MapLocations.MiddleOfHauntedForestOld.x - 1.5f,MapLocations.MiddleOfHauntedForestOld.y, MapLocations.MiddleOfHauntedForestOld.z), new MarkTaskDone(_toManage)))); 
			activateRacePartFive.AddFlagToSet(FlagStrings.siblingOldIntroRaceChatPartFiveFlag);
			Add(activateRacePartFive);
			Add(new TimeTask(5f, new IdleState(_toManage)));

// Move to well near player's house, initiate next schedule		
			Task activateRacePartSix = (new Task(new MoveThenDoState(_toManage,MapLocations.PlayerHouseWaterWellOld, new MarkTaskDone(_toManage)))); 
			activateRacePartSix.AddFlagToSet(FlagStrings.siblingOldIntroRaceChatPartSixFlag);
			Add(activateRacePartSix);
			Add(new TimeTask(.05f, new IdleState(_toManage)));
	}
}
