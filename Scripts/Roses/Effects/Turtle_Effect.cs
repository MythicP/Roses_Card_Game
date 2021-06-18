using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle_Effect : Effect
{
    private Card card;
    private RoseCont cont;

    private int turncount;
    // Start is called before the first frame update
    void Start()
    {
        card = GetComponent<Card>();
        cont = GameObject.Find("Main").GetComponent<RoseCont>();

        turncount = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public new void New_Turn()
    {
        if(!card.GetFaceDown())
        {
            turncount++;

            if(turncount % 2 == 0)
            {
                card.SetStats(2, 2);
            }
        }
    }
}
