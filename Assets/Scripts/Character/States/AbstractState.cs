using UnityEngine;
using System.Collections;

/*
 * AbstractState.cs
 * 	The abstract implementation of a state
 * 	The children of this object will need to override the methods
 */
[System.Serializable]
public abstract class AbstractState : State {
	protected Character character; // The character that is in this state
	private string stateName; // For debuging
	
	public AbstractState(Character toControl){
		character = toControl;
		stateName = this.GetType().ToString();
	}
	
	public abstract void Update();
	public abstract void OnEnter();
	public abstract void OnExit();
	
}
