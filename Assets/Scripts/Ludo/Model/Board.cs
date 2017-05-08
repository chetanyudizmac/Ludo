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

		public void Awake(){
			instance = this;
		}

		public void FillBoard(){			
			
		}

		public void SetRegionsForVsComputer(int colorNumber){
			region1.SetRegionColor ((RegionType)Enum.ToObject (typeof(RegionType), colorNumber % 4));
			region2.SetRegionColor ((RegionType)Enum.ToObject (typeof(RegionType), (1 + colorNumber) % 4));
			region3.SetRegionColor ((RegionType)Enum.ToObject (typeof(RegionType), (2 + colorNumber) % 4));
			region4.SetRegionColor((RegionType)Enum.ToObject(typeof(RegionType) , (3+colorNumber) % 4));		
		}

		public void SetRegionsForLocal(int region1Color, int region2Color, int region3Color, int region4Color){
			region1.SetRegionColor ((RegionType)Enum.ToObject (typeof(RegionType), region1Color % 4));
			region2.SetRegionColor ((RegionType)Enum.ToObject (typeof(RegionType), (1 + region2Color) % 4));
			region3.SetRegionColor ((RegionType)Enum.ToObject (typeof(RegionType), (2 + region3Color) % 4));
			region4.SetRegionColor((RegionType)Enum.ToObject(typeof(RegionType) , (3+region4Color) % 4));	
		}
		public void CreateToken(){
			region1.CreateToken ();
			region3.CreateToken ();
		}

		public void CreateToken(Region region1,Region region3){
			
		}

		public void CreateToken(Region region1, Region region2, Region region3){
		
		}

		public void CreateToken(Region region1, Region region2, Region region3, Region region4){
		
		}






        }
}
