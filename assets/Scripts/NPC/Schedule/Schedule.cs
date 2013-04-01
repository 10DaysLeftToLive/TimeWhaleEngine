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
	
	public Schedule(NPC toManage){
		_tasksToDo = new Queue<Task>();
		_toManage = toManage;
	}
	
	public Schedule(Queue<Task> tasksToDo, NPC toManage){
		_tasksToDo = tasksToDo;
		_toManage = toManage;
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
	
	public void NextTask(){
		if (_tasksToDo.Count > 0) {
			current = _tasksToDo.Dequeue();
			Debug.Log(_toManage.name + " is now switching to " + current.StatePerforming);
			_toManage.ForceChangeToState(current.StatePerforming);
		}
	}
	
	public void Add(Task task){
		_tasksToDo.Enqueue(task);
	}
}
