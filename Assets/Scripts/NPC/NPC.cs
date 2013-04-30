using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// NPC class that all specific NPCs will inherit from
/// 	Has an emotion state that will determine what it will say and take when interacting with the player
/// 	Has a Schedule stack that it will follow and may be interupted by the player or other NPCs
/// 	Contains a dictionary of flag reactions which are called when a certain flag event occurs
/// </summary>
public abstract class NPC : Character {
	#region Fields
	public int id = -1;
	private int npcDisposition;
	
	protected ScheduleStack scheduleStack;
	protected EmotionState currentEmotion;
	protected Dictionary<string, Reaction> flagReactions;
	
	private Player player;
	private bool chatingWithPlayer = false;
	public bool chatingWithNPC = false;
	private Texture charPortrait;
	#endregion
	
	#region Set Values
	private static int DISTANCE_TO_CHAT = 4;
	private static int DISTANCE_TO_SEE = 8;
	private static int DISPOSITION_LOW_END = 0;
	private static int DISPOSITION_HIGH_END = 10;
	private static int CHANCE_TO_CHAT = 3; // Sets chance to be 1 out of this value
	public static int DISPOSITION_LOW = 3; // these should not be hard set
	public static int DISPOSITION_HIGH = 7;
	#endregion
	
	#region Initialization
	protected override void Awake() {
		base.Awake();
		NPCManager.instance.Add(this.gameObject);
	}
	
	protected override void Init(){
		if (id == -1)Debug.LogWarning("Id for " + name + " was not set.");	
		FindInitialObjects();
		currentEmotion = GetInitEmotionState();
		scheduleStack = new ScheduleStack();
		flagReactions = new Dictionary<string, Reaction>();
		SetUpSchedules();
		SetFlagReactions();
		scheduleStack.Add(GetSchedule());
	}
	
	private void FindInitialObjects(){
		charPortrait = (Texture)Resources.Load("" + this.name, typeof(Texture));
		player = GameObject.Find("PlayerCharacter").GetComponent<Player>();
		animationData = GetComponent<SmoothMoves.BoneAnimation>();
		if (animationData == null){
			Debug.LogError("No animation data attached to " + name);	
		}
		if (charPortrait == null){
			Debug.LogError("No character portait found for " + name);	
		}
		if (player == null){
			Debug.LogError("No player was found by " + name);	
		}
	}
	#endregion
	
	#region Update
	protected override void CharacterUpdate(){
		if (chatingWithPlayer && !NearPlayer()){
			CloseChat();
		} else {
			PassiveChat();
		}
		scheduleStack.Run(Time.deltaTime);
	}
	#endregion
	
	#region Chat/Interaction 
	private void PassiveChat(){
		if (scheduleStack.CanPassiveChat()) {
			if (InChatDistance(player.gameObject)) {
				// Say hi (one off chat)
			} else if (InSight(player.gameObject)) {
				//Debug.Log("Trying to chat");
				// Try to start conversation with nearby NPC or say hi (one off chat) if the player is in sight
				Dictionary<string, GameObject> npcDict = NPCManager.instance.getNPCDictionary();
				NPC npcClass;
				foreach (var npc in npcDict.Values) {
					npcClass = npc.GetComponent<NPC>();
					if(npcClass != this && InChatDistance(npc) /*TODO - Check if past chat timer to chat again*/) {
						if (Random.Range(1, CHANCE_TO_CHAT) > 1) { // Roll dice to check if they will chat
							if (RequestChat(npcClass) && this.scheduleStack.CanPassiveChat()) {
								
								
								// chat Schedule
								/*Debug.Log("Going to chat");
								List<ChatInfo> chats = new List<ChatInfo>();
								chats.Add(new ChatInfo(this, "Chat 1"));
								chats.Add(new ChatInfo(npcClass, "Chat 2"));
								chats.Add(new ChatInfo(this, "Chat 3"));
								chats.Add(new ChatInfo(npcClass, "Chat 4"));
								chats.Add(new ChatInfo(this, "Chat 5"));
								NPCChat tempChat = new NPCChat(chats);
								ChatSchedule chatSchedule1 = new ChatSchedule(this, tempChat);
								this.AddSchedule(chatSchedule1);
								ChatSchedule chatSchedule2 = new ChatSchedule(npcClass, tempChat);
								npcClass.AddSchedule(chatSchedule2);
								chatingWithNPC = true;*/
								break;
							} else { 
								// Say hi (one off chat)
								break;
							}
						}
					}
				}	
			}
		}
	}
	
	public bool RequestChat(NPC npc) {
		if (npc.scheduleStack.CanPassiveChat()) {
			return true;
		} else {
			return false;
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
	
	private void CloseChat(){
	}
	
	public void StarTalkingWithPlayer(){
		EnterState(new InteractingWithPlayerState(this));
	}
	
	public void UpdateDefaultText(string newText){
		currentEmotion.SetDefaultText(newText);	
	}
	#endregion	
	
	#region Schedule
	public void AddSchedule(Schedule scheduleToAdd){
		scheduleStack.Add(scheduleToAdd);	
	}
	
	// Can be overriden by children. Is recomended to do this.
	protected virtual void SetUpSchedules(){}
	#endregion
	
	#region Functions specific to each NPC
	protected abstract void SetFlagReactions();
	protected abstract Schedule GetSchedule(); // TODO read/set this from file?
	protected abstract EmotionState GetInitEmotionState();
	#endregion
	
	#region Reactions
	public void ReactToFlag(string flagName){
		Debug.Log(name + " is reacting to the flag " + flagName);
		flagReactions[flagName].React();
	}
	
	public void ReactToChoice(string choice){
		currentEmotion.ReactToChoice(choice);	
	}
	#endregion
	
	#region Getters
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
	
	public int GetHighDisposition(){
		return (11);	
	}
	
	public int GetLowDisposition(){
		return (-5);	
	}
	#endregion
	
	#region Utility Methods
	private bool NearPlayer(){
		return Vector3.Distance(player.transform.position, this.transform.position) < DISTANCE_TO_CHAT;
	}
	
	public void NextTask(){
		//EventManager.instance.RiseOnNPCInteractionEvent(new NPCEnviromentInteraction(this.gameObject, "Task Done"));
		Debug.Log(name + " NPC next task");
		scheduleStack.NextTask();
	}
	
	public void UpdateEmotionState(EmotionState newEmotionState){
		currentEmotion = newEmotionState;	
	}
	
	private bool InSight(GameObject gameObject) {
		float xDistance = Mathf.Abs(this.transform.position.x - gameObject.transform.position.x);
		float yDistance = Mathf.Abs(this.transform.position.y - gameObject.transform.position.y);
		
		if (xDistance < DISTANCE_TO_SEE && yDistance < DISTANCE_TO_SEE) {
			return true;
		} else {
			return false;
		}
	}
	#endregion
	
	#region Disposition
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