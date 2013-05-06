using UnityEngine;
using System.Collections;

/// <summary>
/// NPC callback action. The most customizable reaction. It will be given a function with no arguments and will call that
/// when it reacts
/// </summary>
public class NPCCallbackOnNPCAction : Action {
	public delegate void Callback(NPC _toControl);
	NPC toControl;
	Callback functionToCall;
	
	public NPCCallbackOnNPCAction(Callback _functionToCall, NPC _toControl){
		functionToCall = _functionToCall;
		toControl = _toControl;
	}
	
	public override void Perform(){
		functionToCall(toControl);
	}
}