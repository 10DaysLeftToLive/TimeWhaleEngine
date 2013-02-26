using UnityEngine;
using System.Collections;

public class LockedDoor : LinkedObject {
	public GameObject[] keysThatUnlock;
	
	void Start(){
		LockedDoorOnClick newOnClick = this.gameObject.AddComponent<LockedDoorOnClick>();
		newOnClick.door = this;
		newOnClick.FindPlayer();
	}
	
	public void Unlock(){
		transform.renderer.enabled = false;
		transform.collider.enabled = false;
	}
}

