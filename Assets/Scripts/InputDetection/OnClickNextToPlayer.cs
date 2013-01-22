using UnityEngine;
using System.Collections;

public class OnClickNextToPlayer : OnClick {
	protected PlayerController playerCharacter;
	public float minimumDistance = 1.5f;
	
	void Awake(){
		playerCharacter = FindObjectOfType(typeof(PlayerController)) as PlayerController;		
	}

	void Start(){
		base.InitEvent();
	}
	
	protected virtual void DoClickNextToPlayer(){}
	
	protected override void DoClick(){
		Vector3 player = playerCharacter.transform.position;
		Vector3 position = transform.position;
		
		Vector2 flatPlayerPos = new Vector2(player.x, player.y);
		Vector2 flatPos = new Vector2(position.x, position.y);
		
		if (Vector2.Distance(flatPlayerPos, flatPos) < minimumDistance){
			DoClickNextToPlayer();
		}
	}
}
