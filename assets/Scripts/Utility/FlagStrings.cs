using UnityEngine;
using System.Collections;

public static class FlagStrings {
	public static string CrushFrog = "Crush Frog";
	
	#region Mother
	public static string EnterHappyState = "Enter Happy State";
	public static string EnterMadState = "Enter Mad State";
	public static string ExitMadState = "exit Mad State";
	public static string MoveHome = "Move Home";
	public static string GaveApple = "Gave Apple";
	public static string GiveSeeds = "Give Seeds";
	
	public static string MoveToMusician = "Move To Musician";
	public static string FinishMusicianConvo = "Finish Musician Convo";
	public static string GiveItems = "Give Items";
	#endregion
	
	#region Sibling
	// new one
	public static string StartedRace = "Started Race";
	//
	public static string PostSiblingExplore = "Post Sibling Exploration";
	public static string SiblingExplore = "Sibling Explore";
	public static string RaceTime = "Activate Passive";
	public static string RunToBridge = "Run To Bridge"; 
	public static string RunToCarpenter = "Run To Carpenter";
	public static string RunToBeach = "Run To Beach";
	public static string RunToFarmer = "Run To Farmer";
	public static string RunToLighthouse = "Run To Lighthouse";
	public static string RunToMarket = "Run To Market";
	public static string RunToWindmill = "Run To Windmill";
	public static string RunToReflectionTree = "Run To Reflection Tree";
	public static string RunToHome = "Run Home";
	#endregion
	
	#region Carpenter	
	public static string gaveToolsToCarpenterOrSon = "Gave Tools to Carpenter Or Son";
	#endregion
	
	#region Carpenter Son
	public static string gaveFishingRodToCarpenterSon = "Gave Fishingrod to Carpenter Son";
	public static string carpenterSonStormOff = "Carpenter Son Storms Off";
	public static string carpenterSonReconcile = "Carpenter Son Reconcile";
	
	public static string CarpenterDating = "Carpenter dating";
	public static string CarpenterNoShow = "Carpenter no show";
	#endregion
	
	#region CastleMan Only
	public static string NotInsane = "Not Insane";
	public static string SavingLighthouseGirl = "Saving Lighthouse Girl";
	public static string PostSavingLighthouseGirl = "Post Saving Lighthouse Girl";
	public static string InsaneIntroConvo = "Insane Introduction Conversation";
	public static string PostInsaneIntroConvo = "Post Insane Introduction Conversation";
	
	public static string CastleManDating = "Castleman dating";
	public static string CastleManNoShow = "Castleman no show";
	#endregion
	
	#region Lighthouse Girl
	public static string MarryingCarpenter = "Marrying Carpenter";
	public static string CarpenterDate = "Carpenter Date";
	public static string PostDatingCarpenter = "Post Dating Carpenter";
	public static string StoodUp = "Stood Up";
	public static string NoMarriage = "No Marriage";
	public static string CastleDate = "Castle Date";
	public static string PostCastleDate = "Post Castle Date";
	public static string CastleMarriage = "Castle Marriage";
	public static string NoteNoMarriage = "Note No Marriage";
	public static string TellOnLighthouse = "Tell on Lighthouse";
	public static string TellOnLighthouseConversation = "Tell on Lighthouse Conversation";
	
	public static string ToolsToGirl = "Tools to girl";
	public static string WaitForPlayerBeforeRope = "Wait for player before rope";
	public static string WaitingForDate = "Waiting for date";
	public static string EndOfDate = "End of date";
	public static string ToolsForMarriage = "Tools for marriage";
	
	public static string CounterTellOn = "Counter Tell on to farmer father.";
	public static string MakePieFromScratch = "Make Pie from scratch.";
	public static string MakePieFromApple = "Make Pie from apple.";
	public static string GoDownToBeach = "Go down to beach";
	public static string WaitForItem = "Wait for pie or apple.";
	#endregion
	
	#region FarmerFather
	public static string ConversationInMiddleFather = "Conversation in Middle Father";
	public static string BusinessConversation = "Business";
	public static string BusinessTimer = "Business Timer";
	//young
	public static string GiveSeaShellToFarmerHusband = "Give SeaShell to Farmer";
	public static string GiveAppleToFarmerHusband = "Give Apple to Farmer";
	public static string GiveApplePieToFarmerHusband = "Give ApplePie to Farmer";
	public static string GivePortraitToFarmerHusband = "Give Portrait to Farmer";
	public static string GiveToyPuzzleToFarmerHusband = "Give Toy Puzzle to Farmer";
	public static string GiveToySwordToFarmerHusband = "Give Toy Sword to Farmer";
	public static string GiveCaptainsLogToFarmerHusband = "Give Captains Log to Farmer";
	public static string HusbandSillyStories = "Husband silly stories";
	public static string YourCoward = "Your Coward";
	public static string IllDoIt = "Ill Do it";
	public static string AlreadyBrave = "Already Brave";
	public static string IllSellForYou = "Ill sell for you";
	public static string HusbandHappy = "Husband Happy";
	public static string HusbandOnBoard = "Husband on board";
	#endregion
	
