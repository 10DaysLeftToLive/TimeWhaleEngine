using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmotionState {
	protected NPC _npcInState;
	public string _defaultTextToSay;
	protected ChatChoiceInfo _chatInfo;
	protected Dictionary<Choice, DispositionDependentReaction> _allChoiceReactions;
	protected Dictionary<string, DispositionDependentReaction> _allItemReactions;
	
	public EmotionState(NPC npcInState, string textToSay){
		_npcInState = npcInState;
		_defaultTextToSay = textToSay;
		_allChoiceReactions = new Dictionary<Choice, DispositionDependentReaction>();
		_allItemReactions = new Dictionary<string, DispositionDependentReaction>();
	}
	
	public string GetWhatToSay(){
		return (_defaultTextToSay);
	}

	public bool ItemHasReaction(string itemName){
		return (_allItemReactions.ContainsKey(itemName));
	}
	
	protected void UpdateInteractionDisplay(string newText){
		GUIManager.Instance.UpdateInteractionDisplay(newText);	
	}
	
	public void SetDefaultText(string newText){
		_defaultTextToSay = newText;
	}
	
	public List<string> GetButtonTexts(){
		List<string> toReturn = new List<string>();
		
		foreach (Choice choice in _allChoiceReactions.Keys){
			toReturn.Add(choice._choiceName);
		}
		
		return (toReturn);
	}
	
	/// <summary>
	/// Reacts to choice by calling the corrisponding reaction
	/// </summary>
	/// <param name='choiceName'>
	/// Choice name to react to
	/// </param>
	public void ReactToChoice(string choiceName){
		foreach (Choice choice in _allChoiceReactions.Keys){
			if (choice._choiceName == choiceName){
				PerformReactionBasedOnDisposition(_allChoiceReactions[choice]);
				GUIManager.Instance.UpdateInteractionDisplay(choice._reactionDialog);
			}
		}
	}
	
	/// <summary>
	/// Performs the reaction based on disposition. If the reaction does not have that type of reaction then it will 
	/// 	perform the default reaction
	/// </summary>
	/// <param name='reaction'>
	/// Reaction to do
	/// </param>
	private void PerformReactionBasedOnDisposition(DispositionDependentReaction reaction){
		if (_npcInState.GetDisposition() >= _npcInState.GetHighDisposition() && reaction.HasHighReaction()){
			reaction.PerformHighReaction();
		} else if (_npcInState.GetDisposition() <= _npcInState.GetLowDisposition() && reaction.HasLowReaction()){
			reaction.PerformLowReaction();
		} else {
			reaction.PerformReaction();
		}
	}
	public virtual void ReactToItemInteraction(string npc, GameObject item){}
	public virtual void ReactToChoiceInteraction(string npc, string choice){}
	public virtual void ReactToEnviromentInteraction(string npc, string enviromentAction){}
	public virtual void ReactToItemPickedUp(GameObject item){}
	public virtual void UpdateEmotionState(){}
}
