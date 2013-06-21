using UnityEngine;
using System.Collections;

/*
 * Task.cs
 *  A basic action of what an npc should do until interupted 
 *  if you want it to go one for a specified amount of time use TimeTask
 */
[System.Serializable]
public class Task {
	protected State _stateToPerform;
	protected bool _taskFinished = false;
	protected string _passiveTextToSay;
	protected NPC _toManage;
	float _timeTillPassiveChat = 0;
	protected bool hasPassiveChat = false;
	private string flagToSet = null;
	
	public State StatePerforming {
		get {return _stateToPerform;}
	}
	
	public Task(State stateToPerform){
		_stateToPerform = stateToPerform;
	}
	
	public Task(State stateToPerform, NPC toManage, float timeTillPassiveChat, string passiveTextToSay) {
		_stateToPerform = stateToPerform;
		_passiveTextToSay = passiveTextToSay;
		_toManage = toManage;
		_timeTillPassiveChat = timeTillPassiveChat;
		hasPassiveChat = true;
	}
	
	public void AddFlagToSet(string flag){
		flagToSet = flag;
	}
	
	public void Finish(){
		if (flagToSet != null){
			FlagManager.instance.SetFlag(flagToSet);
		}
	}
	
	public virtual void Decrement(float amount){
		// timeleft = infinity - amount aka do nothing
		if (hasPassiveChat) {
			_timeTillPassiveChat -= amount;
			if (_timeTillPassiveChat <= 0) {
				new ShowOneOffChatAction(_toManage, _passiveTextToSay).Perform();
				hasPassiveChat = false;
			}
		}
	}
	
	public virtual bool IsComplete(){
		return (_taskFinished); // this task will go on forever
	}
}
