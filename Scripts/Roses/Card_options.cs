using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;

public class Card_options : MonoBehaviour
{
    public GameObject options;
    public GameObject move;
    public GameObject flip;
    public GameObject tap;

    // Start is called before the first frame update
    void Start()
    {
        options.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActivate(bool val, Card card)
    {
        options.SetActive(val);

        move.SetActive(!card.GetHasMoved());
        flip.SetActive(card.GetFaceDown());
        tap.SetActive(!card.GetTaped());
    }

    public void SetActivate(bool val)
    {
        options.SetActive(val);
    }


    public bool GetActive()
    {
        return options.activeSelf;
    }
}

