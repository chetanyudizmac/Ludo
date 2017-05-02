using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewLoading : UIView 
{
	public static ViewLoading instance;
	public override void Awake ()
	{
		base.Awake ();
		instance = this;
	}
	public override void Show()
	{
		base.Show ();
		StartCoroutine (StartWelcomeScreen());

	}
	public override void Hide()
	{
		base.Hide();
	}
	IEnumerator StartWelcomeScreen()
	{
		yield return new WaitForSeconds (3f);
		ViewController.instance.ChangeView (ViewController.instance.viewWelcome);
	}
}
