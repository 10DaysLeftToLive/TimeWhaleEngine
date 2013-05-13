using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

// Used for scheduling
// The end of the list is the highest priority
// high 1 - 10 low
public class ScheduleStack {
	Schedule current;
	PriorityStack _schedulesToDo;
	private bool isPaused = false;

    public ScheduleStack() {
      this._schedulesToDo = new PriorityStack();
    }
	
	protected bool HasSchedule() {
		return (current != null);
	}
	
	public bool CanPassiveChat() {
		if (current == null){ // if we are transistioning we cannot chat
			return (false);	
		}
		return (current.CanPassiveChat);
	}
	
	public bool CanInteractWithPlayer(){
		if (current == null){
			return (false);
		}
		return(current.CanInteractWithPlayer);
	}
	
	// Does not run and change schedules the same tick.
	public void Run(float timeSinceLastTick) {		
		if (!isPaused){
			if (HasSchedule()) {
				current.Run(timeSinceLastTick);
				if (current.IsComplete()) {
					//Debug.Log("Current schedule complete");
					//Debug.Log("The next task is " + _schedulesToDo.peek());
					_schedulesToDo.RemoveDoneFlagSchedules(current);
					current = null;
				}
			} else {
				NextSchedule();
			}
		}
	}
	
	public void NextTask() {
		//Debug.Log("Schedule Stack next task");
		if(HasSchedule()){
			current.NextTask();	
		} else {
			NextSchedule();
		}
	}
		
	public void NextSchedule() {
		//Debug.Log("Moving on to next schedule");
		current = _schedulesToDo.Pop();
		if (current == null){
			Debug.LogWarning("No schedule to go to");
			return;
		}
		//Debug.Log((current == null) ? "Current was null" : "Current was not null");
		current.Resume();
	}
	
	public void Add(Schedule schedule) {		
		if (current == null) {
			current = schedule;
		} 
		else if (schedule.CheckPriorityTo(current) >= 0) {
			current.OnInterrupt();
			_schedulesToDo.Push(current);
			current = schedule;
			current.Resume();
		} else {
			_schedulesToDo.Push(schedule);
		}
	}
	
	public void Pause() {
		isPaused = true;
		if (current != null) {
			current.OnInterrupt();
		}
	}
	
	public void Resume() {
		isPaused = false;
		if (current != null) {
			current.Resume();
		}
	}
	
	public void RemoveScheduleWithFlag(string flag) {
		_schedulesToDo.RemoveScheduleWithFlag(flag);
	}
}