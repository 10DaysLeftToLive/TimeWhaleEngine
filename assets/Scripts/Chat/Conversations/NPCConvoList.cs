using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCConvoList {
	protected List<NPCConvoChance> convoList;
	protected NPCConvoChance _convo;
	
	public NPCConvoList () {
		convoList = new List<NPCConvoChance>();
		BuildList();
	}
	
	protected virtual void BuildList() {}
	
	protected void AddConvo(NPCConvoChance convo) {
		convoList.Add(convo);
	}
	
	protected void Add(int npc, string textToSay) {
		_convo.Add (npc, textToSay);
	}
	
	public List<NPCConvoChance> GetList() {
		return convoList;
	}
}
