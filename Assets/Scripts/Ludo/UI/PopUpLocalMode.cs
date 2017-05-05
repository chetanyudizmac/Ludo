using System.Collections;
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
		Color[] selectedColors = { Color.red, Color.green, Color.yellow, Color.blue };
		public GameObject[] selectColorImagesGroup;
		public Text[] threePlayerTextGroup;


		//4p Variables
		public Text[] fourPlayerTextGroup;

		//Data Which we wants to get
		[HideInInspector] Color region1Color;
		[HideInInspector] Color region2Color;
		[HideInInspector] Color region3Color;
		[HideInInspector] Color region4Color;

		[HideInInspector] string region1PlayerName;
		[HideInInspector] string region2PlayerName;
		[HideInInspector] string region3PlayerName;
		[HideInInspector] string region4PlayerName;
			
		private int selectedGroup=0;

		public override void Awake (){
			base.Awake ();
			instance = this; 
			region1Color = Color.blue;
			region2Color = Color.red;
			region3Color = Color.green;
			region4Color = Color.yellow;

			region1PlayerName="Player1";
			region2PlayerName="Player2";
			region3PlayerName="Player3";
			region4PlayerName="Player4";

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
			region1Color = Color.blue;
			region2Color = Color.red;
			region3Color = Color.green;
			region4Color = Color.yellow;
			ChangeGroupColor (groupASelectedImage);
		}
		public void GroupBButtonClicked()
		{
			selectedGroup = 1;
			region1Color = Color.yellow;
			region2Color = Color.green;
			region3Color = Color.red;
			region4Color = Color.blue;
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
				region1Color = selectedColors [selectedColorInGroup [0]];
				region2Color = selectedColors [selectedColorInGroup [1]];
				region3Color = selectedColors [selectedColorInGroup [2]];
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
				region1Color = Color.blue;
				region2Color = Color.red;
				region3Color = Color.green;
				region4Color = Color.yellow;
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
			Debug.Log (region1Color);
			Debug.Log (region2Color);
			Debug.Log (region3Color);
			Debug.Log (region4Color);
			Debug.Log (region1PlayerName);
			Debug.Log (region2PlayerName);
			Debug.Log (region3PlayerName);
			Debug.Log (region4PlayerName);

		}
	}
}