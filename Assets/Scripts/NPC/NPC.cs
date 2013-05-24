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
	protected int id = -1;
	private int npcDisposition;
	
	protected ScheduleStack scheduleStack;
	protected EmotionState currentEmotion;
	protected Dictionary<string, Reaction> flagReactions;
	private Dictionary<int, Reaction> timeReactions;
	private int truncatedTime;
	
	public Player player;
	private bool chatingWithPlayer = false;
	public bool chatingWithNPC = false;
	private Texture charPortrait;
	private Action sayHi;
	public float timeTillPassiveChatAgain = 0;
	#endregion
	
	#region Set Values
	private static int TIME_TRUNCATION = 10; // should be a power of 10
	private static int NEAR_DISTANCE = 8;
	private static int DISTANCE_TO_CHAT = 4;
	private static int DISPOSITION_LOW_END = 0;
	private static int DISPOSITION_HIGH_END = 10;
	public static int DISPOSITION_LOW = 3; // these should not be hard set
	public static int DISPOSITION_HIGH = 7;
	#endregion
	
	#region Initialization
	protected override void Awake() {
		base.Awake();
		NPCManager.instance.Add(this.gameObject);
	}
	
	protected override void Init(){
		FindInitialObjects();
		currentEmotion = GetInitEmotionState();
		scheduleStack = new ScheduleStack();
		flagReactions = new Dictionary<string, Reaction>();
		timeReactions = new Dictionary<int, Reaction>();
		scheduleStack.Add(new DefaultSchedule(this)); // Need to add a default schedule that will never end
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
			Debug.LogError("No initial character portait found for " + name);	
		}
		if (player == null){
			Debug.LogError("No player was found by " + name);	
		}
	}
	#endregion
	
	#region Update
	protected override void CharacterUpdate(){
		if (chatingWithPlayer && !NearPlayerToChat()){
			CloseChat();
			StopTalkingWithPlayer();
		}
		scheduleStack.Run(Time.deltaTime);
	}
	#endregion
	
	#region Chat/Interaction 
	public bool CanTalk(){
		return (this.scheduleStack.CanInteractWithPlayer());
	}
	
	public bool CanPassiveChat() {
		return (scheduleStack.CanPassiveChat());
	}
	
	public bool NearNPC(NPC npcNear) {
		return InDistance(npcNear.gameObject, NEAR_DISTANCE);
	}
	
	public bool NearPlayer() {
		return InDistance(player.gameObject, NEAR_DISTANCE);
	}
	
	static float xDistance;
	static float yDistance;
	private bool InDistance(GameObject gameObject, float distance) {
		xDistance = Mathf.Abs(this.transform.position.x - gameObject.transform.position.x);
		yDistance = Mathf.Abs(this.transform.position.y - gameObject.transform.position.y);
		
		return (xDistance < distance && yDistance < distance);
	}
	
	private void CloseChat(){
		GUIManager.Instance.CloseInteractionMenu();
	}
	
	public void LeaveInteraction(){
		chatingWithPlayer = false;
		scheduleStack.Resume();
		currentEmotion.OnInteractionCloses();
	}
	
	private void StopTalkingWithPlayer(){
		GUIManager.Instance.CloseInteractionMenu();
		LeaveInteraction();
	}
	
	public void StarTalkingWithPlayer(){
		currentEmotion.OnInteractionOpens();
		chatingWithPlayer = true;
		PassiveChatToPlayer.instance.RemoveNPCChat(this);
		scheduleStack.Pause();
		EnterState(new InteractingWithPlayerState(this));
	}
	
	public void UpdateDefaultText(string newText){
		currentEmotion.SetDefaultText(newText);	
	}
	
	public bool IsInteracting(){
		return (chatingWithPlayer);	
	}
	
	/// <summary>
	/// Sets the character portrait.
	/// </summary>
	/// <param name='emotion'>
	/// Name of the emotion in resources folder [npcName][emotion]. Send empty string for default/neutral face
	/// </param>
	public void SetCharacterPortrait(string emotion){
		charPortrait = (Texture)Resources.Load(this.name + emotion, typeof(Texture));
		if(charPortrait == null){
			Debug.LogWarning("Could not find " + this.name + emotion + " in /Resources");
			charPortrait = (Texture)Resources.Load(this.name, typeof(Texture));
		}
	}
	
	/// <summary>
	/// Adds the choice to the current emotion state
	/// </summary>
	public void AddChoice(Choice newChoice, DispositionDependentReaction reaction){
		currentEmotion.AddChoice(newChoice, reaction);
	}
	
	/// <summary>
	/// Removes the choice from the current emotion state
	/// </summary>
	public void RemoveChoice(Choice choiceToRemove){
		currentEmotion.RemoveChoice(choiceToRemove);
	}
	#endregion	
	
	#region Schedule
	public void AddSchedule(Schedule scheduleToAdd){
		// Special case since conversation schedules are shared
		if (scheduleToAdd is NPCConvoSchedule) {
			((NPCConvoSchedule)scheduleToAdd)._npcTwo.AddSharedSchedule((NPCConvoSchedule)scheduleToAdd);
		}
		
		scheduleStack.Add(scheduleToAdd);	
	}
	
	public void AddSharedSchedule(NPCConvoSchedule scheduleToAdd){
		scheduleStack.Add(scheduleToAdd);
	}
	
	// Can be overriden by children. Is recomended to do this.
	protected virtual void SetUpSchedules(){}
	
	public void RemoveScheduleWithFlag(string flag) {
		scheduleStack.RemoveScheduleWithFlag(flag);
	}
	#endregion
	
	#region Functions specific to each NPC
	protected abstract void SetFlagReactions();
	protected abstract Schedule GetSchedule(); // TODO read/set this from file?
	protected abstract EmotionState GetInitEmotionState();
	#endregion
	
	#region Reactions
	public void ReactToFlag(string flagName){
		DebugManager.instance.Log(name + " is reacting to the flag " + flagName, "Flag", name, flagName, "NPC");
		
		flagReactions[flagName].React();
	}
	
	/// <summary>
	/// timeToReact must be within 0800 - 2000
	/// </summary>
	public void AddTimeReaction(int timeToReact, Reaction reaction) {
		if (timeToReact > 800 && timeToReact < 2000) {
			timeReactions.Add(TruncateTime(timeToReact), reaction);
			TimeReactionManager.instance.Add(this);
		} else {
			Debug.LogWarning("TimeToReact must be within 0800 - 2000");
		}
	}
	
	public void ReactToTime(int gameDayTime) {
		truncatedTime = TruncateTime(gameDayTime);
		if (timeReactions.ContainsKey(truncatedTime)) {
			timeReactions[truncatedTime].React();
			timeReactions.Remove(truncatedTime);
			if (timeReactions.Count < 1) {
				TimeReactionManager.instance.Remove(this);
			}
		}
	}
	
	/// <summary>
	/// Used to truncate military time to 10 minute intervals
	/// </summary>
	public int TruncateTime(int gameDayTime) {
		return Mathf.RoundToInt((float)gameDayTime/TIME_TRUNCATION);
	}
	
	public void ReactToChoice(string choice){
		currentEmotion.ReactToChoice(choice);	
	}
	
	public void ReactToBeingGivenItem(GameObject item){
		currentEmotion.ReactToGiveItem(item);
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
	
	public int ID{
		get {return id;}
	}
	#endregion
	
	#region Utility Methods
	private bool NearPlayerToChat(){
		return Vector3.Distance(player.transform.position, this.transform.position) < DISTANCE_TO_CHAT;
	}
	
	public void NextTask(){
		scheduleStack.NextTask();
	}
	
	public void UpdateEmotionState(EmotionState newEmotionState){
		currentEmotion = newEmotionState;	
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