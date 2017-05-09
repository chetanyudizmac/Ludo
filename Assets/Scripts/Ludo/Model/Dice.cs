using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class Dice : MonoBehaviour {

	public static Dice instance;

	//dice sprites 1 to 6
	public Sprite[] diceSprites;
	//dice rotation sprites for animation
	public Sprite[] diceAnimationSprites;

	//dice Image to change sprite during gameplay
	 Image diceImage;

	//How much it should scale during Animation
	public Vector3 scaleDice=new Vector3(1.5f,1.5f,1.5f);
	//dice Scale speed during animation
	public float scaleSpeed = 0.3f;
	//how fast sprite should change 
	public float spriteChangeSpeed=0.1f;
	//random numbers
	[HideInInspector]
	public int DiceNumber;
	public bool isEnabled=false;

	public bool isTesting;
	public int testingNumber;
	void Awake()
	{
		instance = this;
		diceImage = this.GetComponent<Image> ();
		DisableDice ();
	}

	public void EnableDice(){
		isEnabled = true;
		diceImage.enabled = true;
	}

	public void DisableDice(){
		isEnabled = false;
		diceImage.enabled = false;
	}


	public IEnumerator DiceAnimation(Action<bool> isFinished, Action<int> number ){

		if (isEnabled) {
			
			isEnabled = false;
			WaitForSeconds seconds = new WaitForSeconds (spriteChangeSpeed);
			if (isTesting) {
				DiceNumber = testingNumber;
			} else {
				DiceNumber = UnityEngine.Random.Range (1, 7);
			}
			yield return seconds;
			iTween.ScaleTo (this.gameObject, scaleDice, scaleSpeed);
			for (int spriteNo = 0; spriteNo < diceAnimationSprites.Length; spriteNo++) {
				diceImage.sprite = diceAnimationSprites [spriteNo];
				yield return seconds;
			}
			iTween.ScaleTo (this.gameObject, Vector3.one, scaleSpeed);
			for (int spriteNo = 0; spriteNo < diceAnimationSprites.Length; spriteNo++) {
				diceImage.sprite = diceAnimationSprites [spriteNo];
				yield return seconds;
			}
			diceImage.sprite = diceSprites [DiceNumber-1];
			isFinished (true);
			number (DiceNumber);
			yield return null;
		}
	}


}
