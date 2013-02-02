using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
	public PlayerController playerCharacter;
	public InteractionManager interactionManager;
	
	public TimeSwitchObject[] timeSwitchObjects;
	
	//Make these private later: Also assign these targets based off prior choices / dispositions
	public Transform youngSectionTarget;
	public Transform middleSectionTarget;
	public Transform oldSectionTarget;
	
	public AudioSource youngBGM;
	public AudioSource middleBGM;
	public AudioSource oldBGM;
	
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
		
		if(interactionManager == null){
			Debug.LogWarning("Warning: No InteractionManager attached to LevelManager");	
		}
		
		if (levelDataName == ""){
			Debug.LogError("Error: No Level data file given to LevelManager.");
		}
	}
	
	// Use this for initialization
	void Start () {
		Init ();
	}
	
	private void Init(){
		SetGender(playerGender);
		SetFiles();		
		SetNPCData();
		CharacterAgeManager.SetPlayer(playerCharacter);
		SetUpAges();
		CharacterAgeManager.SetAgeStart(CharacterAgeState.YOUNG);
		
		CharacterAgeManager.PlayCurrentSong();		
		playerCharacter.ChangeAnimation(genderAnimationInUse.youngBoneAnimation);
		
		InitBuildings();
	}
	
	private void InitBuildings(){
		Transform buildingRootYoung = youngSectionTarget.FindChild("Buildings");
		Transform buildingRootMiddle = middleSectionTarget.FindChild("Buildings");
		Transform buildingRootOld = oldSectionTarget.FindChild("Buildings");
		
		BuildingManager.instance.LoadInBuildings(buildingRootYoung, CharacterAgeState.YOUNG);
		BuildingManager.instance.LoadInBuildings(buildingRootMiddle, CharacterAgeState.MIDDLE);
		BuildingManager.instance.LoadInBuildings(buildingRootOld, CharacterAgeState.OLD);
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
			CharacterAgeManager.TransistionUp();
			
			UpdateTimeSwitchObjects(CharacterAgeManager.GetCurrentAgeState());
		}
	}
	
	private void ShiftDownAge(){
		if (CharacterAgeManager.GetCurrentAgeState() != CharacterAgeState.YOUNG){

			CharacterAgeManager.TransistionDown();
			
			UpdateTimeSwitchObjects(CharacterAgeManager.GetCurrentAgeState());
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
		CharacterAgeManager.SetupYoung(genderAnimationInUse.youngBoneAnimation, 
						youngSectionTarget, 
			youngBGM);
		CharacterAgeManager.SetupMiddle(genderAnimationInUse.middleBoneAnimation, middleSectionTarget, middleBGM);
		CharacterAgeManager.SetupOld(genderAnimationInUse.oldBoneAnimation, oldSectionTarget, oldBGM);
	}
	
	void OnApplicationQuit(){
		SaveDispositions();
	}
}
