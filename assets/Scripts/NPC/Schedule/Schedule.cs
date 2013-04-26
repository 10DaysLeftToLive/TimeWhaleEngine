using UnityEngine;
using System;
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
	public List<string> flagList;
	protected bool endOnFirstFlagCompleted = false; // When flags are checked to remove schedules, this says if it is removed if one flag in its list is done (true) or all (false)
	public bool EndOnFirstFlagCompleted {
		get { return endOnFirstFlagCompleted; }
	}
	protected Queue<Task> _tasksToDo;
	protected NPC _toManage;
	protected Task current;
	protected bool canPassiveChat = false; // Should only be true for priorities that are low or less
	public bool CanPassiveChat {
		get { return canPassiveChat; }
	}
	public int schedulePriority; // high 1 - 10 low
	
	public enum priorityEnum {
		DoNow=1,
		High,
		Medium,
		Low,
		Default
	};
	
	public Schedule(NPC toManage){
		_tasksToDo = new Queue<Task>();
		_toManage = toManage;
		schedulePriority = (int)priorityEnum.Low;
		flagList = new List<string>();
	}
	
	public Schedule(NPC toManage, Enum priority){
		_tasksToDo = new Queue<Task>();
		_toManage = toManage;
		schedulePriority = Convert.ToInt32(priority);
		flagList = new List<string>();
	}
	
	public Schedule(Queue<Task> tasksToDo, NPC toManage, Enum priority){
		_tasksToDo = tasksToDo;
		_toManage = toManage;
		schedulePriority = Convert.ToInt32(priority);
		flagList = new List<string>();
	}
	
	~Schedule() {
		_tasksToDo.Clear ();
	}
	
	public bool HasTask(){
		return (current != null);
	}
	
	public void AddFlag(string flag) {
		flagList.Add(flag);
	}
	
	// REMOVE - for testing
	public void SetCanChat(bool _canChat){
		canPassiveChat = _canChat;
	}
	
	public void Run(float timeSinceLastTick){
		if (HasTask()){
			current.Decrement(timeSinceLastTick);
			if (current.IsComplete()){
				Debug.Log("Completed task");
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
	
	public virtual void NextTask(){
		Debug.Log(_toManage + " schedule next task");
		
		if (_tasksToDo.Count > 0) {
			current = _tasksToDo.Dequeue();
			Debug.Log("There are " + _tasksToDo.Count + " tasks");
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
	
	/// <summary>
	/// 1 = Higher priority,
	/// 0 = Same priority,
	/// -1 = Lower priority
	/// </summary>
	/// <returns>
	/// 1 = Higher priority
	/// 0 = Same priority
	/// -1 = Lower priority
	/// </returns>
	/// <param name='schedule'>
	/// Schedule.
	/// </param>
	public int CheckPriorityTo(Schedule schedule) {
		if (this.schedulePriority < schedule.schedulePriority) {
			return 1;
		} 
		else if (this.schedulePriority == schedule.schedulePriority) {
			return 0;
		} else {
			return -1;
		}
	}
}
