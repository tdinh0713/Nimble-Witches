using UnityEngine;
using System.Collections;

public class SpellBehavior : MonoBehaviour {

	float lifeTime = 10.0f;

	void Awake () {
		Destroy (gameObject, lifeTime);
	}
}
