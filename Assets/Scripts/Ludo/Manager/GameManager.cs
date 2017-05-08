using UnityEngine;
using System.Collections;

namespace Games.Ludo {
	/// <summary>
	/// Base class for all games in the app
	/// </summary>
	public class GameManager : MonoBehaviour {

		public static GameManager instance;
		public  GameType currentGameType=GameType.VsComputer;
		public GameStatus gameStatus = GameStatus.InMenu;
		void Awake() {
			instance = this;
		}



		public  void LoadGame() {
			
			BoardManager.instance.SetMode ();
		}


		public  void StartGame() {
			BoardManager.instance.PlayMatch ();
			//Board.instance.SetRegions (selectedColor);	
		}
			
		public  void GameOver() {
			
		}

		public  void Pause() {
			
		}

		public  void Resume() {

		}

	}
}