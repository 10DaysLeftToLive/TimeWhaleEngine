using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

public class NPCData {
	[XmlAttribute("name")]
    public string name;
    public int disposition;
}