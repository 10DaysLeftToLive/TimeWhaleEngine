
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
/// <summary>
/// Emotion state. Holds the list of choices and items that the NPC will be able to react to. 
///   NPCs should move between emotion states with little updating of a single emotion state.
/// </summary>
public class EmotionState {
	private string className; // For debuging only so you know the current emotion state's name
	
	protected NPC _npcInState;
	public string _defaultTextToSay;
	protected ChatChoiceInfo _chatInfo;
	protected Dictionary<Choice, DispositionDependentReaction> _allChoiceReactions;
	protected Dictionary<string, DispositionDependentReaction> _allItemReactions;
	protected DispositionDependentReaction defaultItemReaction;
	protected DispositionDependentReaction interactionOpeningReaction;
	protected DispositionDependentReaction interactionClosingReaction;
	
	public EmotionState(NPC npcInState){
		className = this.GetType().FullName;
		_npcInState = npcInState;
		_defaultTextToSay = null;
		_allChoiceReactions = new Dictionary<Choice, DispositionDependentReaction>();
		_allItemReactions = new Dictionary<string, DispositionDependentReaction>();
		Reaction defaultItemReact = new Reaction();
		defaultItemReact.AddAction(new UpdateCurrentTextAction(npcInState, "No thank you."));
		defaultItemReaction = new DispositionDependentReaction(defaultItemReact);
		interactionOpeningReaction = null;
	}
	
	public EmotionState(NPC npcInState, string textToSay) : this (npcInState){
		_defaultTextToSay = textToSay;
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
	
	protected void SetOnOpenInteractionReaction(DispositionDependentReaction reaction){
		interactionOpeningReaction = reaction;
	}
	
	protected void SetOnCloseInteractionReaction(DispositionDependentReaction reaction){
		interactionClosingReaction = reaction;
	}
	
	public void OnInteractionOpens(){
		if (interactionOpeningReaction != null){
			PerformReactionBasedOnDisposition(interactionOpeningReaction);
		}
	}
	
	public void OnInteractionCloses(){
		if (interactionClosingReaction != null){
			PerformReactionBasedOnDisposition(interactionClosingReaction);
		}
	}
	
	/// <summary>
	/// Reacts to choice by calling the corrisponding reaction
	/// </summary>
	/// <param name='choiceName'>
	/// Choice name to react to
	/// </param>
	public void ReactToChoice(string choiceName){
		Choice choiceToSetOff = null;
		foreach (Choice choice in _allChoiceReactions.Keys){
			if (choice._choiceName == choiceName){
				choiceToSetOff = choice;
			break;
			}
		}
		
		PerformReactionBasedOnDisposition(_allChoiceReactions[choiceToSetOff]);
		GUIManager.Instance.UpdateInteractionDisplay(choiceToSetOff._reactionDialog);
	}
	
	/// <summary>
	/// Reacts to the given item by calling the corrisponding reaction or the default one if none have been set for it
	/// </summary>
	/// <param name='item'>
	/// Item.
	/// </param>
	public void ReactToGiveItem(GameObject item){
		if (_allItemReactions.ContainsKey(item.name)){
			PerformReactionBasedOnDisposition(_allItemReactions[item.name]);
		} else {
			Debug.LogWarning("No give reaction was set for " + item.name);
			PerformReactionBasedOnDisposition(defaultItemReaction);
		}
	}
	
	public void AddChoice(Choice newChoice, DispositionDependentReaction reaction){
		_allChoiceReactions.Add(newChoice, reaction);
	}
	
	public void AddItemReaction(string item, Reaction itemReaction){
		_allItemReactions.Add(item, new DispositionDependentReaction(itemReaction));
	}
	
	public void RemoveChoice(Choice choiceToRemove){
		if (_allChoiceReactions.ContainsKey(choiceToRemove)){
			_allChoiceReactions.Remove(choiceToRemove);
		} else {
			Debug.LogWarning(choiceToRemove._choiceName + " was not in " + this.ToString() + " of " + _npcInState.name);
		}
	}
	
	public bool CanTakeItem(string itemName){
		return (_allItemReactions.ContainsKey(itemName));
	}
	
	/// <summary>
	/// Performs the reaction based on disposition. If the reaction does not have that type of reaction then it will 
	/// 	perform the default reaction
	/// </summary>
	/// <param name='reaction'>
	/// Reaction to do
	/// </param>
	private void PerformReactionBasedOnDisposition(DispositionDependentReaction reaction){
		reaction.PerformReaction();
	}
	public virtual void PassStringToEmotionState(string text){}
}
