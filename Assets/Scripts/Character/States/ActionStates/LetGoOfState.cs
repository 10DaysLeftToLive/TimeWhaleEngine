using UnityEngine;
using System.Collections;

public class LetGoOfState : AbstractState {
	private GameObject _toLetGoOf;
	
	public LetGoOfState (Character toControl, GameObject toLetGoOf) : base(toControl){
		_toLetGoOf = toLetGoOf;
	}
	
	public override void Update(){
		Debug.Log(character.name + ": LetGoOfState Update");
		
		character.DetachFrom(_toLetGoOf);
		
		character.EnterState(new IdleState(character));
	}
	
	public override void OnEnter(){
		Debug.Log(character.name + ": LetGoOfState Enter to grab onto " + _toLetGoOf.name);
		// TODO let go of item
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": LetGoOfState Exit");
	}
}