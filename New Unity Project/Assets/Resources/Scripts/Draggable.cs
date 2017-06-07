using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public Transform parentToReturnTo = null;
	private ResourceManager rm;

	// PlaceHolder used to keep track of the position where the card will be when the drag ends
	public Transform placeHolderParent = null;
	private GameObject placeHolder = null;

	public void Awake() {
		rm = GameObject.FindObjectOfType<ResourceManager>();
	}

	public void OnBeginDrag(PointerEventData eventData) {
		Debug.Log("OnBeginDrag");
		// TODO compute the diff between the point of click and the center, and make an offset that will be used when dragging

		CreatePlaceHolder();

		parentToReturnTo = this.transform.parent;
		placeHolderParent = parentToReturnTo; 
		this.transform.SetParent(this.transform.parent.parent);

		GetComponent<CanvasGroup>().blocksRaycasts = false;

		// TODO refactor that part, to make it glow (shader?) and more optimal by prestoring those values
		foreach (DropZone d in rm.dropZones) {
			if (d.type == DropZone.DropZoneType.BOARD) {
				d.Highlight();
			}
		}

	}

	public void OnDrag(PointerEventData eventData) {
		this.transform.position = eventData.position;

		if (placeHolder.transform.parent != placeHolderParent) {
			placeHolder.transform.SetParent(placeHolderParent);
		}

		int newSiblingIndex = placeHolderParent.childCount;
		for (int i = 0; i < placeHolderParent.childCount; i++) {
			if (this.transform.position.x < placeHolderParent.GetChild(i).position.x) {

				newSiblingIndex = i;
				if (placeHolder.transform.GetSiblingIndex() < newSiblingIndex)
					newSiblingIndex--;

				break;
			}
		}

		placeHolder.transform.SetSiblingIndex(newSiblingIndex);
	}

	public void OnEndDrag(PointerEventData eventData) {
		Debug.Log("OnEndDrag");
		this.transform.SetParent(parentToReturnTo);	// if dragged on a new zone, this is when it is attached to the zone
		this.transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());

		GetComponent<CanvasGroup>().blocksRaycasts = true;

		foreach (DropZone d in rm.dropZones) {
			if (d.type == DropZone.DropZoneType.BOARD) {
				d.StopHighlight();
			}
		}

		Destroy(placeHolder);
	}

	private void CreatePlaceHolder() {
		placeHolder = new GameObject();
		placeHolder.transform.SetParent(this.transform.parent);
		LayoutElement placeHolderLayout = placeHolder.AddComponent<LayoutElement>();
		placeHolderLayout.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
		placeHolderLayout.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
		placeHolderLayout.flexibleWidth = 0;
		placeHolderLayout.flexibleHeight = 0;

		placeHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());	// set the same index for the placeholder than the one of the card 
	}



}
