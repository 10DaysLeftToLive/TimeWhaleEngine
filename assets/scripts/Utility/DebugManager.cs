using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Debug manager will handle logging for the app through the use of the log function
/// which can take several string tags and if one of them has been chosen then it will be displayed
/// </summary>
public class DebugManager : MonoBehaviour {
	[SerializeField]
	public List<string> tagsToDisplay = new List<string>();
	public bool showLogs = true;
	
	private static DebugManager manager_instance = null;
    
    public static DebugManager instance{
        get { 
           if (manager_instance == null) {
                manager_instance = FindObjectOfType(typeof (DebugManager)) as DebugManager;
            }
 
            // If it is still null, create a new instance
            if (manager_instance == null) {
                GameObject obj = new GameObject("DebugManager");
                manager_instance = obj.AddComponent(typeof (DebugManager)) as DebugManager;
            }
            
            return manager_instance;
        }
    }
	
	/// <summary>
	/// Log the specified message without any check against the tags
	/// </summary>
	/// <param name='message'>
	/// Message to display
	/// </param>
	public void Log(string message){
		if (showLogs){
			Debug.Log(message);
		}
	}
	
	/// <summary>
	/// Log the specified message if all of the given tags should be shown
	/// </summary>
	public void Log(string message, params string[] tags){
		if (showLogs && ShouldShow(tags)){
			Log (message);
		}
	}
	
	/// <summary>
	/// Determine if any of the given tags are in what we should show
	/// </summary>
	/// <returns>
	/// true if any tag tag is what we should show or false if there are none
	/// </returns>
	private bool ShouldShow(params string[] tags){
		for (int i = 0; i < tags.Length; i++){
			if (TagSet(tags[i])) return true;
		}
		return false;
	}
	
	private bool TagSet(string tag){
		return (tagsToDisplay.Contains(tag));			
	}
}
