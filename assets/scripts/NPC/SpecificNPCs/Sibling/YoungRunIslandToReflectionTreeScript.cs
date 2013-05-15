using UnityEngine;
using System.Collections;

public class YoungRunIslandToReflectionTreeScript : Schedule {
	
	public YoungRunIslandToReflectionTreeScript (NPC toManage) : base (toManage) {
		
		schedulePriority = (int)priorityEnum.Low;			
	}
	
	protected override void Init() {
		Add(new TimeTask(.2f, new IdleState(_toManage))); //or self-triggering
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (-24f, 18.2f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(10f, new WaitTillPlayerCloseState(_toManage, _toManage.player)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (-40f, 18.2f, .3f), new MarkTaskDone(_toManage))));
		Task setOffHomeFlagTask = new Task(new MoveThenDoState(_toManage, new Vector3 (-35f, 18.2f, .3f), new MarkTaskDone(_toManage)));
		setOffHomeFlagTask.AddFlagToSet(FlagStrings.RunToHome);
		Add(setOffHomeFlagTask);
		Add(new TimeTask(4f, new IdleState(_toManage)));
	}
}
