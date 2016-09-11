using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public GUIText debugText;
    public float acceleration;
    public float highSpeed;
    public float linearDrag;
    public float chargeDrag;
    public float highSpeedDrag;
    public float braking;
    public float boost;
    public float chargeTime;
    public float turnSpeed;
    public float chargeTurnSpeed;
    public float chargeTurnFactor;

    private Rigidbody2D myRigidbody2D;
    private SpriteRenderer mySpriteRenderer;
    private float physicsRotation;
    private float boostTime;
    private float charge;
    private float angleOffset = 90;
    private bool isCharging = false;
    private bool isBoosting = false;

    // Use this for initialization
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myRigidbody2D.drag = linearDrag;
        myRigidbody2D.freezeRotation = true;
        physicsRotation = myRigidbody2D.rotation;
        charge = 0;
	}

    void Update ()
    {
        isCharging = Input.GetButton("Boost");

        if (Input.GetButton("Boost")) {
            if (Input.GetButtonDown("Boost")) {
                boostTime = Time.time;
            }
            charge = Mathf.Clamp(charge + chargeTime * Time.deltaTime, 0f, 1f);
        }

        if (Input.GetButtonUp("Boost")) {
            isBoosting = true;
        }

        mySpriteRenderer.color = new Color(1f - charge, charge, 0f, 1f);
    }

	void FixedUpdate ()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        physicsRotation = isCharging ? Mathf.Lerp(physicsRotation, myRigidbody2D.rotation, chargeTurnFactor) : myRigidbody2D.rotation;

        float extraTurnSpeed = (chargeTurnSpeed - turnSpeed) * (isCharging ? 1 : 0);
        myRigidbody2D.rotation -= horizontalInput * (turnSpeed + extraTurnSpeed) * Time.deltaTime;

        float dx = Mathf.Cos(Mathf.Deg2Rad * (physicsRotation + angleOffset));
        float dy = Mathf.Sin(Mathf.Deg2Rad * (physicsRotation + angleOffset));
        float forcex = acceleration * dx * (isCharging ? boostTime / (braking * Time.time) : 1);
        float forcey = acceleration * dy * (isCharging ? boostTime / (braking * Time.time) : 1);
        Vector2 movement = new Vector2(forcex, forcey);
        myRigidbody2D.AddForce(movement);

        float finalChargeDrag = chargeDrag + braking * (Time.time - boostTime);
        myRigidbody2D.drag = (isCharging ? finalChargeDrag : linearDrag);

        if (Mathf.Abs(myRigidbody2D.velocity.x) > (highSpeed) || Mathf.Abs(myRigidbody2D.velocity.y) > (highSpeed))
            myRigidbody2D.drag = highSpeedDrag;

        if (isBoosting) {
            float boostx = boost * Mathf.Pow(charge, 2) * dx;
            float boosty = boost * Mathf.Pow(charge, 2) * dy;
            Vector2 boostImpulse = new Vector2(boostx, boosty);
            myRigidbody2D.AddForce(boostImpulse, ForceMode2D.Impulse);
            isBoosting = false;
            charge = 0;
        }

        if (debugText != null)
        {
            debugText.text =
                "Player Info:\n"+
                "Rotation: " + myRigidbody2D.rotation + "\n"+
                "Speed: " + myRigidbody2D.velocity + "\n"+
                "Drag: " + myRigidbody2D.drag + "\n"+
                "Charge: " + charge + "\n";
        }
	}
}
