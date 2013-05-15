using UnityEngine;
using System.Collections;

public class YoungRunIslandToHomeScript : Schedule {
	
	public YoungRunIslandToHomeScript (NPC toManage) : base (toManage) {
		
		schedulePriority = (int)priorityEnum.Low;			
	}
	
		protected override void Init() {
			
			Add(new TimeTask(.25f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (0f, -1.8f, .3f), new MarkTaskDone(_toManage))));
			Task setOffInTroubleFlagTask = new Task(new MoveThenDoState(_toManage, new Vector3 (0f, -1.8f, .3f), new MarkTaskDone(_toManage))); // at carpenter
			setOffInTroubleFlagTask.AddFlagToSet(FlagStrings.PostSiblingExplore);
			Add(setOffInTroubleFlagTask);
			Add(new TimeTask(.25f, new IdleState(_toManage)));
		}
}
