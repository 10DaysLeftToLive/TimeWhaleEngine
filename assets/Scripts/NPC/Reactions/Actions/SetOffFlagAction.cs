using UnityEngine;
using System.Collections;

/// <summary>
/// Sets off the given flag.
/// </summary>
public class SetOffFlagAction : Action {
	private string flagToSetOff;
	
	public SetOffFlagAction(string _flagToSetOff){
		flagToSetOff = _flagToSetOff;
	}
	
	public override void Perform(){
		FlagManager.instance.SetFlag(flagToSetOff);
	}
}
