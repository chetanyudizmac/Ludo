using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Games.Ludo{

public class Tile : MonoBehaviour {

		[HideInInspector]
		public TileUI tileUI;
		public int tileNumber;
		private RegionType _regionType;

		public RegionType RegionType{
			get{ return _regionType;}
			set{ _regionType = value;}
		}

		public bool isSafePlace;

		public void Awake(){
			tileUI = this.GetComponent<TileUI> ();
		}


}
}
