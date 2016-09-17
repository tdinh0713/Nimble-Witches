using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public GUIText debugText;
    public float acceleration; // The force applied every fixed step.
    public float highSpeed; // The speed at which an increased drag will be applied.
    public float linearDrag; // The default drag.
    public float chargeDrag; // The drag while player is charging.
    public float highSpeedDrag; // The drag while player's speed is higher than highSpeed
    public float braking; // A breaking coefficient that changes how fast player brakes.
    public float boost; // The impulse force applied at a full boost.
    public float chargeTime; // The speed of charging in "percent per second"
    public float turnSpeed; // The speed at which the player can turn.
    public float chargeTurnSpeed; // The speed the player turns while charging.
    public float chargeTurnFactor; // A coefficient that determines how much momentum is conserved when turning.

    private Rigidbody2D myRigidbody2D;
    private SpriteRenderer mySpriteRenderer;
    private float physicsRotation; // The rotation of the player which updates when isCharging is false
    private float boostTime; // The time in seconds when the player started boosting.
    private float charge; // The current level of charge.
    private float angleOffset = 90; // An angle offset determined by the default sprite. 0 would be for a right-facing sprite.
    private bool isCharging = false; // Is the player charging
    private bool isBoosting = false; // Is the player boosting

    // Use this for initialization
    void Start()
    {
        // Get our components
        myRigidbody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        // Set drag and freeze our rotation.
        myRigidbody2D.drag = linearDrag;
        myRigidbody2D.freezeRotation = true;
        physicsRotation = myRigidbody2D.rotation;
        charge = 0;
	}

    void Update ()
    {
        mySpriteRenderer.sortingOrder = -1 * 2 * Mathf.RoundToInt(this.transform.position.y);
        isCharging = Input.GetButton("Boost"); // isCharging true if pressing Boost

        if (isCharging)
        {
            if (Input.GetButtonDown("Boost")) // If the Boost button has just been pressed,
                boostTime = Time.time; // then set the current time as our boostTime

            charge = Mathf.Clamp(charge + chargeTime * Time.deltaTime, 0f, 1f); // Increase our charge, but clamp it between 0 and 1.
        }
        else if (Input.GetButtonUp("Boost")) // If the Boost button has just been released
            isBoosting = true; // Then the player is boosting

        mySpriteRenderer.color = new Color(1f - charge, charge, 0f, 1f); // Set our sprite color according to our charge.
    }

	void FixedUpdate ()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Get our horizontal input

        // If charging, set our physicsRotation according to a linear interpolation between itself and the Rigidbody's rotation
        // by the factor defined as our chargeTurnFactor. Otherwise, we can just directly use the Rigidbody's rotation.
        physicsRotation = isCharging ? Mathf.Lerp(physicsRotation, myRigidbody2D.rotation, chargeTurnFactor) : myRigidbody2D.rotation;

        // Calculate the extra speed we should apply to our rotation by the defined chargeTurnSpeed, and whether the player is charging.
        // Apply our rotation in terms of our horizontal input and our calculated turn speed.
        float extraTurnSpeed = (chargeTurnSpeed - turnSpeed) * (isCharging ? 1 : 0);
        myRigidbody2D.rotation -= horizontalInput * (turnSpeed + extraTurnSpeed) * Time.deltaTime;

        // Determine the amount of force we should apply to the player in the x and y directionsbased on the physicsRotation,
        // sprite angleOffset, the player's braking power, and whether the player is charging (and for how long they were charging).
        // This gives us a nice "Kirby Air Ride" feel.
        float dx = Mathf.Cos(Mathf.Deg2Rad * (physicsRotation + angleOffset));
        float dy = Mathf.Sin(Mathf.Deg2Rad * (physicsRotation + angleOffset));
        float forcex = acceleration * dx * (isCharging ? boostTime / (braking * Time.time) : 1);
        float forcey = acceleration * dy * (isCharging ? boostTime / (braking * Time.time) : 1);
        Vector2 movement = new Vector2(forcex, forcey);
        myRigidbody2D.AddForce(movement);

        // Calculate how much drag to apply to our player in terms of the defined chargeDrag
        // as well as how long the player has been charging for.
        float finalChargeDrag = chargeDrag + braking * (Time.time - boostTime);
        myRigidbody2D.drag = (isCharging ? finalChargeDrag : linearDrag);

        // If the x or y velocities of our player are higher than our highSpeed limit,
        // Set the player's drag to the highSpeedDrag.
        if (Mathf.Abs(myRigidbody2D.velocity.x) > (highSpeed) || Mathf.Abs(myRigidbody2D.velocity.y) > (highSpeed))
            myRigidbody2D.drag = highSpeedDrag;

        // If the player is boosting, then we need to apply an impulse.
        if (isBoosting) {
            // So calculate the impulse in terms of the boost force and how much charge they had accumulated.
            // The charge factor is squared because a linear charge function was too abusable.
            float boostx = boost * Mathf.Pow(charge, 2) * dx;
            float boosty = boost * Mathf.Pow(charge, 2) * dy;
            Vector2 boostImpulse = new Vector2(boostx, boosty);
            myRigidbody2D.AddForce(boostImpulse, ForceMode2D.Impulse);
            isBoosting = false; // Set isBoosting to false
            charge = 0; // And set our charge back to 0.
        }

        // This is the text that shows the stats on the screen.
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
