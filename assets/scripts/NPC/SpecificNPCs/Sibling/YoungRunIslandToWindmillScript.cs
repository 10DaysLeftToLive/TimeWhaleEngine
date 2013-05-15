using UnityEngine;
using System.Collections;

public class YoungRunIslandToWindmillScript : Schedule {
	
	public YoungRunIslandToWindmillScript (NPC toManage) : base (toManage) {
		
		schedulePriority = (int)priorityEnum.Low;			
	}
	
	protected override void Init() {
		Add(new TimeTask(.25f, new IdleState(_toManage)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (0, 7.5f, .3f), new MarkTaskDone(_toManage))));	
		Add(new TimeTask(5f, new IdleState(_toManage)));
		Task setOffReflectionTreeFlagTask = new Task(new MoveThenDoState(_toManage, new Vector3 (-2.5f, -11.5f, .3f), new MarkTaskDone(_toManage))); // at top staircase
		setOffReflectionTreeFlagTask.AddFlagToSet(FlagStrings.RunToReflectionTree);
		Add(setOffReflectionTreeFlagTask);
		Add(new TimeTask(1f, new IdleState(_toManage)));
		
		
		
	}
}
