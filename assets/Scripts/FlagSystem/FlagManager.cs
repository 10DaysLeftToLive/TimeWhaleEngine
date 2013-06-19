using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlagManager : MonoBehaviour {
	public List<Flag> _flags;
	
	private static FlagManager manager_instance = null;
    
    public static FlagManager instance{
        get { 
           if (manager_instance == null) {
                manager_instance = FindObjectOfType(typeof (FlagManager)) as FlagManager;
            }
 
            // If it is still null, create a new instance
            if (manager_instance == null) {
                GameObject obj = new GameObject("FlagManager");
                manager_instance = obj.AddComponent(typeof (FlagManager)) as FlagManager;
				DontDestroyOnLoad(obj);
            }

            return manager_instance;
        }
    }
	
	public void Init(){
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
		DebugManager.instance.Log("Flag " + name + " was not found", "Flag", "Warning");
	}
	
	public bool FlagIsSet(string flagName){
		if (_flags == null) return (false);
		foreach (Flag flag in _flags){
			if (flag.Equals(flagName)){
				return (flag._isSetOff);
			}
		}
		return (false);
	}
	
	public void UnSetFlag(string name){
		foreach (Flag flag in _flags){
			if (flag.Equals(name)){
				flag.UnSet();	
				return;
			}
		}
		DebugManager.instance.Log("Flag " + name + " was not found", "Flag", "Warning");
	}	
}
