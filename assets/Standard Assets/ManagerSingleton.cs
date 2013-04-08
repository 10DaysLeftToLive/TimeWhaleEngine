using UnityEngine;
using System.Collections;

/*
 * Base class for managers this allows them to easily be singletons and be referenced by instance
 */

// The manager singleton will be of it's own type so that the instance can be referenced
public class ManagerSingleton<T> where T : ManagerSingleton<T>, new(){
	private static T manager_instance = null;
	
	public static T instance{
		get { 
            if (manager_instance == null) {
				manager_instance = new T();
				manager_instance.Init();
            }
 
            return manager_instance;
        }
	}
	
	// This is what will be called when this manager is first instantiated, this will take the place of Awake
	public virtual void Init(){}
}