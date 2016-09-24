using UnityEngine;
using System.Collections;

public class ReticuleMovement : MonoBehaviour {

    public Transform mySpellcaster;
    public Sprite[] mySprites;

    private Transform myTransform;
    private SpriteRenderer mySpriteRenderer;
    private int mode;
    private float aimMaxRadius;
    private float aimRotation;

	// Use this for initialization
	void Start ()
    {
        myTransform = GetComponent<Transform>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 aimInput = new Vector2(Input.GetAxis("Spell Horizontal"), Input.GetAxis("Spell Vertical"));

        switch (mode)
        {
            case 0: // NO RETICULE
                break;

            case 1: // STRAIGHT LINE
                    float degrees = Vector2.Angle(new Vector2(0, 1), aimInput.normalized);
                    aimRotation = degrees;
                break;

            case 2: // CIRCLE
                break;
        }
	}

    public void SetMode (int mode)
    {
        if (this.mode != mode)
        {
            this.mode = mode;
            mySpriteRenderer.sprite = mySprites[mode];
        }
    }

    public void SetAimMaxRadius (float radius)
    {
        aimMaxRadius = radius;
    }

    public void SetAimRotation (float rotation)
    {
        aimRotation = rotation;
    }

    public int GetMode ()
    {
        return mode;
    }

    public float GetAimRotation ()
    {
        return aimRotation;
    }
}
