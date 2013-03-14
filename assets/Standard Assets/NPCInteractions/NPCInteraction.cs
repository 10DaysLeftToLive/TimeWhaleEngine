using UnityEngine;
using System.Collections;
using System; 

public abstract class NPCInteraction {
	public GameObject _npcReacting;
	
	public NPCInteraction(){}
	
	public NPCInteraction(GameObject npcReacting) {
		_npcReacting = npcReacting;
	}
}