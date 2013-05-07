using UnityEngine;
using System.Collections;

/// <summary>
/// Abstract Wait state will be implemented by children. Will constantly check if the set conditions are satisfied
/// </summary>
public abstract class WaitState : AbstractState {
	public WaitState(Character toControl) : base(toControl){
	}
	
	public override void Update(){
		if (ConditionsSatisfied()){
			Finish();	
		}
	}
	
	public override void OnEnter(){
		Debug.Log(character.name + ": WaitState Enter");
		if (character is Player){
			Debug.LogWarning("Putting player in a wait state.");
			character.EnterState(new IdleState(character));
		}
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": WaitState Exit");
	}
	
	/// <summary>
	/// Check if the conditions for interupting the wait state. 
	/// </summary>
	/// <returns>
	/// True if all are done.
	/// </returns>
	protected abstract bool ConditionsSatisfied();
	
	private void Finish(){
		((NPC)character).NextTask();
	}
}