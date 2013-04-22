using System.Collections.Generic;
using UnityEngine;

// Used for scheduling
// The end of the list is the highest priority
// high 1 - 10 low
public class ScheduleStack {
	Schedule current;
	PriorityStack _schedulesToDo;

    public ScheduleStack() {
      this._schedulesToDo = new PriorityStack();
    }
	
	protected bool HasSchedule() {
		return (current != null);
	}
	
	public bool CanChat() {
		return current.CanChat;
	}
	
	// Does not run and change schedules the same tick.
	public void Run(float timeSinceLastTick) {
		if (HasSchedule()) {
			current.Run(timeSinceLastTick);
			if (current.IsComplete()) {
				Debug.Log("Current schedule complete");
				current = null;
			}
		} else {
			NextSchedule();
		}
	}
	
	public void NextTask() {
		Debug.Log("Schedule Stack next task");
		
		current.NextTask();
	}
		
	public void NextSchedule() {
		current = _schedulesToDo.Pop();
		current.Resume();
	}
	
	public void Add(Schedule schedule) {
		if (current == null) {
			current = schedule;
		} 
		else if (schedule.CheckPriorityTo(current) >= 0) {
			_schedulesToDo.Push(current);
			current = schedule;
			current.Resume();
		} else {
			_schedulesToDo.Push(schedule);
		}
	}
}