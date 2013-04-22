using UnityEngine;
using System.Collections;

/// <summary>
/// NPC callback action. The most customizable reaction. It will be given a function with no arguments and will call that
/// 	when it reacts
/// </summary>
public class NPCCallbackAction : Action {
	public delegate void Callback();
	Callback functionToCall;
	
	public NPCCallbackAction(Callback _functionToCall){
		functionToCall = _functionToCall;
	}
	
	public override void Perform(){
		functionToCall();
	}
}