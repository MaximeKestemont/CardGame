using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Only work if associated with a component having an image
public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	public enum DropZoneType {HAND, BOARD};
	public DropZoneType type;

	public Color highlightColor;
	private Color normalColor;

	void Awake() {
		normalColor = this.GetComponent<Image>().color;
	}

	public void OnPointerEnter(PointerEventData eventData) {
		// Check that we are dragging something
		if (eventData.pointerDrag == null)
			return;

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if (d != null) {
			d.placeHolderParent = this.transform;
		}
	}

	public void OnPointerExit(PointerEventData eventData) {
		// Check that we are dragging something
		if (eventData.pointerDrag == null)
			return;

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if (d != null && d.placeHolderParent == this.transform) {
			d.placeHolderParent = d.parentToReturnTo;
		}
	}

	public void OnDrop(PointerEventData eventData) {
		Debug.Log(eventData.pointerDrag.name + "was drop to " + gameObject.name);

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		// TODO can add conditions here (check if enough food to play the card, if the DropZoneType is correct, etc.)
		if (d != null) {
			d.parentToReturnTo = this.transform;
		}
	}

	public void Highlight() {
		this.GetComponent<Image>().color = this.highlightColor;
	}

	public void StopHighlight() {
		this.GetComponent<Image>().color = this.normalColor;
	}
}
