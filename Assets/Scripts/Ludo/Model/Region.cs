using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

namespace Games.Ludo{

public class Region : MonoBehaviour {


		//specific by time
		public RegionType regionType;

		/// <summary>
		///  collection of Tile list residing in particular region
		/// </summary>
		public List<Tile> tileList;


		public int GetRemainingToken(){		
			return 0;
		}

		public void SetRegionColor(RegionType type){
		}


        
	
}
}
