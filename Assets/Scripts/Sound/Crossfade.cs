
using UnityEngine;
using System.Collections;

public class Crossfade: MonoBehaviour {	
	public static void FadeBetween(CharacterAge current, CharacterAge next){
		Crossfade.CrossfadeInstance.StartCoroutine(CoroutineFadeOverTime(current.backgroundMusic, next.backgroundMusic));
	}

	static public Crossfade CrossfadeInstance;
	
	void Awake(){
		Crossfade.CrossfadeInstance = this;
	}
	
	public static IEnumerator CoroutineFadeOverTime(AudioSource current, AudioSource next){
		float fTimeCounter = 0f;
		
		next.timeSamples = current.timeSamples;
		next.Play();
		
	    while(!(Mathf.Approximately(fTimeCounter, 1f)))
	    {
    	   	fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
       		current.volume = 1f - fTimeCounter;
       		next.volume = fTimeCounter;
       		yield return new WaitForSeconds(0.02f);
    	}
		
		current.Stop();
		
		
    	Crossfade.CrossfadeInstance.StopCoroutine("CoroutineFadeOverTime");
	}
}