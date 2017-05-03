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

		public RegionType   currentRegionType;  //current player region 
		public Region currentPlayerRegion;
		public void Start(){
			currentPlayerRegion = Board.instance.region1;
		}

		public void SetCurrentRegion(int colorNumber){
			currentRegionType = (RegionType)Enum.ToObject(typeof(RegionType) , colorNumber);
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
			Board.instance.SetRegionsForLocal ();
		}

		public void  StartGameOfTypeLocal(){
		}

}
}
