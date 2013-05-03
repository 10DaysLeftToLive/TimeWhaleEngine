using UnityEngine;
using System.Collections;

public class DefaultSchedule : ScheduleLoop {
	public DefaultSchedule(NPC toManage) : base(toManage) {
		init();
	}
	
	private void init() {
		Add(new Task(new IdleState(_toManage)));
		schedulePriority = (int)Schedule.priorityEnum.Default;
		canPassiveChat = false;
		canInteractWithPlayer = true;
	}
}
