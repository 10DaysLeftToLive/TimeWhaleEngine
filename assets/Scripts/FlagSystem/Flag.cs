using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class Flag : IEquatable<Flag>, IComparable<Flag>{ // needs to implement IEquatable and IComparable to play nice with list operations 
	public string _name;
	private List<NPC> npcsThatCareAboutFlag;
	public bool _isSetOff;
	
	public Flag(string name){
		_name = name;
		_isSetOff = false;
		npcsThatCareAboutFlag = new List<NPC>();
	}
	
	public bool Equals(string name){
		return (_name == name);	
	}
	
	public void SetOff(){
		if (_isSetOff) Debug.LogWarning("Flag " + _name + " was already set off");
		DebugManager.instance.Log("Setting off " + _name, "Flag", _name);
		foreach (NPC npc in npcsThatCareAboutFlag){
			npc.ReactToFlag(_name);	
		}
		_isSetOff = true;	
	}
	
	public void UnSet(){
		DebugManager.instance.Log("Unsetting " + _name, "Flag", _name);
		_isSetOff = false;
	}
	
	public void AddNPC(NPC npc){
		npcsThatCareAboutFlag.Add(npc);
	}
	
	public static bool operator ==(Flag flag1, Flag flag2){
	    // If both are null, or both are same instance, return true.
	    if (System.Object.ReferenceEquals(flag1, flag2))
	    {
	        return true;
	    }
	
	    // If one is null, but not both, return false.
	    if (((object)flag1 == null) || ((object)flag2 == null))
	    {
	        return false;
	    }
	
	    // Return true if the fields match:
	    return flag1._name == flag2._name;
	}
	
	public static bool operator != (Flag flag1, Flag flag2)
	{
		if (flag1 == null || flag2 == null)
			return ! System.Object.Equals(flag1, flag2);
		
		return !(flag1.Equals(flag2));
	}
	
	public bool Equals(Flag other) {
		if (other == null) 
			return false;
		
		if (this._name == other._name)
			return true;
		else 
			return false;
	}

	public override bool Equals(System.Object obj)
	{
		if (obj == null) 
			return false;
		
		Flag flagObj = obj as Flag; // check if it is of type flag
		if (flagObj == null)
			return false;
		else    
			return Equals(flagObj);   
	}   
	
	public override int GetHashCode(){
		return this._name.GetHashCode();
	}
	
	public int CompareTo(Flag other) {
       if (other == null) 
			return -1;
		
		Flag flagObj = other as Flag; // check if it is of type flag
		if (flagObj == null)
			return -1;
		else    
			return _name.CompareTo(other._name);
    }
	
	public int Compare(Flag first, Flag second) {
        if (System.Object.ReferenceEquals(first, null))
            return (System.Object.ReferenceEquals(second, null) ? 0 : -1);

        return first.CompareTo(second);
    }
}