	#region FarmerMother
	public static string ConversationInMiddleFarmerMother = "Conversation in Middle Farmer Mother";
	public static string FarmAlive = "Farm Alive";
	//young
	public static string NotSilly = "Not Silly";
	public static string WorkAndStories = "Work and Stories";
	public static string TellOnDaughter = "Tell on Daughter";
	public static string StoriesAreSilly = "Stories are silly";
	public static string YourRight = "Your Right";
	public static string GivePendantToFarmer = "Give Pendant to Farmer";
	public static string GiveAppleToFarmer = "Give Apple to Farmer";
	public static string GiveApplePieToFarmer = "Give Apple pie to Farmer";
	public static string GiveShovelToFarmer = "Give Shovel to Farmer";
	public static string GiveRopeToFarmer = "Give Rope to Farmer";
	public static string FarmerOnBoard = "Farmer on Board";
	#endregion
	
	#region Sea Captain
	public static string StolenFishingRod = StringsItem.FishingRod;
	#endregion
	
	#region Musician
	public static string MusicianCommentOnSon = "Is he mute?";
	public static string MusicianFinishedTalkingFriends = "Musician finished talking friends.";
	public static string MusicianFinishedTalkingNOTFriends = "Musician finished talking not friends";
	#endregion
	
	#region CastleMan
	public static string ConvinceToTalkWithCarpenterSonRoundOne = "Talk CSON Round One.";
	public static string FinishedCSONConversation = "Finished Carpenter Son conversation";
	public static string FinishedLightHouseConversation = "Finished lighthouse conversation.";
	public static string FinishedBothConversations = "Finished Lighthouse and Carpenter's Son conversations.";
	
	public static string InitialConversationWithCSONNOTFriend = "Initial conversation with the carpenter's son NotFriend.";
	public static string FinishedInitialConversationWithCSONNOTFriend = "Finished initial conversation with the carpenter's son NotFriend.";
	public static string SecondConversationWithCSONNOTFriend = "Second conversation with the carpenter's son NotFriend.";
	public static string FinishedSecondConversationWithCSONNOTFriend = "Finished second conversation with the carpenter's son NotFriend.";
	public static string ThirdConvoWithCSONNOTFriend = "Third conversation with the carpenter's son NotFriend.";
	public static string FinishedThirdConvoWithCSONNOTFriend = "Finished third conversation with the carpenter's son NotFriend.";
	
	public static string InitialConversationWithCSONFriend = "Initial conversation with the carpenter's son Friend.";
	public static string FinishedInitialConversationWithCSONFriend = "Finished initial conversation with the carpenter's son Friend.";
	public static string SecondConversationWithCSONFriend = "Second conversation with the carpenter's son Friend.";
	public static string FinishedSecondConversationWithCSONFriend = "Finished second conversation with the carpenter's son Friend.";
	public static string ThirdConvoWithCSONFriend = "Third conversation with the carpenter's son Friend.";
	public static string FinishedThirdConvoWithCSONFriend = "Finished third conversation with the carpenter's son Friend.";
	
	public static string CSONAndCastleNOTFriends = "CSON and Castle not friends";
	public static string CSONAndCastleFriends = "CSON and Castle Friends.";
	public static string PlayerAndCastleFriends = "Player and Castle Friends";
	public static string PlayerAndCastleNOTFriends = "Player and Castle NOT friends.";
	
	public static string BeachBeforeConvoFriendsString = "Beach before conversation friends.";
	public static string BeachBeforeConvoNotFriendsString = "Beach before conversation not as friends.";
	public static string BeachPreparedForConvo = "Beach Prepared for Conversation.";
	public static string BeachNotPreparedForConvo = "Beach not prepared for conversation";
	public static string BeachAfterConvoGood = "Beach After Conversation good.";
	public static string BeachAfterConvoBad = "Beach after conversation bad.";
	
	public static string StartTalkingToLighthouse = "Start talking to lighthouse.";
	#endregion
}
