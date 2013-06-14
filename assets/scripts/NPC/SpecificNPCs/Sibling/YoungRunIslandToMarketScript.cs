using UnityEngine;
using System.Collections;

public class YoungRunIslandToMarketScript : Schedule {
	
	public YoungRunIslandToMarketScript (NPC toManage) : base (toManage) {
		
		schedulePriority = (int)priorityEnum.Low;			
	}
	
	protected override void Init() {
		Add(new TimeTask(.2f, new IdleState(_toManage))); //or self-triggering
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (22.5f, 7.5f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(10f, new WaitTillPlayerCloseState(_toManage, ref _toManage.player)));
		Add(new TimeTask(5f, new IdleState(_toManage)));
		Task setOffWindmillFlag = new Task((new MoveThenDoState(_toManage, new Vector3 (20.5f, 7.5f,.3f), new MarkTaskDone(_toManage))));
		setOffWindmillFlag.AddFlagToSet(FlagStrings.RunToWindmill);
		Add(setOffWindmillFlag);
		Add(new TimeTask(.2f, new IdleState(_toManage)));
	}
}
