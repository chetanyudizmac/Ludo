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

}
}
