using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Games.Ludo{

public class Tile : MonoBehaviour {

		[HideInInspector]
		public TileUI tileUI;
		public int tileNumber;
		private RegionType _regionType;
		public bool isDestination;
		public RegionType RegionType{
			get{ return _regionType;}
			set{ _regionType = value;}
		}

		public bool isSafePlace;

		public List<Token> tokenInTile = new List<Token>();

		public void Awake(){
			tileUI = this.GetComponent<TileUI> ();
		}

		public void SetTokenInTile(Token token){
			tokenInTile.Add(token);
		}

		public void RemoveTokenInTile(Token token){
			if (tokenInTile == null)
				return;
			if (tokenInTile.Contains (token))
			tokenInTile.Remove (token);
		}

		public Token TokenToKill(Token activatedToken){
			Token tokenToRemove=new Token();
			foreach(Token token in tokenInTile){
				if(token!=activatedToken)
				if (token.region.regionType != activatedToken.region.regionType) {
					tokenToRemove = token;
					break;
				}
			}
			return tokenToRemove;
		}

		public void ArrangeTokenInTile(){
			tileUI.ArrangeTileUI (tokenInTile);
		}

}
}
