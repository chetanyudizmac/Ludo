using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Games.Ludo{
	public class PopUpVsComputerMode :UIPopUpMenu  {

		public static PopUpVsComputerMode instance;

		[HideInInspector]
		public int selectedColor=0;

		public Image[] buttonsImage;

		Color[] buttonColors=new Color[] {Color.red, Color.green,Color.blue,Color.yellow};

		[HideInInspector]
		Image currentSelectedButtonImage;

		public override void Awake (){
			base.Awake ();
			instance = this; 
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
			currentSelectedButtonImage = buttonsImage [0];
		}

		public void PlayButtonClicked(){
			GameManager.instance.StartGame ();
			Hide (true);
			ViewController.instance.ChangeView(ViewController.instance.viewInPlay);
		}
		public void CloseButtonClicked(){
			Hide (true);
		}

		public void SelectColorButtonClick(int buttonNo){
			currentSelectedButtonImage.color = Color.white;
			currentSelectedButtonImage = buttonsImage [buttonNo];
			buttonsImage [buttonNo].color = buttonColors [buttonNo];
			selectedColor = buttonNo;	
		}
}
}