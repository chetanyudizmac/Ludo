using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Games.Ludo{
public class Token : MonoBehaviour {

		public RegionType regionType;
		public Image image;

		public void Awake(){
			image = this.GetComponent<Image> ();
		}

		public void SetColor(Color color){
			
		}

		public void SetTokenProperty(RegionType type, Color color){
			regionType = type;
			image.color = color;
		}

		public void TokenCliked(){
			Debug.Log("TokenClicked");
		}
}
}
