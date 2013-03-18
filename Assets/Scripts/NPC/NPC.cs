using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class NPC : Character {
	protected Player player;
	Chat chatObject;
	public int npcDisposition; // NOTE should not be public but this makes testing easier
	private List<Item> itemReactions;
	private bool chating = false;
	private Texture charPortrait;
	private static int DISTANCE_TO_CHAT = 20;
	public int id;
	private Schedule npcSchedule;
	public EmotionState currentEmotion;
	
	protected override void Init(){
		chatObject = GameObject.Find("Chat").GetComponent<Chat>();
		charPortrait = (Texture)Resources.Load("" + this.name, typeof(Texture));
		Debug.Log ("TEXTURE LOADED IS CALLED: " + charPortrait.name);
		player = GameObject.Find("PlayerCharacter").GetComponent<Player>();
		EventManager.instance.mOnNPCInteractionEvent += new EventManager.mOnNPCInteractionDelegate(ReactToInteractionEvent);
		EventManager.instance.mOnPlayerPickupItemEvent += new EventManager.mOnPlayerPickupItemDelegate(ReactToItemPickedUp);
		//npcSchedule = GetSchedule();
		Debug.Log (name + ": Is player initialized: " + (player != null));
		currentEmotion = GetInitEmotionState();
	}
	
	protected override void CharacterUpdate(){
		if (chating && !NearPlayer()){
			CloseChat();
		}
		//npcSchedule.Run(Time.deltaTime);
	}
	
	// ONLY PUT SPECIFIC NPC THINGS IN THESE IN THE CHILDREN
	protected abstract void LeftButtonCallback(string choice);
	protected abstract void RightButtonCallback();
	protected abstract void DoReaction(GameObject itemToReactTo);
	protected abstract Schedule GetSchedule(); // TODO read/set this from file?
	protected abstract EmotionState GetInitEmotionState();
	
	private void ReactToInteractionEvent(EventManager EM, NPCInteraction otherInteraction){
		Debug.Log(name + " is reacting to event with " + otherInteraction._npcReacting.name);
		if (otherInteraction.GetType().Equals(typeof(NPCChoiceInteraction))){
			Debug.Log("Calling Choice Interaction for " + otherInteraction._npcReacting.name);
			NPCChoiceInteraction choiceInteraction = (NPCChoiceInteraction) otherInteraction;
			currentEmotion.ReactToChoiceInteraction(choiceInteraction._npcReacting.name, choiceInteraction._choice);
		} else if (otherInteraction.GetType().Equals(typeof(NPCItemInteraction))) {
			Debug.Log("Calling Item Interaction for " + otherInteraction._npcReacting.name);
			NPCItemInteraction itemInteraction = (NPCItemInteraction) otherInteraction;
			currentEmotion.ReactToItemInteraction(itemInteraction._npcReacting.name, itemInteraction._item);
		} else if (otherInteraction.GetType().Equals(typeof(NPCEnviromentInteraction))) {
			NPCEnviromentInteraction enviromentInteraction = (NPCEnviromentInteraction) otherInteraction;
			currentEmotion.ReactToEnviromentInteraction(enviromentInteraction._npcReacting.name, enviromentInteraction._enviromentAction);			
		}
	}
	
	private void ReactToItemPickedUp(EventManager EM, PickUpStateArgs itemPickedUp){
		currentEmotion.ReactToItemPickedUp(itemPickedUp.itemPickedUp);
	}
	
	private List<Choice> GetChoices(){
		return(currentEmotion.GetChoices());
	}
	
	protected string GetWhatToSay(){
		return (currentEmotion.GetWhatToSay());
	}
	
	private void LeftButtonClick(string choice){
		LeftButtonCallback(choice);
	}
	
	private void RightButtonClick(){
		if (player.Inventory.GetItem() != null && currentEmotion.ItemHasReaction(player.Inventory.GetItem().name)){
			EventManager.instance.RiseOnNPCInteractionEvent(new NPCItemInteraction(this.gameObject, player.Inventory.GetItem()));
			RightButtonCallback();
			Debug.Log("Right click");
			player.Inventory.DisableHeldItem();
			UpdateChatButtons();
		}
	}
	
	public void OpenChat(){
		//Debug.Log("Player does " + (player.Inventory.HasItem() ? "" : "not") + "have an item");
		Debug.Log(this.name + " What is player value: " + player);
		if (player.Inventory.HasItem() && currentEmotion.ItemHasReaction(player.Inventory.GetItem().name)){
			chatObject.SetButtonCallbacks(LeftButtonClick, RightButtonClick);
			chatObject.SetGrabText(player.Inventory.GetItem().name);
		} else {
			chatObject.SetButtonCallbacks(LeftButtonClick);
		}
		
		chating = true;
		Debug.Log ("INFRONT OF setCharPortrait: " + charPortrait.name);
		chatObject.setCharPortrait(charPortrait);
		chatObject.CreateChatBox(GetChoices(), GetWhatToSay());
	}
	
	public void ToggleChat(){
		if (chating){
			player.EnterState(new IdleState(player));
			CloseChat();
		} else {
			Debug.Log(player);
			player.EnterState(new TalkState(player, this));
			OpenChat();
		}
	}
	
	public void UpdateChat(string newMessage){
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
		return Vector3.Distance(player.transform.position, this.transform.position) < DISTANCE_TO_CHAT;
	}
	
	public void NextTask(){
		EventManager.instance.RiseOnNPCInteractionEvent(new NPCEnviromentInteraction(this.gameObject, player.Inventory.GetItem().name));
		
		npcSchedule.NextTask();
	}
	
	#region disposition
	public void SetDisposition(int disp) {
		npcDisposition = disp;
	}
	
	public int GetDisposition() {
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