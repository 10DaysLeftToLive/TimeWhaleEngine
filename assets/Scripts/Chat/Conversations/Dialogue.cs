using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dialogue {
	public NPC _npc;
	private string _textToSay;
	public string _TextToSay {
		get {return _textToSay;}
	}
	
	public Dialogue(NPC npc, string textToSay) {
		_npc = npc;
		_textToSay = textToSay;
	}
}
