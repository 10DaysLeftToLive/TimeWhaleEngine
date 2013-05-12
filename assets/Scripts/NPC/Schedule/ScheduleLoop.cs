using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ScheduleLoop : Schedule {
	public ScheduleLoop(NPC toManage) : base(toManage) {
	}
	
	public ScheduleLoop(NPC toManage, Enum priority) : base(toManage, priority) {
	}
	
	public ScheduleLoop(Queue<Task> tasksToDo, NPC toManage, Enum priority) : base(tasksToDo, toManage, priority) {
	}
	
	~ScheduleLoop() {
		_tasksToDo.Clear();
	}
	
	public override void NextTask() {
		if (current != null){ // if we are going to skip the current task but it has not finished
			current.Finish();
		}
		
		if (_tasksToDo.Count > 0) {
			current = _tasksToDo.Dequeue();
			Debug.Log (_toManage.name + " is now switching to " + current.StatePerforming);
			_toManage.ForceChangeToState(current.StatePerforming);
			
			if (current != null) {
				_tasksToDo.Enqueue(current);
			}
		}
	}
}
