using UnityEngine;
using System.Collections;


/// <summary>
/// NPC look in direction action will look in the given direction (left = -1 or right = 1)
/// </summary>
public class NPCLookInDirectionAction : Action {
	private NPC _npcToLook;
	private float _direction;
	
	public NPCLookInDirectionAction(){}
	
	/// <summary>
	/// Initializes a new instance of the <see cref="NPCLookInDirectionAction"/> class.
	/// Will take the the direction to look at where left and right are defined in Character
	/// </summary>
	public NPCLookInDirectionAction(NPC npcToLook, float direction){
		_npcToLook = npcToLook;
		_direction = direction;
	}

	public override void Perform(){
		if (_direction == -1){
			_npcToLook.LookLeft();
		} else {
			_npcToLook.LookRight();
		}
	}
}
