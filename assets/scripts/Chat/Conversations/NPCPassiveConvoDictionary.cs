using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCPassiveConvoDictionary : ManagerSingleton<NPCPassiveConvoDictionary> {
	public Dictionary<string, List<NPCConvoChance>> convoDict;
	private List<NPCConvoChance> convoList;
	private static int UPPERBOUND = 100;
	
	public override void Init() {
		convoDict = new Dictionary<string, List<NPCConvoChance>>();
		AddConversationLists();
	}
	
	protected void AddConversationLists() {
		convoDict.Add (StringsConvos.GenericConvos, new GenericConvos().GetList());
		
		#region Young Conversations
		convoDict.Add (StringsConvos.YoungSpecialConvos, new YoungSpecialConvos().GetList());
		convoDict.Add (StringsNPC.SiblingYoung, new YoungSiblingConvos().GetList());
		convoDict.Add (StringsNPC.MomYoung, new YoungMotherConvos().GetList());
		convoDict.Add (StringsNPC.CarpenterYoung, new YoungCarpenterConvos().GetList());
		convoDict.Add (StringsNPC.CarpenterSonYoung, new YoungCarpenterSonConvos().GetList());
		convoDict.Add (StringsNPC.FarmerMotherYoung, new YoungFarmerConvos().GetList());
		convoDict.Add (StringsNPC.FarmerFatherYoung, new YoungFarmerHusbandConvos().GetList());
		convoDict.Add (StringsNPC.LighthouseGirlYoung, new YoungLighthouseGirlConvos().GetList());
		convoDict.Add (StringsNPC.CastlemanYoung, new YoungCastlemanConvos().GetList());
		convoDict.Add (StringsNPC.SeaCaptainYoung, new YoungSeaCaptainConvos().GetList());
		convoDict.Add (StringsNPC.BazaarmanYoung, new YoungBazaarmanConvos().GetList());
		convoDict.Add (StringsNPC.MusicianYoung, new YoungMusicianConvos().GetList());
		convoDict.Add (StringsNPC.FortuneTellerYoung, new YoungFortunetellerConvos().GetList());
		#endregion
		
		#region Middle Conversations
		convoDict.Add (StringsConvos.MiddleSpecialConvos, new MiddleSpecialConvos().GetList());
		#endregion
		
		#region Old Conversations
		convoDict.Add (StringsConvos.OldSpecialConvos, new OldSpecialConvos().GetList());
		#endregion
	}
	
	public NPCConversation GetConversation(NPC npc) {	
		// go through special conversations
		switch (CharacterAgeManager.currentAge) {
			case CharacterAgeState.YOUNG:
				foreach(NPCConvoChance convo in convoDict[StringsConvos.YoungSpecialConvos]) {
					if (IsConvoDrop(convo)) {
						return (NPCConversation)convo;
					}
				}
				break;
			case CharacterAgeState.MIDDLE:
				foreach(NPCConvoChance convo in convoDict[StringsConvos.MiddleSpecialConvos]) {
					if (IsConvoDrop(convo)) {
						return (NPCConversation)convo;
					}
				}
				break;
			default:
				foreach(NPCConvoChance convo in convoDict[StringsConvos.OldSpecialConvos]) {
					if (IsConvoDrop(convo)) {
						return (NPCConversation)convo;
					}
				}
				break;
		}
		
		// go through specific conversations
		if (convoDict.ContainsKey(npc.name)) {
			foreach(NPCConvoChance convo in convoDict[npc.name]) {
				if (IsConvoDrop(convo)) {
					return (NPCConversation)convo;
				}
			}
		}
		
		// Guranteed to return a generic conversation if previous choices fail
		return ChooseGenericConvo();
		
	}
	
	protected bool IsConvoDrop(NPCConvoChance convoChance) {
		if (Random.Range(0, UPPERBOUND) <= convoChance._talkChanceCurrent) {
			// if hit set to initial chance
			convoChance._talkChanceCurrent = convoChance.TalkChanceInitial;
			return true;
		} else {
			// if miss, increase current chance to increase likelyhood of beinghit
			++convoChance._talkChanceCurrent;
			return false;
		}
	}
	
	// TODO - Make generics show up more equally
	protected NPCConversation ChooseGenericConvo() {
		convoList = convoDict[StringsConvos.GenericConvos]; // TODO - Get from correct age
		return (NPCConversation)convoList[Random.Range(0, convoList.Count - 1)];
	}
}
