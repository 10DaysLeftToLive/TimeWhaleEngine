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
    private static float delay = 0.02f;

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
        Strings.CURRENTFADING = AreaName;

        AudioSource CurrentSong;
        AudioSource CurrentAmbient;
        float fTimeCounter = 0f;

        switch (AreaName)
        {
            case "Forest":
                CurrentSong = CrossfadeInstance.ForestBGM;
                CurrentAmbient = CrossfadeInstance.ForestAmbient;
                delay = 0.075f;
                break;
            case "Beach":
                CurrentSong = CrossfadeInstance.BeachBGM;
                CurrentAmbient = CrossfadeInstance.BeachAmbient;
                delay = 0.075f;
                break;
            case "Lighthouse":
                CurrentSong = CrossfadeInstance.LighthouseBGM;
                CurrentAmbient = CrossfadeInstance.LighthouseAmbient;
                delay = 0.1f;
                break;
            case "Windmill":
                CurrentSong = CrossfadeInstance.WindmillBGM;
                CurrentAmbient = CrossfadeInstance.WindmillAmbient;
                delay = 0.02f;
                break;
            case "Market":
                CurrentSong = CrossfadeInstance.MarketBGM;
                CurrentAmbient = CrossfadeInstance.MarketAmbient;
                delay = 0.1f;
                break;
            case "ReflectionTree":
                CurrentSong = null;
                CurrentAmbient = CrossfadeInstance.LighthouseAmbient;
                delay = 0.1f;
                break;
            default:
                CurrentSong = null;
                CurrentAmbient = null;
                break;
        }

        if (CurrentSong != null && CurrentAmbient != null && SoundManager.instance.SFXOn && SoundManager.instance.BGMOn)
        {
            // 1f will need to be changed to match the final volume used
            while (!(Mathf.Approximately(fTimeCounter, 1f)))
            {
                fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
                CurrentSong.volume = (1f - fTimeCounter) * CurrentSong.volume;
                CurrentAmbient.volume = (1f - fTimeCounter) * CurrentAmbient.volume;
                yield return new WaitForSeconds(delay);
            }

            CurrentSong.Stop();
            CurrentAmbient.Stop();
        }
        else if (CurrentSong != null && CurrentAmbient != null && !SoundManager.instance.SFXOn && SoundManager.instance.BGMOn)
        {
            // 1f will need to be changed to match the final volume used
            while (!(Mathf.Approximately(fTimeCounter, 1f)))
            {
                fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
                CurrentSong.volume = (1f - fTimeCounter) * CurrentSong.volume;
                yield return new WaitForSeconds(delay);
            }

            CurrentSong.Stop();
        }
        else if (CurrentSong != null && CurrentAmbient != null && SoundManager.instance.SFXOn && !SoundManager.instance.BGMOn)
        {
            // 1f will need to be changed to match the final volume used
            while (!(Mathf.Approximately(fTimeCounter, 1f)))
            {
                fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
                CurrentAmbient.volume = (1f - fTimeCounter) * CurrentAmbient.volume;
                yield return new WaitForSeconds(delay);
            }

            CurrentAmbient.Stop();
        }
        else if (CurrentSong == null && CurrentAmbient != null && SoundManager.instance.SFXOn)
        {
            // 1f will need to be changed to match the final volume used
            while (!(Mathf.Approximately(fTimeCounter, 1f)))
            {
                fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
                CurrentAmbient.volume = (1f - fTimeCounter) * CurrentAmbient.volume;
                yield return new WaitForSeconds(delay);
            }

            CurrentAmbient.Stop();
        }

        Crossfade.CrossfadeInstance.StopCoroutine("CoroutineFadeDown");
    }

    public static IEnumerator CoroutineFadeUp(string AreaName)
    {
        delay = 0.02f;

        Strings.CURRENTFADING = AreaName;

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
            case "ReflectionTree":
                CurrentSong = null;
                CurrentAmbient = CrossfadeInstance.LighthouseAmbient;
                break;
            default:
                CurrentSong = null;
                CurrentAmbient = null;
                break;
        }

        if (CurrentSong != null && SoundManager.instance.BGMOn)
        {
            CurrentSong.volume = 0;
            CurrentSong.Play();
        }
        if (CurrentAmbient != null && SoundManager.instance.SFXOn)
        {
            CurrentAmbient.volume = 0;
            CurrentAmbient.Play();
        }

        if (CurrentSong != null && CurrentAmbient != null && SoundManager.instance.SFXOn && SoundManager.instance.BGMOn)
        {
            // 1f will need to be changed to match the final volume used
            while (!(Mathf.Approximately(fTimeCounter, 1f)))
            {
                fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
                CurrentSong.volume = fTimeCounter;
                CurrentAmbient.volume = fTimeCounter;
                yield return new WaitForSeconds(delay);
            }
        }
        else if (CurrentSong != null && CurrentAmbient != null && !SoundManager.instance.SFXOn && SoundManager.instance.BGMOn)
        {
            // 1f will need to be changed to match the final volume used
            while (!(Mathf.Approximately(fTimeCounter, 1f)))
            {
                fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
                CurrentSong.volume = fTimeCounter;
                yield return new WaitForSeconds(delay);
            }
        }
        else if (CurrentSong != null && CurrentAmbient != null && SoundManager.instance.SFXOn && !SoundManager.instance.BGMOn)
        {
            // 1f will need to be changed to match the final volume used
            while (!(Mathf.Approximately(fTimeCounter, 1f)))
            {
                fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
                CurrentAmbient.volume = fTimeCounter;
                yield return new WaitForSeconds(delay);
            }
        }
        else if (CurrentSong == null && CurrentAmbient != null && SoundManager.instance.SFXOn)
        {
            while (!(Mathf.Approximately(fTimeCounter, 1f)))
            {
                fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
                CurrentAmbient.volume = fTimeCounter;
                yield return new WaitForSeconds(delay);
            }
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