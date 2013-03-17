using UnityEngine;
using System.Collections;

public class NPCItemInteraction : NPCInteraction {
	public GameObject _item;
	
	public NPCItemInteraction(GameObject npcReacting, GameObject item) {
		_npcReacting = npcReacting;
		_item = item;
	}
}