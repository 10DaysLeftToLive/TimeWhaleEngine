using UnityEngine;
using System.Collections;

public class WaitTillPlayerGoneState : WaitState {
	private Player _player;
	private float _distance = 10f;
	
	public WaitTillPlayerGoneState(NPC toControl, Player player) : base (toControl){
		_player = player;
	}
	
	public WaitTillPlayerGoneState(NPC toControl, Player player, float distance) : base (toControl){
		_player = player;
		_distance = distance;
	}
	
	private Vector2 flatPlayerPos;
	private Vector2 flatPos;
	protected override bool ConditionsSatisfied(){
		flatPlayerPos = new Vector2(_player.transform.position.x, _player.transform.position.y);
		flatPos = new Vector2(character.transform.position.x, character.transform.position.y);
		return (Vector2.Distance(flatPlayerPos, flatPos) > _distance);
	}
}
