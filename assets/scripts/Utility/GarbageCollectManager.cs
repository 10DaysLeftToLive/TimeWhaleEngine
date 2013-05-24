using UnityEngine;  
  
/// <summary>
/// Garbage collect manager will force a garbage collection every 30 frames to prevent build up
/// </summary>
class GarbageCollectManager : MonoBehaviour {  
    public int frameFreq = 30;  
    void Update(){  
        if (Time.frameCount % frameFreq == 0) { 
            System.GC.Collect();  
		}
    }  
}  