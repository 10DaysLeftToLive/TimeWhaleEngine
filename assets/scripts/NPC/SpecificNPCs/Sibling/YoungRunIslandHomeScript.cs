using UnityEngine;
using System.Collections;

public class YoungRunHomeIslandScript : Schedule {
	
	public YoungRunHomeIslandScript (NPC toManage) : base (toManage) {
		
		schedulePriority = (int)priorityEnum.Low;			
	}
	
		protected override void Init() {
			
			Add(new TimeTask(.25f, new IdleState(_toManage)));
			//MapLocations.
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (63, 16f, .3f), new MarkTaskDone(_toManage))));
			Add(new TimeTask(1f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (55, 16f, .3f), new MarkTaskDone(_toManage))));
			Add(new TimeTask(1.25f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (50, 10f, .3f), new MarkTaskDone(_toManage))));
			Add(new TimeTask(1f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (37, 10f, .3f), new MarkTaskDone(_toManage))));
			Add(new TimeTask(.5f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (40, 10f, .3f), new MarkTaskDone(_toManage))));
			Add(new TimeTask(.5f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (27, 10f, .3f), new MarkTaskDone(_toManage))));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (27.05f, 10f, .3f), new MarkTaskDone(_toManage))));
			
		/*
			Add(new TimeTask(.25f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (-40, 16f, .3f), new MarkTaskDone(_toManage))));
			Add(new TimeTask(2f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (-49, 18f, .3f), new MarkTaskDone(_toManage))));
			Add(new TimeTask(2f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (-41, 25f, .3f), new MarkTaskDone(_toManage))));
			Add(new TimeTask(.5f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (-48, 30f, .3f), new MarkTaskDone(_toManage))));
			Add(new TimeTask(.1f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (-66, 29.5f, .3f), new MarkTaskDone(_toManage))));
			
			Add(new TimeTask(.25f, new IdleState(_toManage)));
		*/	
	/*
			//
			Add(new TimeTask(1f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (-5, -1.5f, .3f), new MarkTaskDone(_toManage))));
			Add(new TimeTask(.5f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (-2, -1.5f, .3f), new MarkTaskDone(_toManage))));
			
			//
			Add(new TimeTask(3f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (0, -1.5f, .3f), new MarkTaskDone(_toManage))));
			*/
		}
}
