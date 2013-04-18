using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlagManager : ManagerSingleton<FlagManager> {
	private List<Flag> flags;
	
	public override void Init(){
		flags = new List<Flag>();
		flags.Add(new Flag("Flag 1"));
		flags.Add(new Flag("Flag 2"));
		flags.Add(new Flag("Flag 3"));
		flags.Add(new Flag("Flag 4"));
	}
	
	public void SetFlag(string name){
		foreach (Flag flag in flags	){
			if (flag.Equals(name)){
				flag.SetOff();	
			}
		}
	}
	
	public void UnSetFlag(string name){
		foreach (Flag flag in flags	){
			if (flag.Equals(name)){
				flag.UnSet();	
			}
		}
	}	
}
