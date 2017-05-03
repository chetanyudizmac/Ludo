using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Games.Ludo{

	public class ViewWelcome : UIView {
		public static ViewWelcome instance;
		public UIPopUpMenu VsComputerPopUp;
		GameObject currentPopup;



		public override void Awake(){
			base.Awake ();
			instance = this;
		}

		public override void Show(){
			base.Show ();
			VsComputerPopUp.Hide(false);


		}
		public override void Hide(){
			base.Hide();
		}


		public void LocalModeButtonClicked(){
			GameManager.instance.currentGameType = GameType.LocalMode;
		}

		public void VsComputerButtonClicked(){
			GameManager.instance.currentGameType = GameType.VsComputer;			
			PopUpVsComputerMode.instance.Show (true);

		}





}
}