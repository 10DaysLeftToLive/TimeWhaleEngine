using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* 
 * AbstractContainerManager.cs
 * 	Abstract base class for all container managers, it is of generic T where T 
 * 	is the type of base object that this will manage and is a child of LinkedObject
 */
public abstract class AbstractContainerManager<T> : ManagerSingleton<AbstractContainerManager<T> > where T : LinkedObject{
	Dictionary<int, LinkObjectContainer<T> > containersInLevel;
	
	public override void Init(){
		containersInLevel = new Dictionary<int, LinkObjectContainer<T> >();
	}
	
	public void Add(T objectToAdd, CharacterAgeState ageToAdd){
		containersInLevel[objectToAdd.id].Add(objectToAdd, ageToAdd);
	}
	
	// Load in all objects that this manager should handle from the given age root
	public void LoadInObjectsToManage(Transform rootOfAge, CharacterAgeState ageRootIn){
		Component[] componentsToManage = (Component[])rootOfAge.GetComponentsInChildren(typeof(T));
		
		foreach (T objectToManage in componentsToManage){
			if (!containersInLevel.ContainsKey(objectToManage.id)){
				containersInLevel.Add(objectToManage.id, new LinkObjectContainer<T>());
			}
			Add (objectToManage, ageRootIn);
		}
	}
}
