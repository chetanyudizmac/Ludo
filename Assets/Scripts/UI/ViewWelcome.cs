using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ViewWelcome : UIView 
{
	public static ViewWelcome instance;
	public Image[] buttonsImage;
	public Color[] buttonColors;
	int currentSelectedbutton;
	Image currentSelectedButtonImage;
	public override void Awake ()
	{
		base.Awake ();
		instance = this;
	}
	public override void Show()
	{
		base.Show ();
		Debug.Log ("Inside View Welcome");
		currentSelectedButtonImage = buttonsImage [0];

	}
	public override void Hide()
	{
		base.Hide();
	}
	public void SelectColorButtonClick(int buttonNo)
	{
		currentSelectedButtonImage.color = Color.white;
		currentSelectedButtonImage = buttonsImage [buttonNo];
		buttonsImage [buttonNo].color = buttonColors [buttonNo];
		currentSelectedbutton = buttonNo;
		Debug.Log ("Button Clicked");
	}

}
