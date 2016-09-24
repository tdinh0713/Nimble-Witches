using UnityEngine;
using System.Collections;

public class DirectionAnimation : MonoBehaviour {

    public Transform playerTransform;
    public SpriteRenderer playerRenderer;
    public float heightOffset;
    private Animator myAnimator;
    private Transform myTransform;
    private SpriteRenderer mySpriteRenderer;
    private int rotation;

	// Use this for initialization
	void Start () {
        myAnimator = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        rotation = 0;
	}
	
	// Update is called once per frame
	void Update () {

        rotation = Mathf.RoundToInt((playerTransform.rotation.eulerAngles.z % 360) / 45);
        if (rotation == 8)
            rotation = 0;
        myAnimator.SetInteger("direction", rotation);

        myTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + heightOffset, playerTransform.position.z-1);
        mySpriteRenderer.sortingOrder = playerRenderer.sortingOrder;

	}
}
