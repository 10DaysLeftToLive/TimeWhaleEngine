using UnityEngine;
using System.Collections;

public class TriggerCollision : MonoBehaviour {
	// Send event that this trigger was entered by the player, then destroy this trigger
	void OnTriggerEnter(Collider other) {
        EventManager.instance.RiseOnPlayerTriggerCollisionEvent(new TriggerCollisionArgs(this.gameObject));
    }
}
