using UnityEngine;
using System.Collections;

public class SiblingOldRaceToForestSchedule : Schedule {
	
	public SiblingOldRaceToForestSchedule (NPC toManage) : base (toManage) {
		schedulePriority = (int)priorityEnum.High;			
	}
	protected override void Init() {
			
			Add(new TimeTask(.25f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, MapLocations.PlayerHouseWaterWellOld, new MarkTaskDone(_toManage)))); 
			Task activateRacePartOne = (new Task(new MoveThenDoState(_toManage, MapLocations.PlayerHouseWaterWellOld, new MarkTaskDone(_toManage)))); 
			activateRacePartOne.AddFlagToSet(FlagStrings.siblingOldIntroRaceChatPartOneFlag);
			Add(activateRacePartOne);
			Add(new TimeTask(3f, new IdleState(_toManage)));	
		
			Task activateRacePartTwo = (new Task(new MoveThenDoState(_toManage, MapLocations.PlayerHouseWaterWellOld, new MarkTaskDone(_toManage)))); 
			activateRacePartTwo.AddFlagToSet(FlagStrings.siblingOldIntroRaceChatPartTwoFlag);
			Add(activateRacePartTwo);
			Add(new TimeTask(.1f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage,MapLocations.MiddleOfHauntedForestOld , new MarkTaskDone(_toManage)))); 
			Add(new TimeTask(1.5f, new IdleState(_toManage)));
		
			Task activateRacePartThree = (new Task(new MoveThenDoState(_toManage, new Vector3(MapLocations.MiddleOfHauntedForestOld.x - 1.5f,MapLocations.MiddleOfHauntedForestOld.y, MapLocations.MiddleOfHauntedForestOld.z), new MarkTaskDone(_toManage)))); 
			activateRacePartThree.AddFlagToSet(FlagStrings.siblingOldIntroRaceChatPartThreeFlag);
			Add(activateRacePartThree);
		
			Add(new TimeTask(8.5f, new IdleState(_toManage)));
		
			Task activateRacePartFour = (new Task(new MoveThenDoState(_toManage, new Vector3(MapLocations.MiddleOfHauntedForestOld.x + 6f,MapLocations.MiddleOfHauntedForestOld.y, MapLocations.MiddleOfHauntedForestOld.z), new MarkTaskDone(_toManage)))); 
			activateRacePartFour.AddFlagToSet(FlagStrings.siblingOldIntroRaceChatPartFourFlag);
			Add(activateRacePartFour);
		
			Add(new TimeTask(12f, new IdleState(_toManage)));
		
			Task activateRacePartFive = (new Task(new MoveThenDoState(_toManage,new Vector3(MapLocations.MiddleOfHauntedForestOld.x - 1.5f,MapLocations.MiddleOfHauntedForestOld.y, MapLocations.MiddleOfHauntedForestOld.z), new MarkTaskDone(_toManage)))); 
			activateRacePartFive.AddFlagToSet(FlagStrings.siblingOldIntroRaceChatPartFiveFlag);
			Add(activateRacePartFive);
		
			Add(new TimeTask(8f, new IdleState(_toManage)));
		
			Task activateRacePartSix = (new Task(new MoveThenDoState(_toManage,MapLocations.PlayerHouseWaterWellOld, new MarkTaskDone(_toManage)))); 
			activateRacePartSix.AddFlagToSet(FlagStrings.siblingOldIntroRaceChatPartSixFlag);
			Add(activateRacePartSix);
		
			Add(new TimeTask(.05f, new IdleState(_toManage)));
	}
}
