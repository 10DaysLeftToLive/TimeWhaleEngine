using UnityEngine;
using System.Collections;

public class PauseObject : MonoBehaviour {
	private bool gamePaused = false;
	
	void Awake() {
		EventManager.instance.mOnPauseToggleEvent += new EventManager.mOnPauseToggleDelegate (TogglePauseEvent);
	}
	
	private void TogglePauseEvent(EventManager EM, PauseStateArgs e){
		gamePaused = e.isPaused;
	}
	
	protected virtual void UpdateObject(){}
	
	void Update () {
		if (!gamePaused){
			UpdateObject();
		}
	}
}
