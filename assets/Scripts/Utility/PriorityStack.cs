using System.Collections.Generic;

// Used for scheduling
// The end of the list is the highest priority
// high 1 - 10 low
public class PriorityStack {
	List<Schedule> _schedulesToDo;

    public PriorityStack() {
      this._schedulesToDo = new List<Schedule>();
    }
	
	~PriorityStack() {
		_schedulesToDo.Clear ();
	}
	
	// Push new value onto the list depending on the priority
    public void Push(Schedule item) {
		_schedulesToDo.Add(item);
		int ci = _schedulesToDo.Count - 1;
		while (ci > 0) {
			int pi = (ci - 1);
			if(_schedulesToDo[ci].CheckPriorityTo(_schedulesToDo[pi]) >= 0) {
				break;
			}
			Schedule tmp = _schedulesToDo[ci];
			_schedulesToDo[ci] = _schedulesToDo[pi];
			_schedulesToDo[pi] = tmp;
			ci = pi;
		}
    }
	
    public Schedule Pop() {
		// Pop the front of the list
		int li = _schedulesToDo.Count - 1; // last index (before removal)
		Schedule toReturn = _schedulesToDo[li];
		_schedulesToDo.RemoveAt(li);
		return toReturn;
    }
	
	public Schedule peek() {
		return _schedulesToDo[_schedulesToDo.Count - 1];
	}
	
    public bool IsEmpty() {
        if (_schedulesToDo.Count > 0) {
			return true;
		} else {
			return false;
		}
    }
	
	public void RemoveScheduleWithFlag(string flag) {
		foreach (Schedule sched in _schedulesToDo) {
			sched.RemoveScheduleWithFlag(flag);
		}
	}
	
	// Uses magic
	public void RemoveDoneFlagSchedules(Schedule current) {
		foreach (List<string> flagGroupCur in current.flagList) {
			foreach (string flagCur in flagGroupCur) {
				foreach (Schedule sched in _schedulesToDo) {
					foreach (List<string> flagGroupSched in sched.flagList) {
						if (flagGroupSched.Count == 1 && flagGroupSched.Contains(flagCur)) {
							_schedulesToDo.Remove(sched);
						} else {
							flagGroupSched.Remove(flagCur);
						}
					}
				}
			}
		}
	}
}