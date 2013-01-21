using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("NPCCollection")]
public class NPCCollection {
    [XmlArray("Npcs"),XmlArrayItem("Npcs")]
    public List<NPCData> npcs;
	
	public NPCCollection(){
		npcs = new List<NPCData>();
	}
	
	public void Add(NPCData npcData){
		npcs.Add(npcData);
	}
	
	public float GetDisposition(string npcName){
		NPCData npc = GetNPC(npcName);
		return npc.disposition;
	}
	
	public NPCData GetNPC(string npcName){
		foreach (NPCData npc in npcs){
			if (npc.name == npcName){
				return (npc);
			}
		}
		Debug.LogError("Could not find data for npc with name " + npcName + " in the file");
		return (null);
	}
    
    public void Save(string path) {
        var serializer = new XmlSerializer(typeof(NPCCollection));
        using(var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }
   
    public static NPCCollection Load(string path) {
        var serializer = new XmlSerializer(typeof(NPCCollection));
        using(var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as NPCCollection;
        }
    }
}