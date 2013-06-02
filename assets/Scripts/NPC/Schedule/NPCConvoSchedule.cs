using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class NPCConvoSchedule : Schedule {
	public NPC _npcTwo;
	protected NPC _npcTalking;
	protected bool _waitForPlayer = true;
	protected List<ChatInfo> chatInfoList;
	protected ChatInfo chatInfo;
	protected NPCChat chatToPerform;
	protected Task currentTwo;
	protected Queue<ConvoTask> convoTasksToDo;
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
		convoTasksToDo = new Queue<ConvoTask>();
		_npcTwo = npcTwo;
		SetConvoTasks(conversation);
	}
	
	public void SetCanNotInteractWithPlayer() {
		canInteractWithPlayer = false;
	}
	
	public void Add(ConvoTask task){
		convoTasksToDo.Enqueue(task);
	}
	
	protected void SetConvoTasks(NPCConversation convo) {
		_tasksToDo.Clear();
		CheckWithinDistanceOfEachOther();
		CheckWaitForPlayer();
		
		foreach (Dialogue textToSay in convo.dialogueList) {
			if (textToSay._npc == 1) {
				chatInfo = new ChatInfo(_toManage, textToSay._TextToSay);
				chatInfoList = new List<ChatInfo>();
				chatInfoList.Add(chatInfo);
				chatToPerform = new NPCChat(chatInfoList);
				Add(new ConvoTask(new Task(new NPCConvoState((Character)_toManage, (Character)_npcTwo, chatToPerform)), 
					new Task(new IdleState(_npcTwo))));
			} else {
				chatInfo = new ChatInfo(_npcTwo, textToSay._TextToSay);
				chatInfoList = new List<ChatInfo>();
				chatInfoList.Add(chatInfo);
				chatToPerform = new NPCChat(chatInfoList);
				Add(new ConvoTask(new Task(new IdleState(_toManage)), 
					new Task(new NPCConvoState((Character)_npcTwo, (Character)_toManage, chatToPerform))));
			}
		}
	}
	
	public override void Run(float timeSinceLastTick){
		if (current != null && currentTwo != null){
			if (current.IsComplete() || currentTwo.IsComplete()){
				current = null;
				currentTwo = null;
			}
		} else {
			NextTask();
		}
	}
	
	// Schedule sets what it manages to the correct state the schedule is in
	public override void Resume() {
		_npcTwo.AddSharedSchedule(this);
		if (current == null || currentTwo == null) {
			NextTask();
		}
		
		_toManage.ForceChangeToState(current.StatePerforming);
		_npcTwo.ForceChangeToState(currentTwo.StatePerforming);
	}
	
	public override void NextTask(){	
		DebugManager.instance.Log(_toManage.name + " schedule next task", "Schedule", _toManage.name);
		if (convoTasksToDo.Count > 0) {
			current = convoTasksToDo.Peek().taskOne;
			currentTwo = convoTasksToDo.Dequeue().taskTwo;
			DebugManager.instance.Log(_toManage.name + " is now switching to " + current.StatePerforming, "Schedule", _toManage.name);
			DebugManager.instance.Log(_npcTwo.name + " is now switching to " + currentTwo.StatePerforming, "Schedule", _npcTwo.name);
			_toManage.ForceChangeToState(current.StatePerforming);
			_npcTwo.ForceChangeToState(currentTwo.StatePerforming);
		}
	}
	
	private void CheckWithinDistanceOfEachOther() {
		if (!Utils.InDistance(_toManage.gameObject, _npcTwo.gameObject, TALK_DISTANCE)) {
			Add(new ConvoTask(new Task(new MoveToObjectState(_toManage, _npcTwo.gameObject)), 
				new Task(new IdleState(_npcTwo)))); // move to talk positions
		}
	}
	
	private void CheckWaitForPlayer() {
		if (_waitForPlayer) {
			Add(new ConvoTask(new Task(new WaitTillPlayerCloseState(_toManage, _toManage.player, DISTANCE_CLOSE_TO_PLAYER)), 
				new Task(new WaitTillPlayerCloseState(_npcTwo, _npcTwo.player, DISTANCE_CLOSE_TO_PLAYER))));
		}
	}
	
	// Schedule is complete if the current task is complete and there are no other tasks to complete
	public override bool IsComplete(){
		if (current == null || currentTwo == null) {
			if (convoTasksToDo.Count == 0) {
				return true;
			}
		}
		return false;
	}
	
	public override void OnInterrupt() {
		SetComplete();
	}
	
	public class ConvoTask {
		public Task taskOne;
		public Task taskTwo;
		
		public ConvoTask(Task one, Task two) {
			taskOne = one;
			taskTwo = two;
		}
	}
}
