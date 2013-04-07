using UnityEngine;
using System.Collections;

public class TriggerOnceCollision : MonoBehaviour {
	// Send event that this trigger was entered by the player, then destroy this trigger
	void OnTriggerEnter(Collider other) {
        EventManager.instance.RiseOnPlayerTriggerCollisionEvent(new TriggerCollisionArgs(this.gameObject));
		
		// TODO - Check if destroying the object happens after the trigger envents use it
		Destroy(this.gameObject);
    }
}
