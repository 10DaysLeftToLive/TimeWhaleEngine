using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class NPC : Character {
	private Player player;
	public int npcDisposition; // NOTE should not be public but this makes testing easier
	private List<Item> itemReactions;
	private bool chating = false;
	private Texture charPortrait;
	private static int DISTANCE_TO_CHAT = 12;
	private static int DISPOSITION_LOW_END = 0;
	private static int DISPOSITION_HIGH_END = 10;
	public static int DISPOSITION_LOW = 3; // these should not be hard set
	public static int DISPOSITION_HIGH = 7;
	public int id;
	protected ScheduleStack scheduleStack;
	protected Schedule defaultSchedule;
	public EmotionState currentEmotion;
	protected Dictionary<string, Reaction> flagReactions;
	
	protected override void Init(){
		charPortrait = (Texture)Resources.Load("" + this.name, typeof(Texture));
		player = GameObject.Find("PlayerCharacter").GetComponent<Player>();
		currentEmotion = GetInitEmotionState();
		NPCManager.instance.Add(this.gameObject);
		scheduleStack = new ScheduleStack();
		flagReactions = new Dictionary<string, Reaction>();
		SetFlagReactions();
		defaultSchedule = GetSchedule();
		scheduleStack.Add(defaultSchedule);
	}
	
	protected override void CharacterUpdate(){
		if (chating && !NearPlayer()){
			CloseChat();
		}
		scheduleStack.Run(Time.deltaTime);
	}
	
	private void PassiveChat(){
		// if can chat
		if (scheduleStack.CanChat()) {
			// if near player say hello
			Dictionary<string, GameObject> npcDict = NPCManager.instance.getNPCDictionary();
			// check npc close (not self)
			foreach (var npc in npcDict) {
				if(npc.Value != this && !chating && InChatDistance(npc.Value)) {
					// send request if so
					// if other NPC can chat
						// chat
				}
			}	
		}
	}
	
	private bool InChatDistance(GameObject gameObject) {
		float xDistance = Mathf.Abs(this.transform.position.x - gameObject.transform.position.x);
		float yDistance = Mathf.Abs(this.transform.position.y - gameObject.transform.position.y);
		
		if (xDistance < DISTANCE_TO_CHAT && yDistance < DISTANCE_TO_CHAT) {
			return true;
		} else {
			return false;
		}
	}
	
	protected abstract void SetFlagReactions();
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
	
	public void ReactToFlag(string flagName){
		Debug.Log(name + " is reacting to the flag " + flagName);
		flagReactions[flagName].React();
	}
	
	public void ReactToChoice(string choice){
		currentEmotion.ReactToChoice(choice);	
	}
	
	public string GetDisplayText(){
		return (currentEmotion.GetWhatToSay());	
	}
	
	public Texture GetPortrait(){
		return (charPortrait);	
	}
	
	public List<string> GetButtonChats(){
		return (currentEmotion.GetButtonTexts());
	}
	
	public List<string> GetFlags(){
		List<string> flags = new List<string>();
		foreach (string flag in flagReactions.Keys){
			flags.Add(flag);	
		}
		return (flags);
	}

	private bool NearPlayer(){
		return Vector3.Distance(player.transform.position, this.transform.position) < DISTANCE_TO_CHAT;
	}
	
	public void NextTask(){
		//EventManager.instance.RiseOnNPCInteractionEvent(new NPCEnviromentInteraction(this.gameObject, "Task Done"));
		Debug.Log(name + " NPC next task");
		scheduleStack.NextTask();
	}
	
	private void CloseChat(){
		
	}
	
	public void StarTalkingWithPlayer(){
		EnterState(new InteractingWithPlayerState(this));
	}
	
	public void UpdateEmotionState(EmotionState newEmotionState){
		currentEmotion = newEmotionState;	
	}
	
	public int GetHighDisposition(){
		return (11);	
	}
	
	public int GetLowDisposition(){
		return (-5);	
	}
	
	#region disposition
	public void SetDisposition(int disp) {
		npcDisposition = disp;
	}
	
	public int GetDisposition() {
		return npcDisposition;	
	}
	
	// Changes the disposition to be within the disposition bounds
	public void UpdateDisposition(int deltaDisp) {
		Debug.Log("Updating disposition to " + deltaDisp);
		int disp = npcDisposition + deltaDisp;
		
		if (disp < DISPOSITION_LOW_END){
			disp = DISPOSITION_LOW_END;
		}
		else if (disp > DISPOSITION_HIGH_END) {
			disp = DISPOSITION_HIGH_END;
		}
		
		NPCDispositionManager.instance.UpdateWithId(id, disp);
	}
	#endregion
}