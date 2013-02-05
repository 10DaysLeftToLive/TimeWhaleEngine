using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class DeathbedNPCLoad : MonoBehaviour {
	public GameObject paperBoy;
	public GameObject sister;
	
	private static bool ENABLED = true;
	private static bool DISABLED = false;
	
	public TextAsset disData;
	
	
	// Use this for initialization
	void Start () {
		float likesEnough = 7;
		float dispositionSister;
		float dispositionPaperboy;
		/*
		XmlSerializer serializer = new XmlSerializer(typeof(NPCCollection));
		MemoryStream assetStream = new MemoryStream(disData.bytes);
		NPCCollection npcCollection = (NPCCollection)serializer.Deserialize(assetStream);
		assetStream.Close();*/
		
		// Load npcs into positions to be displayed
		dispositionSister = PlayerPrefs.GetInt("Sister", 0);// npcCollection.GetDisposition("Sister");
		dispositionPaperboy = PlayerPrefs.GetInt("PaperBoy", 0);//npcCollection.GetDisposition("PaperBoy");
		
		Debug.Log("dis sis = " + dispositionSister);
		Debug.Log("dis paperboy = " + dispositionPaperboy);
		
		if (dispositionSister != null && dispositionPaperboy != null){
			if (dispositionSister > likesEnough || dispositionPaperboy > likesEnough) {
				EnableNpcs();
			} else {
				DisableNPCs();
			}
		} else {
			DisableNPCs();
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
