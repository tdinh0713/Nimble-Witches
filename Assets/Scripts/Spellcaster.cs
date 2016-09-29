using UnityEngine;
using System.Collections;

public class Spellcaster : MonoBehaviour {

    public ReticuleMovement reticule;
    public GameObject[] spells;

    private float inputTime;
    private bool expectingSecondInput;

    // Spell elements are powers of 2 for bitmasking.
    private const int NUMBER_OF_SPELLS = 4;
    private const int FIRE = 1;
    private const int EARTH = 2;
    private const int WATER = 4;
    private const int AIR = 8;
    private bool isCasting = false;
    private Queue spellQueue;
    private Transform myTransform;

	// Use this for initialization
	void Start ()
    {
        myTransform = GetComponent<Transform>();
        reticule.SetMode(0);
        spellQueue = new Queue();
	}

    // Update is called once per frame
    void Update()
    {
        myTransform.rotation = Quaternion.identity;
        // Check all spell element inputs, enqueue the input.
        if (isCasting == false)
        {
            for (int i = 1; i <= NUMBER_OF_SPELLS; i++)
            {
                bool pressedButton = Input.GetButtonDown("Spell " + i.ToString());
                if (pressedButton == true)
                {
                    spellQueue.Enqueue(i);
                    if (spellQueue.Count > 2)
                        spellQueue.Dequeue();
                }
            }
        }

        // Cancel Spell
        if (Input.GetButtonDown("Cancel Spell"))
        {
            spellQueue.Clear();
            reticule.SetMode(0);
        }

        // Cast Spell
        if (Input.GetButtonDown("Cast Spell"))
        {
            if (spellQueue.Count == 2)
            {
                if (isCasting == true)
                {
                    CastSpell((int)spellQueue.Dequeue(), (int)spellQueue.Dequeue());
                    reticule.SetMode(0);
                    isCasting = false;
                }
                else
                {
                    isCasting = true;
                    reticule.SetMode(ResolveSpellMode(spellQueue));
                }
            }
        }
    }

    int ResolveSpellMode (Queue spellQueue)
    {
        Queue copy = new Queue(spellQueue);
        int spell = ResolveSpell((int)copy.Dequeue(), (int)copy.Dequeue());
        return reticule.GetSpellMode(spell);
    }

    int ResolveSpell (int spell1, int spell2)
    {
        if (spell1 == spell2)
            return spell1;
        else
            return spell1 + spell2;
    }

    void CastSpell (int spell1, int spell2)
    {
        int spell = ResolveSpell(spell1, spell2);

        switch (spell)
        {
            case 1: // FIRE
                // instantiate spells[1]
                break;

            case 2: // EARTH
                break;

            case 3: // FIRE + EARTH
                break;

            case 4: // WATER
                break;

            case 5: // FIRE + WATER
                break;

            case 6: // EARTH + WATER
                break;

            case 7: // INVALID
                break;

            case 8: // AIR
                break;

            case 9: // FIRE + AIR
                break;

            case 10: // EARTH + AIR
                break;

            case 11: // INVALID
                break;

            case 12: // WATER + AIR
                break;
        }
    }
}
