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
}
