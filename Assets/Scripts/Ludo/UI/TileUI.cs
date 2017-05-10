using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Games.Ludo{

public class TileUI : MonoBehaviour {

		public Image  tileImage;
		public RectTransform rectTransform;
		void Awake(){
			rectTransform = this.GetComponent<RectTransform> ();
			tileImage = this.GetComponent<Image> ();
		}

}

}
