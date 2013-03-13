using UnityEngine;
using System.Collections;

/*
 * Task.cs
 *  A basic action of what an npc should do until interupted 
 *  if you want it to go one for a specified amount of time use TimeTask
 */
public class Task {
	State _stateToPerform;
	
	public State StatePerforming {
		get {return _stateToPerform;}
	}
	
	public Task(State stateToPerform){
		_stateToPerform = stateToPerform;
	}
	
	public virtual void Decrement(float amount){
		// timeleft = infinity - amount aka do nothing
	}
	
	public virtual bool IsComplete(){
		return (false); // this task will go on forever
	}
}
