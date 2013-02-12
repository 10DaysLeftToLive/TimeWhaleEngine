using UnityEngine;
using System.Collections;

/*
 * LinkedObject.cs
 * This is the base of every object that will link to objects in another age.
 * Every type of object will need to have a specific id that should match the id's of the objects in
 * other ages that you want linked.
 */
public class LinkedObject : MonoBehaviour {
	private int _id = -1;
	
	public int id{
		get {
			return (_id);
		}
	}
	
	private void OnAwake(){
		if (_id == -1){
			Debug.LogWarning(gameObject.name + " : Has no id set");
		}
	}
}
