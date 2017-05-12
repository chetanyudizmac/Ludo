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

		public void SetRegionsForLocal(RegionType region1Color, RegionType region2Color, RegionType region3Color, RegionType region4Color){
			region1.SetRegionColor (region1Color);
			region2.SetRegionColor (region2Color);
			region3.SetRegionColor (region3Color);
			region4.SetRegionColor(region4Color);	
		}
		public void CreateToken(){
			region1.CreateToken ();
			region3.CreateToken ();
			region3.isAI = true;
		}

		public void CreateToken(Region region1,Region region3){
			region1.CreateToken ();
			region3.CreateToken ();
			
		}

		public void CreateToken(Region region1, Region region2, Region region3){
			region1.CreateToken ();
			region2.CreateToken ();
			region3.CreateToken ();
			
		}

		public void CreateToken(Region region1, Region region2, Region region3, Region region4){
			region1.CreateToken ();
			region2.CreateToken ();
			region3.CreateToken ();
			region4.CreateToken ();
		
		}






        }
}
