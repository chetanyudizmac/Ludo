using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Games.Ludo{
public class BoardManager : MonoBehaviour {

		public static BoardManager instance;

		public void Awake(){
			instance = this;
		}

		//region who have turn to play
		public Region currentActivatedeRegion;

		public void Start(){
		}

		public void PlayMatch(){
			if (GameManager.instance.currentGameType == GameType.LocalMode) {
				StartGameOfTypeLocal ();
			}
			else if(GameManager.instance.currentGameType == GameType.VsComputer){
				StartGameOfTypeVsMachine();
			}
		}

		public void StartGameOfTypeVsMachine(){
			Board.instance.SetRegionsForVsComputer (PopUpVsComputerMode.instance.selectedColor);
			Board.instance.CreateToken ();
			GameManager.instance.gameStatus = GameStatus.InGameplay;
			InitiateTurn (Board.instance.region1,Board.instance.region3);
		}


		public void InitiateTurn(Region region1,Region region3){
			int random = 1.RandomNumber (2);
			if (random == 1) {
				currentActivatedeRegion = region1;
			} else {
				currentActivatedeRegion = region3;
			}
		}

		public void InitiateTurn(Region region1, Region region2,Region region3,Region region4){
			
		}

		public void InitiateTurn(Region region1,Region region2, Region region3){
		}


		public void  StartGameOfTypeLocal(){
			
		}

}
}
