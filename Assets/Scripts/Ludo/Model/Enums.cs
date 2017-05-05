using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Games.Ludo{


	/// <summary>
	/// color specification for certain region
	/// </summary>
	public 	enum RegionType { Red=0, Green=1, Blue=2, Yellow=3 };


	public enum GameType{LocalMode,VsComputer};

	public enum GameStatus{InMenu,InGameplay,Paused};

}
