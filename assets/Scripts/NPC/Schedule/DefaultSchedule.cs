using UnityEngine;
using System.Collections;

public class DefaultSchedule : ScheduleLoop {
	public DefaultSchedule(NPC toManage) : base(toManage) {
	}
	
	protected override void Init() {
		Add(new Task(new IdleState(_toManage)));
		schedulePriority = (int)Schedule.priorityEnum.Default;
		canPassiveChat = false;
		canInteractWithPlayer = true;
	}
}