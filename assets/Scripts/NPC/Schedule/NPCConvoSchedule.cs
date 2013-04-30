using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class NPCConvoSchedule : Schedule {
	protected NPC _npcTwo;
	protected Queue<NPC> _npcTalkingQueue; // Keeps track of who is talking
	protected NPC _npcTalking;
	protected bool isFirstPass = true; // Used to only decrement time once per update (each npc will call update on this shared schedule)
	protected Task currentListening;
	protected List<ChatInfo> chatInfoList;
	protected ChatInfo chatInfo;
	protected NPCChat chatToPerform;
	protected NPC npcNotTalking;
	protected NPC npcTalking;
	
	public NPCConvoSchedule(NPC npcOne, NPC npcTwo, NPCConversation conversation) : base(npcOne) {
		SetConvoTasks(conversation);
	}
	
	public NPCConvoSchedule(NPC npcOne, NPC npcTwo, NPCConversation conversation, Enum priority) : base(npcOne, priority) {
		SetConvoTasks(conversation);
	}
	
	protected void SetConvoTasks(NPCConversation convo) {
		foreach (Dialogue textToSay in convo.dialogueList) {
			if (textToSay._npc == 1) {
				npcNotTalking = _npcTwo;
				npcTalking = _toManage;
			} else {
				npcNotTalking = _toManage;
				npcTalking = _npcTwo;
			}
			
			chatInfo = new ChatInfo(npcTalking, textToSay._TextToSay);
			chatInfoList = new List<ChatInfo>();
			chatInfoList.Add(chatInfo);
			chatToPerform = new NPCChat(chatInfoList);
			float timeToChat = chatToPerform.AddUpTimeToChat();
			
			Add (new TimeTask(timeToChat, new NPCConvoState((Character)npcTalking, (Character)npcNotTalking, chatToPerform)));
			
			_npcTalkingQueue.Enqueue(npcTalking);
		}
	}
	
	public override void Run(float timeSinceLastTick){
		if (HasTask()){
			if (isFirstPass) {
				current.Decrement(timeSinceLastTick);
				currentListening.Decrement(timeSinceLastTick);
				if (current.IsComplete()){
					Debug.Log("Completed task");
					current = null;
					currentListening = null;
				}
				isFirstPass = false;
			} else {
				isFirstPass = true;
			}
		} else {
			NextTask();
		}
	}
	
	// Schedule sets what it manages to the correct state the schedule is in
	public override void Resume() {
		if (!HasTask()) {
			NextTask();
		}
		
		_toManage.ForceChangeToState(current.StatePerforming);
		_npcTwo.ForceChangeToState(currentListening.StatePerforming);
	}
	
	public override void NextTask(){
		Debug.Log(_toManage + " schedule next task");
		
		if (_tasksToDo.Count > 0) {
			
			current = _tasksToDo.Dequeue();
			Debug.Log("There are " + _tasksToDo.Count + " tasks");
			npcTalking = _npcTalkingQueue.Dequeue();
			
			if (npcTalking == _toManage) {
				Debug.Log(_toManage.name + " is now switching to " + current.StatePerforming);
				_toManage.ForceChangeToState(current.StatePerforming);
				currentListening = new Task(new IdleState(_npcTwo));
				Debug.Log(_npcTwo.name + " is now switching to " + currentListening.StatePerforming);
				_npcTwo.ForceChangeToState(currentListening.StatePerforming);
			} else {
				Debug.Log(_npcTwo.name + " is now switching to " + current.StatePerforming);
				_npcTwo.ForceChangeToState(current.StatePerforming);
				currentListening = new Task(new IdleState(_toManage));
				Debug.Log(_toManage.name + " is now switching to " + currentListening.StatePerforming);
				_toManage.ForceChangeToState(currentListening.StatePerforming);
			}
		}
	}
}
