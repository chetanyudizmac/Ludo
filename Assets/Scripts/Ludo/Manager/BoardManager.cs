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

		public void  StartGameOfTypeLocal(){
			
			
		}
		public void NextTurnLocalMode(){
			Debug.Log ("NextTurnLocalMode");
		}

		#region localMode
		#endregion


		#region VsComputer integration
		public void StartGameOfTypeVsComputer(){
			Board.instance.SetRegionsForVsComputer (PopUpVsComputerMode.instance.selectedColor);
			Board.instance.CreateToken ();
			GameManager.instance.gameStatus = GameStatus.InGameplay;
			activeRegionList.AddLast (Board.instance.region1);
			activeRegionList.AddLast (Board.instance.region3);
			InitiateTurn (activeRegionList);
		}

		public void InitiateTurn(LinkedList<Region> regionList){
			int random = 0.RandomNumber (2);
			Debug.Log (random);
			if (random == 1) {
				currentActivatedeRegion = regionList.First;			   
			} else {
				currentActivatedeRegion = regionList.Last;
			}
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
				Debug.Log (token.region.name);
				token.region.MakeMove (token);
			} else {
				
			}
		}


}
}
