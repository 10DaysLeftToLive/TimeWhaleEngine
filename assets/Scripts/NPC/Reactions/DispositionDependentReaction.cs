using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DispositionDependentReaction {
	private Reaction defaultReaction;
	private Reaction highReaction = null;
	private Reaction lowReaction = null;
	
	public DispositionDependentReaction(Reaction _defaultReaction){
		defaultReaction = _defaultReaction;
	}
	
	public void SetHighReaction(Reaction _highReaction){
		highReaction = _highReaction;
	}
	
	public void SetLowReaction(Reaction _lowReaction){
		lowReaction = _lowReaction;	
	}
	
	public bool HasHighReaction(){
		return (highReaction != null);	
	}
	
	public bool HasLowReaction(){
		return (lowReaction != null);	
	}
	
	public void PerformReaction(){
		defaultReaction.React();
	}
	
	public void PerformLowReaction(){
		if (!HasLowReaction()) {
			Debug.LogError("Low reaction was not set");	
			return;
		}
		lowReaction.React();	
	}
	
	public void PerformHighReaction(){
		if (!HasHighReaction()) {
			Debug.LogError("High reaction was not set");	
			return;
		}
		highReaction.React();	
	}
}