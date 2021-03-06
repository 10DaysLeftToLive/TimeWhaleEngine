using UnityEngine;
using System.Collections;

/*
 * PauseObject.cs
 * 		This object will listen for the pause event and will stop updating when paused
 * 		If you want your object to pause then make it a child of this 
 */
public abstract class PauseObject : MonoBehaviour {
	private bool gamePaused = false;
	
	protected bool isGamePaused() {
		return gamePaused;
	}
	
	protected virtual void Awake() {
		EventManager.instance.mOnPauseToggleEvent += new EventManager.mOnPauseToggleDelegate (TogglePauseEvent);
	}
	
	private void TogglePauseEvent(EventManager EM, PauseStateArgs pauseState){
		gamePaused = pauseState.isPaused;
		if (gamePaused){
			OnPause();	
		} else {
			OnResume();	
		}
	}
	
	// To be implemented by child
	protected virtual void UpdateObject(){}
	protected virtual void OnPause(){}
	protected virtual void OnResume(){}
	
	void Update () {
		if (!gamePaused){
			UpdateObject();
		}
	}
	
	public void OnDestroy(){
		EventManager.instance.mOnPauseToggleEvent -= TogglePauseEvent;
	}
}
