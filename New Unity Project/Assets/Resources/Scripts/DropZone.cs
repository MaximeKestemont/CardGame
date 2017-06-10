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
		Debug.Log(eventData.pointerDrag.name + " was drop to " + gameObject.name);

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

		if (d != null) {

			if (this.type == DropZoneType.GRAVEYARD) {
				// Can only be dropped to the graveyard in the maintenance phase
				if (gm.currentPhase == GameManager.GamePhase.ACTIVE_MAINTENANCE_INVALID) {
					DeploymentZone previousZone = d.parentToReturnTo.GetComponent<DeploymentZone>();

					// Remove the card from the card list of the previous zone
					previousZone.RemoveCard(d);

					// One less unit to kill in the deployment zone
					gm.activePlayer.RemoveUnitToKill(previousZone, 1);

					// Parent of the card is now the graveyard
					d.parentToReturnTo = this.transform;

					// Make the card non interactable
					d.isActive = false;

					// Update the maintenance
					gm.activePlayer.MaintenanceCheck();

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
