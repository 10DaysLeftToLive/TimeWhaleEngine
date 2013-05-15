using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlagManager : ManagerSingleton<FlagManager> {
	private List<Flag> _flags;
	
	public override void Init(){
		_flags = NPCManager.instance.GetFlags();
	}
	
	public void SetFlags(List<Flag> flags){
		_flags = flags;
	}
	
	public void SetFlag(string name){
		foreach (Flag flag in _flags){
			if (flag.Equals(name)){
				flag.SetOff();	
				return;
			}
		}
		Debug.LogWarning("Flag " + name + " was not found");
	}
	
	public void UnSetFlag(string name){
		foreach (Flag flag in _flags){
			if (flag.Equals(name)){
				flag.UnSet();	
				return;
			}
		}
		Debug.LogWarning("Flag " + name + " was not found");
	}	
}
