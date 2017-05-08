using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Games.Ludo{

public class Region : MonoBehaviour {

		public delegate void MakeMoveDelegate(Token token);
		public MakeMoveDelegate  MakeMove;

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
		public Dice dice;

		public Region(){
		}

		void OnEnable(){
		}

		void OnDisable(){
			MakeMove = null;
		}


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
			if (currentColor!=null){
				foreach (Tile tile in tileList){
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
				currentToken.SetTokenProperty (this, currentColor);
				tokenList.Add (currentToken);
			}
		}

		public void RollDice(){
			StartCoroutine (RollDiceCoroutine());
		}

		public IEnumerator RollDiceCoroutine(){
			bool isFinished=false;
			int number=0;
			StartCoroutine (dice.DiceAnimation(value=>isFinished=value, value=>number=value));
			WaitForEndOfFrame endFrame = new WaitForEndOfFrame ();
			while (!isFinished) {
				yield return endFrame;
			}
			yield return new WaitForSeconds (1);
			CheckMove (number);
		}

		public void CheckMove(int number){
			int numberOfTokenInYard = TokenInYard ();
			if (numberOfTokenInYard == 4) {
				if (number != 6) {
					Debug.Log ("All Players in home. and you havent got 6");
					EndTurn ();
				} else {
					Debug.Log ("gotcha");
					ActivateAllToken ();
				}
			}
		}


		public void ActivateAllToken(){
			foreach (Token token in tokenList) {
				token.ActivateToken ();
			}
		}

		public void DeactivateAllToken(){
			foreach (Token token in tokenList) {
				token.DeActivateToken ();
			}
		}


		public void MakeMoveLocalMode(Token token){
			if (token.inHome) {

				token.DriveToken (token.GetComponent<RectTransform>(),tokenPath[0].GetComponent<RectTransform>());
			}
			Debug.Log ("localMode Move");
		}


		public void MakeMoveVsComputerMode(Token token){	
			if (token.inHome) {	
				Debug.Log (this.gameObject.name);
				Debug.Log (tokenPath[0].gameObject.name);
				token.DriveToken (yard.rectTransform,tokenPath[0].GetComponent<RectTransform>());
			}
			Debug.Log ("ComputerModeMove");
		}
	
		/// <summary>
		/// number of players in yard
		/// </summary>
		/// <returns>The in home.</returns>
		public int TokenInYard(){
			int count = 0;
			foreach (Token token in tokenList) {
				if (token.inHome) {
					count++;
				}
			}
			return count;
		}

		void EndTurn(){
			DeactivateAllToken ();
			BoardManager.instance.NextTurn ();
		}

		public void GetBackToHome(){
			
		}

		public void CreateTokenInRegion(){
		}

	}


        
	
}

