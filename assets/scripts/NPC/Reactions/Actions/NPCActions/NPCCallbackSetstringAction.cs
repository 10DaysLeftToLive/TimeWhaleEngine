using UnityEngine;
using System.Collections;

/// <summary>
/// NPC callback action. The most customizable reaction. It will be given a function with no arguments and will call that
/// when it reacts
/// </summary>
public class NPCCallbackSetStringAction : Action {
	public delegate void Callback(NPC _toControl, string _updateText);
	NPC toControl;
	string updateText;
	Callback functionToCall;
	
	public NPCCallbackSetStringAction(Callback _functionToCall, NPC _toControl, string _updateText){
		functionToCall = _functionToCall;
		toControl = _toControl;
		updateText = _updateText;
	}
	
	public override void Perform(){
		functionToCall(toControl, updateText);
		//ChatInfo chatInfo = new ChatInfo(toControl, functionToCall());
		//GUIManager.Instance.AddNPCChat(new NPCOneOffChat(chatInfo));
	}
}