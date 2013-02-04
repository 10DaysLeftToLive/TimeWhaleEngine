using UnityEngine;
using System.Collections;

public class GenderSelect : MonoBehaviour {
	public void PickMale(){
		PlayerPrefs.SetFloat(Strings.Gender, (float) LevelManager.CharacterGender.MALE);
		StartMainMenu();
	}
	
	public void PickFemale(){
		PlayerPrefs.SetFloat(Strings.Gender, (float) LevelManager.CharacterGender.FEMALE);
		StartMainMenu();
	}
	
	private void StartMainMenu(){
		Application.LoadLevel(Strings.Level1);
	}
}
