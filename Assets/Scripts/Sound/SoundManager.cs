using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public bool AudioOn = true;
    public bool BGMOn = true;
    public bool SFXOn = true;

    public AudioSource DoorCloseSFX;
    public AudioSource GiveItemSFX;
    public AudioSource PickUpItemSFX;
    public AudioSource PutDownItemSFX;

    public AudioSource WalkGrassSFX;
    public AudioSource WalkSandSFX;
    public AudioSource WalkWoodSFX;

    public AudioSource AgeTransitionSFX;

    // Needs to be a monobehavior to be put in scene, so make a singleton here instead of using managersingleton
    // Should be changed to be given these files from the loader
    private static SoundManager manager_instance = null;

    public static SoundManager instance
    {
        get
        {
            if (manager_instance == null)
            {
                manager_instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;
            }

            // If it is still null, create a new instance
            if (manager_instance == null)
            {
                GameObject obj = new GameObject("SoundManager");
                manager_instance = obj.AddComponent(typeof(SoundManager)) as SoundManager;
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
        string Area = null;
        if (Strings.CURRENTAREA == "Forest" || Strings.CURRENTAREA == "Market" || Strings.CURRENTAREA == "ReflectionTree" || Strings.CURRENTAREA == "Lighthouse" || Strings.CURRENTAREA == "Windmill")
        {
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
        if (SoundManager.instance.SFXOn)
        {
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
    /// StopWalkSFX will stop all walking SFX that have been played using PlayWalkSFX.
    /// </summary>
    public void StopWalkSFX()
    {
        SoundManager.instance.WalkGrassSFX.Stop();
        SoundManager.instance.WalkSandSFX.Stop();
        SoundManager.instance.WalkWoodSFX.Stop();
    }

    /// <summary>
    /// PlaySFX plays SFX that occur during an interaction.
    /// </summary>
    /// <param name="type">"type" is used to determine which interaction SFX you want to play. Possible strings are "PickUp", "PutDown", "GiveItem", and "OpenDoor".</param>
    public void PlaySFX(string type)
    {
        if (SoundManager.instance.AudioOn && SoundManager.instance.SFXOn)
        {
            switch (type)
            {
                case "AgeTransition":
                    AgeTransitionSFX.Play();
                    break;
                case "PickUp":
                    PickUpItemSFX.Play();
                    break;
                case "PutDown":
                    PutDownItemSFX.Play();
                    break;
                case "OpenDoor":
                    DoorCloseSFX.Play();
                    break;
                case "GiveItem":
                    GiveItemSFX.Play();
                    break;
                default:
                    break;
            }
        }
    }
}
