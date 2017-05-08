using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Games.Ludo{
	public class PopUpLocalMode :UIPopUpMenu  {

		public static PopUpLocalMode instance;

		public int noOfActivePlayers=2;

		//Main Popup UI Variables
		public GameObject[] popUpsWindows;
		public Button[] selectButtons;
		GameObject currentSelectedPopUpWindow;
		Button currentSelectedButton;

		//2pVAriabbles
		public Button[] selectGroupsButtons;
		public Text[] groupAText;
		public Text[] groupBText;
		public Image groupASelectedImage;
		public Image groupBSelectedImage;
		Image currenGroupSelectedImage;

		//3P Variables
		int[] selectedColorInGroup={0,1,3};
		Stack<int> forUnSelected=new Stack<int>();
		public GameObject[] selectColorImagesGroup;
		public Text[] threePlayerTextGroup;

		//4p Variables
		public Text[] fourPlayerTextGroup;

		//Data Which we wants to get
		RegionType r1Color;
		RegionType r2Color;
		RegionType r3Color;
		RegionType r4Color;



		[HideInInspector] string region1PlayerName;
		[HideInInspector] string region2PlayerName;
		[HideInInspector] string region3PlayerName;
		[HideInInspector] string region4PlayerName;
			
		private int selectedGroup=0;

		public override void Awake (){
			base.Awake ();
			instance = this; 
	

			r1Color = RegionType.Blue;
			r2Color = RegionType.Red;
			r3Color = RegionType.Green;
			r4Color = RegionType.Yellow;

			region1PlayerName="Player1";
			region2PlayerName="Player2";
			region3PlayerName="Player3";
			region4PlayerName="Player4";
			forUnSelected.Push (0);
			forUnSelected.Push (1);
			forUnSelected.Push (2);
			forUnSelected.Push (3);
		}

		public override void PopUpAppeared ()
		{
			base.PopUpAppeared ();
		}

		public override void Hide (bool animated)
		{
			base.Hide (animated);
		}
		public override void Show (bool animated)
		{
			base.Show (animated);
			for (int noOfPopUps = 1; noOfPopUps < popUpsWindows.Length; noOfPopUps++) {
				popUpsWindows [noOfPopUps].SetActive (false);
			}
			selectButtons [0].interactable = false;
			currentSelectedButton = selectButtons [0];
			currentSelectedPopUpWindow = popUpsWindows [0];
			selectGroupsButtons [0].Select ();
			groupASelectedImage.color = Color.green;
			currenGroupSelectedImage = groupASelectedImage;

		}
		#region 2Player

		public void GroupAButtonClicked()
		{
			selectedGroup = 0;

			r1Color = RegionType.Blue;
			r2Color = RegionType.Red;
			r3Color = RegionType.Green;
			r4Color = RegionType.Yellow;
			ChangeGroupColor (groupASelectedImage);
		}
		public void GroupBButtonClicked()
		{
			selectedGroup = 1;

			r1Color = RegionType.Yellow;
			r2Color = RegionType.Green;
			r3Color = RegionType.Red;
			r4Color = RegionType.Blue;
			ChangeGroupColor (groupBSelectedImage);
		}
		#endregion

		#region 3Player
		public void Player1ButtonGroupClicked(int selectedImageNo)
		{
			ChangeSelection (0, selectedImageNo);

		}
		public void Player2ButtonGroupClicked(int selectedImageNo)
		{
			ChangeSelection (1, selectedImageNo);

		}
		public void Player3ButtonGroupClicked(int selectedImageNo)
		{
			ChangeSelection (2, selectedImageNo);

		}
		private void ChangeSelection(int groupNo,int selectionNumber)
		{
			bool conflictFound = false;
			for (int gno = 0; gno < selectedColorInGroup.Length; gno++) {
				if (selectedColorInGroup [gno] == selectionNumber) {
					selectedColorInGroup [gno] = selectedColorInGroup [groupNo];
					selectedColorInGroup [groupNo] = selectionNumber;
					conflictFound = true;
				} 
			}
			if (!conflictFound) {
				selectedColorInGroup [groupNo] = selectionNumber;
			}

			ChangeRightIconOnSelectedButton ();
		}
		private int SetRegion4ColorFor3p()
		{
			for (int noOfSelected = 0; noOfSelected < selectedColorInGroup.Length; noOfSelected++) {
				for (int j = 0; j < selectedColorInGroup.Length; j++) {
					if (selectedColorInGroup [j] == forUnSelected.Peek ()) {
						forUnSelected.Pop ();
					}
				}
			}
			return forUnSelected.Pop ();
		}
		private void ChangeRightIconOnSelectedButton()
		{//0 red ,1 green,2 yellow,3 blue
			for (int groupNo = 0; groupNo < selectedColorInGroup.Length; groupNo++) {
				for (int noOfButtonsInGroup = 0; noOfButtonsInGroup < selectColorImagesGroup [groupNo].transform.childCount; noOfButtonsInGroup++) {
					if (selectedColorInGroup [groupNo] == noOfButtonsInGroup) {
						selectColorImagesGroup [groupNo].transform.GetChild (noOfButtonsInGroup).GetChild (0).GetComponent<Image> ().enabled = true;
					} else {
						selectColorImagesGroup [groupNo].transform.GetChild (noOfButtonsInGroup).GetChild (0).GetComponent<Image> ().enabled = false;
					}
				}
			}
		}
		#endregion

		#region 4Player

		#endregion

		public void TwoPlayerButtonClick()
		{
			noOfActivePlayers = 2;
			ChangeUIWindow (0);
		}
		public void ThreePlayerButtonClick()
		{
			noOfActivePlayers = 3;
			ChangeUIWindow (1);
		}
		public void FourPlayerButtonClick()
		{
			noOfActivePlayers = 4;
			ChangeUIWindow (2);
		}

		private void ChangeUIWindow(int targetWindow)
		{
			currentSelectedPopUpWindow.SetActive (false);
			currentSelectedButton.interactable = true;

			currentSelectedPopUpWindow = popUpsWindows [targetWindow];
			currentSelectedButton = selectButtons [targetWindow];

			currentSelectedPopUpWindow.SetActive (true);
			currentSelectedButton.interactable = false;
		}
		private void ChangeGroupColor(Image target)
		{
			currenGroupSelectedImage.color = Color.white;
			currenGroupSelectedImage = target;
			currenGroupSelectedImage.color = Color.green;
		}
		public void PlayButtonClicked()
		{
			if (noOfActivePlayers == 2) {
				if (selectedGroup == 0) {
					if(groupAText[0].text=="")
					{
						region1PlayerName = "Player 1";
						region3PlayerName = "Player 3";
					}
					else
					{
						region1PlayerName = groupAText [0].text;
						region3PlayerName = groupAText [1].text;
					}

				} else {
					if(groupBText[0].text=="")
					{
						region1PlayerName = "Player 1";
						region3PlayerName = "Player 3";
					}
					else
					{
						region1PlayerName = groupBText [0].text;
						region3PlayerName = groupBText [1].text;
					}
				}
			} else if (noOfActivePlayers == 3) {
		
				r1Color = (RegionType)Enum.ToObject (typeof(RegionType),selectedColorInGroup[0]);
				r2Color = (RegionType)Enum.ToObject (typeof(RegionType),selectedColorInGroup[1]);
				r3Color = (RegionType)Enum.ToObject (typeof(RegionType),selectedColorInGroup[2]);
				r4Color = (RegionType)Enum.ToObject (typeof(RegionType),SetRegion4ColorFor3p ());

				if (threePlayerTextGroup [0].text == "") {
					region1PlayerName="Player 1";	
					region2PlayerName="Player 2";	
					region3PlayerName="Player 3";	
				} else {
					region1PlayerName = threePlayerTextGroup[0].text;
					region2PlayerName = threePlayerTextGroup [1].text;
					region3PlayerName = threePlayerTextGroup [2].text;
				}
			} 
			else {
	
				r1Color = RegionType.Blue;
				r2Color = RegionType.Red;
				r3Color = RegionType.Green;
				r4Color = RegionType.Yellow;
				if (fourPlayerTextGroup [0].text == "") {
					region1PlayerName = "Player 1";	
					region2PlayerName = "Player 2";	
					region3PlayerName = "Player 3";	
					region4PlayerName = "Player 4";
				} else {
					region1PlayerName = fourPlayerTextGroup[0].text;
					region2PlayerName = fourPlayerTextGroup [1].text;
					region3PlayerName = fourPlayerTextGroup [2].text;
					region4PlayerName = fourPlayerTextGroup [3].text;
				}
			}

//			GameManager.instance.StartGame ();
//			Hide (true);
//			ViewController.instance.ChangeView(ViewController.instance.viewInPlay);

		}
	}
}