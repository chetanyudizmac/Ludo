using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Games.Ludo{

public class Tile : MonoBehaviour {

		public int tileNumber;

		 private RegionType _regionType;

		public RegionType RegionType{
			get{ return _regionType;}
			set{ _regionType = value;}
		}


		public bool isSafePlace;


}
}
