using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LinkObject<T> {
	protected Dictionary<CharacterAgeState, T> linkedObjects;
	
	public LinkObject(){
		linkedObjects = new Dictionary<CharacterAgeState, T>();
	}
	
	protected T Get(CharacterAgeState age){
		if (linkedObjects.ContainsKey(age)){
			return (linkedObjects[age]);
		} else {
			return (default(T));
		}
	}
	
	public void Add(T toAdd, CharacterAgeState age){
		linkedObjects.Add(age, toAdd);
	}
}
