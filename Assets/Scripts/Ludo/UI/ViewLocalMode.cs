using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Games.Ludo{
public class ViewLocalMode : UIView 
{
	public static ViewLocalMode instance;

	public Image[] buttonsImage;

	Color[] buttonColors=new Color[] {Color.red, Color.green,Color.blue,Color.yellow};
	int selectedColor=0;
	Image currentSelectedButtonImage;

	public override void Awake ()
	{
		base.Awake ();
		instance = this;
	}
	public override void Show()
	{
		base.Show ();
		currentSelectedButtonImage = buttonsImage [0];

	}
	public override void Hide()
	{
		base.Hide();
	}

	public void SelectColorButtonClick(int buttonNo){
		currentSelectedButtonImage.color = Color.white;
		currentSelectedButtonImage = buttonsImage [buttonNo];
		buttonsImage [buttonNo].color = buttonColors [buttonNo];
		selectedColor = buttonNo;		
	}

	public void PlayButtonClicked(){
			Board.instance.SetRegions (selectedColor);
			Hide ();
			ViewInPlay.instance.Show ();
	}

	}
}
