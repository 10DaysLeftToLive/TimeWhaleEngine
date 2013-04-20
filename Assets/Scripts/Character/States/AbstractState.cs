using UnityEngine;
using System.Collections;

/*
 * AbstractState.cs
 * 	The abstract implementation of a state
 * 	The children of this object will need to override the methods
 */
public abstract class AbstractState : State {
	protected Character character; // The character that is in this state
	protected bool _isComplete = false;
	public bool IsComplete {
		get { return _isComplete;}	
	}
	
	public AbstractState(Character toControl){
		character = toControl;
	}
	
	public abstract void Update();
	public abstract void OnEnter();
	public abstract void Pause();
	public abstract void Resume();
	public abstract void OnExit();
	
}
