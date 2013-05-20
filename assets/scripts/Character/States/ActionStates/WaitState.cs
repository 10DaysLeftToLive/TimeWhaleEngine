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
		DebugManager.instance.Log(character.name + ": WaitState Enter", character.name, "State");
		
		if (character is Player){
			character.EnterState(new IdleState(character));
		}
	}
	
	public override void OnExit(){
		DebugManager.instance.Log(character.name + ": WaitState Exit", character.name, "State");
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