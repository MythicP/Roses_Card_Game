using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMover : MonoBehaviour
{
    public int speed = 1;

    private float startTime;
    private Vector3 startMarker;
    private Vector3 endMarker;
    private float journeyLength;

    private float startTimef;
    private Vector3 startMarkerf;
    private Vector3 endMarkerf;
    private float journeyLengthf;


    private bool moving;
    private bool fliping;


    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        fliping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(moving)
            PerformMove();
        if (fliping)
            PerformFlip();
    }


    public void SetStartPos(Tile tile)
    {
        startTime = Time.time;

        startMarker = transform.position;
        endMarker = tile.transform.position + new Vector3(0, 0.1f, 0);

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker, endMarker);

        moving = true;
    }

    public void PerformMove()
    {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(startMarker, endMarker, fractionOfJourney);

        if (fractionOfJourney >= 1)
        {
            moving = false;
        }
    }

    public void SetStartFlip()
    {
        startTimef = Time.time;

        startMarkerf = transform.eulerAngles;
        endMarkerf = transform.eulerAngles + new Vector3(180, 0, 0);

        // Calculate the journey length.
        journeyLengthf = Vector3.Distance(startMarkerf, endMarkerf);

        fliping = true;
    }

    public void PerformFlip()
    {
        // Distance moved equals elapsed time times speed..
        float distCoveredf = (Time.time - startTimef) * (speed + 20) * 10;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourneyf = distCoveredf / journeyLengthf;

        // Set our position as a fraction of the distance between the markers.
        transform.eulerAngles = Vector3.Lerp(startMarkerf, endMarkerf, fractionOfJourneyf);

        if (fractionOfJourneyf >= 1)
        {
            fliping = false;
        }
    }
}
