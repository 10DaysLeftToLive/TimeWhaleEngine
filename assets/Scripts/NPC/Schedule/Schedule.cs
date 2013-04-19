using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/*
 * Schedule.cs
 *  Used for creating a series of tasks for an NPC to do
 *  For moving you need to use MoveThenDo(MarkTaskDone)
 * 		So we know when the npc completed that task
 *  See Task and TimeTask for the basics of what you can schedule
 */
public class Schedule {
	Queue<Task> _tasksToDo;
	NPC _toManage;
	Task current;
	public int schedulePriority; // high 1 - 10 low
	
	public enum priorityEnum {
		Default=1, 
		Low, 
		Medium, 
		High, 
		DoNow
	};
	
	public Schedule(NPC toManage){
		_tasksToDo = new Queue<Task>();
		_toManage = toManage;
		schedulePriority = (int)priorityEnum.Low;
	}
	
	public Schedule(NPC toManage, int priority){
		_tasksToDo = new Queue<Task>();
		_toManage = toManage;
		schedulePriority = priority;
	}
	
	public Schedule(Queue<Task> tasksToDo, NPC toManage, int priority){
		_tasksToDo = tasksToDo;
		_toManage = toManage;
		schedulePriority = priority;
	}
	
	~Schedule() {
		_tasksToDo.Clear ();
	}
	
	public bool HasTask(){
		return (current != null);
	}
	
	public void Run(float timeSinceLastTick){
		if (HasTask()){
			current.Decrement(timeSinceLastTick);
			if (current.IsComplete()){
				current = null;
			}
		} else {
			NextTask();
		}
	}
	
	// Schedule sets what it manages to the correct state the schedule is in
	public void Resume() {
		if (!HasTask()) {
			NextTask();
		}
		
		_toManage.ForceChangeToState(current.StatePerforming);	
	}
	
	public void NextTask(){
		if (_tasksToDo.Count > 0) {
			current = _tasksToDo.Dequeue();
			Debug.Log(_toManage.name + " is now switching to " + current.StatePerforming);
			_toManage.ForceChangeToState(current.StatePerforming);
		}
	}
	
	// Schedule is complete if the current task is complete and there are no other tasks to complete
	public bool IsComplete(){
		if (current == null && _tasksToDo.Count == 0) {
			return true;
		} else {
			return false;
		}
	}
	
	public void Add(Task task){
		_tasksToDo.Enqueue(task);
	}
	
	// Checks if this schedule has a higher priority than the schedule passed in
	public bool HigherPriority(Schedule schedule) {
		if (this.schedulePriority < schedule.schedulePriority) {
			return true;
		} else {
			return false;
		}
	}
}
