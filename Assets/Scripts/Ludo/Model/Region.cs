using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

		bool isAnyTokenMoving = false; //prevents more than 1 token at single time

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
				currentToken.transform.SetParent (Board.instance.transform, false);	
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
			ActivateMovableToken (number);
		}

	
		public void ActivateMovableToken(int number){
			int numberOfTokenInYard = TokenInYard ();
			if (numberOfTokenInYard == 4) {   //check if all token in home
				if (number != 6) {
					Debug.Log ("All Players in home. and you havent got 6");
					EndTurn ();
				} else {
					Debug.Log ("gotcha");
					foreach (Token token in tokenList) {
						token.ActivateToken ();
					}
				}
			}
			else {  
				foreach (Token token in tokenList) {
					if (number == 6) {    // if number is 6 and there are  tokens in home
						token.ActivateToken ();
					} else {
						if ((token.pathIndex + number) < tokenPath.Count && !token.inHome)
							token.ActivateToken ();
					}
				}
			}
		}

		public void DeactivateRegion(){
			
			foreach (Token token in tokenList) {
				token.DeActivateToken ();
			}
		}




		public void MakeMoveLocalMode(Token token){
			if (isAnyTokenMoving)
				return;
			isAnyTokenMoving = true;
			if (token.inHome) {	
				token.inHome = false;
				token.DriveToken (token.GetComponent<RectTransform> (), tokenPath [0].GetComponent<RectTransform> ());
			}
			else {
				List<RectTransform> path = new List<RectTransform> (); 
				for(int count=1;count<=dice.DiceNumber;count++){
					path.Add (tokenPath[token.pathIndex+count].GetComponent<RectTransform>());
				}
				token.DriveToken (token.GetComponent<RectTransform> (), path);

			}
		}


		public void MakeMoveVsComputerMode(Token token){	
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

		public 	void EndTurn(){	
			isAnyTokenMoving = false;
			DeactivateRegion ();
			if (dice.DiceNumber == 6) {
				dice.EnableDice ();
			}
			else {
				BoardManager.instance.NextTurn ();
			}
		}

		public void GetBackToHome(){
			
		}

		public void CreateTokenInRegion(){
		}



	}


        
	
}

