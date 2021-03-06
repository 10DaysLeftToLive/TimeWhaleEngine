using UnityEngine;
using System.Collections;
using SmoothMoves;

public class TitleMenu : MonoBehaviour {
	//public FadeEffect fadeCamera;
	
	public MenuTransition menuTransition;
	
	public UIPanel titleMenuPanel;
	public UISlider titleMenuSlider;
	
	public UIPanel mainMenuPanel;
	public UISlider newGameSlider;
	private bool newGameSliderEnabled = true;
	
	public UIPanel openingScenePanel;
	
	public UIPanel[] autoPlayPanels;
	public int autoPlayPanelsIndex = 0;
	
	
	public UIPanel chooseGenderPanel;
	public UISprite genderPickFilter;
	
	public Arrow[] arrows;
	
	public FadeToBlackTransition fadeToBlackSprite;
	
	private bool timerStarted = false;
	private float sceneTimer = 0.0f;
	public float holdTimeInSeconds = 5.0f;
	
	public bool fadedOpeningScene = false;
	
	private readonly float PERCENTAGE_OF_SWITCH_EFFECT_TO_WAIT = 0.75f;
	
	// Use this for initialization
	void Start () {
		titleMenuSlider.sliderValue = 0;
		newGameSlider.sliderValue = 0;

	}
	
	// Update is called once per frame
	void Update () {
		
		if(titleMenuPanel.enabled) CheckTitleSlider();
		if(mainMenuPanel.enabled) CheckMainMenuSliders();
		if(timerStarted) TickFadeTimer();
		if(sceneTimer > holdTimeInSeconds && !fadedOpeningScene){
			ArrayList disable = new ArrayList();
			//disable.Add(tree.gameObject);
			disable.Add(openingScenePanel.gameObject);
			fadeToBlackSprite.StartFadeToBlack(disable,autoPlayPanels[autoPlayPanelsIndex].gameObject);
			fadedOpeningScene = true;
			ResetFadeTimer();
		}
		if(sceneTimer > holdTimeInSeconds && fadedOpeningScene){
			if(autoPlayPanelsIndex <= autoPlayPanels.Length){
				fadeToBlackSprite.StartFadeToBlack(autoPlayPanels[autoPlayPanelsIndex - 1].gameObject,autoPlayPanels[autoPlayPanelsIndex].gameObject);
			}
			ResetFadeTimer();
		}
		if(fadeToBlackSprite.DoneFading){
			fadeToBlackSprite.RestartFadeToBlack();
		}
	}
		
	
	void CheckTitleSlider(){
		if(titleMenuSlider.sliderValue != 0 && !Input.GetMouseButton(0)){
			titleMenuSlider.sliderValue = 0;
		}
		if(titleMenuSlider.sliderValue == 1){
			TransitionPanels(titleMenuPanel, mainMenuPanel);
		}	
	}
	
	void CheckMainMenuSliders(){
		if(newGameSlider.sliderValue != 0 && !Input.GetMouseButton(0)){
			newGameSlider.sliderValue = 0;
		}
		if(newGameSlider.sliderValue == 1 && newGameSliderEnabled){
			DisableArrows();
			newGameSliderEnabled = false;
			TransitionPanels(mainMenuPanel, openingScenePanel);
			FadeToText();
		}
	}
	
	void DisableArrows(){
		foreach(Arrow arrow in arrows){
			arrow.DisableArrow();
		}
	}
	
	IEnumerator FadePanels(UIPanel toDisable, UIPanel toEnable, bool forceFade){
		Debug.Log("Start Fade. ToDisable: " + toDisable.name + " || ToEnable: " + toEnable.name);
		if(forceFade)
			menuTransition.DoTheFade();

		yield return new WaitForSeconds(menuTransition.SWITCHTIMESECONDS * PERCENTAGE_OF_SWITCH_EFFECT_TO_WAIT);

        if (toDisable == titleMenuPanel && !Crossfade.instance.LighthouseAmbient.isPlaying)
        {
            Crossfade.instance.startCoroutineFadeOverTime("ReflectionTree", "Lighthouse");
        }
        else if (toDisable == mainMenuPanel && !Crossfade.instance.IntroBGM.isPlaying)
        {
            Crossfade.instance.startCoroutineFadeOverTime("Lighthouse", "Intro");
        }
        

		Utils.SetActiveRecursively(toEnable.gameObject, true);
		Utils.SetActiveRecursively(toDisable.gameObject, false);
		DisableArrows();
	}
	
	void TransitionPanels(UIPanel toDisable, UIPanel toEnable){
		StartCoroutine(FadePanels(toDisable,toEnable, true));
	}
	
	void FadeToText(){
		BeginFadeTimer();
	}
	
	void BeginFadeTimer(){
		timerStarted = true;
	}
	
	void TickFadeTimer(){
		sceneTimer += Time.deltaTime;	
	}
	
	void ResetFadeTimer(){
		sceneTimer = 0.0f;
		timerStarted = false;
	}
	
	void FadeToNextAutoPanel(){
		autoPlayPanelsIndex++;
		BeginFadeTimer();
	}
	
	void FadetoNextAutoPanelImmediately(){
		autoPlayPanelsIndex++;
		holdTimeInSeconds = 0.0f;
		BeginFadeTimer();
	}
	
	public void ChoseGenderMale(){
		
        UnfadeGenderFilter();
		PlayerPrefs.SetFloat(Strings.Gender, (float)CharacterGender.MALE);
		FadeToNextAutoPanel();
	}
	
	public void ChoseGenderFemale(){

		UnfadeGenderFilter();
		PlayerPrefs.SetFloat(Strings.Gender, (float)CharacterGender.FEMALE);
		FadeToNextAutoPanel();
	}
	
	void UnfadeGenderFilter(){
		genderPickFilter.alpha = 0.0f;	
	}
		
	public void TransitionToMainMenu(){
		StartCoroutine(FadePanels(titleMenuPanel,mainMenuPanel, false));
	}
}
