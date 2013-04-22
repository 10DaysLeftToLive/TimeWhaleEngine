using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCOneOffChat : NPCChat {
	public NPCOneOffChat(ChatInfo chat) : base(){
		_chatTexts.Add(chat);	
	}	
}
