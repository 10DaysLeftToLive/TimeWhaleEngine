using UnityEngine;
using System.Collections;

/*
 * Base class for managers this allows them to easily be singletons and be referenced by instance
 */

// The manager singleton will be of it's own type so that the instance can be referenced
public abstract class ManagerSingleton<T> : MonoBehaviour  where T : ManagerSingleton<T>{
	private static T manager_instance = null;
	
	public static T instance{
		get {
            if (manager_instance == null) {
                manager_instance = FindObjectOfType(typeof (T)) as T;
            }
 
            // If it is still null after searching, create a new instance
            if (manager_instance == null) {
                GameObject newManger = new GameObject(typeof(T).ToString());
                manager_instance = newManger.AddComponent(typeof (T)) as T;
				manager_instance.Init();
            }
 
            return manager_instance;
        }
	}
	
	// Manually initialize the manager if no other earlier Awake function called for the instance
	private void Awake(){
		if (manager_instance == null){
			manager_instance = this as T;
			manager_instance.Init();
		}
	}
	
	// This is what will be called when this manager is first instantiated, this will take the place of Awake
	public virtual void Init(){}
	
	private void OnApplicationQuit(){
		manager_instance = null;
	}
}
