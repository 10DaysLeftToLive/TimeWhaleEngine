using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlagManager : ManagerSingleton<FlagManager> {
	private List<Flag> _flags;
	
	public override void Init(){
		_flags = NPCManager.instance.GetFlags();
		
		/*_flags.Add(new Flag("Flag 1"));
		_flags.Add(new Flag("Flag 2"));
		_flags.Add(new Flag("Flag 3"));
		_flags.Add(new Flag("Flag 4"));*/
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
		Debug.LogError("flag " + name + " was not found");
	}
	
	public void UnSetFlag(string name){
		foreach (Flag flag in _flags){
			if (flag.Equals(name)){
				flag.UnSet();	
				return;
			}
		}
		Debug.LogError("flag " + name + " was not found");
	}	
}
