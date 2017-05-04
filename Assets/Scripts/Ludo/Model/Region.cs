using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Games.Ludo{

public class Region : MonoBehaviour {


		//specific by time
		public RegionType regionType;
		public Token token;
		/// <summary>
		///  collection of Tile list residing in particular region
		/// </summary>
		public List<Tile> tileList;

		Color currentColor;

		public Image destinationHome;
		public Image yard;

		List<Token> tokenList = new List<Token>();

		public List<Transform> tokenPosition = new List<Transform>();


		public List<Tile> tokenPath = new List<Tile>();

		public int GetRemainingToken(){		
			return 0;
		}

		public void SetRegionColor(RegionType type){
			regionType = type;
			if (type == RegionType.Red) {
				currentColor = Color.red;
			} else if (type == RegionType.Blue) {
				currentColor = Color.blue;
			} else if (type == RegionType.Green) {
				currentColor = Color.green;
			} else if (type == RegionType.Yellow) {
				currentColor = Color.yellow;
			}
			FillColorInRegion ();
		}

		public void FillColorInRegion(){
			if (currentColor!=null) {
				foreach (Tile tile in tileList) {
					tile.RegionType = regionType;
					if(tile.isSafePlace)
					tile.tileUI.tileImage.color = currentColor;
				}

				destinationHome.color = currentColor;
				yard.color = currentColor;
			}
			else{
				Debug.Log("Region Color not assigned");
			}
		}


		public void CreateToken(){
			
			foreach(Transform currentPosition in tokenPosition){
				Token currentToken = Instantiate (token) as Token;
				currentToken.transform.SetParent (this.transform, false);	
				currentToken.transform.position = currentPosition.position;
				currentToken.SetTokenProperty (regionType, currentColor);
			}
		}

		public void GetBackToHome(){
			
		}

		public void CreateTokenInRegion(){
		}

	}


        
	
}

