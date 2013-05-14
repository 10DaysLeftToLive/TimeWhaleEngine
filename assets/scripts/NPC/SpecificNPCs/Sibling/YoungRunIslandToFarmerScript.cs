using UnityEngine;
using System.Collections;

public class YoungRunIslandToFarmerScript : Schedule {
	
	public YoungRunIslandToFarmerScript (NPC toManage) : base (toManage) {
		
		schedulePriority = (int)priorityEnum.Low;			
	}
	
	protected override void Init() {
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (66, -7.6f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(.25f, new IdleState(_toManage)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (59, .2f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(2f, new WaitTillPlayerCloseState(_toManage, _toManage.player)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (47, -1f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(2f, new WaitTillPlayerCloseState(_toManage, _toManage.player)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (53, 5f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(2f, new WaitTillPlayerCloseState(_toManage, _toManage.player)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (50, 10f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(2f, new WaitTillPlayerCloseState(_toManage, _toManage.player)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (55, 16f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(2f, new WaitTillPlayerCloseState(_toManage, _toManage.player)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (63, 16f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(15f, new IdleState(_toManage)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (75, 16f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(20f, new IdleState(_toManage)));
			//runToLighthouse.Add(new Task(new MoveThenDoState(_toManage, new Vector3 (28, .2f, .3f), new MarkTaskDone(_toManage))));
	}
}
