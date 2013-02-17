using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
	public PlayerController playerCharacter;
	
	public TimeSwitchObject[] timeSwitchObjects;
	
	//Make these private later: Also assign these targets based off prior choices / dispositions
	public Transform youngSectionTarget;
	public Transform middleSectionTarget;
	public Transform oldSectionTarget;
	
	public AudioSource youngBGM;
	public AudioSource middleBGM;
	public AudioSource oldBGM;
	
	public LevelLoader levelLoader;
	
	public enum CharacterGender{
		MALE = 0,
		FEMALE = 1,
	}
	
	public PlayerAnimationContainer[] genderAnimations;
	public CharacterGender playerGender = CharacterGender.MALE;
	private PlayerAnimationContainer genderAnimationInUse;
	public string levelDataName;
	
	private string dispositionDataFile;
	private string levelInteractionFile;
	
	void Awake(){
		if(playerCharacter == null){
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
		SetGender(playerGender);
		CharacterAgeManager.SetAgeStart(CharacterAgeState.YOUNG);
		CharacterAgeManager.SetPlayer(playerCharacter);
		playerCharacter.ChangeAnimation(genderAnimationInUse.youngBoneAnimation);
		SetNPCData();	
	}
	
	// Use this for initialization
	void Start () {
		StartCoroutine(Init());
	}
	
	private IEnumerator Init(){		
		playerCharacter.gravity = 0; // The player will fall through the unloaded floor if gravit exsists at the start
		
		Debug.Log("Awake" + Time.time);
		StartCoroutine(levelLoader.Load("LevelYoung", "LevelMiddle", "LevelOld"));
		while (!levelLoader.HasLoaded()){ // wait untill the outside scenes have been loaded in.
			yield return new WaitForSeconds(.1f);
		}
		Debug.Log("Done "+ Time.time);
		FindSections();
		SpreadSections();
		SetUpAges();		
		ManagerLoader.LoadManagers(youngSectionTarget, middleSectionTarget, oldSectionTarget);
		
		playerCharacter.gravity = 50;
	}
	
	private void SetFiles(){
		levelInteractionFile = Application.dataPath + "/Data/LevelData/" + levelDataName + ".xml";
		dispositionDataFile = Application.dataPath + "/Data/DispositionData/" + Strings.DispositionFile + ".xml";
		
		if (!System.IO.File.Exists(levelInteractionFile)){
			Debug.LogError("Error: " + levelInteractionFile + " was not found.");
		} else if (!System.IO.File.Exists(dispositionDataFile)){
			Debug.LogError("Error: " + dispositionDataFile + " was not found.");
		}
	}
	
	private void FindSections(){
		Debug.Log("FindSections "+ Time.time);
		youngSectionTarget = GameObject.Find(Strings.YoungAge).transform;
		middleSectionTarget = GameObject.Find(Strings.MiddleAge).transform;
		oldSectionTarget = GameObject.Find(Strings.OldAge).transform;
	}
	
	// After being loaded in they are all at 0,0,0 so move them apart along the y axis
	private void SpreadSections(){
		middleSectionTarget.transform.position = new Vector3(0,50,0);
		oldSectionTarget.transform.position = new Vector3(0,100,0);
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput();
	}
	
	void SetNPCData(){		
		InteractionManager.instance.InitilizeNPCs(dispositionDataFile, levelInteractionFile);
	}	
	
	void SaveDispositions(){
		InteractionManager.instance.SaveNPCDispositions(dispositionDataFile);
	}
	
	void HandleInput(){	
		if(Input.GetButtonDown(Strings.ButtonAgeShiftDown)){
			ShiftDownAge();
		}
		else if(Input.GetButtonDown(Strings.ButtonAgeShiftUp)){
			ShiftUpAge();
		} 
	}
	
	private void ShiftUpAge(){		
		if (CharacterAgeManager.GetCurrentAgeState() != CharacterAgeState.OLD){
			if(playerCharacter.CheckTransitionPositionSuccess(CharacterAgeManager.GetAgeTransitionUp(), CharacterAgeManager.GetCurrentAge())){
				CharacterAgeManager.TransistionUp();
				UpdateTimeSwitchObjects(CharacterAgeManager.GetCurrentAgeState());
			}
			else{
				Debug.Log("CAN'T TELEPORT THERE!");	
			}
		}
	}
	
	private void ShiftDownAge(){
		if (CharacterAgeManager.GetCurrentAgeState() != CharacterAgeState.YOUNG){
			if(playerCharacter.CheckTransitionPositionSuccess(CharacterAgeManager.GetAgeTransitionDown(), CharacterAgeManager.GetCurrentAge())){
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
		CharacterAgeManager.SetupYoung(genderAnimationInUse.youngBoneAnimation, youngSectionTarget, youngBGM);
		CharacterAgeManager.SetupMiddle(genderAnimationInUse.middleBoneAnimation, middleSectionTarget, middleBGM);
		CharacterAgeManager.SetupOld(genderAnimationInUse.oldBoneAnimation, oldSectionTarget, oldBGM);
	}
	
	void OnApplicationQuit(){
		SaveDispositions();
	}
}
