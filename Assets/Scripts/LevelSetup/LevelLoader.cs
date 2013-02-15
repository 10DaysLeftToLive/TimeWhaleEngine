using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour{
	private bool _hasLoaded = false;
	
	public void Load(string youngeAge, string middleAge, string oldAge){
		StartCoroutine(LoadAges(youngeAge, middleAge, oldAge));
	}
	
	public bool HasLoaded(){
		return (_hasLoaded);
	}
	
	private IEnumerator LoadAges(string youngeAge, string middleAge, string oldAge){
		StartCoroutine (LoadAge(youngeAge));
		StartCoroutine (LoadAge(middleAge));
		StartCoroutine (LoadAge(oldAge));
		yield return null;
		_hasLoaded = true;		
	}
	
	private IEnumerator LoadAge(string age){
		yield return null; 
		Application.LoadLevelAdditive(age);		
	}
}
