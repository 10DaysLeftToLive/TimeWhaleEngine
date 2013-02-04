using UnityEngine;
using System.Collections;

public class DeathbedNPCLoad : MonoBehaviour {
	public GameObject paperBoy;
	public GameObject sister;
	
	private static bool ENABLED = false;
	private static bool DISABLED = true;

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
		dispositionPaperboy = npcCollection.GetDisposition("PaperBoy");
		
		if (dispositionSister != null && dispositionPaperboy != null){
			if (dispositionSister > likesEnough || dispositionPaperboy > likesEnough) {
				DisableNPCs();
			} else {
				EnableNpcs();
			}
		}
	}
	
	private void DisableNPCs(){
		SetStatusNpcs(DISABLED);
	}
	
	private void EnableNpcs(){
		SetStatusNpcs(ENABLED);
	}
	
	private void SetStatusNpcs(bool status){
		paperBoy.SetActiveRecursively(status);
		sister.SetActiveRecursively(status);
	}
}
