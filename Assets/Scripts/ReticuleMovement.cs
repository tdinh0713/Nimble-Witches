using UnityEngine;
using System.Collections;

public class ReticuleMovement : MonoBehaviour {

    public Transform mySpellcaster;
    public Sprite[] mySprites;

    public Transform myTransform;
    private SpriteRenderer mySpriteRenderer;
    private int mode = 0;
    private float maxRadius;
    private float aimRotation;

    public float deadZone = 0.25f;
    public float aimSlerp = 0.5f;

	// Use this for initialization
	void Start ()
    {
        myTransform = GetComponent<Transform>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector3 aimInput = new Vector3(Input.GetAxis("Spell Horizontal"), Input.GetAxis("Spell Vertical"), 0);

        switch (mode)
        {
            case 0: // NO RETICULE
                break;

            case 1: // STRAIGHT LINE
                if (aimInput.magnitude >= deadZone)
                {
                    Quaternion rotateTo = Quaternion.LookRotation(Vector3.forward, aimInput);
                    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, rotateTo, aimSlerp);
                }
                break;

            case 2: // CIRCLE
                myTransform.localPosition = maxRadius * aimInput;
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

        switch(mode)
        {
            case 1:
                myTransform.localPosition = new Vector3(0, 0, 0);
                break;

            case 2:
                myTransform.rotation = Quaternion.identity;
                break;
        }
            
    }

    public void SetMaxRadius (float radius)
    {
        maxRadius = radius;
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
