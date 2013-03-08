using UnityEngine;
using System.Collections;

/*
 * NPC_Click.cs
 * 	Implements the DoClickNextToPlayer and will call the InteractionManager's perform interaction
 */

public class NPC_Click : OnClickNextToPlayer {
	Chat chat;
	
	void Start(){
		chat = GameObject.Find("Chat").GetComponent<Chat>();
		base.InitEvent();
	}
	
	public void TestClick(){
		
	}
	
	protected override void DoClickNextToPlayer(){
		chat.SetButtonCallbacks(TestClick);
		chat.CreateChatBox(this.gameObject, "This is a test");
		//InteractionManager.instance.PerformInteraction(this.gameObject);
	}
}