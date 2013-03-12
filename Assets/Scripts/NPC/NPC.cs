using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class NPC : Character {
	protected Player player;
	Chat chatObject;
	public int npcDisposition; // NOTE should not be public but this makes testing easier
	private List<Item> itemReactions;
	private bool chating = false;
	private static int DISTANCE_TO_CHAT = 2;
	public int id;
	
	protected override void Init(){
		chatObject = GameObject.Find("Chat").GetComponent<Chat>();
		player = GameObject.Find("PlayerCharacter").GetComponent<Player>();
	}
	
	protected override void CharacterUpdate(){
		if (chating && !NearPlayer()){
			CloseChat();
		}
	}
	
	protected abstract string GetWhatToSay();
	protected abstract void LeftButtonCallback();
	protected abstract void RightButtonCallback();
	
	private void LeftButtonClick(){
		LeftButtonCallback();
	}
	
	private void RightButtonClick(){
		RightButtonCallback();
		Debug.Log("Right click");
		Debug.Log("Player does " + (player.Inventory.HasItem() ? "" : "not") + "have an item");
		UpdateChatButtons();
		//player.EnterState(new IdleState(this));
	}
	
	public void OpenChat(){
		Debug.Log("Player does " + (player.Inventory.HasItem() ? "" : "not") + "have an item");
		if (player.Inventory.HasItem()){
			chatObject.SetButtonCallbacks(LeftButtonClick, RightButtonClick);
		} else {
			chatObject.SetButtonCallbacks(LeftButtonClick);
		}
		
		chating = true;
		chatObject.CreateChatBox(this.gameObject, player.gameObject, GetWhatToSay());
	}
	
	protected void UpdateChat(string newMessage){
		chatObject.UpdateMessage(newMessage);
	}
	
	protected void UpdateChatButtons(){
		// TODO change words on buttons
		if (player.Inventory.HasItem()){
			chatObject.SetButtonCallbacks(LeftButtonClick, RightButtonClick);
		} else {
			chatObject.SetButtonCallbacks(LeftButtonClick);
		}
	}
	
	public void CloseChat(){
		chatObject.RemoveChatBox();
		chating = false;
	}
	
	private bool NearPlayer(){
		return (Utils.CalcDistance(player.transform.position.x, this.transform.position.x) < DISTANCE_TO_CHAT);
	}
	
	#region disposition
	public void SetDisposition(int disp) {
		npcDisposition = disp;
	}
	
	public int GetDisposition(){
		return npcDisposition;	
	}
	
	public void UpdateDisposition(int disp) {
		NPCDispositionManager.instance.UpdateWithId(id, disp);
	}
	#endregion
	
	#region item interactions
	public void SetInteractions(List<Item> items){
		itemReactions = items;
	}
	#endregion
	
	#region item reaction
	public void ReactTo(GameObject itemToReactTo){
		DoReaction(itemToReactTo);
		CloseChat();
	}
	
	protected abstract void DoReaction(GameObject itemToReactTo);
	#endregion
}