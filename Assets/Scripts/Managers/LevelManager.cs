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
	private Player playerCharacter;
	
	void Awake(){
		playerCharacter = GameObject.Find(Strings.Player).GetComponent<Player>();
		CharacterAgeManager.SetPlayer(playerCharacter);
		
		SetGender(playerGender);
		SetupInitialAgeSettings();
		playerCharacter.EnterState(new IdleState(playerCharacter));
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
		
		if (initialAge != CharacterAgeState.YOUNG){
			MovePlayerToRightAge(initialAge);
		}
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
		if (CharacterAgeManager.GetAgeOf(age) == null) Debug.LogError("hai");
		AgeSwapMover.instance.ChangeAgePosition(CharacterAgeManager.GetAgeOf(age), 
			       		      CharacterAgeManager.GetAgeOf(CharacterAgeState.YOUNG)); // we start at the young int he scene
	}
	
	#region AgeTransition
	public void ShiftUpAge(){
		if (CanAgeTransitionUp()) {
			CharacterAgeManager.TransistionUp();
		}
	}
	
	public void ShiftDownAge(){
		if (CanAgeTransitionDown()) {
			CharacterAgeManager.TransistionDown();
		}
	}

	public bool CanAgeTransitionDown(){
		return (CharacterAgeManager.GetCurrentAgeState() != CharacterAgeState.YOUNG);
	}
	
	public bool CanAgeTransitionUp(){
		return (CharacterAgeManager.GetCurrentAgeState() != CharacterAgeState.OLD);
	}
	#endregion
	
	/// <summary>
	/// Sets the gender. And will destroy the other gender data to save space
	/// </summary>
	private void SetGender(CharacterGender gender){
		GameObject[] maleAnimations = GetMaleAnimations();
		GameObject[] femaleAnimations = GetFemaleAnimations();
		
		switch(gender){
			case CharacterGender.MALE:
				genderAnimationInUse = genderAnimations[(int)CharacterGender.MALE];
				DestroyAnimations(femaleAnimations);
				DisableAnimations(maleAnimations);
				break;
			case CharacterGender.FEMALE:
				genderAnimationInUse = genderAnimations[(int)CharacterGender.FEMALE];
				DestroyAnimations(maleAnimations);
				DisableAnimations(femaleAnimations);
				break;		
		}
	}
	
	private GameObject[] GetFemaleAnimations(){
		GameObject[] femaleAnimations = new GameObject[3];
		femaleAnimations[0] = GameObject.Find("FemaleYoungAnimation");
		femaleAnimations[1] = GameObject.Find("FemaleMiddleAnimation");
		femaleAnimations[2] = GameObject.Find("FemaleOldAnimation");
		return (femaleAnimations);
	}
	
	private GameObject[] GetMaleAnimations(){
		GameObject[] maleAnimations = new GameObject[3];
		maleAnimations[0] = GameObject.Find("MaleYoungAnimation");
		maleAnimations[1] = GameObject.Find("MaleMiddleAnimation");
		maleAnimations[2] = GameObject.Find("MaleOldAnimation");
		return (maleAnimations);
	}
	
	private void SetupInitialAgeSettings(){
		if (initialAge != CharacterAgeState.YOUNG){
			if (initialAge == CharacterAgeState.MIDDLE){
				playerCharacter.ChangeAnimation(genderAnimationInUse.middleBoneAnimation);
			} else {
				playerCharacter.ChangeAnimation(genderAnimationInUse.oldBoneAnimation);
			}
		} else {
			playerCharacter.ChangeAnimation(genderAnimationInUse.youngBoneAnimation);
		}
	}
	
	private void DestroyAnimations(GameObject[] animationArray){
		for (int i = 0; i < 3; i++){
			Destroy(animationArray[i]);
		}
	}
	
	private void DisableAnimations(GameObject[] animationArray){
		for (int i = 0; i < 3; i++){
			Utils.SetActiveRecursively(animationArray[i].gameObject, false);
		}
	}
	
	private void SetUpAges(){
		CharacterAgeManager.SetupYoung(genderAnimationInUse.youngBoneAnimation, youngSectionTarget);
		CharacterAgeManager.SetupMiddle(genderAnimationInUse.middleBoneAnimation, middleSectionTarget);
		CharacterAgeManager.SetupOld(genderAnimationInUse.oldBoneAnimation, oldSectionTarget);
		CharacterAgeManager.SetAgeStart(initialAge);
	}
}

public enum CharacterGender{
	MALE = 0,
	FEMALE = 1,
}