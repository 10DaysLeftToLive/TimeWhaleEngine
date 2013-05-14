using UnityEngine;
using System.Collections;

public class YoungRunIslandToCarpenterScript : Schedule {
	
	public YoungRunIslandToCarpenterScript (NPC toManage) : base (toManage) {
		
		schedulePriority = (int)priorityEnum.Low;			
	}
	
	protected override void Init() {
		//NPCManager.instance.RemoveInstanceFlag("");
		AddFlagGroup(FlagStrings.StartedRace);
		Add(new TimeTask (2f, new IdleState(_toManage))); //or self-triggering
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (12, .2f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(.2f, new IdleState(_toManage)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (11.8f, .2f, .3f), new MarkTaskDone(_toManage)))); // at bridge
		Add(new TimeTask(10f, new WaitTillPlayerCloseState(_toManage, _toManage.player)));
		Task reachCarpenterTask = new Task(new MoveThenDoState(_toManage, new Vector3 (28, .2f, .3f), new MarkTaskDone(_toManage))); // at carpenter
		reachCarpenterTask.AddFlagToSet(FlagStrings.RunToCarpenter);
		Add(reachCarpenterTask);
		Add(new TimeTask(.2f, new IdleState(_toManage)));
		//Add(new Task (new IdleState(_toManage)));
		
	}
}
