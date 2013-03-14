using UnityEngine;
using System.Collections;

public class NPCEnviromentInteraction : NPCInteraction {
	public string _enviromentAction;
	
	public NPCEnviromentInteraction(GameObject npcReacting, string enviromentAction) {
		_npcReacting = npcReacting;
		_enviromentAction = enviromentAction;
	}
}