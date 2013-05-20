using UnityEngine;
using System.Collections;

/*
 * ChatInfo.cs
 * 	Responsible for holding the data for a single part of a chat between npcs
 *  will count down a timer based on the size of the text and will return a sentry value indicating
 * 	this chat part is done
 */
public class ChatInfo {
	public NPC npcTalking;
	public string text;
	private float displayTime;
	
	public ChatInfo(NPC _npcTalking, string _text){
		npcTalking = _npcTalking;
		text = _text;
		displayTime = Utils.CalcTimeToDisplayText(text);
		
		// Make sure chats don't disapear too quick
		if (displayTime < 2) {
			displayTime = 2;
		}
	}
	
	public ChatInfo(NPC _npcTalking, string _text, float time){
		npcTalking = _npcTalking;
		text = _text;
		displayTime = time;
	}
	
	public bool DecrementTime(float deltaTime){
		displayTime -= deltaTime;
		return (displayTime <= 0);	
	}
	
	public float GetTime(){
		return (displayTime);	
	}
}
