using UnityEngine;
using System.Collections;

/*
 * OnClickNextToPlayer.cs
 *   Will be called when the attached object is clicked on. If the player is close enough then DoClickNextToPlayer() will be called.
 *   if the player is not close enough then is will set of the event that there was a click but it was too far away.
 */

public class OnClickNextToPlayer : OnClick {
	protected PlayerController playerCharacter;
	public float minimumDistance = 1.5f;
	
	void Awake(){
		FindPlayer ();
	}
	
	public void FindPlayer(){
		playerCharacter = FindObjectOfType(typeof(PlayerController)) as PlayerController;		
	}

	void Start(){
		base.InitEvent();
	}
	
	protected virtual void DoClickNextToPlayer(){}
	
	protected override void DoClick(ClickPositionArgs e){
		Vector3 player = playerCharacter.transform.position;
		Vector3 position = transform.position;
		
		Vector2 flatPlayerPos = new Vector2(player.x, player.y);
		Vector2 flatPos = new Vector2(position.x, position.y);
		
		if (Vector2.Distance(flatPlayerPos, flatPos) < minimumDistance){
			DoClickNextToPlayer();
		} else {
			EventManager.instance.RiseOnClickOnObjectAwayFromPlayerEvent(e);
		}
	}
}
