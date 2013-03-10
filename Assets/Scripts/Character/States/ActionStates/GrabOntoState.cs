using UnityEngine;
using System.Collections;

/*
 * GrabOntoState.cs
 * 	Called when the player should grab onto an object
 */
public class GrabOntoState : AbstractState {
	private GameObject _toGrabOn;
	
	public GrabOntoState (Character toControl, GameObject toGrabOn) : base(toControl){
		_toGrabOn = toGrabOn;
	}
	
	public override void Update(){
		Debug.Log(character.name + ": GrabOntoState Update");
		
		character.AttachTo(_toGrabOn); // TODO better implementation
		
		character.EnterState(new GrabIdleState(character));
	}
	
	public override void OnEnter(){
		Debug.Log(character.name + ": GrabOntoState Enter to grab onto " + _toGrabOn.name);
		//TODO Grab item
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": GrabOntoState Exit");
	}
}