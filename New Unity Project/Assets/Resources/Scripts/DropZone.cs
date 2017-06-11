using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Only work if associated with a component having an image
public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {



	private GameManager gm;

	void Awake() {
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
			// TODO all this logic should be put in separate class (PhaseUtils ? )

			Zone.ZoneType parentType = d.parentToReturnTo.GetComponent<Zone>().type;
			Zone currentZone = this.GetComponent<Zone>();

			switch (currentZone.type) {

			case Zone.ZoneType.GRAVEYARD: 
				// Can only be dropped to the graveyard in the maintenance phase, from the deployment zone
				if (gm.currentPhase == GameManager.GamePhase.ACTIVE_MAINTENANCE_INVALID
					&& parentType == Zone.ZoneType.DEPLOYMENT_ZONE) {
					DeploymentZone previousZone = d.parentToReturnTo.GetComponent<DeploymentZone>();

					// Remove the card from the card list of the previous zone
					previousZone.RemoveCard(d);

					// Add the card to the graveyard
					currentZone.AddCard(d);

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
				break;

			case Zone.ZoneType.DEPLOYMENT_ZONE:
				// TODO
				// from BOARD TO DEPLOYMENT ZONE
				// from DEPLOYMENT ZONE TO DEPLOYMENT ZONE
				d.parentToReturnTo = this.transform;
				break;

			case Zone.ZoneType.HAND:
				// Should never get a card back in the hand
				break;

			case Zone.ZoneType.BOARD:
				// Can only be dropped to the board from the hand, during the military phase
				if (gm.currentPhase == GameManager.GamePhase.MILITARY
					&& parentType == Zone.ZoneType.HAND
					&& gm.activePlayer.GetActionCounter() > 0) {

					Card c = d.GetComponent<Card>();
					// Check that the player can pay this card
					if (gm.activePlayer.GetFoodNumber() >= c.foodCost) {
						Hand handZone = d.parentToReturnTo.GetComponent<Hand>();

						// Remove the card from the hand
						handZone.RemoveCard(d);

						// Add the card to the board
						currentZone.AddCard(d);

						// Remove the cost from the food of the player
						gm.activePlayer.AddFood(c.foodCost * -1);

						// Cost one action to the player
						gm.activePlayer.DecrementActionCounter();

						// Parent of the card is now the board
						d.parentToReturnTo = this.transform;

					} else {
						// TODO popup to say that the card cannot be played because not enough food
						Debug.Log("This card is too expensive to play !");
					}

				}
				// d.parentToReturnTo = this.transform;
				break;

			}
		}
	}
}
