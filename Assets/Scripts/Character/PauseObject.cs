using UnityEngine;
using System.Collections;

/*
 * PauseObject.cs
 * 		This object will listen for the pause event and will stop updating when paused
 * 		If you want your object to pause then make it a child of this 
 */
public abstract class PauseObject : MonoBehaviour {
	private bool gamePaused = false;
	
	void Awake() {
		EventManager.instance.mOnPauseToggleEvent += new EventManager.mOnPauseToggleDelegate (TogglePauseEvent);
	}
	
	private void TogglePauseEvent(EventManager EM, PauseStateArgs pauseState){
		gamePaused = pauseState.isPaused;
	}
	
	// To be implemented by child
	protected abstract void UpdateObject();
	
	void Update () {
		if (!gamePaused){
			UpdateObject();
		}
	}
}
