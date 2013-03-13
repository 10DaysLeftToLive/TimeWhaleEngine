using UnityEngine;
using System.Collections;

public class Choice {
	public string _choiceName;
	public string _reactionDialog;
	
	public Choice(string choiceName, string reactionDialog){
		_choiceName = choiceName;
		_reactionDialog = reactionDialog;
	}
}