using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class LinkObjectContainer<T> {
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
	
	public void Add(T toAdd, CharacterAgeState age){
		if (Get(age) != null){
			linkedObjects[age] = toAdd;
		} else {
			linkedObjects.Add(age, toAdd);
		}
	}
	/*
	protected void ExecuteOnAll(){
		foreach(CharacterAgeState state in linkedObjects.Keys){
			Perform(Get (state));
		}
	}
	
	protected virtual void Perform(T toPerformOn){}*/
}
