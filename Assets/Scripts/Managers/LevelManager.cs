using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Level manager will handle the initialization of the game and will hold the sections of the map
/// </summary>
public class LevelManager : MonoBehaviour {
	public static float levelYOffSetFromCenter = 100;
	public CharacterAgeState initialAge;
	
	private ParallaxManager parallaxManager;
	private CloudManager cloudManager;
	
	//Make these private later: Also assign these targets based off prior choices / dispositions
	private static Transform youngSectionTarget;
	private static Transform middleSectionTarget;
	private static Transform oldSectionTarget;
	
	public LevelLoader levelLoader;
	
	public PlayerAnimationContainer[] genderAnimations;
	public static CharacterGender playerGender = CharacterGender.FEMALE;
	private PlayerAnimationContainer genderAnimationInUse;
	private PlayerAnimationContainer siblingGenderAnimations;
	private Player playerCharacter;
	
	void Awake(){
		playerGender = (PlayerPrefs.GetFloat(Strings.Gender) == ((float)CharacterGender.MALE) ? CharacterGender.MALE : CharacterGender.FEMALE);
		
		playerCharacter = GameObject.Find(Strings.Player).GetComponent<Player>();
		CharacterAgeManager.SetPlayer(playerCharacter);
		
		SetGender(playerGender);
		SetupInitialAgeSettings();
		playerCharacter.EnterState(new IdleState(playerCharacter));
	}
	
	void Start () {
		
		
		parallaxManager = GameObject.Find(Strings.PARALLAXMANAGER).GetComponent<ParallaxManager>();
		cloudManager = GameObject.Find(Strings.CLOUDMANAGER).GetComponent<CloudManager>();
		ScreenSetup.CalculateSettings();
		StartCoroutine(Init());
	}
	
