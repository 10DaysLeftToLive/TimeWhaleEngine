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
	
	public static bool operator ==(Choice choice1, Choice choice2){
	    // If both are null, or both are same instance, return true.
	    if (System.Object.ReferenceEquals(choice1, choice2)){
	        return true;
	    }
	
	    // If one is null, but not both, return false.
	    if (((object)choice1 == null) || ((object)choice2 == null))
	    {
	        return false;
	    }
	
	    // Return true if the fields match:
	    return choice1._choiceName == choice2._choiceName;
	}
	
	public static bool operator !=(Choice choice1, Choice choice2){
		if (choice1 == null || choice2 == null)
			return ! System.Object.Equals(choice1, choice2);
		
		return !(choice1.Equals(choice2));
	}
	
	public bool Equals(Choice other) {
		if (other == null) 
			return false;
		
		if (this._choiceName == other._choiceName)
			return true;
		else 
			return false;
	}

	public override bool Equals(System.Object obj){
		if (obj == null) 
			return false;
		
		Choice choiceObj = obj as Choice; // check if it is of type Choice
		if (choiceObj == null)
			return false;
		else    
			return Equals(choiceObj);   
	}   
	
	public override int GetHashCode(){
		return this._choiceName.GetHashCode();
	}
}