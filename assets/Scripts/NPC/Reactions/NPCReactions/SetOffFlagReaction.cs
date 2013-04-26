using UnityEngine;
using System.Collections;

/// <summary>
/// Starts with an action to set off the given flag
/// </summary>
public class SetFlagReaction : Reaction {
	public SetFlagReaction(string flagToSet) : base(){
		AddAction(new SetOffFlagAction(flagToSet));
	}
}