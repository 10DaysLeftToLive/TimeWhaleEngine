using UnityEngine;
using System.Collections;

public class DeathbedNPCLoad : MonoBehaviour {
	//Object npc;
	//Vector3 pos;

	// Use this for initialization
	void Start () {
		float likesEnough = 7;
		float dispositionSister;
		float dispositionPaperboy;

		// Check for npcs to be displayed
		string dispositionDataFile = Application.dataPath + "/Data/DispositionData/" + Strings.DispositionFile + ".xml";

		if (!System.IO.File.Exists(dispositionDataFile))
		{
			Debug.LogError("Error: " + dispositionDataFile + " was not found.");
		}

		NPCCollection npcCollection = NPCCollection.Load(dispositionDataFile);
		
		// Load npcs into positions to be displayed
		dispositionSister = npcCollection.GetDisposition("Sister");
		dispositionPaperboy = npcCollection.GetDisposition("Paperboy");
		if (dispositionSister != null && dispositionPaperboy != null){
			if (dispositionSister > likesEnough || dispositionPaperboy > likesEnough) {
				guiTexture.enabled = true;
			} else {
				guiTexture.enabled = false;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
