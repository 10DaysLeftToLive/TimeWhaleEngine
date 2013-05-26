using UnityEngine;
using System.Collections;

//This class will not work if the game has to pause!
//STILL WORKING ON THIS!!! NO TOUCHY!!!
public class SunriseSunsetTimer : ShaderBase {
	
	public bool FullDay = false;
	
	public float sunsetStartTime;
	
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
	
	private static class HueShaderConstants {
		public const float GREEN_COLOR_MAX = 350;
		
		public const float GREEN_COLOR_MIN = 300;
		
		public const float GREEN_FILTER_THRESHOLD = 0.25f;
		
		public const float GREEN_FALLOFF = 1f/20;
		
		public const float GREEN_FALLOFF_TIME_RATE = 10f;
		
		public const float HUE_MAIN_MAX = 300f;
		
		public const float HUE_MAIN_MIN = 190f;
		
		public const float SUNSET_COLOR_CHANGE_RATE = 1f/80;
		
		public const float SUNSET_LAST_THRESHOLD = 150f;
		
		public const float SUNSET_TIME_CHANGE_RATE = 5f;
		
		public const float REMAINING_FALLOFF = 5f;
	
		
	}
	
	private float gradientInterpolationFactor = 0f;
	
	// Use this for initialization
	protected override void Initialize () {
		sunsetEndTime = sunsetStartTime + sunsetDuration;
		_hue = StartHue;
	}
	
	// Update is called once per frame
	protected override void UpdateObject () {
		float currentTime = OneDayClock.Instance.time;
		if ((currentTime > sunsetStartTime) && (currentTime < sunsetEndTime)) {
			//Debug.Log ("Current time: " + currentTime);
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
		interpolationFactor += Time.deltaTime;
		if (_hue < HueShaderConstants.GREEN_COLOR_MAX && _hue > HueShaderConstants.GREEN_COLOR_MIN) {
			if (greenFilter < HueShaderConstants.GREEN_FILTER_THRESHOLD) {
				//Debug.Log ("Green threshold");
				float greenFilterRate = Time.deltaTime * HueShaderConstants.GREEN_FALLOFF;
				greenFilter += greenFilterRate;
				interpolationFactor += Time.deltaTime * HueShaderConstants.GREEN_FALLOFF_TIME_RATE;
			}
			_hue -= interpolationFactor;
		}
		else if (_hue > HueShaderConstants.HUE_MAIN_MIN && _hue < HueShaderConstants.HUE_MAIN_MAX){
			if (greenFilter > 0) {
				//Debug.Log ("Hue Threshold");
				float newVal = Time.deltaTime * HueShaderConstants.SUNSET_COLOR_CHANGE_RATE;
				greenFilter -= newVal;
				interpolationFactor += Time.deltaTime * HueShaderConstants.SUNSET_TIME_CHANGE_RATE;
				gradientInterpolationFactor = ((HueShaderConstants.HUE_MAIN_MAX - _hue)  + HueShaderConstants.HUE_MAIN_MIN)
					/ HueShaderConstants.HUE_MAIN_MAX;
				//Debug.Log ("Gradient Interpolation Factor: " + gradientInterpolationFactor);
			}
			else {
				greenFilter = 0;
			}
		}
		else {
			//Debug.Log ("Last Threshold");
			if (_hue > HueShaderConstants.SUNSET_LAST_THRESHOLD) {
				_hue = StartHue - interpolationFactor / HueShaderConstants.REMAINING_FALLOFF;
			}
		}
		
		if (_hue > HueShaderConstants.SUNSET_LAST_THRESHOLD) {
				_hue = StartHue - interpolationFactor;
			}
		else {
			Debug.Log ("Interpolation Factor after Hue change: " + interpolationFactor);
		}
		
		return _hue;
	}
	
	protected virtual float ChangeBrightness(bool isSunrise) {
		float currentTime;
		if (sunsetDuration <= 0) return minBrightness;
			currentTime = (Time.time - sunsetStartTime) / sunsetDuration;
			//Debug.Log (currentTime);
			return Mathf.Lerp(maxBrightness, minBrightness, currentTime);
	}
	
	protected virtual float ChangeSaturation(bool isSunrise) {
		return 0.5f;
	}
	
	protected override void FadeIn() {
		renderer.material.SetFloat("_Hue", ChangeHue(true));
		renderer.material.SetFloat("_GreenFilter", greenFilter);
		renderer.material.SetFloat("Brightness", ChangeBrightness(true));
		renderer.material.SetFloat("Saturation", ChangeSaturation(true));
		renderer.material.SetFloat("InterpolationFactor", gradientInterpolationFactor);
	}
	

}
