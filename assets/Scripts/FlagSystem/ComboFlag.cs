using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComboFlag {
	private List<Flag> flagParts;
	
	public ComboFlag(List<string> parts){
		flagParts = new List<Flag>();
		foreach (string part in parts){
			flagParts.Add(new Flag(part));
		}
	}
}
