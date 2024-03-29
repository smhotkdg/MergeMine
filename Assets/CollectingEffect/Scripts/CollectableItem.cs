﻿using UnityEngine;
using System.Collections;

public class CollectableItem : MonoBehaviour {

	// The amount of item collect
	[Tooltip("The amount of item collect")]
	public int _quantity = 1;

	// Collect the item when detecting a collision with player
	void OnTriggerEnter2D(Collider2D other) {
		// Start collecting animation at this position
		CollectingEffectController._instance.CollectItemAtPosition (_quantity, Camera.main.WorldToScreenPoint (transform.position));

		// Destroy this element
		Destroy (this.gameObject);
	}
}
