using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Games.Ludo{
public class UIPopUpMenu : MonoBehaviour {

	public Camera camera;
	public Image tint;
	public bool isVisible = false;
	private CanvasGroup canvasGroup;
	private float animationTime = 0.2f;


	public virtual void Awake() {
		canvasGroup = GetComponent<CanvasGroup>();

		//move on right place
		if (camera != null) {
			tint.transform.position = camera.transform.position;
		}
	}


	void Start() {
		Hide(false);
	}



	public virtual void Show(bool animated) {

		if (animated) {
			iTween.ValueTo(this.gameObject, iTween.Hash("from", 0, "to", 1f, "time", animationTime, "onupdate", "Animate", "oncomplete", "PopUpAppeared"));
		} else Animate(1f);
	
	}


	public virtual void Show(object param, bool animated) {

		Show(animated);
	}

	public virtual void Show(Vector3 worlsSpacePosition, bool animated) {
		
		Show(animated);
	}



	public virtual void Hide(bool animated) {

		if (animated) {
			iTween.ValueTo(this.gameObject, iTween.Hash("from", 1f, "to", 0f, "time", animationTime, "onupdate", "Animate", "oncomplete", "PopUpDisappeared"));
		} else Animate(0);

	}


	void Animate(float normalizedPosition) {

		if (tint != null) tint.color = new Color(tint.color.r, tint.color.g, tint.color.b, normalizedPosition);
		//this.transform.localPosition = new Vector3(normalizedPosition.Remap(0, 1f, 1500f, 0), this.transform.localPosition.y, this.transform.localPosition.z);

		canvasGroup.alpha = Mathf.Ceil(normalizedPosition);

		tint.GetComponent<Image>().enabled = normalizedPosition > 0;
		canvasGroup.interactable = normalizedPosition == 1f;
		canvasGroup.blocksRaycasts = normalizedPosition > 0;;
		isVisible = normalizedPosition > 0;

	}


	public virtual void PopUpAppeared() {

	}

	public virtual void PopUpDisappeared() {

	}
	}
}
