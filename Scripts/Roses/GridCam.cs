using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCam : MonoBehaviour
{
    public Card_options co;
    public GridCont grid;
    public float speed = 1.0F;
    public float rotatespeed = 1.0F;
    public Tile target; //target tile

    private bool playerTurn;

    private Card selected_unit; //curently selected card
    private Tile last_target; //tile a unit was picked up from

    private (int, int) movement = (0, 0);
    private bool moving = false;
    private Animator anim;

    // Transforms to act as start and end markers for the journey.
    private Vector3 relation;
    private GameObject parent;
    private Vector3 startMarker;
    private Vector3 endMarker;
    private float startTime;
    private float journeyLength;

    private bool once = true;
    private bool rotating;

    // Start is called before the first frame update
    void Start()
    {
        anim = transform.parent.GetComponent<Animator>();

        relation = transform.position - target.transform.position;
        parent = transform.parent.gameObject;
        startMarker = parent.transform.position;
        endMarker = parent.transform.position;

        playerTurn = true;
        rotating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(once)
        {
            target.SetHover(true);
            target.GetTerrain().SetPulse(true);
            grid.HighlightCross(target, true);
            once = false;
        }     
    }

    public void MoveUnit()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && selected_unit == null)
        {
            selected_unit = target.GetUnit();
            if (selected_unit != null)
            {
                if (selected_unit.GetOwner().name.Equals("Yang") && playerTurn || selected_unit.GetOwner().name.Equals("Yin") && !playerTurn)
                    co.SetActivate(true, selected_unit);
                else
                    selected_unit = null;
            }     
        }
        else if(Input.GetKeyDown(KeyCode.Mouse0) && target.GetUnit() == null && grid.CheckDistance(last_target, target) == 1)
        {
            last_target.SetUnit(null);
            selected_unit.SetTilePosition(target);
            selected_unit.SetHasMoved(true);

            selected_unit = null;
            grid.UnHighlightDistance();
            grid.HighlightCross(last_target, false);
            grid.HighlightCross(target, true);
        }
        else if(Input.GetKeyDown(KeyCode.Mouse0) && target.GetUnit().GetOwner() != selected_unit.GetOwner() && grid.CheckDistance(last_target, target) == 1)
        {
            StartCombat(selected_unit, target.GetUnit());

            selected_unit = null;
            grid.UnHighlightDistance();
            grid.HighlightCross(last_target, false);
            grid.HighlightCross(target, true);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && selected_unit != null)
        {
            selected_unit = null;
            grid.UnHighlightDistance();
        }
    }

    public void FlipTarget()
    {
        if(selected_unit.GetFaceDown() == true)
            selected_unit.Flip();
        co.SetActivate(false);
        selected_unit = null;
    }

    public void TapTarget()
    {
        if (selected_unit.GetTaped() == false)
            selected_unit.Tap();
        co.SetActivate(false);
        selected_unit = null;
    }

    public void SetUnit(Card val)
    {
        selected_unit = val;
    }

    public void EnterMovement()
    {
        last_target = target;
        grid.HighlightDistance(1, target);
        co.SetActivate(false);
    }

    public void StartCombat(Card unit_a, Card unit_b) //unit a is freindly unit b is enemmy
    {
        if(!unit_a.GetFaceDown() && !unit_a.GetFaceDown())
        {
            Combat(unit_a, unit_b);
        }
        else
        {
            if (unit_a.GetFaceDown())
                unit_a.Flip();

            if (unit_b.GetFaceDown())
                unit_b.Flip();

            Combat(unit_a, unit_b);
        }
    }
    public void Combat(Card unit_a, Card unit_b) //unit a is freindly unit b is enemmy
    {
        if (unit_a.IsUnit() && unit_b.IsUnit())
        {
            int aAttack = unit_a.GetAttack();
            int bAttack = unit_b.GetAttack();

            unit_b.TakeDamage(aAttack);
            Debug.Log("Enemy took: " + aAttack);
            unit_a.TakeDamage(bAttack);
            Debug.Log("Ally took: " + bAttack);
        }
        else if (unit_a.IsUnit() && unit_b.IsCommander())
        {
            unit_b.TakeDamage(unit_a.GetAttack());
        }
        else
        {
            unit_b.Die();
        }

        if (unit_b.IsDead() && !unit_a.IsDead())
        {
            last_target.SetUnit(null);
            unit_a.SetTilePosition(target);
            unit_a.SetHasMoved(true);
        }
            
    }

    public void Movement()
    {
        if ((Input.GetKey(KeyCode.D) && playerTurn) || (Input.GetKey(KeyCode.A) && !playerTurn))
        {
            movement = (1,0);
        }
        else if ((Input.GetKey(KeyCode.A) && playerTurn) || (Input.GetKey(KeyCode.D) && !playerTurn))
        {
            movement = (-1, 0);
        }
        else if ((Input.GetKey(KeyCode.S) && playerTurn) || (Input.GetKey(KeyCode.W) && !playerTurn))
        {
            movement = (0, -1);
        }
        else if ((Input.GetKey(KeyCode.W) && playerTurn) || (Input.GetKey(KeyCode.S) && !playerTurn))
        {
            movement = (0, 1);
        }

        if(movement != (0, 0))
        {
            Tile tile = grid.GetNextTile(target, movement);

            if (tile != null)
            {
                tile.SetHover(true);
                target.SetHover(false);
                //set terrain to pulse

                tile.GetTerrain().SetPulse(true);
                target.GetTerrain().SetPulse(false);

                grid.HighlightCross(target, false);
                grid.HighlightCross(tile, true);

                target = tile;
                // Keep a note of the time the movement started.
                startTime = Time.time;

                startMarker = parent.transform.position;
                endMarker = tile.transform.position;

                // Calculate the journey length.
                journeyLength = Vector3.Distance(startMarker, endMarker);

                moving = true;
            }
            else
            {
                //play error;
            }
        }  
    }

    public void PerformMove()
    {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        parent.transform.position = Vector3.Lerp(startMarker, endMarker, fractionOfJourney);

        if (parent.transform.position == endMarker)
        {
            movement = (0, 0);
            moving = false;
        }
    }

    public void Rotate()
    {
        startTime = Time.time;
        startMarker = parent.transform.eulerAngles;
        endMarker = parent.transform.eulerAngles - new Vector3(0, 180, 0);
        rotating = true;
        journeyLength = Vector3.Distance(startMarker, endMarker);
    }

    public bool Rotating()
    {
        bool temp = false;
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * rotatespeed * 10;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        parent.transform.eulerAngles = Vector3.Lerp(startMarker, endMarker, fractionOfJourney);

        if (fractionOfJourney >= 1)
        {
            rotating = false;
            temp = true;
        }
        return temp;
    }

    public bool GetRotating()
    {
        return rotating;
    }

    public bool GetMoving()
    {
        return moving;
    }

    public Tile GetCurrent()
    {
        return target;
    }

    public void SetPlayerTurn(bool val)
    {
        playerTurn = val;
    }

    public bool GetPlayerTurn()
    {
        return playerTurn;
    }

    public Animator GetAnimator()
    {
        return anim;
    }
}
