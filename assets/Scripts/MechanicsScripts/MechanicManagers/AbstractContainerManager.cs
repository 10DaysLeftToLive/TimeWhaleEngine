using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* 
 * AbstractContainerManager.cs
 * 	Abstract base class for all container managers, it's generics are a linked object to hold, the container of that linked object, and a reference to this class for singelton
 * 	
 */
public class AbstractContainerManager<LinkedObj, Manager, Container> : ManagerSingleton<Manager> where LinkedObj :LinkedObject 
																							     where Manager : ManagerSingleton<Manager>, new() 
																								 where Container : LinkObjectContainer<LinkedObj>, new(){
	static protected Dictionary<int, Container > containersInLevel;
	
	public override void Init(){
		containersInLevel = new Dictionary<int, Container >();
	}
	
	public void Add(LinkedObj objectToAdd, CharacterAgeState ageToAdd){
		containersInLevel[objectToAdd.id].Add(objectToAdd, ageToAdd);
	}
	
	// Load in all objects that this manager should handle from the given age root
	public void LoadInObjectsToManage(Transform rootOfAge, CharacterAgeState ageRootIn){
		Component[] componentsToManage = (Component[])rootOfAge.GetComponentsInChildren(typeof(LinkedObj));
		
		foreach (LinkedObj objectToManage in componentsToManage){
			if (!containersInLevel.ContainsKey(objectToManage.id)){
				containersInLevel.Add(objectToManage.id, new Container());
			}
			Add (objectToManage, ageRootIn);
		}
	}
}
