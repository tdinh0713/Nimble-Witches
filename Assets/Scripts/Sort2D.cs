using UnityEngine;
using System.Collections;

public class Sort2D : MonoBehaviour
{

    private SpriteRenderer myRenderer;
    private Transform myTransform;

    // Use this for initialization
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myTransform = GetComponent<Transform>();
        myRenderer.sortingOrder = -1 * 2 * (int)myTransform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
