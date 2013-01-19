using UnityEngine;
using System.Collections;

public class PauseObject : MonoBehaviour {
	private bool gamePaused = false;
	
	void Awake() {
		EventManager.instance.mOnPauseToggleEvent += new EventManager.mOnPauseToggleDelegate (TogglePauseEvent);
	}
	
	private void TogglePauseEvent(EventManager EM, PauseStateArg e){
		gamePaused = e.IsPaused;
	}
	
	protected virtual void UpdateObject(){}
	
	void Update () {
		if (!gamePaused){
			UpdateObject();
		}
	}
}
