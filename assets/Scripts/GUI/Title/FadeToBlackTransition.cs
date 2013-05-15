using UnityEngine;
using System.Collections;

[RequireComponent (typeof (UISprite))]
public class FadeToBlackTransition : MonoBehaviour {
	public float fadeTime = 3.0f;
	float timer = 0.0f;
	bool fadingToBlack = false;
	bool fadeToClear = false;
	bool doneFading = false;
	public bool DoneFading{
		get{return doneFading;}
	}
	UISprite blackSprite;
	
	ArrayList toDisableOnFadeGO;
	ArrayList toEnableOnFadeGO;
	public float opacity;

	// Use this for initialization
	void Start () {
		blackSprite = GetComponent<UISprite>();
	}
	
	// Update is called once per frames
	void Update () {
		opacity = blackSprite.alpha;
		if(fadingToBlack && (timer < fadeTime)){
			timer += Time.deltaTime;
			blackSprite.alpha = timer / fadeTime;
		}
		if(fadingToBlack && (timer >= fadeTime)){
			foreach(GameObject go in toDisableOnFadeGO) Utils.SetActiveRecursively(go, false);
			foreach(GameObject go in toEnableOnFadeGO) Utils.SetActiveRecursively(go, true);
			fadingToBlack = false;
			fadeToClear = true;
		}
		if(fadeToClear && (timer > 0)){
			timer -= Time.deltaTime;
			blackSprite.alpha = timer / fadeTime;
		}
		if(fadeToClear && (timer <= 0.0f)){
			fadeToClear = false;
			doneFading = true;
		}
	}
	
	public void StartFadeToBlack(GameObject toDisable, GameObject toEnable){
		
		toDisableOnFadeGO = new ArrayList();
		toDisableOnFadeGO.Add(toDisable);
		toEnableOnFadeGO = new ArrayList();
		toEnableOnFadeGO.Add(toEnable);
		fadingToBlack = true;	
	}
	
	public void StartFadeToBlack(ArrayList toDisable, GameObject toEnable){
		toDisableOnFadeGO = toDisable;
		
		toEnableOnFadeGO = new ArrayList();
		toEnableOnFadeGO.Add(toEnable);
		
		fadingToBlack = true;	
	}
	
	public void RestartFadeToBlack(){
		blackSprite.alpha = 0.0f;
		timer = 0.0f;
		fadingToBlack = false;
		fadeToClear = false;
		doneFading = false;
		toDisableOnFadeGO = null;
		toEnableOnFadeGO = null;
	}
}
