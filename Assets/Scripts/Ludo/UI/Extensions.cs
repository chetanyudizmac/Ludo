using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;


public static class Extensions {

	public static float Remap (this float value, float from1, float to1, float from2, float to2) {
	
	    return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}


	public static Color fromRGB (this Color value, int r, int g, int b) {
		return new Color((float)r/255f, (float)g/255f, (float)b/255f);
	}
	


	public static List<Color> Shades (this Color value, int shadesCount, float spectrum) {



		List<Color> shades = new List<Color>();

		for (int i=0; i<shadesCount; i++) {

			float step = spectrum / (float)shadesCount;
			float delta = (step * (float)i) - (spectrum * 0.5f);

			Color shade = value + new Color(delta, delta, delta, 0);

			shades.Add(shade);
		}
		return shades;
	}



	public static string ToStringWithScoreFormat (this int value) {
		if (value == 0) return "0";
		else return string.Format("{0:#,###,###}", value);
	}
	

	public static Vector3 Center (this List<Vector3> value) {

		if (value.Count == 0) return Vector3.zero;
		else if (value.Count == 1) return value[0];
		else {

			Bounds bounds = new Bounds(value[0], Vector3.zero);
			for (int i=1; i<value.Count; i++) {
				bounds.Encapsulate(value[i]);
			}
			return bounds.center;
		}
	}



	public static List<DateTime> SortAscending(this List<DateTime> list)
	{
		list.Sort((a, b) => a.CompareTo(b));
		return list;
	}



//
//	public static void FadeIn(this Image value, float time) {
//
////		iTween.ValueTo(value.gameObject, iTween.Hash("from", 0, "to", 1f, "time", time, 
////			"onupdate", (System.Action<object>) (newVal => {
////				value.color = new Color(value.color.r, value.color.g, value.color.b, (float)newVal);
////			})));
////
//
//
//			
//	} 
//

}

