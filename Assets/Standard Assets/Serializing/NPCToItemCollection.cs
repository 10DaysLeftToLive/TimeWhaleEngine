using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("NPCToItemCollection")]
public class NPCToItemCollection {
    [XmlArray("Npcs"),XmlArrayItem("Npcs")]
    public List<NPCItemsReactions> npcs;
	
	public NPCToItemCollection(){
		npcs = new List<NPCItemsReactions>();
	}
	
	public void Add(NPCItemsReactions npcData){
		npcs.Add(npcData);
	}
	
	public NPCItemsReactions GetNPC(string npcName){
		foreach (NPCItemsReactions npc in npcs){
			if (npc.name == npcName){
				return (npc);
			}
		}
		Debug.LogError("Could not find item reaction data for npc with name " + npcName + " in the file");
		
		return (null);
	}
    
    public void Save(string path) {
        var serializer = new XmlSerializer(typeof(NPCToItemCollection));
        using(var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }
   
    public static NPCToItemCollection Load(string path) {
        var serializer = new XmlSerializer(typeof(NPCToItemCollection));
        using(var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as NPCToItemCollection;
        }
    }
}