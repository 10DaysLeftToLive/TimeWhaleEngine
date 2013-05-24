using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crossfade: MonoBehaviour {

    public AudioSource BeachBGM;
    public AudioSource BeachAmbient;
    public AudioSource ForestBGM;
    public AudioSource ForestAmbient;
    public AudioSource MarketBGM;
    public AudioSource MarketAmbient;
    public AudioSource WindmillBGM;
    public AudioSource WindmillAmbient;
    public AudioSource LighthouseBGM;
    public AudioSource LighthouseAmbient;

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

    public static IEnumerator CoroutineFadeDown(string AreaName)
    {
        AudioSource CurrentSong;
        AudioSource CurrentAmbient;
        float fTimeCounter = 0f;

        switch (AreaName)
        {
            case "Forest":
                CurrentSong = CrossfadeInstance.ForestBGM;
                CurrentAmbient = CrossfadeInstance.ForestAmbient;
                break;
            case "Beach":
                CurrentSong = CrossfadeInstance.BeachBGM;
                CurrentAmbient = CrossfadeInstance.BeachAmbient;
                break;
            case "Lighthouse":
                CurrentSong = CrossfadeInstance.LighthouseBGM;
                CurrentAmbient = CrossfadeInstance.LighthouseAmbient;
                break;
            case "Windmill":
                CurrentSong = CrossfadeInstance.WindmillBGM;
                CurrentAmbient = CrossfadeInstance.WindmillAmbient;
                break;
            case "Market":
                CurrentSong = CrossfadeInstance.MarketBGM;
                CurrentAmbient = CrossfadeInstance.MarketAmbient;
                break;
            case "Tree":
                CurrentSong = null;
                CurrentAmbient = null;
                break;
            default:
                CurrentSong = null;
                CurrentAmbient = null;
                break;
        }
        
        // 1f will need to be changed to match the final volume used
        while (!(Mathf.Approximately(fTimeCounter, 1f)))
        {
            fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
            CurrentSong.volume = (1f - fTimeCounter) * CurrentSong.volume;
            CurrentAmbient.volume = (1f - fTimeCounter) * CurrentAmbient.volume;
            yield return new WaitForSeconds(0.05f);
        }

        CurrentSong.Stop();
        CurrentAmbient.Stop();

        Crossfade.CrossfadeInstance.StopCoroutine("CoroutineFadeDown");
    }

    public static IEnumerator CoroutineFadeUp(string AreaName)
    {
        AudioSource CurrentSong;
        AudioSource CurrentAmbient;
        float fTimeCounter = 0f;

        switch (AreaName)
        {
            case "Forest":
                CurrentSong = CrossfadeInstance.ForestBGM;
                CurrentAmbient = CrossfadeInstance.ForestAmbient;
                break;
            case "Beach":
                CurrentSong = CrossfadeInstance.BeachBGM;
                CurrentAmbient = CrossfadeInstance.BeachAmbient;
                break;
            case "Lighthouse":
                CurrentSong = CrossfadeInstance.LighthouseBGM;
                CurrentAmbient = CrossfadeInstance.LighthouseAmbient;
                break;
            case "Windmill":
                CurrentSong = CrossfadeInstance.WindmillBGM;
                CurrentAmbient = CrossfadeInstance.WindmillAmbient;
                break;
            case "Market":
                CurrentSong = CrossfadeInstance.MarketBGM;
                CurrentAmbient = CrossfadeInstance.MarketAmbient;
                break;
            case "Tree":
                CurrentSong = null;
                CurrentAmbient = null;
                break;
            default:
                CurrentSong = null;
                CurrentAmbient = null;
                break;
        }

        CurrentSong.volume = 0;
        CurrentSong.Play();
        CurrentAmbient.volume = 0;
        CurrentAmbient.Play();

        // 1f will need to be changed to match the final volume used
        while (!(Mathf.Approximately(fTimeCounter, 1f)))
        {
            fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
            CurrentSong.volume = fTimeCounter;
            CurrentAmbient.volume = fTimeCounter;
            yield return new WaitForSeconds(0.05f);
        }

        Crossfade.CrossfadeInstance.StopCoroutine("CoroutineFadeUp");
    }

    public static IEnumerator CoroutineFadeOverTime(string Current, string Next)
    {

        AudioSource CurrentSong;
        AudioSource CurrentAmbient;
        AudioSource NextSong;
        AudioSource NextAmbient;

        switch (Current)
        {
            case "Forest":
                CurrentSong = CrossfadeInstance.ForestBGM;
                CurrentAmbient = CrossfadeInstance.ForestAmbient;
                break;
            case "Beach":
                CurrentSong = CrossfadeInstance.BeachBGM;
                CurrentAmbient = CrossfadeInstance.BeachAmbient;
                break;
            case "Cliff":
                CurrentSong = CrossfadeInstance.LighthouseBGM;
                CurrentAmbient = CrossfadeInstance.LighthouseAmbient;
                break;
            case "Windmill":
                CurrentSong = CrossfadeInstance.WindmillBGM;
                CurrentAmbient = CrossfadeInstance.WindmillAmbient;
                break;
            case "Market":
                CurrentSong = CrossfadeInstance.MarketBGM;
                CurrentAmbient = CrossfadeInstance.MarketAmbient;
                break;
            default:
                CurrentSong = null;
                CurrentAmbient = null;
                break;
        }

        switch (Next)
        {
            case "Forest":
                NextSong = CrossfadeInstance.ForestBGM;
                NextAmbient = CrossfadeInstance.ForestAmbient;
                break;
            case "Beach":
                NextSong = CrossfadeInstance.BeachBGM;
                NextAmbient = CrossfadeInstance.BeachAmbient;
                break;
            case "Cliff":
                NextSong = CrossfadeInstance.LighthouseBGM;
                NextAmbient = CrossfadeInstance.LighthouseAmbient;
                break;
            case "Windmill":
                NextSong = CrossfadeInstance.WindmillBGM;
                NextAmbient = CrossfadeInstance.WindmillAmbient;
                break;
            case "Market":
                NextSong = CrossfadeInstance.MarketBGM;
                NextAmbient = CrossfadeInstance.MarketAmbient;
                break;
            default:
                NextSong = null;
                NextAmbient = null;
                break;
        }

        float fTimeCounter = 0f;

        NextSong.volume = 0;
        NextSong.Play();
        NextAmbient.volume = 0;
        NextAmbient.Play();
        
        while(!(Mathf.Approximately(fTimeCounter, 1f)))
        {
            fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
            CurrentSong.volume = 1f - fTimeCounter;
            CurrentAmbient.volume = 1f - fTimeCounter;
            NextSong.volume = fTimeCounter;
            NextAmbient.volume = fTimeCounter;
            yield return new WaitForSeconds(0.02f);
        }

        /*while (!(Mathf.Approximately(fTimeCounter, 1f)))
        {
            fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
            NextSong.volume = fTimeCounter;
            NextAmbient.volume = fTimeCounter;
            yield return new WaitForSeconds(0.02f);
        }*/
        
        CurrentSong.Stop();
        CurrentAmbient.Stop();
        
        Crossfade.CrossfadeInstance.StopCoroutine("CoroutineFadeOverTime");
    }
}