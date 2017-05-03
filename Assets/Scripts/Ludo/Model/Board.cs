using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


namespace Games.Ludo{

public class Board : MonoBehaviour {

		public static Board instance;

		public Region region1;
		public Region region2;
		public Region region3;
		public Region region4;


		public RegionType currentRegionType;

		public void Awake(){
			instance = this;
		}

		public void FillBoard(){			
			
		}


		public void SetRegionsForLocal(){
			int colorNumber = PopUpVsComputerMode.instance.selectedColor;
			BoardManager.instance.SetCurrentRegion (colorNumber);
			region1.SetRegionColor ((RegionType)Enum.ToObject (typeof(RegionType), colorNumber % 4));
			region2.SetRegionColor ((RegionType)Enum.ToObject (typeof(RegionType), (1 + colorNumber) % 4));
			region3.SetRegionColor ((RegionType)Enum.ToObject (typeof(RegionType), (2 + colorNumber) % 4));
			region4.SetRegionColor((RegionType)Enum.ToObject(typeof(RegionType) , (3+colorNumber) % 4));		
		}

        }
}
