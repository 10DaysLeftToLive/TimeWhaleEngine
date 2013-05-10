using UnityEngine;
using System.Collections;

public class OneDayClock : PauseObject {
	
	public OneDayClock Instance = null;
	public float timeSinceGameStarted { get; private set; }
	public float timeSinceLastPaused { get; private set; }
	public float time { 
		get {
			return Time.time - lostTime;
		}
	}
	private float lostTime = 0;
	
	
	// Use this for initialization
	void Start () {
		if (Instance != null) {
			Instance = new OneDayClock();
		}
		timeSinceGameStarted = timeSinceLastPaused = Time.time;
	}
	
	protected override void UpdateObject() {
		
	}
	
	protected override void OnPause() {
		timeSinceLastPaused = Time.time;
	}
	
	protected virtual void OnResume() {
		lostTime += (Time.time - timeSinceLastPaused);
		timeSinceLastPaused = Time.time - (Time.time - timeSinceLastPaused);
	}
	
}
