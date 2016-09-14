using UnityEngine;
using System.Collections;

public class CameraFollowGameObject : MonoBehaviour {

    public Transform targetTransform;
    private Transform myTransform;

	// Use this for initialization
	void Start ()
    {
        // Get camera's transform component.
        myTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Get the difference between the target position and my position.
        float dx = targetTransform.position.x - myTransform.position.x;
        float dy = targetTransform.position.y - myTransform.position.y;

        // Move position
        myTransform.Translate(dx, dy, 0);
	}
}
