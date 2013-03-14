using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmotionState {
	string _textToSay;
	List<Choice> _choices;
	List<string> _acceptableItems;
	
	public List<Choice> ChoiceOptions{
		get {return _choices;}
	}
	
	public EmotionState(string textToSay){
		_textToSay = textToSay;
	}
	
	public EmotionState(string textToSay, List<Choice> choices, List<string> acceptableItems) {
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
	
	public bool ItemHasReaction(string itemName){
		return (_acceptableItems.Contains(itemName));
	}
}
