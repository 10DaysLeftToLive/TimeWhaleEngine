using UnityEngine;
using System.Collections;

public abstract class InteractionUpdateAction : Action {
	protected string newText;
	
	public InteractionUpdateAction(){}
	
	public InteractionUpdateAction(string _newText){
		newText = _newText;	
	}
}