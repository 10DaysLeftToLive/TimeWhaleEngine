using UnityEngine;
using System.Collections;

public class YoungRunIslandToFarmerScript : Schedule {
	
	public YoungRunIslandToFarmerScript (NPC toManage) : base (toManage) {
		
		schedulePriority = (int)priorityEnum.Low;			
	}
	
	protected override void Init() {
		Add(new TimeTask(.25f, new IdleState(_toManage)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (38f, -.7f, .3f), new MarkTaskDone(_toManage)))); // base of 1st floor stairs right
		Add(new TimeTask(5f, new WaitTillPlayerCloseState(_toManage, ref _toManage.player)));
		//Add(new Task(new MoveThenDoState(_toManage,(MapLocations._TopOfFirstFloorStairsRightYoung), new MarkTaskDone(_toManage)))); // top of 1st floor stairs right
		//Add(new TimeTask(5f, new WaitTillPlayerCloseState(_toManage, _toManage.player)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (37.8f, 7.5f, .3f), new MarkTaskDone(_toManage)))); // top of 1st floor stairs right
		Add(new TimeTask(.25f, new IdleState(_toManage)));
		Add(new TimeTask(5f, new WaitTillPlayerCloseState(_toManage, ref _toManage.player)));
		//Add(new Task(new MoveThenDoState(_toManage, new Vector3 (45f, 7.4f, .3f), new MarkTaskDone(_toManage)))); // base of 2nd floor stairs right
		//Add(new TimeTask(2f, new WaitTillPlayerCloseState(_toManage, _toManage.player)));
		//Add(new Task(new MoveThenDoState(_toManage, new Vector3 (41f, 11f, .3f), new MarkTaskDone(_toManage)))); // middle of 2nd floor stairs
		//Add(new TimeTask(2f, new WaitTillPlayerCloseState(_toManage, _toManage.player)));
		//Add(new Task(new MoveThenDoState(_toManage, new Vector3 (46f, 16f, .3f), new MarkTaskDone(_toManage)))); // top of 2nd floor stairs
		//Add(new TimeTask(5f, new WaitTillPlayerCloseState(_toManage, _toManage.player)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (55f, 15.5f, .3f), new MarkTaskDone(_toManage)))); // Reached Destination: Farmer family
		Add(new TimeTask(5f, new IdleState(_toManage)));
		Task setOffMarketFlag = new Task(new MoveThenDoState(_toManage, new Vector3 (54f, 15.5f, .3f), new MarkTaskDone(_toManage))); // at top staircase
		setOffMarketFlag.AddFlagToSet(FlagStrings.RunToMarket);
		Add(setOffMarketFlag);
		Add(new TimeTask(.2f, new IdleState(_toManage)));
		//Add(new Task(new MoveThenDoState(_toManage, new Vector3 (75, 16f, .3f), new MarkTaskDone(_toManage))));
		//Add(new TimeTask(20f, new IdleState(_toManage)));
	}
}