	private IEnumerator Init(){
		GUIManager.Instance.ShowLoadingScreen();
		StartCoroutine(levelLoader.Load("LevelYoung", "LevelMiddle", "LevelOld"));
		while (levelLoader.HasNotFinished()){ // wait untill the outside scenes have been loaded in.
			yield return new WaitForSeconds(.1f);
		}
		FindSections();
		SpreadSections();
		SetUpAges();
		
		
		SetSiblingAnimations(siblingGenderAnimations);
		
		NPCManager.instance.Init();
		FlagManager.instance.Init();
		ManagerLoader.LoadManagers(youngSectionTarget, middleSectionTarget, oldSectionTarget);
		
		parallaxManager.Init();
		cloudManager.Init();
		
		if (initialAge != CharacterAgeState.YOUNG){
			MovePlayerToRightAge(initialAge);
		}
		GUIManager.Instance.StartFadingLoadingScreen();
		GUIManager.Instance.AddInGameMenu();
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
	
	public static WayPoints[] GetCurrentAgeWaypoints(){
		if (CharacterAgeManager.currentAge == CharacterAgeState.YOUNG){
			return (youngSectionTarget.GetComponentsInChildren<WayPoints>());
		} else if (CharacterAgeManager.currentAge == CharacterAgeState.MIDDLE){
			return (middleSectionTarget.GetComponentsInChildren<WayPoints>());
		} else {
			return (oldSectionTarget.GetComponentsInChildren<WayPoints>());
		}
	}
	
	/// <summary>
	/// Sets the gender. And will destroy the other gender data to save space
	/// </summary>
	private void SetGender(CharacterGender gender){
		GameObject[] maleAnimations = GetMaleAnimations();
		GameObject[] femaleAnimations = GetFemaleAnimations();
		
		switch(gender){
			case CharacterGender.MALE:
				genderAnimationInUse = genderAnimations[(int)CharacterGender.MALE];
				siblingGenderAnimations = genderAnimations[(int)CharacterGender.FEMALE];
				DisableAnimations(maleAnimations);
				DisableAnimations(femaleAnimations);
				break;
			case CharacterGender.FEMALE:
				genderAnimationInUse = genderAnimations[(int)CharacterGender.FEMALE];
				siblingGenderAnimations = genderAnimations[(int)CharacterGender.MALE];
				DisableAnimations(femaleAnimations);
				DisableAnimations(maleAnimations);
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
	
	/// <summary>
	/// Sets the sibling animations by destroying what the sibling has then replacing it
	/// </summary>
	private void SetSiblingAnimations(PlayerAnimationContainer genderAnimation){
		Sibling youngSibling = GameObject.Find(StringsNPC.SiblingYoung).GetComponent<Sibling>();
		Sibling middleSibling = GameObject.Find(StringsNPC.SiblingMiddle).GetComponent<Sibling>();
		Sibling oldSibling = GameObject.Find(StringsNPC.SiblingOld).GetComponent<Sibling>();
		
		string gender = (playerGender == CharacterGender.MALE ? "Female" : "Male");
		
		youngSibling.ChangeGender(gender, genderAnimation.youngBoneAnimation);
		middleSibling.ChangeGender(gender, genderAnimation.middleBoneAnimation);
		oldSibling.ChangeGender(gender, genderAnimation.oldBoneAnimation);
		/*
		SmoothMoves.BoneAnimation youngSiblingAnimation = youngSibling.GetComponent<SmoothMoves.BoneAnimation>();
		SmoothMoves.BoneAnimation middleSiblingAnimation = middleSibling.GetComponent<SmoothMoves.BoneAnimation>();
		SmoothMoves.BoneAnimation oldSiblingAnimation = oldSibling.GetComponent<SmoothMoves.BoneAnimation>();
		
		for (int i = 0; i < youngSiblingAnimation.GetClipCount(); i++){
			youngSiblingAnimation.RemoveClip(youngSiblingAnimation[youngSiblingAnimation.GetAnimationClipName(i)].clip);
			middleSiblingAnimation.RemoveClip(middleSiblingAnimation[middleSiblingAnimation.GetAnimationClipName(i)].clip);
			oldSiblingAnimation.RemoveClip(oldSiblingAnimation[oldSiblingAnimation.GetAnimationClipName(i)].clip);
		}
		
		youngSibling.animationData = genderAnimation.youngBoneAnimation;
		middleSibling.animationData = genderAnimation.middleBoneAnimation;
		oldSibling.animationData = genderAnimation.oldBoneAnimation;
		
		for (int i = 0; i < genderAnimation.middleBoneAnimation.GetClipCount(); i++){
			string clipName = genderAnimation.youngBoneAnimation.GetAnimationClipName(i);
			youngSiblingAnimation.AddClip(genderAnimation.youngBoneAnimation[clipName].clip, clipName); 
			Debug.Log("clip = " + clipName);
			
			clipName = genderAnimation.middleBoneAnimation.GetAnimationClipName(i);
			middleSiblingAnimation.AddClip(genderAnimation.middleBoneAnimation[clipName].clip, clipName);
			
			clipName = genderAnimation.oldBoneAnimation.GetAnimationClipName(i);
			oldSiblingAnimation.AddClip(genderAnimation.oldBoneAnimation[clipName].clip, clipName); 
		}
		
		
		/*
		genderAnimation.youngBoneAnimation.transform.position = youngSibling.transform.position;
		SiblingYoung newSiblingYoung = genderAnimation.youngBoneAnimation.transform.gameObject.AddComponent<SiblingYoung>();
		newSiblingYoung.Init();
		newSiblingYoung.tag = Strings.tag_NPC;
		Collider tempCollider = newSiblingYoung.gameObject.AddComponent<BoxCollider>();
		tempCollider = youngSibling.collider;
		tempCollider.isTrigger = true;
		newSiblingYoung.animationData = genderAnimation.youngBoneAnimation;
		newSiblingYoung.SpriteLookingLeft = youngSibling.SpriteLookingLeft;
		genderAnimation.youngBoneAnimation.transform.parent = youngSibling.transform.parent;
		
		genderAnimation.middleBoneAnimation.transform.name = middleSibling.name;
		genderAnimation.middleBoneAnimation.transform.position = middleSibling.transform.position;
		SiblingMiddle newSiblingMiddle = genderAnimation.middleBoneAnimation.transform.gameObject.AddComponent<SiblingMiddle>();
		newSiblingMiddle.Init();
		newSiblingMiddle.tag = Strings.tag_NPC;
		tempCollider = newSiblingMiddle.gameObject.AddComponent<BoxCollider>();
		tempCollider.isTrigger = true;
		tempCollider = middleSibling.collider;
		newSiblingMiddle.animationData = genderAnimation.middleBoneAnimation;
		newSiblingMiddle.SpriteLookingLeft = middleSibling.SpriteLookingLeft;
		genderAnimation.middleBoneAnimation.transform.parent = middleSibling.transform.parent;
		
		genderAnimation.oldBoneAnimation.transform.name = oldSibling.name;
		genderAnimation.oldBoneAnimation.transform.position = oldSibling.transform.position;
		SiblingOld newSiblingOld = genderAnimation.oldBoneAnimation.transform.gameObject.AddComponent<SiblingOld>();
		newSiblingOld.Init();
		newSiblingOld.tag = Strings.tag_NPC;
		tempCollider = newSiblingOld.gameObject.AddComponent<BoxCollider>();
		tempCollider.isTrigger = true;
		newSiblingOld.gameObject.collider = tempCollider;
		newSiblingOld.animationData = genderAnimation.oldBoneAnimation;
		newSiblingOld.SpriteLookingLeft = oldSibling.SpriteLookingLeft;
		genderAnimation.oldBoneAnimation.transform.parent = oldSibling.transform.parent;
		
		Destroy(youngSibling.gameObject);
		Destroy(middleSibling.gameObject);
		Destroy(oldSibling.gameObject);*/
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