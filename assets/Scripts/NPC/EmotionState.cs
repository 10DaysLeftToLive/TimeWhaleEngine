using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmotionState {
	protected NPC _npcInState;
	public string _defaultTextToSay;
	protected List<Choice> _choices;
	protected List<string> _acceptableItems;
	protected ChatChoiceInfo _chatInfo;
	
	public List<Choice> ChoiceOptions{
		get {return _choices;}
	}
	
	public EmotionState(NPC npcInState, string textToSay){
		_npcInState = npcInState;
		_defaultTextToSay = textToSay;
		_choices = new List<Choice>();
		_acceptableItems = new List<string>();
	}
	
	public EmotionState(NPC npcInState, string textToSay, List<Choice> choices, List<string> acceptableItems) {
		_npcInState = npcInState;
		_defaultTextToSay = textToSay;
		_choices = choices;
		_acceptableItems = acceptableItems;
	}
	
	public string GetWhatToSay(){
		return (_defaultTextToSay);
	}
	
	public List<Choice> GetChoices(){
		return (_choices);
	}
	
	public void AddChoice(Choice newChoice){
		_choices.Add(newChoice);
	}
	
	public bool ItemHasReaction(string itemName){
		return (_acceptableItems.Contains(itemName));
	}
	
	protected void UpdateInteractionDisplay(string newText){
		GUIManager.Instance.UpdateInteractionDisplay(newText);	
	}
	
	protected void SetDefaultText(string newText){
		_defaultTextToSay = newText;
	}
	
	public List<string> GetButtonTexts(){
		List<string> tempTexts = new List<string>();
		tempTexts.Add("Hai1");
		tempTexts.Add("Hai2");
		tempTexts.Add("Hai3");
		return (tempTexts);
	}
	
	//Virtual so children don't have to override
	public virtual void ReactToItemInteraction(string npc, GameObject item){}
	public virtual void ReactToChoiceInteraction(string npc, string choice){}
	public virtual void ReactToEnviromentInteraction(string npc, string enviromentAction){}
	public virtual void ReactToItemPickedUp(GameObject item){}
	public virtual void UpdateEmotionState(){}
}
