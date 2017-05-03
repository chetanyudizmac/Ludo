using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Games.Ludo{
public class BoardUI : MonoBehaviour {


		public Board board;



		public void Start(){
			board = this.GetComponent<Board> ();
		}

	

	
}
}
