using UnityEngine;
using System.Collections;

public class WaitTillPlayerCloseState : WaitState {
	private Player _player;
	private float distance = 6f;
	
	public WaitTillPlayerCloseState(NPC toControl, Player player) : base (toControl){
		_player = player;
	}
	
	private Vector2 flatPlayerPos;
	private Vector2 flatPos;
	protected override bool ConditionsSatisfied(){
		flatPlayerPos = new Vector2(_player.transform.position.x, _player.transform.position.y);
		flatPos = new Vector2(character.transform.position.x, character.transform.position.y);
		return (Vector2.Distance(flatPlayerPos, flatPos) < distance);
	}
}