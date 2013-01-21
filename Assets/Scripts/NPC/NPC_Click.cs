using UnityEngine;
using System.Collections;

public class NPC_Click : OnClickNextToPlayer {
	void Start(){
		base.InitEvent();
	}
	
	protected override void DoClickNextToPlayer(){
		InteractionManager.instance.PerformInteraction(this.gameObject);
	}
}