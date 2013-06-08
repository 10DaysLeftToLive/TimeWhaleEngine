using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crossfade : MonoBehaviour
{

    public AudioSource BeachBGM;
    public AudioSource BeachAmbient;
    public float BeachAmbientVolume = 0.25f;
    public AudioSource ForestBGM;
    public AudioSource ForestAmbient;
    public float ForestAmbientVolume = 0.5f;
    public AudioSource MarketBGM;
    public AudioSource MarketAmbient;
    public float MarketAmbientVolume = 0.5f;
    public AudioSource WindmillBGM;
    public AudioSource WindmillAmbient;
    public float WindmillAmbientVolume = 0.5f;
    public AudioSource LighthouseBGM;
    public AudioSource LighthouseAmbient;
    public float LighthouseAmbientVolume = 1.0f;
    public AudioSource ReflectionTreeAmbient;
    public float ReflectionTreeVolume = 0.5f;
    private static float delay = 0.01f;
    public bool FadeDown = false;
    public bool FadeUp = false;
    public bool CrossFade = false;

    static public Crossfade instance;

    void Awake()
    {
        Crossfade.instance = this;
    }

    public static IEnumerator CoroutineFadeDown()
    {
        instance.FadeDown = true;

        Strings.FADINGAREA = Strings.PREVIOUSAREA;

        //Debug.Log("CoroutineFadeDown start in " + Strings.FADINGAREA + ", FadeDown is " + instance.FadeDown);

        AudioSource CurrentSong;
        AudioSource CurrentAmbient;
        float fTimeCounter = 0f;

        switch (Strings.FADINGAREA)
        {
            case "Forest":
                CurrentSong = instance.ForestBGM;
                CurrentAmbient = instance.ForestAmbient;
                //delay = 0.03f;
                break;
            case "Beach":
                CurrentSong = instance.BeachBGM;
                CurrentAmbient = instance.BeachAmbient;
                //delay = 0.02f;
                break;
            case "Lighthouse":
                CurrentSong = instance.LighthouseBGM;
                CurrentAmbient = instance.LighthouseAmbient;
                //delay = 0.03f;
                break;
            case "Windmill":
                CurrentSong = instance.WindmillBGM;
                CurrentAmbient = instance.WindmillAmbient;
                //delay = 0.02f;
                break;
            case "Market":
                CurrentSong = instance.MarketBGM;
                CurrentAmbient = instance.MarketAmbient;
                //delay = 0.03f;
                break;
            case "ReflectionTree*":
                CurrentSong = null;
                CurrentAmbient = instance.ReflectionTreeAmbient;
                //delay = 0.04f;
                break;
            default:
                CurrentSong = null;
                CurrentAmbient = null;
                break;
        }

        if (CurrentSong != null && SoundManager.instance.BGMOn)
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
        /*else if (CurrentAmbient != null && SoundManager.instance.SFXOn)
        {
            // 1f will need to be changed to match the final volume used
            while (!(Mathf.Approximately(fTimeCounter, 1f)))
            {
                fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
                CurrentAmbient.volume = (1f - fTimeCounter) * CurrentAmbient.volume;
                yield return new WaitForSeconds(delay);
            }

            CurrentAmbient.Stop();
        }*/

        instance.FadeDown = false;

        //Debug.Log("CoroutineFadeDown end, FadeDown is " + instance.FadeDown);

        Crossfade.instance.StopCoroutine("CoroutineFadeDown");
    }

    public static IEnumerator CoroutineFadeUp()
    {
        instance.FadeUp = true;

        Strings.FADINGAREA = Strings.CURRENTAREA;

        //Debug.Log("CoroutineFadeUp start in "+Strings.FADINGAREA+", FadeUp is " + instance.FadeUp);

        //delay = 0.02f;

        AudioSource CurrentSong;
        AudioSource CurrentAmbient;
        float CurrentAmbientVolume;
        float fTimeCounter = 0f;

        switch (Strings.FADINGAREA)
        {
            case "Forest":
                CurrentSong = instance.ForestBGM;
                CurrentAmbient = instance.ForestAmbient;
                CurrentAmbientVolume = instance.ForestAmbientVolume;
                //delay = 0.03f;
                break;
            case "Beach":
                CurrentSong = instance.BeachBGM;
                CurrentAmbient = instance.BeachAmbient;
                CurrentAmbientVolume = instance.BeachAmbientVolume;
                //delay = 0.02f;
                break;
            case "Lighthouse":
                CurrentSong = instance.LighthouseBGM;
                CurrentAmbient = instance.LighthouseAmbient;
                CurrentAmbientVolume = instance.LighthouseAmbientVolume;
                //delay = 0.03f;
                break;
            case "Windmill":
                CurrentSong = instance.WindmillBGM;
                CurrentAmbient = instance.WindmillAmbient;
                CurrentAmbientVolume = instance.WindmillAmbientVolume;
                //delay = 0.02f;
                break;
            case "Market":
                CurrentSong = instance.MarketBGM;
                CurrentAmbient = instance.MarketAmbient;
                CurrentAmbientVolume = instance.MarketAmbientVolume;
                //delay = 0.03f;
                break;
            case "ReflectionTree":
                CurrentSong = null;
                CurrentAmbient = instance.ReflectionTreeAmbient;
                CurrentAmbientVolume = instance.ReflectionTreeVolume;
                //delay = 0.04f;
                break;
            default:
                CurrentSong = null;
                CurrentAmbient = null;
                CurrentAmbientVolume = 0;
                break;
        }

        if (CurrentSong != null && SoundManager.instance.BGMOn)
        {
            if (CurrentSong.volume != 0 && CurrentSong.isPlaying)
            {
                fTimeCounter = CurrentSong.volume;
            }
            else
            {
                CurrentSong.volume = 0;
                CurrentSong.Play();
            }
        }
        /*if (CurrentAmbient != null && SoundManager.instance.SFXOn)
        {
            if (CurrentAmbient.volume != 0)
            {
                fTimeCounter = CurrentAmbient.volume;
            }
            else
            {
                CurrentAmbient.volume = 0;
                CurrentAmbient.Play();
            }
        }*/

        if (CurrentSong != null && SoundManager.instance.BGMOn)
        {
            // 1f will need to be changed to match the final volume used
            while (!(Mathf.Approximately(fTimeCounter, 1f)))
            {
                fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
                CurrentSong.volume = fTimeCounter;
                yield return new WaitForSeconds(delay);
            }
        }
        /*else if (CurrentAmbient != null && SoundManager.instance.SFXOn)
        {
            // 1f will need to be changed to match the final volume used
            while (!(Mathf.Approximately(fTimeCounter, 1f)))
            {
                fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
                //CurrentAmbient.volume = (fTimeCounter * CurrentAmbientVolume);
                CurrentAmbient.volume = (fTimeCounter);
                yield return new WaitForSeconds(delay);
            }
        }*/

        instance.FadeUp = false;

        //Debug.Log("CoroutineFadeUp end, FadeUp is " + instance.FadeUp);

        Crossfade.instance.StopCoroutine("CoroutineFadeUp");
    }

    public static IEnumerator CoroutineFadeOverTime(string CurrentArea, string NextArea)
    {
        //Debug.Log("Fade Over Time, current is " + CurrentArea + ", next is " + NextArea);
        Strings.CROSSFADINGAREA = CurrentArea;
        instance.CrossFade = true;

        AudioSource CurrentSong;
        AudioSource CurrentAmbient;
        AudioSource NextSong;
        AudioSource NextAmbient;
        float CurrentAmbientVolume;
        float NextAmbientVolume;
        float fTimeCounter = 0f;
        //delay = 0.01f;

        switch (CurrentArea)
        {
            case "Forest":
                CurrentSong = instance.ForestBGM;
                CurrentAmbient = instance.ForestAmbient;
                CurrentAmbientVolume = instance.ForestAmbientVolume;
                break;
            case "Beach":
                CurrentSong = instance.BeachBGM;
                CurrentAmbient = instance.BeachAmbient;
                CurrentAmbientVolume = instance.BeachAmbientVolume;
                break;
            case "Lighthouse":
                CurrentSong = instance.LighthouseBGM;
                CurrentAmbient = instance.LighthouseAmbient;
                CurrentAmbientVolume = instance.LighthouseAmbientVolume;
                break;
            case "Windmill":
                CurrentSong = instance.WindmillBGM;
                CurrentAmbient = instance.WindmillAmbient;
                CurrentAmbientVolume = instance.WindmillAmbientVolume;
                break;
            case "Market":
                CurrentSong = instance.MarketBGM;
                CurrentAmbient = instance.MarketAmbient;
                CurrentAmbientVolume = instance.MarketAmbientVolume;
                break;
            case "ReflectionTree":
                CurrentSong = null;
                CurrentAmbient = instance.ReflectionTreeAmbient;
                CurrentAmbientVolume = instance.ReflectionTreeVolume;
                break;
            default:
                CurrentSong = null;
                CurrentAmbient = null;
                CurrentAmbientVolume = 0;
                break;
        }

        switch (NextArea)
        {
            case "Forest":
                NextSong = instance.ForestBGM;
                NextAmbient = instance.ForestAmbient;
                NextAmbientVolume = instance.ForestAmbientVolume;
                break;
            case "Beach":
                NextSong = instance.BeachBGM;
                NextAmbient = instance.BeachAmbient;
                NextAmbientVolume = instance.BeachAmbientVolume;
                break;
            case "Lighthouse":
                NextSong = instance.LighthouseBGM;
                NextAmbient = instance.LighthouseAmbient;
                NextAmbientVolume = instance.LighthouseAmbientVolume;
                break;
            case "Windmill":
                NextSong = instance.WindmillBGM;
                NextAmbient = instance.WindmillAmbient;
                NextAmbientVolume = instance.WindmillAmbientVolume;
                break;
            case "Market":
                NextSong = instance.MarketBGM;
                NextAmbient = instance.MarketAmbient;
                NextAmbientVolume = instance.MarketAmbientVolume;
                break;
            case "ReflectionTree":
                NextSong = null;
                NextAmbient = instance.ReflectionTreeAmbient;
                NextAmbientVolume = instance.ReflectionTreeVolume;
                break;
            default:
                NextSong = null;
                NextAmbient = null;
                NextAmbientVolume = 0;
                break;
        }

        /*if (NextSong != null && SoundManager.instance.BGMOn && AudioType == "BGM"){
            if (NextSong.volume != 0)
            {
                fTimeCounter = NextSong.volume;
            }
            else
            {
                NextSong.volume = 0;
                NextSong.Play();
            }
        }*/
        if (NextAmbient != null && SoundManager.instance.SFXOn){
            if (NextAmbient.volume != 0 && NextAmbient.isPlaying)
            {
                //Debug.Log("NextAmbient is not at 0 volume.");
                fTimeCounter = NextAmbient.volume;
            }
            else
            {
                //Debug.Log("NextAmbient is at 0 volume.");
                NextAmbient.volume = 0;
                NextAmbient.Play();
            }
        }

        /*while (!(Mathf.Approximately(fTimeCounter, 1f)))
        {
            fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
            CurrentSong.volume = 1f - fTimeCounter;
            CurrentAmbient.volume = 1f - fTimeCounter;
            NextSong.volume = fTimeCounter;
            NextAmbient.volume = fTimeCounter;
            yield return new WaitForSeconds(0.02f);
        }*/

        /*if (CurrentSong != null && SoundManager.instance.BGMOn && AudioType == "BGM")
        {
            // 1f will need to be changed to match the final volume used
            while (!(Mathf.Approximately(fTimeCounter, 1f)))
            {
                fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
                CurrentSong.volume = (1f - fTimeCounter) * CurrentSong.volume;
                NextSong.volume = fTimeCounter;
                yield return new WaitForSeconds(//delay);
            }
            CurrentSong.Stop();
        }
        else*/ if (CurrentAmbient != null && NextAmbient != null && SoundManager.instance.SFXOn)
        {
            // 1f will need to be changed to match the final volume used
            while (!(Mathf.Approximately(fTimeCounter, 1f)))
            {
                fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
                CurrentAmbient.volume = (1f - fTimeCounter) * CurrentAmbientVolume;
                NextAmbient.volume = (fTimeCounter * NextAmbientVolume);
                yield return new WaitForSeconds(delay);
            }
            CurrentAmbient.Stop();
        }

        instance.CrossFade = false;

        Crossfade.instance.StopCoroutine("CoroutineFadeOverTime");
    }

    public void StartCoroutineFadeDown()
    {
        if (SoundManager.instance.BGMOn)
        {
            if (Strings.FADINGAREA == Strings.CURRENTAREA && Crossfade.instance.FadeUp)
            {
                StopAllCoroutines();
                /*StopCoroutine("CoroutineFadeDown");
                Crossfade.instance.FadeUp = false;*/
            }
            StartCoroutine(Crossfade.CoroutineFadeDown());
            /*StartCoroutine("CoroutineFadeDown");
            yield return new WaitForSeconds(1);*/
        }
    }

    public void StartCoroutineFadeUp()
    {
        if (SoundManager.instance.BGMOn)
        {
            if (Strings.FADINGAREA == Strings.CURRENTAREA && Crossfade.instance.FadeDown)
            {
                StopAllCoroutines();
                /*StopCoroutine("CoroutineFadeUp");
                Crossfade.instance.FadeDown = false;*/

            }
            StartCoroutine(Crossfade.CoroutineFadeUp());
            //yield return StartCoroutine("CoroutineFadeUp");
        }
    }

    public void startCoroutineFadeOverTime(string Current, string Next)
    {
        if (SoundManager.instance.SFXOn)
        {
            if (Crossfade.instance.CrossFade)
            {
                if (Strings.CROSSFADINGAREA == Current)
                {
                    //Debug.Log("crossfade occuring, stopping current crossfade before starting reversed order");
                    StopAllCoroutines();
                    Crossfade.instance.CrossFade = false;
                    StartCoroutine(Crossfade.CoroutineFadeOverTime(Next, Current));
                }
                else
                {
                    //Debug.Log("crossfade occuring, stopping current crossfade before starting normal order");
                    StopAllCoroutines();
                    Crossfade.instance.CrossFade = false;
                    StartCoroutine(Crossfade.CoroutineFadeOverTime(Current, Next));
                }
            }
            else
            {
                //Debug.Log("no crossfading happening, starting crossfade");
                StartCoroutine(Crossfade.CoroutineFadeOverTime(Current, Next));
            }
        }
    }
}