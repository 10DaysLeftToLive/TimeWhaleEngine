using UnityEngine;
using System.Collections;

public class Game  {
	public static void Reset(){
		string dispositonData = Application.dataPath + "/Data/DispositionData/" + Strings.DispositionFile + ".xml";
		NPCCollection npcCollection = NPCCollection.Load(dispositonData);
		npcCollection.ResetDispositions();
		npcCollection.Save(dispositonData);
	}
}
