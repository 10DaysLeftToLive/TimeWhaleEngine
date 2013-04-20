using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class NPC : Character {
	protected Player player;
	public int npcDisposition; // NOTE should not be public but this makes testing easier
	private List<Item> itemReactions;
	private bool chating = false;
	private Texture charPortrait;
	private static int DISTANCE_TO_CHAT = 12;
	private static int DISPOSITION_LOW_END = 0;
	private static int DISPOSITION_HIGH_END = 10;
	public static int DISPOSITION_LOW = 3;
	public static int DISPOSITION_HIGH = 7;
	public int id;
	protected ScheduleStack scheduleStack;
	public EmotionState currentEmotion;
	
	private Dictionary<string, Reaction> flagReactions;
	
	protected override void Init(){
		charPortrait = (Texture)Resources.Load("" + this.name, typeof(Texture));
		player = GameObject.Find("PlayerCharacter").GetComponent<Player>();
		//npcSchedule = GetSchedule();
		currentEmotion = GetInitEmotionState();
		NPCManager.instance.Add(this.gameObject);
		scheduleStack = new ScheduleStack();
		flagReactions = new Dictionary<string, Reaction>();
	}
	
	protected override void CharacterUpdate(){
		if (chating && !NearPlayer()){
			CloseChat();
		}
		//scheduleStack.Run(Time.deltaTime);
	}
	
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
	
	private List<Choice> GetChoices(){
		return(currentEmotion.GetChoices());
	}
	
	public void ReactToChoice(string choice){
		currentEmotion.ReactToChoice(choice);	
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
		EventManager.instance.RiseOnNPCInteractionEvent(new NPCEnviromentInteraction(this.gameObject, "Task Done"));
		
		//npcSchedule.NextTask();
	}
	
	private void CloseChat(){
		
	}
	
	public void UpdateEmotionState(EmotionState newEmotionState){
		currentEmotion = newEmotionState;	
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