using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crossfade: MonoBehaviour {	

	public AudioSource ForestBGM;
	public AudioSource BeachBGM;
	public AudioSource CliffBGM;
	public AudioSource LighthouseBGM;
	public AudioSource TreeBGM;
	public AudioSource MarketBGM;

	public static void FadeBetween()
	{
		string Current = null;
		string Next = null;

		Crossfade.CrossfadeInstance.StartCoroutine(CoroutineFadeOverTime(Current, Next));
	}

	static public Crossfade CrossfadeInstance;
	
	void Awake()
	{
		Crossfade.CrossfadeInstance = this;
	}

	public static IEnumerator CoroutineFadeOverTime(string Current, string Next)
	{

		AudioSource CurrentSong;
		AudioSource NextSong;

		switch (Current)
		{
			case "Forest":
				CurrentSong = CrossfadeInstance.ForestBGM;
				break;
			case "Beach":
				CurrentSong = CrossfadeInstance.BeachBGM;
				break;
			case "Cliff":
				CurrentSong = CrossfadeInstance.CliffBGM;
				break;
			case "Light":
				CurrentSong = CrossfadeInstance.LighthouseBGM;
				break;
			case "Tree":
				CurrentSong = CrossfadeInstance.TreeBGM;
				break;
			case "Market":
				CurrentSong = CrossfadeInstance.MarketBGM;
				break;
			default:
				CurrentSong = null;
				break;
		}

		switch (Next)
		{
			case "Forest":
				NextSong = CrossfadeInstance.ForestBGM;
				break;
			case "Beach":
				NextSong = CrossfadeInstance.BeachBGM;
				break;
			case "Cliff":
				NextSong = CrossfadeInstance.CliffBGM;
				break;
			case "Light":
				NextSong = CrossfadeInstance.LighthouseBGM;
				break;
			case "Tree":
				NextSong = CrossfadeInstance.TreeBGM;
				break;
			case "Market":
				NextSong = CrossfadeInstance.MarketBGM;
				break;
			default:
				NextSong = null;
				break;
		}

		float fTimeCounter = 0f;

		NextSong.volume = 0;
		NextSong.Play();
		
		while(!(Mathf.Approximately(fTimeCounter, 1f)))
		{
			fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
			CurrentSong.volume = 1f - fTimeCounter;
			NextSong.volume = fTimeCounter;
			yield return new WaitForSeconds(0.02f);
		}
		
		CurrentSong.Stop();
		
		
		Crossfade.CrossfadeInstance.StopCoroutine("CoroutineFadeOverTime");
	}
}