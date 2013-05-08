using UnityEngine;
using System.Collections;

/// <summary>
/// NPC callback action. The most customizable reaction. It will be given a function with no arguments and will call that
/// when it reacts
/// </summary>
public class NPCCallbackSetStringAction : Action {
	public delegate string Callback();
	NPC toControl;
	Callback functionToCall;
	
	public NPCCallbackSetStringAction(Callback _functionToCall, NPC _toControl){
		functionToCall = _functionToCall;
		toControl = _toControl;
	}
	
	public override void Perform(){
		ChatInfo chatInfo = new ChatInfo(toControl, functionToCall());
		GUIManager.Instance.AddNPCChat(new NPCOneOffChat(chatInfo));
	}
}