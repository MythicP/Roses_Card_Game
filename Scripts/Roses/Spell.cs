using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : Card
{
    // Start is called before the first frame update
    void Start()
    {
        facedown = true;
        taped = true;
        flipEffect = false;
        tapEffect = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
