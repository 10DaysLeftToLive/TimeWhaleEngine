using UnityEngine;
using System.Collections;

//This class will not work if the game has to pause!
//STILL WORKING ON THIS!!! NO TOUCHY!!!
public class SunriseSunsetTimer : ShaderBase {
	
	public bool FullDay = false;
	
	public float sunriseStartTime;
	
	public float sunsetStartTime;
	
	public float sunriseDuration = 100f;
	
	public float sunsetDuration = 100f;
	
	public float minBrightness = 0.35f;
	
	public float maxBrightness = 0.5f;
	
	public float StartHue = 360;
	
	public OneDayClock sunsetTimer;
	
	private float sunriseEndTime;
	
	private float sunsetEndTime;
	
	private float _hue;
	
	private int signFlag = 1;
	
	private float greenFilter = 0;
	
	// Use this for initialization
	protected override void Initialize () {
		sunriseEndTime = sunriseStartTime + sunriseDuration;
		sunsetEndTime = sunsetStartTime + sunsetDuration;
		_hue = StartHue;
	}
	
	// Update is called once per frame
	protected override void UpdateObject () {
		float currentTime = sunsetTimer.time;
		if ((currentTime > sunsetStartTime) && (currentTime < sunsetEndTime)) {
			FadeIn();
		}
	}
	
	
	//TODO: 1. Refactor to use OneDayClock instead of Timer so that sunset sunrise times don't
	//      get screwed up during scene changes and pauses.
	//		2. Divide the clock by the duration so the sunset doesn't occur to quickly.
	//		3. Only does sunset, may do sunrise?
	//Insert mathematical function for sunrise/sunset here.
	/// <summary>
	/// Changes the hue.  Hue is parameterized as a piece-wise function with
	/// respect to time.  A green color filter is used to mask the unwanted green
	/// color channel that gets generated when a hue roation in HSV space occurs.
	/// </summary>
	/// <returns>
	/// The hue.
	/// </returns>
	/// <param name='isSunrise'>
	/// Is a sunrise occurring.
	/// </param>
	protected virtual float ChangeHue(bool isSunrise) {
//		interpolationFactor = isSunrise 
//			? sunsetTimer.time - sunriseStartTime : sunsetTimer.time - sunsetStartTime;
		
		interpolationFactor += Time.deltaTime;
		if (_hue < 350 && _hue > 300) {
			if (greenFilter < 0.25) {
				float newVal = Time.deltaTime * (1f/20);
				greenFilter += newVal;
				interpolationFactor += Time.deltaTime * 10;
			}
		}
		else if (_hue > 190 && _hue < 300){
			if (greenFilter > 0) {
				float newVal = Time.deltaTime * (1f/80);
				greenFilter -= newVal;
				interpolationFactor += Time.deltaTime * 5;
			}
			else {
				greenFilter = 0;
			}
		}
		else {
			if (_hue > 150) {
				_hue = StartHue - interpolationFactor / 10;
			}
		}
		
		if (_hue > 150) {
			_hue = StartHue - interpolationFactor;
		}
		return _hue;
	}
	
	protected virtual float ChangeBrightness(bool isSunrise) {
		float currentTime;
		if (isSunrise) {
			if (sunriseDuration <= 0) return minBrightness;
			currentTime = (Time.time - sunriseStartTime) / sunriseDuration;
			//Debug.Log (currentTime);
			return Mathf.Lerp(minBrightness, maxBrightness, currentTime);
		}
		else {
			if (sunsetDuration <= 0) return minBrightness;
			currentTime = (Time.time - sunsetStartTime) / sunsetDuration;
			//Debug.Log (currentTime);
			return Mathf.Lerp(maxBrightness, minBrightness, currentTime);
		}
	}
	
	protected virtual float ChangeSaturation(bool isSunrise) {
		return 0.5f;
	}
	
	protected override void FadeIn() {
		renderer.material.SetFloat("_Hue", ChangeHue(true));
		renderer.material.SetFloat("_GreenFilter", greenFilter);
		renderer.material.SetFloat("Brightness", ChangeBrightness(true));
		renderer.material.SetFloat("Saturation", ChangeSaturation(true));
	}
	

}
