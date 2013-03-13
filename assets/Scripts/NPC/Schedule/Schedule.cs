using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
			current = _tasksToDo.Dequeue();
			Debug.Log(_toManage.name + " is now switching to " + current.StatePerforming);
			_toManage.ForceChangeToState(current.StatePerforming);
		}
	}
	
	public void Add(Task task){
		_tasksToDo.Enqueue(task);
	}
}
