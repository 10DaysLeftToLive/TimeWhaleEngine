using UnityEngine;
using System.Collections;

/// <summary>
/// Will make the npc look at the given gameobject
/// </summary>
public class NPCLookAtAction : Action {
	private NPC _npcToLook;
	private GameObject _objectToLookAt;
	
	public NPCLookAtAction(){}
	
	public NPCLookAtAction(NPC npcToLook, GameObject objectToLookAt){
		_npcToLook = npcToLook;
		_objectToLookAt = objectToLookAt;
	}

	public override void Perform(){
		_npcToLook.LookAt(_objectToLookAt);
	}
}
