using UnityEngine;
using System.Collections;

/*
 * TimeTask.cs
 *  A basic action that is to be performed over a specified period of time
 */
public class TimeTask : Task {
	float _timeTillMoveOn;

	public TimeTask(float timeTillMoveOn, State stateToPerform) : base(stateToPerform){
		_timeTillMoveOn = timeTillMoveOn;
	}
	
	public override void Decrement(float amount){
		_timeTillMoveOn -= amount;
		Debug.Log("Time left = " + _timeTillMoveOn);
	}
	
	public override bool IsComplete(){
		return (_timeTillMoveOn <= 0);
	}
}
