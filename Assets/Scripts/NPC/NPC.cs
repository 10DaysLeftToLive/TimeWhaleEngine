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
	protected EmotionState currentEmotion;
	
	protected override void Init(){
		chatObject = GameObject.Find("Chat").GetComponent<Chat>();
		player = GameObject.Find("PlayerCharacter").GetComponent<Player>();
		EventManager.instance.mOnNPCInteractionEvent += new EventManager.mOnNPCInteractionDelegate(ReactToInteractionEvent);
		npcSchedule = GetSchedule();
		currentEmotion = GetInitEmotionState();
	}
	
	protected override void CharacterUpdate(){
		if (chating && !NearPlayer()){
			CloseChat();
		}
		npcSchedule.Run(Time.deltaTime);
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
			NPCChoiceInteraction choiceInteraction = (NPCChoiceInteraction) otherInteraction;
			currentEmotion.ReactToChoiceInteraction(choiceInteraction._npcReacting.name, choiceInteraction._choice);
		} else if (otherInteraction.GetType().Equals(typeof(NPCItemInteraction))) {
			NPCItemInteraction itemInteraction = (NPCItemInteraction) otherInteraction;
			currentEmotion.ReactToItemInteraction(itemInteraction._npcReacting.name, itemInteraction._itemName);
		} else if (otherInteraction.GetType().Equals(typeof(NPCEnviromentInteraction))) {
			NPCEnviromentInteraction enviromentInteraction = (NPCEnviromentInteraction) otherInteraction;
			currentEmotion.ReactToItemInteraction(enviromentInteraction._npcReacting.name, enviromentInteraction._enviromentAction);			
		}
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
		EventManager.instance.RiseOnNPCInteractionEvent(new NPCItemInteraction(this.gameObject, player.Inventory.GetItem().name));
		RightButtonCallback();
		Debug.Log("Right click");
		UpdateChatButtons();
	}
	
	public void OpenChat(){
		Debug.Log("Player does " + (player.Inventory.HasItem() ? "" : "not") + "have an item");
		if (player.Inventory.HasItem()){
			chatObject.SetButtonCallbacks(LeftButtonClick, RightButtonClick);
			chatObject.SetGrabText(player.Inventory.GetItem().name);
		} else {
			chatObject.SetButtonCallbacks(LeftButtonClick);
		}
		
		chating = true;
		chatObject.CreateChatBox(GetChoices(), GetWhatToSay());
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