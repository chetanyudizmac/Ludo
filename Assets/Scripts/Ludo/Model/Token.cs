using System.Collections;
using System.Collections.Generic;
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

		RectTransform  rectTransform;
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
			iTween.PunchScale(this.gameObject, iTween.Hash("amount", new Vector3(0.2f, 0.2f, 1f), "time", 1f, "looptype", iTween.LoopType.pingPong));
		}

		public void DeActivateToken(){
			isActivated = false;
			iTween.Stop ();
		}


		public void DriveToken(RectTransform origin, RectTransform destination){
			iTween.ValueTo(this.gameObject, iTween.Hash(
				"from",origin.anchoredPosition,
				"to", destination.anchoredPosition,
				"time", .5f,
				"onupdatetarget", this.gameObject, 
				"onupdate", "MoveGuiElement"));
		 }

			

			public void MoveGuiElement(Vector2 position){
			rectTransform.anchoredPosition = position;
			}

	}
}
