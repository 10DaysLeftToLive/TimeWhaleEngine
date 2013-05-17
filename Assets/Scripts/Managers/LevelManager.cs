using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
	public Player playerCharacter;
	private ParallaxManager parallaxManager;
	
	public TimeSwitchObject[] timeSwitchObjects;
	
	//Make these private later: Also assign these targets based off prior choices / dispositions
	public Transform youngSectionTarget;
	public Transform middleSectionTarget;
	public Transform oldSectionTarget;
	
	public static float levelYOffSetFromCenter = 50;
	
	public LevelLoader levelLoader;
	
	public PlayerAnimationContainer[] genderAnimations;
	public CharacterGender playerGender = CharacterGender.MALE;
	private PlayerAnimationContainer genderAnimationInUse;
	public string levelDataName;
	
	private string dispositionDataFile;
	
	void Awake(){
		if(playerCharacter == null) {
			Debug.LogWarning("Warning: No PlayerCharacter attached to LevelManager");
		}
		
		if (levelDataName == ""){
			Debug.LogError("Error: No Level data file given to LevelManager.");
		}
		
		float genderAsFloat = PlayerPrefs.GetFloat(Strings.Gender);
		
		if (genderAsFloat == 0f){
			playerGender = CharacterGender.MALE;
		} else {
			playerGender = CharacterGender.FEMALE;
		}
		
		SetFiles();		
		SetGender(CharacterGender.MALE);
		CharacterAgeManager.SetAgeStart(CharacterAgeState.YOUNG);
		CharacterAgeManager.SetPlayer(playerCharacter);
		playerCharacter.ChangeAnimation(genderAnimationInUse.youngBoneAnimation);
		SetNPCData();	
	}
	
	// Use this for initialization
	void Start () {
		parallaxManager = GameObject.Find(Strings.PARALLAXMANAGER).GetComponent<ParallaxManager>();
		ScreenSetup.CalculateSettings();
		StartCoroutine(Init());
		GUIManager.Instance.AddInGameMenu();
	}
	
	private IEnumerator Init(){
		StartCoroutine(levelLoader.Load("LevelYoung", "LevelMiddle", "LevelOld"));
		while (!levelLoader.HasLoaded()){ // wait untill the outside scenes have been loaded in.
			yield return new WaitForSeconds(.1f);
		}
		FindSections();
		SpreadSections();
		SetUpAges();
		ManagerLoader.LoadManagers(youngSectionTarget, middleSectionTarget, oldSectionTarget);
		
		NPCManager.instance.Init();
		FlagManager.instance.Init();
		
		parallaxManager.Init();
	}
	
	private void SetFiles(){
		dispositionDataFile = Application.dataPath + "/Data/DispositionData/" + Strings.DispositionFile + ".xml";
		
		if (!System.IO.File.Exists(dispositionDataFile)){
			Debug.LogError("Error: " + dispositionDataFile + " was not found.");
		}
	}
	
	private void FindSections(){
		youngSectionTarget = GameObject.Find(Strings.YoungAge).transform;
		middleSectionTarget = GameObject.Find(Strings.MiddleAge).transform;
		oldSectionTarget = GameObject.Find(Strings.OldAge).transform;
	}
	
	// After being loaded in they are all at 0,0,0 so move them apart along the y axis
	private void SpreadSections(){
		middleSectionTarget.transform.position = new Vector3(0,levelYOffSetFromCenter,0);
		oldSectionTarget.transform.position = new Vector3(0,levelYOffSetFromCenter*2,0);
	}
	
	void Update () {
	}
	
	void SetNPCData(){		
		InteractionManager.instance.InitilizeNPCs(dispositionDataFile);
	}	
	
	void SaveDispositions(){
		InteractionManager.instance.SaveNPCDispositions(dispositionDataFile);
	}
	
	public void ShiftUpAge(){		
		if (CharacterAgeManager.GetCurrentAgeState() != CharacterAgeState.OLD){
			CharacterAge newAge = CharacterAgeManager.GetAgeTransitionUp();
			CharacterAge currentAge = CharacterAgeManager.GetCurrentAge();
			
			if (playerCharacter.CheckTransitionPositionSuccess(newAge, currentAge)){
				CharacterAgeManager.TransistionUp();
				UpdateTimeSwitchObjects(CharacterAgeManager.GetCurrentAgeState());
			}
			else{
				Debug.Log("CAN'T TELEPORT THERE!");	
			}
		}
	}
	
	public void ShiftDownAge(){
		if (CharacterAgeManager.GetCurrentAgeState() != CharacterAgeState.YOUNG){
			CharacterAge newAge = CharacterAgeManager.GetAgeTransitionDown();
			CharacterAge currentAge = CharacterAgeManager.GetCurrentAge();
			
			if (playerCharacter.CheckTransitionPositionSuccess(newAge, currentAge)){
				CharacterAgeManager.TransistionDown();
				UpdateTimeSwitchObjects(CharacterAgeManager.GetCurrentAgeState());
			}
			else{
				Debug.Log("CAN'T TELEPORT THERE!");	
			}
		}
	}
	
	private void UpdateTimeSwitchObjects(CharacterAgeState newAge){
		foreach(TimeSwitchObject timeSwitchObject in timeSwitchObjects){
			timeSwitchObject.ChangeAge(newAge);
		}
	}
	
	public bool CanAgeTransition(string buttonName) {
		if (CharacterAgeManager.GetCurrentAgeState() != CharacterAgeState.OLD 
			&& Strings.ButtonAgeShiftUp.Equals(buttonName)) {
			if (playerCharacter.CheckTransitionPositionSuccess(CharacterAgeManager.GetAgeTransitionUp(),
				CharacterAgeManager.GetCurrentAge())) {
				return true;
			}
			else {
				return false;
			}
		}
		else if (CharacterAgeManager.GetCurrentAgeState() != CharacterAgeState.YOUNG 
			&& Strings.ButtonAgeShiftDown.Equals(buttonName)) {
			if (playerCharacter.CheckTransitionPositionSuccess(CharacterAgeManager.GetAgeTransitionDown(),
				CharacterAgeManager.GetCurrentAge())) {
				return true;
			}
			else {
				return false;
			}
		}
		return false;
	}
	
	public void SetGender(CharacterGender gender){
		switch(gender){
			case CharacterGender.MALE:
				genderAnimationInUse = genderAnimations[(int)CharacterGender.MALE];
				break;
			case CharacterGender.FEMALE:
				genderAnimationInUse = genderAnimations[(int)CharacterGender.FEMALE];
				break;		
		}
	}
	
	private void SetUpAges(){
		CharacterAgeManager.SetupYoung(genderAnimationInUse.youngBoneAnimation, youngSectionTarget);
		CharacterAgeManager.SetupMiddle(genderAnimationInUse.middleBoneAnimation, middleSectionTarget);
		CharacterAgeManager.SetupOld(genderAnimationInUse.oldBoneAnimation, oldSectionTarget);
	}

	private void AllInteractionsComplete() {
		// To fill in with in with easter egg when the player has no more interactions left before time runs out
		/* if (InputManager.UPUPDOWNDOWNLEFTRIGHTLEFTRIGHTABDETECTED())
		 *     TimeWhaleEngine.Begin(); // Needs rewording as there is no begining to Time Whale the glory of Time Whale. It is eternal and does not require a cause
		 */
	}

	void OnApplicationQuit(){
		SaveDispositions();
	}
}

public enum CharacterGender{
		MALE = 0,
		FEMALE = 1,
	}
