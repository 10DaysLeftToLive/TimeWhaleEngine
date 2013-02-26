using UnityEngine;
using System.Collections;

public class GrabOntoState : AbstractState {
	private GameObject _toGrabOn;
	
	public GrabOntoState (Character toControl, GameObject toGrabOn) : base(toControl){
		_toGrabOn = toGrabOn;
	}
	
	public override void Update(){
		Debug.Log(character.name + ": GrabOntoState Update");
		
		character.GrabOnto(_toGrabOn);
		
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