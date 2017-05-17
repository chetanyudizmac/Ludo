using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Games.Ludo{
public class Token : MonoBehaviour {

		public delegate void MoveTokenDelegate(Token token);
		public static event MoveTokenDelegate MoveToken;

		public Region region;
		public Image image;
		public bool inYard = true;
		public bool inHome = false;
		public bool inSafePlace =true;
		public bool isActivated;
		[HideInInspector]
		public RectTransform basePosition;
		public int pathIndex=50;
		[HideInInspector]
		public RectTransform  rectTransform;
		[HideInInspector]
		public Vector2 defaultSize;
		public float timeToReachDestination=.5f;
		[HideInInspector]
		public Tile residingTile; // represent tile which contains the token


		public void Awake(){			
			rectTransform = this.GetComponent<RectTransform> ();
			defaultSize = rectTransform.sizeDelta;
			image = this.GetComponent<Image> ();
		}

		public void SetColor(Color color){			
		
		}


		public void SetTokenProperty(Region reg, Color color){
			region = reg;
			image.color = color;
		}

		public void TokenCliked(){
			MoveToken (this);
		}

		public void AutomatedTokenClicked(Token t)
		{
			MoveToken (t);
		}


		public void ActivateToken(){
			isActivated = true;
			//iTween.PunchScale(this.gameObject, iTween.Hash("amount", new Vector3(0.2f, 0.2f, 1f), "time", 1f, "looptype", iTween.LoopType.pingPong));
		}

		public void DeActivateToken(){
			isActivated = false;
			iTween.Stop ();
		}



		/// <summary>
		/// Drives the token.
		/// </summary>
		/// <returns>The token.</returns>
		/// <param name="destinationRectTransform">Destination  rect transform.</param>
		/// <param name="index">Set Index for current token.</param>
		/// <param name="isFinished">Is finished.</param>
		public IEnumerator DriveToken(RectTransform destinationRectTransform,int index,Action<bool> isFinished,float moveBackOffset=1){
			rectTransform.sizeDelta = defaultSize;
			RectTransform originPlace = GetComponent<RectTransform> ();
			iTween.ValueTo(this.gameObject, iTween.Hash(
				"from",originPlace.anchoredPosition,
				"to", destinationRectTransform.anchoredPosition,
				"time", timeToReachDestination*moveBackOffset,
				"onupdatetarget", this.gameObject, 
				"onupdate", "MoveGuiElement"
			));
			pathIndex += index;
			yield return new WaitForSeconds (timeToReachDestination*moveBackOffset);
			rectTransform.anchoredPosition = destinationRectTransform.anchoredPosition;
			isFinished (true);
		}


		public void MoveGuiElement(Vector2 position){
			rectTransform.anchoredPosition = position;
		}

	}
}
