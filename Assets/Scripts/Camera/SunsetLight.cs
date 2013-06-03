using UnityEngine;
using System.Collections;

[RequireComponent (typeof(UISprite))]
public class SunsetLight : PauseObject {
	
	private Color startColor;
	public ColorFade[] colorFades;
	private int fadeInProgressIndex = 0;
	private bool doFade;
	private bool stopFade = false;
	private UISprite fadeSprite;
	private Color sunsetLightColor = new Color(1f, 1f, 1f);
	
	private float sunsetStartTime = 0;//OneDayClock.MIDDAY;
	
	private float lerpTime = 0.0f;
	
	// Use this for initialization
	void Start () {
		fadeSprite = this.GetComponent<UISprite>();
		startColor = fadeSprite.color;
	}
	
	// Update is called once per frame
	protected override void UpdateObject() {
		if (doFade) {
			if(Fade()){
				if(fadeInProgressIndex < colorFades.Length){
					startColor = colorFades[fadeInProgressIndex].color;
					lerpTime = 0.0f;
					if (fadeInProgressIndex < 255)
					fadeInProgressIndex++;
				}
				else{
					stopFade = true;	
				}
			}
		}
		else if (OneDayClock.Instance.GetGameDayTime() > sunsetStartTime && !stopFade) {
			doFade = true;
		}
	}
	
	/// <summary>
	/// Fade this instance. Returns true when color is reached (done)
	/// </summary>
	bool Fade(){
		//fadeSprite.color = Color.Lerp(startColor, colorFades[fadeInProgressIndex].color, lerpTime);
		//if(lerpTime < 1){
			//lerpTime += Time.deltaTime/colorFades[fadeInProgressIndex].durationForFade;
			return false;
		//}
		//else{
		//	return true;
		//}
	}
	
	/*
	void CrossFadeForeground ()
	{
		if (sunsetLightColor.r > 0.8f) {
			sunsetLightColor.r -= (OneDayClock.Instance.GetGameDayTime() - OneDayClock.MIDDAY) * 1f/255 / sunsetFadeDuration;
		}
		
		if (sunsetLightColor.g > 0) {
			sunsetLightColor.g -= (OneDayClock.Instance.GetGameDayTime() - OneDayClock.MIDDAY)  * 2 * 1f/255 / sunsetFadeDuration;
			sunsetLightColor.b -= (OneDayClock.Instance.GetGameDayTime() - OneDayClock.MIDDAY)  * 2 * 1f/255 / sunsetFadeDuration;
		}
		light.color = sunsetLightColor;
	}*/
}

[System.Serializable]
public class ColorFade{
	public Color color;
	public float durationForFade;
}