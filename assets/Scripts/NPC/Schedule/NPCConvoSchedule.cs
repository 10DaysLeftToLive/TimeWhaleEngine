using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class NPCConvoSchedule : Schedule {
	public NPC _npcTwo;
	protected NPC _npcTalking;
	protected bool _waitForPlayer = true;
	public bool isWaitingForPlayer = false;
	public bool isMovingToNPC = false;
	protected List<ChatInfo> chatInfoList;
	protected ChatInfo chatInfo;
	protected NPCChat chatToPerform;
	public ToTalkWithSchedule toTalkWithSchedule;
	private static float DISTANCE_CLOSE_TO_PLAYER = 6f;
	private static float TALK_DISTANCE = 2f;
	
	public NPCConvoSchedule(NPC npcOne, NPC npcTwo, NPCConversation conversation) : base(npcOne) {
		Init(npcOne, npcTwo, conversation, Schedule.priorityEnum.Low);
	}
	
	public NPCConvoSchedule(NPC npcOne, NPC npcTwo, NPCConversation conversation, bool dontWaitForPlayer) : base(npcOne) {
		Init(npcOne, npcTwo, conversation, Schedule.priorityEnum.Low);
		_waitForPlayer = (!dontWaitForPlayer);
		
	}
	
	public NPCConvoSchedule(NPC npcOne, NPC npcTwo, NPCConversation conversation, Enum priority) : base(npcOne, priority) {
		Init(npcOne, npcTwo, conversation, priority);
	}
	
	public NPCConvoSchedule(NPC npcOne, NPC npcTwo, NPCConversation conversation, Enum priority, bool dontWaitForPlayer) : base(npcOne, priority) {
		Init(npcOne, npcTwo, conversation, priority);
		_waitForPlayer = (!dontWaitForPlayer);
	}
	
	protected void Init(NPC npcOne, NPC npcTwo, NPCConversation conversation, Enum priority) {
		isWaitingForPlayer = _waitForPlayer;
		_npcTwo = npcTwo;
		toTalkWithSchedule = new ToTalkWithSchedule(_npcTwo, priority, this);
		SetConvoTasks(conversation);
	}
	
	public void SetCanNotInteractWithPlayer() {
		canInteractWithPlayer = false;
		toTalkWithSchedule.SetCanNotInteractWithPlayer();
	}
	
	protected void SetConvoTasks(NPCConversation convo) {
		isMovingToNPC = CheckWithinDistanceOfEachOther();
		CheckWaitForPlayer();
		
		foreach (Dialogue textToSay in convo.dialogueList) {
			if (textToSay._npc == 1) {
				chatInfo = new ChatInfo(_toManage, textToSay._TextToSay);
				chatInfoList = new List<ChatInfo>();
				chatInfoList.Add(chatInfo);
				chatToPerform = new NPCChat(chatInfoList);
				Add (new TimeTask(chatInfo.GetTime(), new NPCConvoState((Character)_toManage, (Character)_npcTwo, chatToPerform)));
				toTalkWithSchedule.Add(new Task(new IdleState(_npcTwo)));
			} else {
				chatInfo = new ChatInfo(_npcTwo, textToSay._TextToSay);
				chatInfoList = new List<ChatInfo>();
				chatInfoList.Add(chatInfo);
				chatToPerform = new NPCChat(chatInfoList);
				toTalkWithSchedule.Add (new Task(new NPCConvoState((Character)_npcTwo, (Character)_toManage, chatToPerform)));
				Add(new TimeTask(chatInfo.GetTime(), new IdleState(_toManage)));
			}
		}
	}
	
	public override void Run(float timeSinceLastTick){
		if (HasTask()){
			current.Decrement(timeSinceLastTick);
			if (current.IsComplete()){
				current.Finish();
				current = null;
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
	}
	
	public override void NextTask(){	
		if (isMovingToNPC) {
			isMovingToNPC = false;
		} else if (!isMovingToNPC && isWaitingForPlayer) {
			isWaitingForPlayer = false;
		}
		
		if (current != null){ // if we are going to skip the current task but it has not finished
			current.Finish();
		}
		
		if (_tasksToDo.Count > 0) {
			current = _tasksToDo.Dequeue();
			DebugManager.instance.Log(_toManage.name + " is now switching to " + current.StatePerforming, "Schedule", _toManage.name);
			_toManage.ForceChangeToState(current.StatePerforming);
		}
		
		toTalkWithSchedule.NextTask();
	}
	
	private bool CheckWithinDistanceOfEachOther() {
		if (!Utils.InDistance(_toManage.gameObject, _npcTwo.gameObject, TALK_DISTANCE)) {
			Add(new Task(new MoveToObjectState(_toManage, _npcTwo.gameObject))); // move to talk positions
			toTalkWithSchedule.WaitForNPC();
			return false;
		}
		return true;
	}
	
	private void CheckWaitForPlayer() {
		if (_waitForPlayer) {
			Add(new Task(new WaitTillPlayerCloseState(_toManage, _toManage.player, DISTANCE_CLOSE_TO_PLAYER)));
			toTalkWithSchedule.WaitForPlayer();
		}
	}
	
	public void DoneWaitingForPlayer() {
		if (current != null){ // if we are going to skip the current task but it has not finished
			current.Finish();
		}
		
		if (_tasksToDo.Count > 0) {
			current = _tasksToDo.Dequeue();
			DebugManager.instance.Log(_toManage.name + " is now switching to " + current.StatePerforming, "Schedule", _toManage.name);
			_toManage.ForceChangeToState(current.StatePerforming);
		}
	}
	
	public override void OnInterrupt() {
		SetComplete();
		toTalkWithSchedule.SetComplete();
	}
	
	public void AddOtherNPCSchedule() {
		_npcTwo.AddSchedule(toTalkWithSchedule);
	}
	
	#region Other Npc's special schedule
	public class ToTalkWithSchedule : Schedule {
		private NPCConvoSchedule _convoSchedule;
		public bool isWaitingForNPC = false;
		public bool isWaitingForPlayer = false;
		
		public ToTalkWithSchedule(NPC npc, Enum priority, NPCConvoSchedule convoSchedule) : base(npc, priority) {
			_convoSchedule = convoSchedule;
		}
		
		public void SetCanNotInteractWithPlayer() {
			canInteractWithPlayer = false;
		}
		
		public void WaitForNPC() {
			isWaitingForNPC = true;
			Add(new Task(new IdleState(_toManage)));
		}
		
		public void WaitForPlayer() {
			Add(new Task(new WaitTillPlayerCloseState(_toManage, _toManage.player, DISTANCE_CLOSE_TO_PLAYER)));
			isWaitingForPlayer = true;
		}
		
		public void DoneWaitingForPlayer() {
			isWaitingForPlayer = false;
			NextTask();
		}
		
		public override void Run(float timeSinceLastTick){
			if (HasTask()){
				current.Decrement(timeSinceLastTick);
				if (current.IsComplete()){
					current.Finish();
					current = null;
					if (isWaitingForPlayer) {
						isWaitingForPlayer = false;
						_convoSchedule.DoneWaitingForPlayer();
					}
				}
			} 
		}
		
		public override void NextTask(){
			DebugManager.instance.Log(_toManage.name + " schedule next task", "Schedule", _toManage.name);
			
			if (isWaitingForNPC && !_convoSchedule.isMovingToNPC) {
				GetNextTask();
				isWaitingForNPC = false;
			} else if (!isWaitingForNPC && isWaitingForPlayer && !_convoSchedule.isWaitingForPlayer) {
				GetNextTask();
				isWaitingForPlayer = false;
			} else if (!isWaitingForNPC && isWaitingForPlayer && _convoSchedule.isWaitingForPlayer) {
				GetNextTask();
				isWaitingForPlayer = false;
				_convoSchedule.DoneWaitingForPlayer();
			} else if (!isWaitingForNPC && !isWaitingForPlayer) {
				GetNextTask();
			}
		}
		
		private void GetNextTask() {
			if (current != null){ // if we are going to skip the current task but it has not finished
				current.Finish();
			}
			
			if (_tasksToDo.Count > 0) {
				current = _tasksToDo.Dequeue();
				DebugManager.instance.Log(_toManage.name + " is now switching to " + current.StatePerforming, "Schedule", _toManage.name);
				_toManage.ForceChangeToState(current.StatePerforming);
			}
		}
		
		public override void OnInterrupt() {
			SetComplete();
			_convoSchedule.SetComplete();
		}
	}
	#endregion
}
