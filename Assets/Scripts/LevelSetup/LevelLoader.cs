using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour{
	private bool _hasLoaded = false;
	
	public IEnumerator Load(string youngeAge, string middleAge, string oldAge){
		yield return StartCoroutine(LoadAges(youngeAge, middleAge, oldAge));
	}
	
	public bool HasLoaded(){
		return (_hasLoaded);
	}
	
	private IEnumerator LoadAges(string youngeAge, string middleAge, string oldAge){
		StartCoroutine (LoadAge(youngeAge));
		StartCoroutine (LoadAge(middleAge));
		StartCoroutine (LoadAge(oldAge));
		yield return null; // wait for a tick because unity is dumb
		_hasLoaded = true;		
	}
	
	private IEnumerator LoadAge(string age){
		yield return null; // wait for a tick because unity is dumb
		Application.LoadLevelAdditive(age);		
	}
}