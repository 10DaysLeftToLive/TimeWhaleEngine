using System.Collections.Generic;

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
	
	// Does not run and change schedules the same tick.
	public void Run(float timeSinceLastTick) {
		if (HasSchedule()) {
			current.Run(timeSinceLastTick);
			if (current.IsComplete()) {
				current = null;
			}
		} else {
			NextSchedule();
		}
	}
		
	public void NextSchedule() {
		current = _schedulesToDo.Pop();
		current.Resume();
	}
	
	public void Add(Schedule schedule) {
		_schedulesToDo.Push(schedule);
	}
}