using UnityEngine;
using System.Collections;

public class Game  {
	public static void Reset(){
		PlayerPrefs.SetInt("Sister", 0);
		PlayerPrefs.SetInt("PaperBoy", 0);
		
		
		/*
		string dispositonData = Application.dataPath + "/Data/DispositionData/" + Strings.DispositionFile + ".xml";
		NPCCollection npcCollection = NPCCollection.Load(dispositonData);
		npcCollection.ResetDispositions();
		npcCollection.Save(dispositonData);*/
	}
	
	
	
}
