using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
    public bool AudioOn = false;
    public bool BGMOn = false;
    public bool SFXOn = false;

    public AudioSource ClimbLadderSFX;
    public AudioSource DoorCloseSFX;
    public AudioSource GiveItemSFX;
    public AudioSource PickUpItemSFX;
    public AudioSource PutDownItemSFX;

    public AudioSource WalkGrassSFX;
    public AudioSource WalkSandSFX;
    public AudioSource WalkWoodSFX;

    public AudioSource AgeTranstionSFX;

    // Needs to be a monobehavior to be put in scene, so make a singleton here instead of using managersingleton
    // Should be changed to be given these files from the loader
    private static SoundManager manager_instance = null;
    
    public static SoundManager instance{
        get { 
           if (manager_instance == null) {
                manager_instance = FindObjectOfType(typeof (SoundManager)) as SoundManager;
            }
 
            // If it is still null, create a new instance
            if (manager_instance == null) {
                GameObject obj = new GameObject("SoundManager");
                manager_instance = obj.AddComponent(typeof (SoundManager)) as SoundManager;
            }

            return manager_instance;
        }
    }

    /// <summary>
    /// PlaySFX will play the sound effect that you specify. It is
    /// primarily used for the walking sound effects.
    /// </summary>
    /// <param name="SFXName">The name of the walking sound effect
    /// that should be played.</param>
	public void PlayWalkSFX()
    {
		if (AudioOn) {
	        string Area = null;
	        if (Strings.CURRENTAREA == "Forest" || Strings.CURRENTAREA == "Market" || Strings.CURRENTAREA == "ReflectionTree" || Strings.CURRENTAREA == "Lighthouse" || Strings.CURRENTAREA == "Windmill"){
	            Area = "Grass";
	        }
	        else if (Strings.CURRENTAREA == "Beach")
	        {
	            Area = "Sand";
	        }
	        else if (Strings.CURRENTAREA == "Bridge" || Strings.CURRENTAREA == "Stairs" || Strings.CURRENTAREA == "Pier")
	        {
	            Area = "Wood";
	        }
	        switch (Area)
	        {
	            case "Grass":
	                if (!SoundManager.instance.WalkGrassSFX.isPlaying)
	                {
	                    SoundManager.instance.WalkSandSFX.Stop();
	                    SoundManager.instance.WalkWoodSFX.Stop();
	                    SoundManager.instance.WalkGrassSFX.Play();
	                }
	                break;
	            case "Wood":
	                if (!SoundManager.instance.WalkWoodSFX.isPlaying)
	                {
	                    SoundManager.instance.WalkSandSFX.Stop();
	                    SoundManager.instance.WalkGrassSFX.Stop();
	                    SoundManager.instance.WalkWoodSFX.Play();
	                }
	                break;
	            case "Sand":
	                if (!SoundManager.instance.WalkSandSFX.isPlaying)
	                {
	                    SoundManager.instance.WalkGrassSFX.Stop();
	                    SoundManager.instance.WalkWoodSFX.Stop();
	                    SoundManager.instance.WalkSandSFX.Play();
	                }
	                break;
	            default:
	                DebugManager.instance.Log("No walking sound due to incorrect area specified", "WalkSFX", "SFX");
	                break;
	        }
		}
    }

    /// <summary>
    /// StopSFX will stop all SFX that have been played using PlaySFX.
    /// These are usually the walking sound effects for different areas.
    /// </summary>
    public void StopWalkSFX()
    {
		if (SFXOn) {
	        SoundManager.instance.WalkGrassSFX.Stop();
	        SoundManager.instance.WalkSandSFX.Stop();
	        SoundManager.instance.WalkWoodSFX.Stop();
		}
    }

    public void StartCoroutineFadeDown(string Area)
    {
		if (AudioOn)
        StartCoroutine(Crossfade.CoroutineFadeDown(Area));
    }

    public void StartCoroutineFadeUp(string Area)
    {
		if (AudioOn)
        StartCoroutine(Crossfade.CoroutineFadeUp(Area));
    }
}
