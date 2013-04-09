using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatChoiceInfo : ChatInfo {
	public List<Choice> choices;
	public Choice itemGiveChoice;
	
	public ChatChoiceInfo(NPC _npcTalking, string _text, List<Choice> _choices, Choice _itemGiveChoice) : base(_npcTalking, _text){
		choices = _choices;
		itemGiveChoice = _itemGiveChoice;
	}
}
