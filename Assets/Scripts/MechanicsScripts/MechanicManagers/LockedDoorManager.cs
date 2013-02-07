using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LockedDoorManager : MonoBehaviour {
	Dictionary<int, LockedDoorContainer> lockedDoors;
	
	void Awake(){
		lockedDoors = new Dictionary<int, LockedDoorContainer >();
	}
	
	public void LoadInDoors(Transform doorsRoot, CharacterAgeState age){
		Component[] components = (Component[])doorsRoot.GetComponentsInChildren(typeof(LockedDoor));
		
		foreach (LockedDoor door in components){
			if (!lockedDoors.ContainsKey(door.id)){
				lockedDoors.Add(door.id, new LockedDoorContainer());
			}
			lockedDoors[door.id].Add(door, age);
		}
	}
	
	public void UnlockeWithId(int id){
		if (lockedDoors.ContainsKey(id)){
			lockedDoors[id].UnlockAll();
		} else {
			Debug.LogWarning("LockedDoorManager does not contain a building with id " + id);
		}
	}
	
	public void Add(LockedDoor toAdd, CharacterAgeState age){		
		lockedDoors[toAdd.id].Add(toAdd, age);
	}
	
	#region Singleton
	private static LockedDoorManager ldm_instance = null;
	
	public static LockedDoorManager instance{
		get {
            if (ldm_instance == null) {
                ldm_instance = FindObjectOfType(typeof (LockedDoorManager)) as LockedDoorManager;
            }
 
            // If it is still null, create a new instance
            if (ldm_instance == null) {
                GameObject obj = new GameObject("LockedDoorManager");
                ldm_instance = obj.AddComponent(typeof (LockedDoorManager)) as LockedDoorManager;
            }
 
            return ldm_instance;
        }
	}
	#endregion
}