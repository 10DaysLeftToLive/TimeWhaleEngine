using UnityEngine;
using System.Collections;

public class DeathbedFade : MonoBehaviour {
	float timer = 5.0F;
	Texture2D fadeTexture;
	float fadeSpeed = 0.2F;
	int drawDepth = -1000;

	private float alpha = 1;
	private float fadeDir = -1;
	
	bool fadedIn = false;
	
	public GameObject panelToFade;
	
	private TweenAlpha panelScript;

	void Start() {
		if (panelToFade == null){
			Debug.Log("panelToFade was not set");
		} else {
			panelScript = panelToFade.GetComponent<TweenAlpha>();
			panelScript.alpha = .01f;
		}
	}
	
	void Update(){
		if (panelScript.alpha < .01 && fadedIn){
			NextScene();
		} else if (panelScript.alpha > .98){
			fadedIn = true;
		}
	}

	public void NextScene(){
		Debug.Log("Done");
		Application.LoadLevel(Strings.GenderSelect);
	}
}