using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject model;

    protected float health;
    protected float attack;
    protected float defence;
    protected int movement;

    protected string character_name;
    protected string side;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getName()
    {
        return character_name;
    }

    public void setPosition(Transform pos)
    {
        transform.position = pos.position;
    }

    public string getSide()
    {
        return side;
    }
    public int GetMovement()
    {
        return movement;
    }

    public void Move()
    {

    }

    public void Defend()
    {

    }

    public void Light_attack()
    {

    }

    public void Heavy_attack()
    {

    }

    public void Special()
    {

    }
}
