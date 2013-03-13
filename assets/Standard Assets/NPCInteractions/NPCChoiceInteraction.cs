using UnityEngine;
using System.Collections;

public class NPCChoiceInteraction: NPCInteraction {
	public string _choice;
	
	public NPCChoiceInteraction(GameObject npcReacting, string choice) {
		_npcReacting = npcReacting;
		_choice = choice;
	}
}