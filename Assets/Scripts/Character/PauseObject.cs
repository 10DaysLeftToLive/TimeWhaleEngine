using UnityEngine;
using System.Collections;

public abstract class PauseObject : MonoBehaviour {
	private bool gamePaused = false;
	
	void Awake() {
		EventManager.instance.mOnPauseToggleEvent += new EventManager.mOnPauseToggleDelegate (TogglePauseEvent);
	}
	
	private void TogglePauseEvent(EventManager EM, PauseStateArgs e){
		gamePaused = e.isPaused;
	}
	
	protected abstract void UpdateObject();
	
	void Update () {
		if (!gamePaused){
			UpdateObject();
		}
	}
}
