using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Games.Ludo{

	public class ViewWelcome : UIView {
		public static ViewWelcome instance;
		public GameObject VsComputerPopUp;
		GameObject currentPopup;

		public Image[] buttonsImage;

		Color[] buttonColors=new Color[] {Color.red, Color.green,Color.blue,Color.yellow};
		int selectedColor=0;
		Image currentSelectedButtonImage;

		public override void Awake(){
			base.Awake ();
			instance = this;
		}

		public override void Show(){
			base.Show ();
			VsComputerPopUp.SetActive (false);
			currentSelectedButtonImage = buttonsImage [0];

		}
		public override void Hide(){
			base.Hide();
		}


		public void LocalModeButtonClicked(){
			GameManager.instance.currentGameType = GameType.LocalMode;
		}

		public void VsComputerButtonClicked(){
			GameManager.instance.currentGameType = GameType.VsComputer;			
			ChangePopUp(VsComputerPopUp);
		}
		public void ChangePopUp(GameObject targetPopUp)
		{
			if (currentPopup != null) {
				currentPopup.SetActive (false);
				currentPopup = targetPopUp;
				currentPopup.SetActive (true);
			} else {
				currentPopup = targetPopUp;
				currentPopup.SetActive (true);
			}
		}
		public void SelectColorButtonClick(int buttonNo){
			currentSelectedButtonImage.color = Color.white;
			currentSelectedButtonImage = buttonsImage [buttonNo];
			buttonsImage [buttonNo].color = buttonColors [buttonNo];
			selectedColor = buttonNo;	
		}

		public void PlayButtonClicked(){
			Board.instance.SetRegions (selectedColor);
//			Hide ();
//			ViewInPlay.instance.Show ();
			ViewController.instance.ChangeView(ViewController.instance.viewInPlay);
		}
		public void CloseButtonClicked()
		{
			currentPopup.SetActive (false);
		}

}
}