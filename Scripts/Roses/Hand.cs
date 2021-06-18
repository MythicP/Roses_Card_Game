using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Deck deck;
    public GridCam gc;
    public GameObject yin;
    public GameObject yang;
    private int mode = 0; // 0 is up, 1 is hidden,
    public Vector3 up;
    public Vector3 down;
    public Vector3 zoom;
    public float speed = 1.0F;

    //current card
    private List<Card> cards;
    private int current;
   
    //lerp
    private Vector3 startMarker;
    private Vector3 endMarker;
    private float startTime;
    private float journeyLength;
    private bool moving;

    // Start is called before the first frame update
    void Start()
    {
        current = -1;
        cards = new List<Card>();

        mode = 0;
        transform.localPosition = down;

        for (int i = 0; i <5; i++)
        {
            DrawCard();
            if (i == 0)
                SetCurrent(0);
        }
        adjustCards();
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == 1)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                PlayCard();
            }
            else if(Input.GetKeyDown(KeyCode.A) && current > 0)
            {
                SetCurrent(current - 1);
            }
            else if (Input.GetKeyDown(KeyCode.D) && current < cards.Count - 1)
            {
                SetCurrent(current + 1);
            }
        }
    }

    private void PlayCard()
    {
        Tile placement = gc.GetCurrent();

        if (placement.GetUnit() == null)
        {
            Card card = RemoveCard();
     
            placement.SetUnit(card);
            placement.SetEdge(true);

            if(gc.GetPlayerTurn())
            {
                card.transform.parent = yang.transform;
                card.SetOwner(yang);
            }
            else
            {
                card.transform.parent = yin.transform;
                card.SetOwner(yin);
            }  

            card.transform.position = placement.transform.position + new Vector3(0, 0.1f, 0);
            if(gc.GetPlayerTurn())
                card.transform.eulerAngles = placement.transform.eulerAngles + new Vector3(0, 0, 90);
            else
                card.transform.eulerAngles = placement.transform.eulerAngles + new Vector3(0, 0, -90);

            if (cards.Count > 0 && current > 0)
                SetCurrent(current - 1);
            else if(cards.Count > 0)
                SetCurrent(0);
        }
    }

    private void playSpell()
    {

    }

    private void SetCurrent(int new_cr)
    {
        if (current != -1 && current < cards.Count)
        {
            cards[current].GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        }
           
        current = new_cr;
        cards[current].GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

        adjustCards();
    }

    public void DrawCard()
    {
        Card card = deck.DrawCard();
        //this will be from deck deck.getCard;
        Card temp = Instantiate(card, Vector3.zero, transform.rotation);
        temp.transform.SetParent(transform);
        temp.transform.localPosition = Vector3.zero;
        temp.transform.localEulerAngles = new Vector3(90, 180, 0);
        temp.transform.localScale = new Vector3(1.5f, 1, 1);
        cards.Add(temp);
        adjustCards();
        if (cards.Count == 1)
            SetCurrent(0);
    }

    private void adjustCards()
    {
        IEnumerator<Card> cards_Enum = cards.GetEnumerator();
        cards_Enum.MoveNext();

        for (int i = 0; i < cards.Count; i++)
        {
            //Five cards only
            /*
            if (cards.Count%2 == 0)
            {
                cards_Enum.Current.transform.localPosition = new Vector3(0, 0, ((i - ((cards.Count - 1) / 2)) * 2.3f) - 1.15f);
                //Debug.Log("even");
            }
            else
            {
                cards_Enum.Current.transform.localPosition = new Vector3(0, 0, (i - ((cards.Count - 1) / 2)) * 2.3f);
                //Debug.Log("odd");
            }
            */

            //more than five cards

            cards_Enum.Current.transform.localPosition = new Vector3(0, 0, ((i - current) * 2.3f)); //current = center of hand

            cards_Enum.MoveNext();
        }
    }

    private Card RemoveCard()
    {
        Card temp = cards[current];
        cards.RemoveAt(current);
        adjustCards();
        return temp;
    }

    public void Movement()
    {
        if (Input.GetKeyDown(KeyCode.Q) && mode == 0)
        {
            mode = 1;
            Move(up);
        }
        else if (Input.GetKeyDown(KeyCode.Q) && mode == 1)
        {
            mode = 0;
            Move(down);
        }
    }

    public void Move(Vector3 newpos)
    {
        startTime = Time.time;
        startMarker = transform.localPosition;
        endMarker = newpos;
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
        transform.localPosition = Vector3.Lerp(startMarker, endMarker, fractionOfJourney);

        if (transform.localPosition == endMarker)
        {
            moving = false;
        }
    }

    public bool GetMoving()
    {
        return moving;
    }

    public int GetMode()
    {
        return mode;
    }
}
