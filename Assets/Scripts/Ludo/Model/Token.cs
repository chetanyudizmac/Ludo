using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Games.Ludo{
public class Token : MonoBehaviour {

		public delegate void MoveTokenDelegate(Token token);
		public static event MoveTokenDelegate MoveToken;

		public delegate void EndTurnDelegate(Token token);
		public static event EndTurnDelegate EndTurn;

		public Region region;
		public Image image;
		public bool inHome = true;
		public bool inSafePlace =true;
		public bool isActivated;

		public int pathIndex=-1;
		RectTransform  rectTransform;
		public float movementSpeed=.5f;

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
			Debug.Log ("sdfsd");
			isActivated = true;
			//iTween.PunchScale(this.gameObject, iTween.Hash("amount", new Vector3(0.2f, 0.2f, 1f), "time", 1f, "looptype", iTween.LoopType.pingPong));
		}

		public void DeActivateToken(){
			isActivated = false;
			iTween.Stop ();
		}


		public void DriveToken(RectTransform origin, RectTransform destination){

			pathIndex = 0;
			iTween.ValueTo(this.gameObject, iTween.Hash(
				"from",origin.anchoredPosition,
				"to", destination.anchoredPosition,
				"time", movementSpeed,
				"onupdatetarget", this.gameObject, 
				"onupdate", "MoveGuiElement",
			    "oncomplete","CompleteMove"
			));
		 }

		public void DriveTokenSequence(RectTransform origin, RectTransform destination){
				iTween.ValueTo(this.gameObject, iTween.Hash(
					"from",origin.anchoredPosition,
					"to", destination.anchoredPosition,
					"time", movementSpeed,
					"onupdatetarget", this.gameObject, 
					"onupdate", "MoveGuiElement"
				));
		
		}


		public void CompleteMove(){
			EndTurn (this);
		}

		public void DriveToken(RectTransform origin, List<RectTransform> destination){
			StartCoroutine (MoveTokenInPath (origin,destination));
		}

		IEnumerator MoveTokenInPath(RectTransform origin, List<RectTransform> destination){
			WaitForSeconds waitSeconds = new WaitForSeconds (movementSpeed);
			DriveTokenSequence (origin,destination[0]);
			yield return waitSeconds;
			for (int count = 0; count < destination.Count-1; count++) {
				DriveTokenSequence (destination [count], destination [count + 1]);
				yield return waitSeconds;
			}
			pathIndex += destination.Count;
			CompleteMove ();
		}


		public void MoveGuiElement(Vector2 position){
			rectTransform.anchoredPosition = position;
		}

	}
}
