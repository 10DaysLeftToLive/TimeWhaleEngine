using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LinkObjectContainer<T> /*where T : LinkedObject */{
	protected Dictionary<CharacterAgeState, T> linkedObjects;
	
	public LinkObjectContainer(){
		linkedObjects = new Dictionary<CharacterAgeState, T>();
	}
	
	protected T Get(CharacterAgeState age){
		if (linkedObjects.ContainsKey(age)){
			return (linkedObjects[age]);
		} else {
			return (default(T));
		}
	}
	
	public virtual void Perform(){}
	
	public void Add(T toAdd, CharacterAgeState age){
		if (Get(age) != null){
			linkedObjects[age] = toAdd;
		} else {
			linkedObjects.Add(age, toAdd);
		}
	}
}
