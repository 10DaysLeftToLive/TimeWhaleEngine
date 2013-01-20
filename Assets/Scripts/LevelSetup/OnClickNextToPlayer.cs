using UnityEngine;
using System.Collections;

public class OnClickNextToPlayer : OnClick {
	public PlayerController playerCharacter;
	public float minimumDistance = 1.5f;

	void Start(){
		base.InitEvent();
	}
	
	protected virtual void DoClickNextToPlayer(){}
	
	protected override void DoClick(){
		if (Vector3.Distance(playerCharacter.transform.position, transform.position) < minimumDistance){		
			DoClickNextToPlayer();
		}
	}
}
