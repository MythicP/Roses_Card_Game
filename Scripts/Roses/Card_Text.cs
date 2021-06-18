using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card_Text : MonoBehaviour
{
    public TextMeshPro attack;
    public TextMeshPro health;

    // Start is called before the first frame update
    void Start()
    {
        TextMeshPro[] temp = GetComponentsInChildren<TextMeshPro>();
        attack = temp[0];
        health = temp[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAttack(int val, bool over)
    {
        attack.text = val.ToString();
        if (over)
            health.color = Color.green;
        else
            health.color = Color.black;
    }

    public void SetHealth(int val, bool under)
    {
        health.text = val.ToString();
        if(under)
            health.color = Color.red;
        else
            health.color = Color.black;
    }
}
