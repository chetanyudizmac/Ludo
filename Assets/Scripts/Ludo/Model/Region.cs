using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Games.Ludo{

public class Region : MonoBehaviour {

		public delegate void MakeMoveDelegate(Token token);
		public MakeMoveDelegate  MakeMove;

		public delegate void RegionActivated();
		public RegionActivated OnRegionActivated;
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
		protected List<Token> tokenList = new List<Token>();
		public List<RectTransform> tokenPosition = new List<RectTransform>();
		public List<Tile> tokenPath = new List<Tile>();

		public Dice dice;
		public bool isAI;
		public bool isKilled;
		public bool isInYard;
		public bool isAnyTokenIsActivated;
		Token activatedToken;   //prevents more than 1 token at single time
		public Region(){
		}

		void OnEnable(){
			OnRegionActivated += CheckIsAI;
		}

		void OnDisable(){
			MakeMove = null;
			OnRegionActivated = null;
		}

		public void CheckIsAI()
		{
			if (isAI) {
				RollDice ();
			}
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
			
				for(int count=0; count<tokenPosition.Count;count++){
				Token currentToken = Instantiate (token,tokenPosition[count].position,Quaternion.identity) as Token;
				currentToken.transform.SetParent (Board.instance.transform, false);	
				currentToken.transform.position = tokenPosition[count].position;
				currentToken.basePosition = tokenPosition[count];
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
			yield return new WaitForSeconds (.5f);
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
					if (number == 6) {
						if (token.inHome) 
							token.ActivateToken ();
						
						if ((token.pathIndex + number) < tokenPath.Count+1 && !token.inHome) 
							token.ActivateToken ();	
						
						
					} else {
						if ((token.pathIndex + number) < tokenPath.Count+1 && !token.inHome)
							token.ActivateToken ();
					}
				}
			}
			List<Token> tokenToMove = new List<Token>();
			int noOfTokenInDestinaion = 0;
			NoOfActivatedToken (ref tokenToMove,ref noOfTokenInDestinaion);
			Debug.Log (noOfTokenInDestinaion);
			if (noOfTokenInDestinaion == 4) {
				Debug.Log ("Win");
			}
			if (tokenToMove.Count == 1) {
				tokenToMove [0].AutomatedTokenClicked (tokenToMove [0]);
			} else if (isAI && tokenToMove.Count != 0) {
				int no = Random.Range (0, tokenToMove.Count);
				tokenToMove [no].AutomatedTokenClicked (tokenToMove [no]);
			}
			if (!isAnyTokenIsActivated) {
				EndTurn ();
			}
		}

		public void DeactivateRegion(){			
			foreach (Token token in tokenList) {
				token.DeActivateToken ();
			}
		}

		public void MakeMoveLocalMode(Token token){
			List<Tile> path = new List<Tile> (); 
			if (activatedToken!=null)
				return;
			activatedToken = token;
			if (token.inHome) {	
				token.inHome = false;
				path.Add (tokenPath [0]);
				StartCoroutine(MoveToken(path));
			}
			else {				
				for(int count=1;count<=dice.DiceNumber;count++){
					path.Add (tokenPath[token.pathIndex+count]);
				}
				StartCoroutine(MoveToken(path));
			}
		}

		public IEnumerator MoveToken(List<Tile> tokenPath){	
			
			if(activatedToken.residingTile!=null)
			activatedToken.residingTile.RemoveTokenInTile (activatedToken);
			bool isFinished ;
			WaitForEndOfFrame frame = new WaitForEndOfFrame ();
			foreach (Tile tile in tokenPath) {
				isFinished = false;
				StartCoroutine(activatedToken.DriveToken(tile.tileUI.rectTransform,1,value=>isFinished=value));
				while (!isFinished) {
					yield return frame;
				}
			}
			activatedToken.residingTile = tokenPath [tokenPath.Count - 1];
			tokenPath [tokenPath.Count - 1].SetTokenInTile(activatedToken);
			EndTurn ();
		}
		/// <summary>
		/// Returns no of activated token
		/// </summary>
		/// <returns>The of activated token.</returns>
		public bool NoOfActivatedToken(ref List<Token> tokenToMove,ref int noOfActivatedToken)
		{
			int count=0;
			isAnyTokenIsActivated = false;
			foreach (Token token in tokenList) {
				if (token.isActivated) {
					tokenToMove.Add (token);
					isAnyTokenIsActivated = true;
				}
				if(token.residingTile!=null)
				if (token.residingTile.isDestination) {
					count++;
				}
			}
			noOfActivatedToken = count;
			return isAnyTokenIsActivated;
		}



		public void MakeMoveVsComputerMode(Token token){
			List<Tile> path = new List<Tile> (); 
			if (activatedToken!=null)
				return;
			activatedToken = token;
			if (token.inHome) {	
				token.inHome = false;
				path.Add (tokenPath [0]);
				StartCoroutine(MoveToken(path));
			}
			else {				
				for(int count=1;count<=dice.DiceNumber;count++){
					path.Add (token.region.tokenPath[token.pathIndex+count]);
				}
				StartCoroutine(MoveToken(path));
			}
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

		public void EndTurn(){			
			DeactivateRegion ();
			if(activatedToken!=null)
			if(!activatedToken.inHome)
			if (activatedToken.residingTile != null) {
				if (activatedToken.residingTile.tokenInTile.Count>1) {
					Token tokenToRemove = activatedToken.residingTile.TokenToKill (activatedToken);

					if (tokenToRemove != null) {
						Debug.Log ("kill");
						StartCoroutine (DriveTokenToHome(tokenToRemove));
						isKilled = true;
						tokenToRemove.residingTile.RemoveTokenInTile (tokenToRemove);
					} else {
						Debug.Log ("same region available");
					}
				}
			}
			activatedToken = null;
			if (dice.DiceNumber == 6||isKilled||isInYard) {
				isKilled = false;
				isInYard = false;
				if (!isAnyTokenIsActivated) {
					BoardManager.instance.NextTurn ();
					return;
				}
				dice.EnableDice ();
				OnRegionActivated ();
			}
			else {
				BoardManager.instance.NextTurn ();
			}
		}

		IEnumerator DriveTokenToHome(Token token){
			Debug.Log ( "A:"+token.residingTile.gameObject.name+":B"+token.region.name);
				WaitForEndOfFrame frame = new WaitForEndOfFrame ();
				bool isFinished=false;
			List<Tile> tokenList = new List<Tile> ();
			int index = token.region.tokenPath.IndexOf (token.residingTile);  //token which was caught by current region
			for (int count = index; count >= 0; count--) {
				tokenList.Add (token.region.tokenPath[count]);
		    }
			foreach (Tile tile in tokenList) {
				isFinished = false;
				StartCoroutine (token.DriveToken (tile.tileUI.rectTransform, -1, value => isFinished = value,.1f));
				while (!isFinished) {
					yield return frame;
				}
			}

			isFinished = false;
			StartCoroutine (token.DriveToken (token.basePosition,-1, value => isFinished = value,.1f));
			while (!isFinished) {
				yield return frame;
			}
			token.inHome = true;
			token.inSafePlace = true;
			token.residingTile = null;//irfan

			// first position to yard
			yield return null;
		}

		public void GetBackToHome(){
			
		}

		public void CreateTokenInRegion(){
		}



	}


        
	
}

