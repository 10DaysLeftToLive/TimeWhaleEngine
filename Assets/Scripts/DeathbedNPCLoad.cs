using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class DeathbedNPCLoad : MonoBehaviour {
	public GameObject paperBoy;
	public GameObject sister;
	
	private static bool ENABLED = false;
	private static bool DISABLED = true;
	
	public TextAsset disData;
	
	
	// Use this for initialization
	void Start () {
		float likesEnough = 7;
		float dispositionSister;
		float dispositionPaperboy;
		
		XmlSerializer serializer = new XmlSerializer(typeof(NPCCollection));
		MemoryStream assetStream = new MemoryStream(disData.bytes);
		NPCCollection npcCollection = (NPCCollection)serializer.Deserialize(assetStream);
		assetStream.Close();
		
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
