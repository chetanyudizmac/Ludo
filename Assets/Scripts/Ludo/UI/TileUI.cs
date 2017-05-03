using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Games.Ludo{

public class TileUI : MonoBehaviour {

		public Image  tileImage;

		void Awake(){
			tileImage = this.GetComponent<Image> ();
		}

}

}
