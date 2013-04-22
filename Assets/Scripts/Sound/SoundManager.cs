using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
    public AudioSource ClimbLadderSFX;
    public AudioSource DoorCloseSFX;
    public AudioSource GiveItemSFX;
    public AudioSource PickUpItemSFX;
    public AudioSource PutDownItemSFX;
    public AudioSource WalkForestSFX;

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
    public void PlaySFX(string SFXName)
    {
        switch (SFXName)
        {
            case "WalkForest":
                SoundManager.instance.WalkForestSFX.Play();   
                break;
            case "WalkBeach":
                break;
            case "WalkPier":
                break;
            case "WalkMarket":
                break;
            case "WalkTree":
                break;
            case "WalkFarm":
                break;
            case "WalkLightHouse":
                break;
            case "WalkWindmill":
                break;
            case "WalkStairs":
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// StopSFX will stop all SFX that have been played using PlaySFX.
    /// These are usually the walking sound effects for different areas.
    /// </summary>
    public void StopSFX()
    {
        SoundManager.instance.WalkForestSFX.Stop();
    }
}
