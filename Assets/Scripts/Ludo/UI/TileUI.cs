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

		public void ArrangeTileUI(List<Token> tokenInTile){
			float scale = 1;

			float distanceOffset = 20;
			if (tokenInTile.Count > 1) {
				scale = (float)1 / (1+(tokenInTile.Count*.25f));
			}

			foreach (Token token in tokenInTile) {
				token.gameObject.name = Random.Range (0, 10).ToString();
				token.rectTransform.sizeDelta = new Vector3(token.rectTransform.sizeDelta.x*scale,token.rectTransform.sizeDelta.y*scale);
			}
			for (int count = 0; count < tokenInTile.Count; count++) {
				tokenInTile[count].rectTransform.sizeDelta = new Vector2(tokenInTile[tokenInTile.Count-1].defaultSize.x*scale,tokenInTile[tokenInTile.Count-1].defaultSize.y*scale);			
			}

			for (int count = 0; count <tokenInTile.Count; count++) {
				tokenInTile [count].rectTransform.anchoredPosition = new Vector2 (rectTransform.anchoredPosition.x+(count*distanceOffset),tokenInTile [count].rectTransform.anchoredPosition.y);
			}
		}
	}
}
