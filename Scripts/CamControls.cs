using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControls : MonoBehaviour
{
    public Transform lookpoint;
    /*
    public float Radius = 1;
    public float Hight = 0;
    public Transform lookpoint;

    private int angle = 45;
    private bool rotating = false;
    private bool left = false;
    private bool right = false;

    // Start is called before the first frame update
    // Uses starting cam angle to move the main cam to the correct position of a circal centered at (0,0,0), with set radius 
    void Start()
    {
        float new_angle = angle * Mathf.Deg2Rad;

        float new_x = Radius * Mathf.Sin(new_angle) + lookpoint.position.x;
        float new_z = Radius * Mathf.Cos(new_angle) + lookpoint.position.z; 

        transform.position = new Vector3(new_x, Hight, new_z);
    }
    */
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown("e") && !rotating)
            rotate_To_position(angle -= 90);
        else if (Input.GetKeyDown("q") && !rotating)
            rotate_To_position(angle += 90);
        */
        transform.LookAt(lookpoint);
    }
    /*
    //Rotates the cam to a new position 
    private void rotate_To_position(float new_angle)
    {
        new_angle = new_angle * Mathf.Deg2Rad;

        float new_x = Radius * Mathf.Sin(new_angle) + lookpoint.position.x;
        float new_z = Radius * Mathf.Cos(new_angle) + lookpoint.position.z;

        transform.position = new Vector3(new_x, Hight, new_z);

    }
    */
}
