using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmotionState {
	protected NPC _npcInState;
	string _textToSay;
	protected List<Choice> _choices;
	protected List<string> _acceptableItems;
	
	public List<Choice> ChoiceOptions{
		get {return _choices;}
	}
	
	public EmotionState(NPC npcInState, string textToSay){
		_npcInState = npcInState;
		_textToSay = textToSay;
		_choices = new List<Choice>();
		_acceptableItems = new List<string>();
	}
	
	public EmotionState(NPC npcInState, string textToSay, List<Choice> choices, List<string> acceptableItems) {
		_npcInState = npcInState;
		_textToSay = textToSay;
		_choices = choices;
		_acceptableItems = acceptableItems;
	}
	
	public string GetWhatToSay(){
		return (_textToSay);
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
	
	//Virtual so children don't have to override
	public virtual void ReactToItemInteraction(string npc, string item){}
	public virtual void ReactToChoiceInteraction(string npc, string choice){}
	public virtual void ReactToEnviromentInteraction(string npc, string enviromentAction){}
}
