using UnityEngine;
using System.Collections;

public class TalkState : AbstractState {
	public TalkState(Character toControl) : base(toControl){}
	
	public override void Update(){
		Debug.Log(character.name + ": TalkState Update");
	}
	
	public override void OnEnter(){
		Debug.Log(character.name + ": TalkState Enter");
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": TalkState Exit");
	}
}