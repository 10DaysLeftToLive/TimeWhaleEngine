using UnityEngine;
using System.Collections;

public class NPCItemInteraction : NPCInteraction {
	public string _itemName;
	
	public NPCItemInteraction(GameObject npcReacting, string itemName) {
		_npcReacting = npcReacting;
		_itemName = itemName;
	}
}