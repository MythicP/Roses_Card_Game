using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> deck;

    public GameObject owner;

    // Start is called before the first frame update
    void Start()
    {
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Card DrawCard()
    {
        Card card = deck[0];
        deck.Remove(deck[0]);

        card.SetOwner(owner);
        return card;
    }

    private void Shuffle()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    public void SetOwner(GameObject new_owner)
    {
        owner = new_owner;
    }
    public GameObject GetOwner()
    {
        return owner;
    }
}
