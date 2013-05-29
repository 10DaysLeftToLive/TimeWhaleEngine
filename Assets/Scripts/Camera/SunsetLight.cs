using UnityEngine;
using System.Collections;

public class SunsetLight : PauseObject {
		
	private Color sunsetLightColor = new Color(1f, 1f, 1f);
	
	private float sunsetStartTime = 0;//OneDayClock.MIDDAY;
	
	public float sunsetFadeDuration = 400;
	
	private bool doFade = false;
	// Use this for initialization
	void Start () {
		light.color = sunsetLightColor;
	}
	
	// Update is called once per frame
	protected override void UpdateObject() {
		if (doFade) {
			CrossFadeForeground ();
		}
		else if (OneDayClock.Instance.GetGameDayTime() > OneDayClock.MIDDAY) {
			Debug.Log ("DOING FOREGROUND FADE");
			doFade = true;
		}
	}

	void CrossFadeForeground ()
	{
		if (sunsetLightColor.r > 0.8f) {
			sunsetLightColor.r -= (OneDayClock.Instance.GetGameDayTime() - OneDayClock.MIDDAY) * 1f/255 / sunsetFadeDuration;
		}
		
		if (sunsetLightColor.g > 0) {
			sunsetLightColor.g -= (OneDayClock.Instance.GetGameDayTime() - OneDayClock.MIDDAY)  * 2 * 1f/255 / sunsetFadeDuration;
			sunsetLightColor.b -= (OneDayClock.Instance.GetGameDayTime() - OneDayClock.MIDDAY)  * 2 * 1f/255 / sunsetFadeDuration;
		}
		Debug.Log ("LightColor: " + light.color);
		light.color = sunsetLightColor;
	}
}
