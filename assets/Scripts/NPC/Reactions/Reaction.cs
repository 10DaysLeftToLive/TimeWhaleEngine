using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Reaction is the basis for all NPC reactions. 
/// Contains a list of actions to perform when this goes off. List starts off empty and can be added to
/// by calling AddAction(Action)
/// There are derivations of this class that start with a specific action.
/// The system will handle the calls to react.
/// </summary>
public class Reaction {
	private List<Action> actionsToPerform;
	
	public Reaction(){
		actionsToPerform = new List<Action>();	
	}
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Reaction"/> class.
	/// </summary>
	/// <param name='actionToAdd'>
	/// Action to add.
	/// </param>
	public Reaction(Action actionToAdd){
		actionsToPerform = new List<Action>();
		actionsToPerform.Add (actionToAdd);
	}
	
	/// <summary>
	/// Adds the action to the list of action to perform when this goes off
	/// </summary>
	/// <param name='actionToAdd'>
	/// Action to add.
	/// </param>
	public void AddAction(Action actionToAdd){
		actionsToPerform.Add(actionToAdd);	
	}
	
	public void Clear(){
		actionsToPerform.Clear();
	}
	
	/// <summary>
	/// Performs all of the actions stored in this reaction
	/// </summary>
	public void React(){
		foreach (Action action in actionsToPerform){
			action.Perform();	
		}
	}
}