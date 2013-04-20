using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Reaction {
	private List<Action> actionsToPerform;
	
	public Reaction(){
		actionsToPerform = new List<Action>();	
	}
	
	public void AddAction(Action actionToAdd){
		actionsToPerform.Add(actionToAdd);	
	}
	
	public void React(){
		foreach (Action action in actionsToPerform){
			action.Perform();	
		}
	}
}