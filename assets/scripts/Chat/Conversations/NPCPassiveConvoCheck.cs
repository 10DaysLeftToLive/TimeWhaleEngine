using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCPassiveConvoCheck : MonoBehaviour {
	private static float TIME_INBETWEEN_PASSIVE_CHATS = 17;
	private static int CHANCE_TO_PASSIVE_CHAT = 3;
	private static int DISTANCE_TO_SEE = 8;
	private static int DISTANCE_TO_CHAT = 4;
	private Dictionary<string, NPC> npcDict;
	private Player player;
	private Action sayHi;
	private NPC npcToChatWith;
	private float currentTime;
	private float previousTime;
	private float timeToDecrement;
	private static float UPDATE_TIME_DELAY = .5f; // Delay the running of the passive chat
	private static float currentDelay = 0;
	
	void Start() {
		npcDict = NPCManager.instance.getNPCDictionary();
		player = GameObject.Find("PlayerCharacter").GetComponent<Player>();
		currentTime = previousTime = Time.timeSinceLevelLoad;
	}
	
	void Update() {
		if (currentDelay > UPDATE_TIME_DELAY){
			currentDelay = 0;
			TryToPassiveChat();
		} else {
			currentDelay += Time.deltaTime;
			return;
		}
	}
	
	public void TryToPassiveChat(){
		foreach (NPC npc in npcDict.Values) {
			DecrementPassiveChatTimer(npc);
			if (npc.CanPassiveChat() && npc.timeTillPassiveChatAgain <= 0 && InSight(npc.gameObject, player.gameObject)) {
				SetPassiveChatTimer(npc);
				if (Random.Range(1, CHANCE_TO_PASSIVE_CHAT) > 1) {
					if (InPassiveChatDistance(npc.gameObject, player.gameObject)) {
						sayHi = new ShowOneOffChatAction(npc, PassiveChatToPlayer.instance.GetTextToSay(npc));
						sayHi.Perform();
					} else {
						foreach (NPC npcToCheck in npcDict.Values) {
							if (npc != npcToCheck && InPassiveChatDistance(npc.gameObject, npcToCheck.gameObject) && RequestChat(npcToCheck)) {
								npc.AddSchedule(new NPCConvoSchedule(npc, npcToCheck, NPCPassiveConvoDictionary.instance.GetConversation(npc)));
								break;
							}
						}
					}
				}
			}
		}
	}
	
	private void SetPassiveChatTimer(NPC npc) {
		npc.timeTillPassiveChatAgain = TIME_INBETWEEN_PASSIVE_CHATS;
		currentTime = previousTime = Time.timeSinceLevelLoad;
	}
	
	private void DecrementPassiveChatTimer(NPC npc) {
		currentTime = Time.timeSinceLevelLoad;
		timeToDecrement = Mathf.Abs(currentTime - previousTime);
		npc.timeTillPassiveChatAgain -= timeToDecrement;
		previousTime = currentTime;
	}
	
	public bool RequestChat(NPC npcToRequest) {
		return (npcToRequest.CanPassiveChat());
	}
	
	private bool InPassiveChatDistance(GameObject gameObjOne, GameObject gameObjTwo) {
		return Utils.InDistance(gameObjOne, gameObjTwo, DISTANCE_TO_CHAT);
	}
	
	private bool InSight(GameObject gameObjOne, GameObject gameObjTwo) {
		return (Utils.InDistance(gameObjOne, gameObjTwo, DISTANCE_TO_SEE)); 
	}
}
