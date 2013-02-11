using UnityEngine;
using System.Collections;

/*
 * NPC_Click.cs
 * 	Implements the DoClickNextToPlayer and will call the InteractionManager's perform interaction
 */

public class NPC_Click : OnClickNextToPlayer {
	void Start(){
		base.InitEvent();
	}
	
	protected override void DoClickNextToPlayer(){
		InteractionManager.instance.PerformInteraction(this.gameObject);
	}
}