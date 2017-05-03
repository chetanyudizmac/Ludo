using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Games.Ludo{
public class ViewInPlay : UIView {

		public static ViewInPlay instance;

		public override void Awake ()
		{
			base.Awake ();
			instance = this;
		}
		public override void Show()
		{
			base.Show ();
		}
		public override void Hide()
		{
			base.Hide();
		}
}
}
