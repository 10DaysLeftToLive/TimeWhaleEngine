using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Level manager will handle the initialization of the game and will hold the sections of the map
/// </summary>
public class LevelManager : MonoBehaviour {
	public static float levelYOffSetFromCenter = 50;
	public CharacterAgeState initialAge;
	
	private ParallaxManager parallaxManager;
	
	//Make these private later: Also assign these targets based off prior choices / dispositions
	public Transform youngSectionTarget;
	public Transform middleSectionTarget;
	public Transform oldSectionTarget;
	
	public LevelLoader levelLoader;
	
	public PlayerAnimationContainer[] genderAnimations;
	public CharacterGender playerGender = CharacterGender.MALE;
	private PlayerAnimationContainer genderAnimationInUse;
	
	void Awake(){		
		float genderAsFloat = PlayerPrefs.GetFloat(Strings.Gender);
		
		if (genderAsFloat == 0f){
			playerGender = CharacterGender.MALE;
		} else {
			playerGender = CharacterGender.FEMALE;
		}
		
		SetGender(CharacterGender.MALE);
		Player playerCharacter = GameObject.Find(Strings.Player).GetComponent<Player>();
		CharacterAgeManager.SetPlayer(playerCharacter);
		playerCharacter.ChangeAnimation(genderAnimationInUse.youngBoneAnimation);
	}
	
	void Start () {
		parallaxManager = GameObject.Find(Strings.PARALLAXMANAGER).GetComponent<ParallaxManager>();
		ScreenSetup.CalculateSettings();
		StartCoroutine(Init());
		GUIManager.Instance.AddInGameMenu();
	}
	
	private IEnumerator Init(){
		StartCoroutine(levelLoader.Load("LevelYoung", "LevelMiddle", "LevelOld"));
		while (levelLoader.HasNotFinished()){ // wait untill the outside scenes have been loaded in.
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
	
	private void FindSections(){
		youngSectionTarget = GameObject.Find(Strings.YoungAge).transform;
		middleSectionTarget = GameObject.Find(Strings.MiddleAge).transform;
		oldSectionTarget = GameObject.Find(Strings.OldAge).transform;
		if (!youngSectionTarget || !middleSectionTarget || !oldSectionTarget){
			Debug.LogError("Failed to load a secion");
		}
	}
	
	// After being loaded in they are all at 0,0,0 so move them apart along the y axis
	private void SpreadSections(){
		middleSectionTarget.transform.position = new Vector3(0,levelYOffSetFromCenter,0);
		oldSectionTarget.transform.position = new Vector3(0,levelYOffSetFromCenter*2,0);
	}
	
	/// <summary>
	/// Moves the player to right initial age.
	/// </summary>
	public void MovePlayerToRightAge(CharacterAgeState age){
		AgeSwapMover.instance.ChangeAgePosition(CharacterAgeManager.GetAgeOf(age), 
			       		      CharacterAgeManager.GetAgeOf(CharacterAgeState.YOUNG)); // we start at the young int he scene
	}
	
	#region AgeTransition
	public void ShiftUpAge(){		
		CharacterAgeManager.TransistionUp();
	}
	
	public void ShiftDownAge(){
		CharacterAgeManager.TransistionDown();
	}

	public bool CanAgeTransitionDown(){
		return (CharacterAgeManager.GetCurrentAgeState() != CharacterAgeState.YOUNG);
	}
	
	public bool CanAgeTransitionUp(){
		return (CharacterAgeManager.GetCurrentAgeState() != CharacterAgeState.OLD);
	}
	#endregion
	
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
		CharacterAgeManager.SetAgeStart(initialAge);
		if (initialAge != CharacterAgeState.YOUNG){
			MovePlayerToRightAge(initialAge);
		}
	}
}

public enum CharacterGender{
	MALE = 0,
	FEMALE = 1,
}
