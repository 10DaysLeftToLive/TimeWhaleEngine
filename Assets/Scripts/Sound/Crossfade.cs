
using UnityEngine;
using System.Collections;

public class Crossfade: MonoBehaviour {	
	public static void FadeBetween(CharacterAge current, CharacterAge next){
	//public static void FadeBetween(currentSong, nextSong, Time transitionTime){
		//Crossfade.CrossfadeInstance.StartCoroutine(CoroutineFadeOverTime());
	}

	static public Crossfade CrossfadeInstance;
	
	void Awake(){
		Crossfade.CrossfadeInstance = this;
	}
	
	public static IEnumerator CoroutineFadeOverTime(AudioSource current, AudioSource next){
	//public static IEnumerator CoroutineFadeOverTime(AudioSource current, AudioSource next, Time transition){
	// Time transition might not be necessary if we do multiple transition effects at once, then we can just call this along side other, for example, visual effects.
		float fTimeCounter = 0f;

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