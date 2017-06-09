using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour {

	public int foodCost;
	public int foodConsumption;
	public int battleValue;

	public abstract void SpecialEffect();
}