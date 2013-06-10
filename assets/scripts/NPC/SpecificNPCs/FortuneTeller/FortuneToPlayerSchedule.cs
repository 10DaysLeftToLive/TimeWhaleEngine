using UnityEngine;
using System.Collections;

public class FortuneToPlayerSchedule : Schedule {
	public FortuneToPlayerSchedule (NPC toManage) : base (toManage) {
		schedulePriority = (int)priorityEnum.Medium;			
	}
	
	protected override void Init() {
	
	}
}
