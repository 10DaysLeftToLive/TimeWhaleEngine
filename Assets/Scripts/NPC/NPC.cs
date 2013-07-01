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
	protected string initialPortrait = "";
	protected int id = -1;
	private int npcDisposition;
	public string textureAtlasName;
	private string currentFaceTexture = "Default";
	protected ScheduleStack scheduleStack;
	protected EmotionState currentEmotion;
	protected Dictionary<string, Reaction> flagReactions;
	private Dictionary<int, Reaction> timeReactions;
	private int truncatedTime;
	public Player player;
	public bool chatingWithPlayer = false;
	public bool chatingWithNPC = false;
	public CharacterAgeState ageNPCisIn;
	private Texture charPortrait;
	public float timeTillPassiveChatAgain = 0;
	#endregion
	
	#region Set Values
	private static int TIME_TRUNCATION = 10; // should be a power of 10
	private static int NEAR_DISTANCE = 8;
	private static int DISTANCE_TO_CHAT = 4;
	#endregion
	
	#region Initialization
	protected override void Awake() {
		base.Awake();
	}
	
	protected override void Init(){
		currentState = new IdleState(this);
		FindInitialObjects();
		NPCManager.instance.Add(this.gameObject);
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
		SetCharacterPortrait(initialPortrait);
		player = GameObject.Find("PlayerCharacter").GetComponent<Player>();
		animationData = GetComponent<SmoothMoves.BoneAnimation>();
		if (animationData == null && !(this is Sibling)){
			Debug.LogWarning("No animation data attached to " + name);	
		}
		if (charPortrait == null){
			Debug.LogWarning("No initial character portait found for " + name);	
		}
		if (player == null){
			Debug.LogWarning("No player was found by " + name);	
		}
	}
	#endregion
	
	#region Update
	protected override void CharacterUpdate(){
		if (chatingWithPlayer && !NearPlayerToChat()){
			StopTalkingWithPlayer();
		}
		try {
			scheduleStack.Run(Time.deltaTime);
		} catch {
			Debug.LogError("Something went wrong with " + name + "'s schedule switching it to the default one.");
			ClearAndReplaceSchedule(new DefaultSchedule(this));
		}
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
		scheduleStack.Resume();
		currentEmotion.OnInteractionCloses();
		chatingWithPlayer = false;
	}
	
	private void StopTalkingWithPlayer(){
		GUIManager.Instance.CloseInteractionMenu();
	}
	
	public virtual void StarTalkingWithPlayer(){
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
	public virtual void SetCharacterPortrait(string emotion){
		charPortrait = (Texture)Resources.Load(this.name + "/" + this.name + emotion, typeof(Texture));
		if(charPortrait == null) {
			Debug.LogWarning("Could not find " + this.name + emotion + " in /Resources");
			charPortrait = (Texture)Resources.Load(this.name, typeof(Texture));
		}
	}
	
	public void ChangeFacialExpression(string emotion) {
		if (emotion.Equals(string.Empty)) { emotion = "Default"; }
//		foreach (SmoothMoves.TextureAtlas atlas in animationData.textureAtlases) {
//			Debug.Log (atlas.name);
//		}
		animationData.SwapBoneTexture("Head", textureAtlasName, currentFaceTexture, textureAtlasName, emotion);
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
	
	public bool CanTakeItem(string itemName){
		return (currentEmotion.CanTakeItem(itemName));
	}
	#endregion	
	
	#region Schedule
	public void AddSchedule(Schedule scheduleToAdd){
		scheduleStack.Add(scheduleToAdd);	
	}
	
	public void AddSharedSchedule(Schedule scheduleToAdd){
		scheduleStack.AddShared(scheduleToAdd);	
	}
	
	// Can be overriden by children. Is recomended to do this.
	protected virtual void SetUpSchedules(){}
	
	public void RemoveScheduleWithFlag(string flag) {
		scheduleStack.RemoveScheduleWithFlag(flag);
	}
	
	public void ClearAndReplaceSchedule(Schedule newSchedule){
		scheduleStack.ClearAndReplace(this, newSchedule);
	}
	#endregion
	
	#region Functions specific to each NPC
	protected virtual void SetFlagReactions(){}
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
	
	public int ID{
		get {return id;}
	}
	#endregion
	
	#region Utility Methods
	public void LookAt(GameObject objectToLookAt){
		if (Utils.CalcDifference(this.transform.position.x, objectToLookAt.transform.position.x) < 0){
			LookRight();
		} else {
			LookLeft();
		}
	}
	
	public void LookAtPlayer(){
		LookAt(player.gameObject);
	}
	
	private bool NearPlayerToChat(){
		return Vector3.Distance(player.transform.position, this.transform.position) < DISTANCE_TO_CHAT;
	}
	
	public void NextTask(){
		scheduleStack.NextTask();
	}
	
	public void UpdateEmotionState(EmotionState newEmotionState){
		if (newEmotionState.GetWhatToSay() == null){ // if the current emotion state was not set with a default text carry over the old emotionstate's
			newEmotionState.SetDefaultText(currentEmotion.GetWhatToSay());
		}
		currentEmotion = newEmotionState;	
		if (IsInteracting()){
			GUIManager.Instance.RefreshInteraction();
		}
	}
	
	public virtual void SendStringToNPC(string text){}
	#endregion
}