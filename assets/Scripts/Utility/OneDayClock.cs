using UnityEngine;
using System.Collections;

public class OneDayClock : PauseObject {
	
	public OneDayClock Instance = null;
	public float timeSinceGameStarted { get; private set; }
	public float timeSinceLastPaused { get; private set; }
	public float time { 
		get {
			return GamePlayTime();
		}
	}
	private float lostTime = 0;
	private float timeInHour;
	private float timeOfDay;
	private float minutes;
	private float hours;
	
	public static float GAME_LENGTH = 600f; // seconds
	public static float START_TIME = 800; // day starts at 8 AM, 0800 military
	public static float HOURS_IN_DAY = 12;
	public static float MILITARY_TIME_MULTIPLIER = 100;
	
	// Use this for initialization
	void Start () {
		if (Instance != null) {
			Instance = new OneDayClock();
		}
		timeSinceGameStarted = timeSinceLastPaused = Time.time;
		timeInHour = GAME_LENGTH/HOURS_IN_DAY;
	}
	
	protected override void UpdateObject() {
		TimeReactionManager.instance.Update(GetGameDayTime());
	}
	
	protected override void OnPause() {
		timeSinceLastPaused = Time.time;
	}
	
	protected virtual void OnResume() {
		lostTime += (Time.time - timeSinceLastPaused);
		timeSinceLastPaused = Time.time - (Time.time - timeSinceLastPaused);
	}
	
	private float GamePlayTime() {
		return Time.time - lostTime;
	}
	
	/// <summary>
	/// Returns military time starting at 0800
	/// </summary>
	public int GetGameDayTime() {
		timeOfDay = GamePlayTime()/timeInHour;
		minutes = (timeOfDay - Mathf.Floor(timeOfDay))*60;
		hours = ((Mathf.Floor(timeOfDay)*MILITARY_TIME_MULTIPLIER) + START_TIME);
		return ((int)(hours + minutes));
	}
	
	public bool IsGameDone() {
		if (GAME_LENGTH < GamePlayTime()) {
			return true;
		}
		return false;
	}
}
