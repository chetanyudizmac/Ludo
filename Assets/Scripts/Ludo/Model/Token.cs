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
		public bool inHome = true;
		public bool inSafePlace =true;
		public bool isActivated;
		[HideInInspector]
		public RectTransform basePosition;
		public int pathIndex=-1;
		RectTransform  rectTransform;
		public float timeToReachDestination=.5f;
		[HideInInspector]
		public Tile residingTile; // represent tile which contains the token


		public void Awake(){			
			rectTransform = this.GetComponent<RectTransform> ();
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
			isFinished (true);
		}


		public void MoveGuiElement(Vector2 position){
			rectTransform.anchoredPosition = position;
		}

	}
}
