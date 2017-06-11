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
			// TODO all this logic should be put in separate class (PhaseUtils ? ) + factorised

			Zone parentZone = d.parentToReturnTo.GetComponent<Zone>();
			Zone currentZone = this.GetComponent<Zone>();

			switch (currentZone.type) {

			case Zone.ZoneType.GRAVEYARD: 
				// Can only be dropped to the graveyard in the maintenance phase, from the deployment zone
				if (gm.currentPhase == GameManager.GamePhase.ACTIVE_MAINTENANCE_INVALID
					&& parentZone.type == Zone.ZoneType.DEPLOYMENT_ZONE) {
					DeploymentZone parentDeploymentZone = parentZone as DeploymentZone;

					// Remove the card from the card list of the previous zone
					parentDeploymentZone.RemoveCard(d);

					// Add the card to the graveyard
					currentZone.AddCard(d);

					// One less unit to kill in the deployment zone
					gm.activePlayer.RemoveUnitToKill(parentDeploymentZone, 1);

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
				DeploymentZone currentDeploymentZone = currentZone as DeploymentZone;

				// from BOARD to DEPLOYMENT ZONE
				if (gm.currentPhase == GameManager.GamePhase.MILITARY
					&& parentZone.type == Zone.ZoneType.BOARD
				    && gm.activePlayer.GetActionCounter() >= 0) { // even if 0 action left, may be possible to move
				
					// The player has already paid the action cost of deployment
					if (currentDeploymentZone.nbDeployedHere < gm.activePlayer.nbDeploymentMove) {
						// DO NOT COST AN ACTION TO THE PLAYER

						// Remove the card from the card list of the previous zone
						parentZone.RemoveCard(d);

						// Add the card to the new zone
						currentDeploymentZone.AddCard(d);

						// Update the nb of cards deployed
						currentDeploymentZone.nbDeployedHere++;

						// Parent of the card is now the new deployment zone
						d.parentToReturnTo = this.transform;

					} else {
						// The player has to pay an action to do this move
						if (gm.activePlayer.GetActionCounter() > 0) {
							// Remove the card from the card list of the previous zone
							parentZone.RemoveCard(d);

							// Add the card to the new zone
							currentDeploymentZone.AddCard(d);

							// Update the nb of cards deployed
							currentDeploymentZone.nbDeployedHere++;

							// Parent of the card is now the new deployment zone
							d.parentToReturnTo = this.transform;

							// Cost an action to the player
							gm.activePlayer.DecrementActionCounter();

							// Increase the number of deployment move (may be useful to deploy for free in another zone)
							gm.activePlayer.nbDeploymentMove++;
						} else {
							Debug.Log("Not enough action left to do this move !");
						}
					}

				}
					
				// from DEPLOYMENT ZONE to DEPLOYMENT ZONE
				if (gm.currentPhase == GameManager.GamePhase.MILITARY
					&& parentZone.type == Zone.ZoneType.DEPLOYMENT_ZONE
					&& gm.activePlayer.GetActionCounter() > 0) {

					DeploymentZone parentDeploymentZone = parentZone as DeploymentZone;

					if (currentDeploymentZone.IsNeighbour(parentDeploymentZone)) {
						// Remove the card from the card list of the previous zone
						parentDeploymentZone.RemoveCard(d);

						// Add the card to the new zone
						currentDeploymentZone.AddCard(d);

						// Cost one action to the player
						gm.activePlayer.DecrementActionCounter();

						// Parent of the card is now the new deployment zone
						d.parentToReturnTo = this.transform;

					} else {
						Debug.Log("Cannot move this card to this zone. The 2 zones must be neighbour.");
						// TODO popup
					}
				
				}
				break;

			case Zone.ZoneType.HAND:
				// Should never get a card back in the hand
				break;

			case Zone.ZoneType.BOARD:
				// Can only be dropped to the board from the hand, during the military phase
				if (gm.currentPhase == GameManager.GamePhase.MILITARY
					&& parentZone.type == Zone.ZoneType.HAND
				    && gm.activePlayer.GetActionCounter() > 0) {

					Card c = d.GetComponent<Card>();
					// Check that the player can pay this card
					if (gm.activePlayer.GetFoodNumber() >= c.foodCost) {
						Hand parentHandZone = parentZone as Hand;

						// Remove the card from the hand
						parentHandZone.RemoveCard(d);

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

				} else {
					Debug.Log("This card cannot be dropped on the board.");
				}
				break;
			}
		}
	}
}
