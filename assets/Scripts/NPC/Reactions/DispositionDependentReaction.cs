using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Disposition dependent reaction contains the possibility for 3 types of reaction to be called depending on the disposition
/// of the NPC. 
/// Has High, Default and Low reactions that can be set via SetHighReaction(Reaction) and SetLowReaction(Reaction)
/// It must be given a default reaction but the others are optional. 
/// If the NPC is in a disposition level that does not have a set reaction then the default reaction will be called.
/// Performing the reaction will be handled by the system.
/// </summary>
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