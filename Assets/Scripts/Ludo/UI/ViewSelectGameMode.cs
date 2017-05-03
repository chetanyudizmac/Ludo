using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Games.Ludo{

	public class ViewSelectGameMode : UIView {
		public static ViewSelectGameMode instance;

		public override void Awake(){
			base.Awake ();
			instance = this;
		}

		public override void Show(){
			base.Show ();
		}
		public override void Hide(){
			base.Hide();
		}


		public void LocalModeButtonClicked(){
			GameManager.instance.currentGameType = GameType.LocalMode;
			Hide ();
			ViewLocalMode.instance.Show ();
		}

		public void VsComputerButtonClicked(){
			Hide ();
			GameManager.instance.currentGameType = GameType.VsComputer;			
		}




}
}