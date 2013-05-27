using UnityEngine;
using System.Collections;

public class SunsetLight : PauseObject {
	
	public OneDayClock sunsetTimer;
	
	private Color sunsetLightColor = new Color(1f, 1f, 1f);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected override void UpdateObject() {
		if (sunsetLightColor.r > 0.8f) {
			sunsetLightColor.r -= (sunsetTimer.GetGameDayTime() - OneDayClock.START_TIME) * 1f/255 / OneDayClock.GAME_LENGTH;
		}
		
		if (sunsetLightColor.g > 0) {
			sunsetLightColor.g -= (sunsetTimer.GetGameDayTime() - OneDayClock.START_TIME)  * 2 * 1f/255 / OneDayClock.GAME_LENGTH;
			sunsetLightColor.b -= (sunsetTimer.GetGameDayTime() - OneDayClock.START_TIME)  * 2 * 1f/255 / OneDayClock.GAME_LENGTH;
		}
		
		light.color = sunsetLightColor;
	}
}
