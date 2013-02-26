using UnityEngine;
using System.Collections;

public abstract class AbstractState : State {
	protected Character character; // The character that is in this state
	
	public AbstractState(Character toControl){
		character = toControl;
	}
	
	public abstract void Update();
	public abstract void OnEnter();
	public abstract void OnExit();
}
