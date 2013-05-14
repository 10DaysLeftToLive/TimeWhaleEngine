using UnityEngine;
using System.Collections;

public class YoungRunIslandToBeachScript : Schedule {
	
	public YoungRunIslandToBeachScript (NPC toManage) : base (toManage) {
		
		schedulePriority = (int)priorityEnum.Low;			
	}
	protected override void Init() {
			Add(new TimeTask(1f, new WaitTillPlayerCloseState(_toManage, _toManage.player)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (51, -.05f, .3f), new MarkTaskDone(_toManage)))); // at top staircase
			Add(new TimeTask(1f, new WaitTillPlayerCloseState(_toManage, _toManage.player)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (66, -7.6f, .3f), new MarkTaskDone(_toManage)))); // left side of beach
			Add(new TimeTask(1f, new WaitTillPlayerCloseState(_toManage, _toManage.player)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (50, -7.5f, .3f), new MarkTaskDone(_toManage)))); // at base of stairs (beach)
			Add(new TimeTask(1f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (74, -3.4f, .3f), new MarkTaskDone(_toManage)))); // Pier
			Add(new TimeTask(1f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (66, -7.6f, .3f), new MarkTaskDone(_toManage)))); // left side of beach
			Add(new TimeTask(1f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (57, -6.5f, .3f), new MarkTaskDone(_toManage)))); // at base staircase (beach)
			Add(new TimeTask(1f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (58, -6.5f, .3f), new MarkTaskDone(_toManage)))); // at top staircase
			Add(new TimeTask(1f, new WaitTillPlayerCloseState(_toManage, _toManage.player)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (51, -.05f, .3f), new MarkTaskDone(_toManage)))); // at top staircase
			Add(new TimeTask(1f, new WaitTillPlayerCloseState(_toManage, _toManage.player)));
	}
}
