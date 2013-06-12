using UnityEngine;
using System.Collections;

public class OneDayClock : PauseObject {
	
	public float timeSinceGameStarted { get; private set; }
	public float timeSinceLastPaused { get; private set; }
	public float time { 
		get {
			return GamePlayTime();
		}
	}
	private static OneDayClock managerInstance;
	private float lostTime = 0;
	private float timeInHour;
	private float timeOfDay;
	private float minutes;
	private float hours;
	
	public static float GAME_LENGTH = 600f; // seconds
	public static float START_TIME = 800; // day starts at 8 AM, 0800 military
	public static float HOURS_IN_DAY = 12;
	public static float MIDDAY = START_TIME + GAME_LENGTH / 2;
	public static float MILITARY_TIME_MULTIPLIER = 100;
	
	// Use this for initialization
	
	 public static OneDayClock Instance{
        get { 
           if (managerInstance == null) {
                managerInstance = FindObjectOfType(typeof (OneDayClock)) as OneDayClock;
            }
 
            // If it is still null, create a new instance
            if (managerInstance == null) {
                GameObject obj = new GameObject("OneDayClock");
                managerInstance = obj.AddComponent(typeof (OneDayClock)) as OneDayClock;
            }
            
            return managerInstance;
        }
    }
	
	void Start () {
		timeSinceGameStarted = timeSinceLastPaused = Time.time;
		timeInHour = GAME_LENGTH/HOURS_IN_DAY;
	}
	
	protected override void UpdateObject() {
		TimeReactionManager.instance.Update(GetGameDayTime());
	}
	
	protected override void OnPause() {
		timeSinceLastPaused = Time.time;
	}
	
	protected override void OnResume() {
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
		if (GamePlayTime() > GAME_LENGTH) EndTheGame();
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
	
	protected void EndTheGame() {
		//Application.LoadLevel(
	}
}
