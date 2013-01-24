using UnityEngine;
using System.Collections;

public class Crossfade: MonoBehaviour {

	public AudioSource youngBGM;
	public AudioSource middleBGM;
	public AudioSource oldBGM;
	
	/// Crossfading music when switching ages
	/// adapted from Dreamblur's answer at http://answers.unity3d.com/questions/132174/audio-crossfade-on-single-key-press.html
	/*public static void CrossFadeMusic(AudioSource musicFadeIn, AudioSource musicFadeOut)
	{
	    float fTimeCounter = 0f;
		float timer = Time.time + 0.02f;
		
		musicFadeIn.timeSamples = musicFadeOut.timeSamples;
		musicFadeIn.Play();
		
    	while(!(Mathf.Approximately(fTimeCounter, 1f)))
    	{
    	   	fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
	       	musicFadeOut.volume = 1f - fTimeCounter;
       		musicFadeIn.volume = fTimeCounter;
       		yield return new WaitForSeconds(0.02f);
			
	    }
		
		musicFadeOut.Stop();
	}*/
	
	static public Crossfade CrossfadeInstance;
	
	void Awake()
	{
		Crossfade.CrossfadeInstance = this;
	}
	
	public static void YoungToMiddle()
	{
		Crossfade.CrossfadeInstance.StartCoroutine("CoroutineYoungToMiddle");
	}
	
	public static void MiddleToOld()
	{
		Crossfade.CrossfadeInstance.StartCoroutine("CoroutineMiddleToOld");
	}
	
	public static void OldToYoung()
	{
		Crossfade.CrossfadeInstance.StartCoroutine("CoroutineOldToYoung");
	}
	
	public static void OldToMiddle()
	{
		Crossfade.CrossfadeInstance.StartCoroutine("CoroutineOldToMiddle");
	}
	
	public static void MiddleToYoung()
	{
		Crossfade.CrossfadeInstance.StartCoroutine("CoroutineMiddleToYoung");
	}
	
	public static void YoungToOld()
	{
		Crossfade.CrossfadeInstance.StartCoroutine("CoroutineYoungToOld");
	}
	
	public IEnumerator CoroutineYoungToMiddle()
	{
    	float fTimeCounter = 0f;
		
		middleBGM.timeSamples = youngBGM.timeSamples;
		middleBGM.Play();
		
	    while(!(Mathf.Approximately(fTimeCounter, 1f)))
	    {
    	   	fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
       		youngBGM.volume = 1f - fTimeCounter;
       		middleBGM.volume = fTimeCounter;
       		yield return new WaitForSeconds(0.02f);
    	}
		
		youngBGM.Stop();
		
    	StopCoroutine("CoroutineYoungToMiddle");
	}
	
	public IEnumerator CoroutineMiddleToOld()
	{
    	float fTimeCounter = 0f;
		
		oldBGM.timeSamples = middleBGM.timeSamples;
		oldBGM.Play();
		
	    while(!(Mathf.Approximately(fTimeCounter, 1f)))
	    {
    	   	fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
       		middleBGM.volume = 1f - fTimeCounter;
       		oldBGM.volume = fTimeCounter;
       		yield return new WaitForSeconds(0.02f);
    	}
		
		middleBGM.Stop();
		
    	StopCoroutine("CoroutineMiddleToOld");
	}
	
	public IEnumerator CoroutineOldToYoung()
	{
    	float fTimeCounter = 0f;
		
		youngBGM.timeSamples = oldBGM.timeSamples;
		youngBGM.Play();
		
	    while(!(Mathf.Approximately(fTimeCounter, 1f)))
	    {
    	   	fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
       		oldBGM.volume = 1f - fTimeCounter;
       		youngBGM.volume = fTimeCounter;
       		yield return new WaitForSeconds(0.02f);
    	}
		
		oldBGM.Stop();
		
    	StopCoroutine("CoroutineOldToMiddle");
	}
	
	public IEnumerator CoroutineOldToMiddle()
	{
    	float fTimeCounter = 0f;
		
		middleBGM.timeSamples = oldBGM.timeSamples;
		middleBGM.Play();
		
	    while(!(Mathf.Approximately(fTimeCounter, 1f)))
	    {
    	   	fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
       		oldBGM.volume = 1f - fTimeCounter;
       		middleBGM.volume = fTimeCounter;
       		yield return new WaitForSeconds(0.02f);
    	}
		
		oldBGM.Stop();
		
    	StopCoroutine("CoroutineOldToMiddle");
	}
	
	public IEnumerator CoroutineMiddleToYoung()
	{
    	float fTimeCounter = 0f;
		
		youngBGM.timeSamples = middleBGM.timeSamples;
		youngBGM.Play();
		
	    while(!(Mathf.Approximately(fTimeCounter, 1f)))
	    {
    	   	fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
       		middleBGM.volume = 1f - fTimeCounter;
       		youngBGM.volume = fTimeCounter;
       		yield return new WaitForSeconds(0.02f);
    	}
		
		middleBGM.Stop();
		
    	StopCoroutine("CoroutineMiddleToYoung");
	}
	
	public IEnumerator CoroutineYoungToOld()
	{
    	float fTimeCounter = 0f;
		
		oldBGM.timeSamples = youngBGM.timeSamples;
		oldBGM.Play();
		
	    while(!(Mathf.Approximately(fTimeCounter, 1f)))
	    {
    	   	fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
       		youngBGM.volume = 1f - fTimeCounter;
       		oldBGM.volume = fTimeCounter;
       		yield return new WaitForSeconds(0.02f);
    	}
		
		youngBGM.Stop();
		
    	StopCoroutine("CoroutineYoungToMiddle");
	}
}