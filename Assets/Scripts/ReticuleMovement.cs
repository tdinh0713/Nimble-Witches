using UnityEngine;
using System.Collections;

public class ReticuleMovement : MonoBehaviour {

    public Transform mySpellcaster;
    public Sprite[] mySprites;
    public int[] mySpellModes;

    private Transform myTransform;
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
        Vector2 aimInput = new Vector2(Input.GetAxis("Spell Horizontal"), Input.GetAxis("Spell Vertical"));

        switch (mode)
        {
            case 0: // NO RETICULE
                break;

            case 1: // STRAIGHT LINE
                if (aimInput.magnitude >= deadZone)
                {
                    Quaternion rotateTo = Quaternion.LookRotation(Vector3.forward, new Vector3(Input.GetAxis("Spell Horizontal"), Input.GetAxis("Spell Vertical"), 0));
                    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, rotateTo, aimSlerp);
                }

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

    public int GetSpellMode (int spell)
    {
        return mySpellModes[spell];
    }
}
