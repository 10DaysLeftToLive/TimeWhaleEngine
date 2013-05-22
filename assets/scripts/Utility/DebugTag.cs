using UnityEngine;
using System.Collections;

[System.Serializable]
public class DebugTag {
	public string tag;
	public bool display = false;
	
	public DebugTag(string label){
		tag	= label;
	}
}
