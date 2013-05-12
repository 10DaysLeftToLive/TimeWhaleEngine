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
 *  defaults to not being able to passive chat or be interupted by the player
 */
public class Schedule {
	public List<List<string>> flagList;
	protected Queue<Task> _tasksToDo;
	protected NPC _toManage;
	protected Task current;
	protected bool canPassiveChat = false; // Should only be true for priorities that are low or less
	protected bool canInteractWithPlayer = true;
	public bool CanPassiveChat {
		get { return canPassiveChat; }
	}
	public bool CanInteractWithPlayer {
		get { return canInteractWithPlayer; }	
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
		flagList = new List<List<string>>();
		Init ();
	}
	
	public Schedule(NPC toManage, bool _canPassiveChat){
		_tasksToDo = new Queue<Task>();
		_toManage = toManage;
		canPassiveChat = _canPassiveChat;
		schedulePriority = (int)priorityEnum.Low;
		flagList = new List<List<string>>();
		Init ();
	}
	
	public Schedule(NPC toManage, Enum priority){
		_tasksToDo = new Queue<Task>();
		_toManage = toManage;
		schedulePriority = Convert.ToInt32(priority);
		flagList = new List<List<string>>();
		Init ();
	}
	
	public Schedule(NPC toManage, Enum priority, bool _canPassiveChat){
		_tasksToDo = new Queue<Task>();
		_toManage = toManage;
		canPassiveChat = _canPassiveChat;
		schedulePriority = Convert.ToInt32(priority);
		flagList = new List<List<string>>();
		Init ();
	}
	
	public Schedule(Queue<Task> tasksToDo, NPC toManage, Enum priority){
		_tasksToDo = tasksToDo;
		_toManage = toManage;
		schedulePriority = Convert.ToInt32(priority);
		flagList = new List<List<string>>();
		Init ();
	}
	
	public Schedule(Queue<Task> tasksToDo, NPC toManage, Enum priority, bool _canPassiveChat){
		_tasksToDo = tasksToDo;
		_toManage = toManage;
		canPassiveChat = _canPassiveChat;
		schedulePriority = Convert.ToInt32(priority);
		flagList = new List<List<string>>();
		Init ();
	}
	
	~Schedule() {
		_tasksToDo.Clear ();
	}
	
	protected virtual void Init() {}
	
	public bool HasTask(){
		return (current != null);
	}
	
	
	#region Flag Handling
	public void AddFlagGroup(string flag) {
		List<string> newFlagList = new List<string>();
		newFlagList.Add(flag);
	}
	
	public void AddFlagGroup(List<string> flags) {
		flagList.Add(flags);
	}
	
	public void RemoveScheduleWithFlag(string flag) {
		foreach (List<string> fl in flagList) {
			foreach (string f in fl) {
				if (f == flag) {
					SetComplete();
				}
			}
		}
	}
	#endregion
	
	// REMOVE - for testing
	public void SetCanChat(bool _canChat){
		canPassiveChat = _canChat;
	}
	
	public virtual void Run(float timeSinceLastTick){
		if (HasTask()){
			current.Decrement(timeSinceLastTick);
			if (current.IsComplete()){
				Debug.Log("Completed task");
				current.Finish();
				current = null;
			}
		} else {
			NextTask();
		}
	}
	
	// Schedule sets what it manages to the correct state the schedule is in
	public virtual void Resume() {
		if (!HasTask()) {
			NextTask();
		}
		
		_toManage.ForceChangeToState(current.StatePerforming);	
	}
	
	public virtual void NextTask(){
		Debug.Log(_toManage + " schedule next task");
		if (current != null){ // if we are going to skip the current task but it has not finished
			current.Finish();
		}
		
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
	
	public virtual void OnInterrupt() {	
	}
	
	protected void SetComplete() {
		_tasksToDo.Clear();
		current = null;
	}
}
