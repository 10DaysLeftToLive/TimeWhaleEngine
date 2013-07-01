using UnityEngine;
using System.Collections;

public class WaitTillPlayerCloseState : WaitState {
	private Player _player;
	private float _distance = 4f;
	
	public WaitTillPlayerCloseState(NPC toControl) : base (toControl){
		_player = (Player) Utils.FindObject(Strings.Player).GetComponent<Player>();
	}
	
	public WaitTillPlayerCloseState(NPC toControl, ref Player player) : base (toControl){
		_player = player;
	}
	
	public WaitTillPlayerCloseState(NPC toControl, ref Player player, float distance) : base (toControl){
		_player = player;
		_distance = distance;
	}
	
	private Vector2 flatPlayerPos;
	private Vector2 flatPos;
	protected override bool ConditionsSatisfied() {
		if(_player == null){
			_player = GameObject.Find ("PlayerCharacter").GetComponent<Player>();	
		}
		flatPlayerPos = new Vector2(_player.transform.position.x, _player.transform.position.y);
		flatPos = new Vector2(character.transform.position.x, character.transform.position.y);
		return (Vector2.Distance(flatPlayerPos, flatPos) < _distance);
	}
}