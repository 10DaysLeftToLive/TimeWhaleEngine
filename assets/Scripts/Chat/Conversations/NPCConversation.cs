using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class NPCConversation {
	public List<Dialogue> dialogueList;
	public NPCConversation() {
		dialogueList = new List<Dialogue>();
		DialogueScript();
	}
	
	public void Add(int npc, string textToSay) {
		dialogueList.Add (new Dialogue(npc, textToSay));
	}
	
	public void Add(int npc, string textToSay, string animation) {
		dialogueList.Add(new Dialogue(npc, textToSay, animation));
	}
	
	protected virtual void DialogueScript(){}
}
