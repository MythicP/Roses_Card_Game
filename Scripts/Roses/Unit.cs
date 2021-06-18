using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Card
{
    private int attackVal;
    private int healthVal;

    // Start is called before the first frame update
    void Start()
    {
        facedown = true;
        taped = false;
        flipEffect = false;
        tapEffect = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public new bool IsUnit()
    {
        return true;
    }

    public new void TakeDamage(int dmg)
    {
        healthVal -= dmg;
    }

    public int GetAttackVal()
    {
        return attackVal;
    }

    public int GetHealthVal()
    {
        return healthVal;
    }
}
