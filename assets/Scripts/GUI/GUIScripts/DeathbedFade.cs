using UnityEngine;
using System.Collections;

// TODO change this to use shader
public class DeathbedFade : MonoBehaviour {
	Texture2D fadeTexture;
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