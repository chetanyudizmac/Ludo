using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Games.Ludo{
public class Token : MonoBehaviour {

		public Region region;
		public Image image;
		public bool inHome = true;
		public bool inSafePlace =true;
		public void Awake(){
			image = this.GetComponent<Image> ();
		}

		public void SetColor(Color color){			
		}


		public void SetTokenProperty(Region reg, Color color){
			region = reg;
			image.color = color;
		}

		public void TokenCliked(){
			Debug.Log("TokenClicked");
		}
}
}
