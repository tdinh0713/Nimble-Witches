using UnityEngine;
using System.Collections;

public class CameraFollowGameObject : MonoBehaviour {

    public Transform targetTransform;
    private Transform myTransform;

	// Use this for initialization
	void Start ()
    {
        myTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float dx = targetTransform.position.x - myTransform.position.x;
        float dy = targetTransform.position.y - myTransform.position.y;

        Vector3 dTransform = new Vector3(dx, dy, 0);

        myTransform.Translate(dTransform);
	}
}
