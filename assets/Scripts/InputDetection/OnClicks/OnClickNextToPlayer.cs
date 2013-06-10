using UnityEngine;
using System.Collections;

/*
 * OnClickNextToPlayer.cs
 *   Will be called when the attached object is clicked on. If the player is close enough then DoClickNextToPlayer() will be called.
 *   if the player is not close enough then is will set of the event that there was a click but it was too far away.
 */

public class OnClickNextToPlayer : OnClick {
	public float minimumDistance = 1.5f;
	protected Player player;
	
	void Awake(){
		FindPlayer ();
	}
	
	public void FindPlayer(){	
		player = FindObjectOfType(typeof(Player)) as Player;		
	}

	void Start(){
		base.InitEvent();
	}
	
	protected virtual void DoClickNextToPlayer(){}
	
	protected override void DoClick(ClickPositionArgs e){
		
	}
}
