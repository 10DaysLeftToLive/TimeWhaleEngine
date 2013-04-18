using UnityEngine;
using System.Collections;

public class Choice {
	public string _choiceName;
	public string _reactionDialog;
	
	public Choice(string choiceName, string reactionDialog){
		_choiceName = choiceName;
		_reactionDialog = reactionDialog;
	}
	
	public void Perform(EmotionState toUpdate){
		Debug.Log("Performing on " + toUpdate.ToString());	
		GUIManager.Instance.UpdateInteractionDisplay(_reactionDialog);
	}
}