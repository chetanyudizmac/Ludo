using UnityEngine;
using System.Collections;

namespace Games.Ludo {
	/// <summary>
	/// Base class for all games in the app
	/// </summary>
	public class GameManager : MonoBehaviour {

		public static GameManager instance;
		public  GameType currentGameType=GameType.VsComputer;

		void Awake() {
			instance = this;
		}



		public  void LoadGame(GameType gameType) {
			
			if (gameType == GameType.LocalMode) {
				
			} else if (gameType == GameType.VsComputer) {
				
			}
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