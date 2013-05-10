using UnityEngine;
using System.Collections;

public class ChangeEmotionState : AbstractState {
	EmotionState _emotionStateToChange;
	
	public ChangeEmotionState(Character toControl, EmotionState emotionStateToChange) : base(toControl){
		_emotionStateToChange = emotionStateToChange;
	}
	
	public override void Update(){
		if (character is NPC) {
			((NPC)character).UpdateEmotionState(_emotionStateToChange);
		}
		
		character.EnterState(new MarkTaskDone(character));
	}
	
	public override void OnEnter(){
	}
	
	public override void OnExit(){
	}
}
