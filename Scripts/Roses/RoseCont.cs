using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoseCont : MonoBehaviour
{
    public Hand hand;
    public Deck deck;
    public Hand hand2;
    public Deck deck2;

    public GridCam cam;
    public GridCont grid;
    public Card_options co;
    public TextMeshProUGUI truncountUi;

    public GameObject yin;
    public GameObject yang;

    private bool playerTurn;
    private int turnCount;


    // Start is called before the first frame update
    void Start()
    {
        playerTurn = true;
        turnCount = 1;
        truncountUi.text = "Turn: " + turnCount;
    }

    // Update is called once per frame
    void Update()
    {
        Animator anim = cam.GetAnimator();
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("CamIntro"))
        {

        }
        else
        {
            anim.enabled = false;
            //if(hand is down)
            //Cheack wsad
            if (!cam.GetRotating())
            {
                if (!cam.GetMoving() && playerTurn) // if cam is not moving, can now pull up hand
                {
                    if (!hand.GetMoving())
                        hand.Movement();
                    else
                        hand.PerformMove();
                }
                else if (!cam.GetMoving())
                {
                    if (!hand2.GetMoving())
                        hand2.Movement();
                    else
                        hand2.PerformMove();
                }

                if (hand.isActiveAndEnabled)
                {
                    if (hand.GetMode() == 0 && !hand.GetMoving()) //hand is down, and card options is not active
                    {
                        cam.MoveUnit();
                        if (!cam.GetMoving() && co.GetActive() == false)
                        {
                            EndTurn();
                            cam.Movement();
                        }
                        else if (cam.GetMoving())
                            cam.PerformMove();
                    }
                }
                else
                {
                    if (hand2.GetMode() == 0 && !hand2.GetMoving()) //hand is down, and card options is not active
                    {
                        cam.MoveUnit();
                        if (!cam.GetMoving() && co.GetActive() == false)
                        {
                            EndTurn();
                            cam.Movement();
                        }
                        else if (cam.GetMoving())
                            cam.PerformMove();
                    }
                }


                if (Input.GetKeyDown(KeyCode.Mouse1) && co.GetActive() == true)
                {
                    co.SetActivate(false);
                    cam.SetUnit(null);
                }
            }
            else
            {
                bool temp = cam.Rotating();
                if (temp && playerTurn)
                {
                    hand.gameObject.SetActive(true);
                    hand.DrawCard();
                    resetPlayerCards(yang);
                }
                else if (temp)
                {
                    hand2.gameObject.SetActive(true);
                    if (turnCount != 1)
                        hand2.DrawCard();
                    resetPlayerCards(yin);
                }

            }
        }
    }

    public void EndTurn()
    {
        // reset all units on next players turn (moved, taped), (mana)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerTurn)
            {
                hand.gameObject.SetActive(false);
            }
            else
            {
                hand2.gameObject.SetActive(false);
                SetTurnCount();
            }

            playerTurn = !playerTurn;
            cam.SetPlayerTurn(playerTurn);
            cam.Rotate();
        }
    }

    public void SetTurnCount()
    {
        turnCount++;
        truncountUi.text = "Turn: " + turnCount;
    }

    private void resetPlayerCards(GameObject player)
    {
        Card card;
        for (int i = 0; i < player.transform.childCount; i++)
        {
            card = player.transform.GetChild(i).GetComponent<Card>();
            card.NewTurn();
        }
    }
}
