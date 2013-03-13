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
	private Schedule npcSchedule;
	
	protected override void Init(){
		chatObject = GameObject.Find("Chat").GetComponent<Chat>();
		player = GameObject.Find("PlayerCharacter").GetComponent<Player>();
		EventManager.instance.mOnNPCInteractionEvent += new EventManager.mOnNPCInteractionDelegate(ReactToInteractionEvent);
		npcSchedule = GetSchedule();
	}
	
	protected override void CharacterUpdate(){
		if (chating && !NearPlayer()){
			CloseChat();
		}
		npcSchedule.Run(Time.deltaTime);
	}
	
	// ONLY PUT SPECIFIC NPC THINGS IN THESE IN THE CHILDREN
	protected abstract void ReactToItemInteraction(string npc, string item);
	protected abstract void ReactToChoiceInteraction(string npc, string choice);
	protected abstract string GetWhatToSay();
	protected abstract void LeftButtonCallback();
	protected abstract void RightButtonCallback();
	protected abstract void DoReaction(GameObject itemToReactTo);
	protected abstract Schedule GetSchedule(); // TODO read/set this from file
	
	private void ReactToInteractionEvent(EventManager EM, NPCInteraction otherInteraction){
		Debug.Log(name + " is reacting to event with " + otherInteraction._npcReacting.name);
		if (otherInteraction._npcReacting.name != this.gameObject.name){ // make sure we don't interact to our own interaction
			if (otherInteraction.GetType().Equals(typeof(NPCChoiceInteraction))){
				NPCChoiceInteraction choiceInteraction = (NPCChoiceInteraction) otherInteraction;
				ReactToChoiceInteraction(choiceInteraction._npcReacting.name, choiceInteraction._choice);
			} else {
				NPCItemInteraction itemInteraction = (NPCItemInteraction) otherInteraction;
				ReactToItemInteraction(itemInteraction._npcReacting.name, itemInteraction._itemName);
			}
		}
	}
	
	private void LeftButtonClick(){
		LeftButtonCallback();
	}
	
	private void RightButtonClick(){
		EventManager.instance.RiseOnNPCInteractionEvent(new NPCItemInteraction(this.gameObject, player.Inventory.GetItem().name));
		RightButtonCallback();
		Debug.Log("Right click");
		UpdateChatButtons();
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
	
	public void ToggleChat(){
		if (chating){
			player.EnterState(new IdleState(player));
			CloseChat();
		} else {
			player.EnterState(new TalkState(player, this));
			OpenChat();
		}
		chating = !chating;
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
	
	public void NextTask(){
		npcSchedule.NextTask();
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
}