using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Games.Ludo{

	static class CircularLinkedList {
		public static LinkedListNode<T> NextOrFirst<T>(this LinkedListNode<T> current)
		{
			return current.Next ?? current.List.First;
		}

		public static LinkedListNode<T> PreviousOrLast<T>(this LinkedListNode<T> current)
		{
			return current.Previous ?? current.List.Last;
		}
	}

public class BoardManager : MonoBehaviour {

		public delegate void NextTurnDelegate();
		public NextTurnDelegate NextTurn;

		public delegate void StartGameDelegate();
		public StartGameDelegate StartGame;

		public static BoardManager instance;

		LinkedList<Region> activeRegionList = new LinkedList<Region>();
		int random = 0;


		public void Awake(){
			instance = this;
		}

		//region who have turn to play
		public LinkedListNode<Region> currentActivatedeRegion = new LinkedListNode<Region>(new Region());


		public void Start(){
		}

		public void OnEnable(){
			Token.MoveToken += MoveToken;
		}

		public void OnDisable(){
			Token.MoveToken -= MoveToken;
			NextTurn = null;
			StartGame = null;
		}

		public void SetMode(){
			if (GameManager.instance.currentGameType == GameType.LocalMode) {   
			NextTurn += NextTurnLocalMode;
			StartGame += StartGameOfTypeLocal;
				Board.instance.region1.MakeMove += Board.instance.region1.MakeMoveLocalMode;
				Board.instance.region2.MakeMove += Board.instance.region2.MakeMoveLocalMode;
				Board.instance.region3.MakeMove += Board.instance.region3.MakeMoveLocalMode;
				Board.instance.region4.MakeMove += Board.instance.region4.MakeMoveLocalMode;
			}
			else if(GameManager.instance.currentGameType == GameType.VsComputer){
				NextTurn += NextTurnVsComputerMode;
				StartGame +=StartGameOfTypeVsComputer;
				Board.instance.region1.MakeMove += Board.instance.region1.MakeMoveVsComputerMode;
				Board.instance.region2.MakeMove += Board.instance.region2.MakeMoveVsComputerMode;
				Board.instance.region3.MakeMove += Board.instance.region3.MakeMoveVsComputerMode;
				Board.instance.region4 .MakeMove+= Board.instance.region4.MakeMoveVsComputerMode;
			}
		}


		public void PlayMatch(){
			StartGame ();
		}




		public void InitiateTurn(Region region1, Region region2,Region region3,Region region4){			
		}

		public void InitiateTurn(Region region1,Region region2, Region region3){			
		}

		public void StartDicing(){	
			currentActivatedeRegion.Value.RollDice ();	
		}


		public void NextTurnLocalMode(){
			currentActivatedeRegion.Value.dice.DisableDice ();
			currentActivatedeRegion=currentActivatedeRegion.NextOrFirst();
			currentActivatedeRegion.Value.dice.EnableDice ();
		}

		#region localMode
		#endregion
		public void  StartGameOfTypeLocal(){
			Board.instance.SetRegionsForLocal (PopUpLocalMode.instance.r1Color,PopUpLocalMode.instance.r2Color,PopUpLocalMode.instance.r3Color,PopUpLocalMode.instance.r4Color);
			GameManager.instance.gameStatus = GameStatus.InGameplay;
			if (PopUpLocalMode.instance.noOfActivePlayers == 2) {
				Board.instance.CreateToken (Board.instance.region1,Board.instance.region3);
				activeRegionList.AddLast (Board.instance.region1);
				activeRegionList.AddLast (Board.instance.region3);
			} 
			else if (PopUpLocalMode.instance.noOfActivePlayers == 3) {
				Board.instance.CreateToken (Board.instance.region1,Board.instance.region2,Board.instance.region3);
				activeRegionList.AddLast (Board.instance.region1);
				activeRegionList.AddLast (Board.instance.region2);
				activeRegionList.AddLast (Board.instance.region3);
			} 
			else {
				Board.instance.CreateToken (Board.instance.region1,Board.instance.region2,Board.instance.region3,Board.instance.region4);
				activeRegionList.AddLast (Board.instance.region1);
				activeRegionList.AddLast (Board.instance.region2);
				activeRegionList.AddLast (Board.instance.region3);
				activeRegionList.AddLast (Board.instance.region4);
			}
			InitiateTurn (activeRegionList);
		}

		#region VsComputer integration
		public void StartGameOfTypeVsComputer(){
			Board.instance.SetRegionsForVsComputer (PopUpVsComputerMode.instance.selectedColor);
			Board.instance.CreateToken ();
			GameManager.instance.gameStatus = GameStatus.InGameplay;
			activeRegionList.AddLast (Board.instance.region1);
			activeRegionList.AddLast (Board.instance.region3);
			InitiateTurn (activeRegionList);
		}
		/// <summary>
		/// from no of player it choose which one to initiate the game.
		/// </summary>
		/// <param name="regionList">Region list.</param>
		public void InitiateTurn(LinkedList<Region> regionList){
			currentActivatedeRegion = regionList.First;	
			if (currentActivatedeRegion.Value.dice != null) {
				currentActivatedeRegion.Value.dice.EnableDice ();
			} else {
				Debug.Log ("null");
			}
		}

		public void NextTurnVsComputerMode(){
			currentActivatedeRegion.Value.dice.DisableDice ();
			currentActivatedeRegion=currentActivatedeRegion.NextOrFirst();
			currentActivatedeRegion.Value.dice.EnableDice ();
		}
		#endregion

		public void MoveToken(Token token){
			if (token.isActivated) {
				token.region.MakeMove (token);
			} else {
				
			}
		}

		public void EndTurn(Token token){
			token.region.EndTurn ();
		}


}
}
