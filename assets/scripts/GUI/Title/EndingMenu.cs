using UnityEngine;
using System.Collections;
using SmoothMoves;

public class EndingMenu : MonoBehaviour {
	public DeathBedObject[] deathBedObjects;
	public GameObject malePC;
	public GameObject femalePC;
	public UIPanel[] autoPlayPanels;
	public UIPanel deathBedPanelWithObjects;
	public FadeToBlackTransition fadeToBlackSprite;
	public float holdTimeInSeconds = 2.0f;
	public float MAXSPEEDMULTIPLIER = 10;
	public bool fadedOpeningScene = false;
	
	private int autoPlayPanelsIndex = 0;
	private bool timerStarted = false;
	private float sceneTimer = 0.0f;
	
	void Start () {
		EventManager.instance.mOnClickHoldEvent += new EventManager.mOnClickHoldDelegate (OnHoldClick);
		EventManager.instance.mOnClickHoldReleaseEvent += new EventManager.mOnClickHoldReleaseDelegate(OnHoldRelease);
	}
	
	private void OnHoldClick(EventManager EM, ClickPositionArgs e){
		Time.timeScale = MAXSPEEDMULTIPLIER;
	}
	
	private void OnHoldRelease(EventManager EM){
		Time.timeScale = 1;
	}
	
	void Update () {
		if(timerStarted) TickFadeTimer();

		if(sceneTimer > holdTimeInSeconds){
			if(autoPlayPanelsIndex <= autoPlayPanels.Length){
				if(autoPlayPanels[autoPlayPanelsIndex].gameObject == deathBedPanelWithObjects.gameObject){
					SetDeathbedObjects();
				}else{
					fadeToBlackSprite.StartFadeToBlack(
						autoPlayPanels[autoPlayPanelsIndex - 1].gameObject,
						autoPlayPanels[autoPlayPanelsIndex].gameObject);
				}
			}
			ResetFadeTimer();
		}
		if(fadeToBlackSprite.DoneFading){
			fadeToBlackSprite.RestartFadeToBlack();
		}
	}
	
	private void SetDeathbedObjects(){
		ArrayList toDisable = new ArrayList();
		ArrayList toEnable = new ArrayList();
		
		toDisable.Add(autoPlayPanels[autoPlayPanelsIndex - 1].gameObject);
		toEnable.Add(autoPlayPanels[autoPlayPanelsIndex].gameObject);
		
		//disable all the deathbedobjects that dont have the right flags
		foreach(DeathBedObject dbo in deathBedObjects){
			if(!CheckFlagsForEnable(dbo.flagsEnabledRequirements)){
				toDisable.Add(dbo.gameObject);	
			}
		}
		
		float gender = PlayerPrefs.GetFloat(Strings.Gender);
		if (gender == (float)CharacterGender.MALE){
			toEnable.Add(femalePC);
			toDisable.Add(malePC);
		} else {
			toEnable.Add(malePC);
			toDisable.Add(femalePC);
		}
		
		fadeToBlackSprite.StartFadeToBlack(toDisable,toEnable);
	}
	
	/// <summary>
	/// If all flags are set, return true
	/// </summary>
	bool CheckFlagsForEnable(string[] flags){
		if(flags.Length <= 0) return false;

		foreach(string flag in flags){
			if(FlagManager.instance.FlagIsSet(flag)){
				return true;
			}
		}
		return false;
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
	
	public void ChooseYes(){
		Game.Reset();
		FadeToNextAutoPanel();
	}
	
	public void ChooseNo(){
		Game.GoToMainMenu();
		FadeToNextAutoPanel();
	}	
}

[System.Serializable]
public class DeathBedObject{
	public GameObject gameObject;
	public string[] flagsEnabledRequirements;
}