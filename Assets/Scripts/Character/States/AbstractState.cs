using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * AbstractState.cs
 * 	The abstract implementation of a state
 * 	The children of this object will need to override the methods
 */
public abstract class AbstractState : State {
	protected Character character; // The character that is in this state
	private List<string> flagsToSet = null; // will be null unless there is a flag to set
	
	public AbstractState(Character toControl){
		character = toControl;
	}
	
	public void AddFlag(string flagToSetOff){
		if (flagsToSet == null){
			flagsToSet = new List<string>();
		}
		
		flagsToSet.Add(flagToSetOff);
	}
	
	
	public abstract void Update();
	public abstract void OnEnter();
	public abstract void OnExit();
	
	public void ExitState(){
		if (flagsToSet == null){
			return;	
		}
		
		foreach (string flag in flagsToSet){
			FlagManager.instance.SetFlag(flag);	
		}
		
		OnExit();
	}
}
