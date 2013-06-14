using UnityEngine;
using System.Collections;
using SmoothMoves;

public class EndingMenu : MonoBehaviour {
	
	public DeathBedObject[] deathBedObjects;
	
	public GameObject malePC;
	public GameObject femalePC;
		
	public UIPanel[] autoPlayPanels;
	private int autoPlayPanelsIndex = 0;
	
	public UIPanel deathBedPanelWithObjects;
	
	
	public FadeToBlackTransition fadeToBlackSprite;
	
	private bool timerStarted = false;
	private float sceneTimer = 0.0f;
	public float holdTimeInSeconds = 2.0f;
	
	public bool fadedOpeningScene = false;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		if(timerStarted) TickFadeTimer();

		if(sceneTimer > holdTimeInSeconds){
			if(autoPlayPanelsIndex <= autoPlayPanels.Length){
				if(autoPlayPanels[autoPlayPanelsIndex].gameObject == deathBedPanelWithObjects.gameObject){
					ArrayList toDisable = new ArrayList();
					ArrayList toEnable = new ArrayList();
					
					toDisable.Add(autoPlayPanels[autoPlayPanelsIndex - 1].gameObject);
					toEnable.Add(autoPlayPanels[autoPlayPanelsIndex].gameObject);
					
					//disable all the deathbedobjects that dont have the right flags
					foreach(DeathBedObject dbo in deathBedObjects){
						if(!CheckFlagsForEnable(dbo.flagsEnabledRequirements)){
							toDisable.Add(dbo.gameObject);	
						}else{
							Debug.Log("Leaving Enabled: " + dbo.gameObject.name);	
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
	
	/// <summary>
	/// If all flags are set, return true
	/// </summary>
	/// <returns>
	/// The flags for enable.
	/// </returns>
	/// <param name='flags'>
	/// If set to <c>true</c> flags.
	/// </param>
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