using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Only work if associated with a component having an image
public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	public enum DropZoneType {HAND, BOARD, DEPLOYMENT_ZONE, GRAVEYARD};
	public DropZoneType type;

	public Color highlightColor;
	private Color normalColor;

	private GameManager gm;

	void Awake() {
		normalColor = this.GetComponent<Image>().color;

		// TODO should be retrieved in a safer way
		// currently only needed to get the currentStatus
		gm = GameObject.Find("GameManager").GetComponent<GameManager>(); 
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

			if (this.type == DropZoneType.GRAVEYARD) {
				// Can only be dropped to the graveyard in the maintenance phase
				Debug.Log(gm);
				if (gm.currentPhase == GameManager.GamePhase.ACTIVE_MAINTENANCE_INVALID) {
					// Remove the card from the list
					d.parentToReturnTo.GetComponent<DeploymentZone>().RemoveCard(d);

					// Parent of the card is now the graveyard
					d.parentToReturnTo = this.transform;	// TODO should add it to the graveyard class

					// Update the maintenance
					gm.activePlayer.MaintenanceCheck();

					// TODO should make the card non interactable
					// CONTINUE HERE
				} else {
					// Do nothing
				}
			} else {
				d.parentToReturnTo = this.transform;
			}
		}
	}

	public void Highlight() {
		this.GetComponent<Image>().color = this.highlightColor;
	}

	public void StopHighlight() {
		this.GetComponent<Image>().color = this.normalColor;
	}
}
