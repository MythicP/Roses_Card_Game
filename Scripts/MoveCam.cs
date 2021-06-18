using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public float speed = 1;
    public float zoomspeed;
    private Vector3 velocity = Vector3.zero;
    private Rigidbody rb;

    private bool rotatingRight = false;
    private bool rotatingLeft = false;

    private Vector3 from;
    private Vector3 to;
    public float rotationSpeed = 1f;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        from = transform.eulerAngles;
        to = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        Wsadmovement();
        Rotation();
        Zoom();
    }

    void Zoom()
    {
        if(Input.mouseScrollDelta.y > 0) //Zoom in
        {
            if( Vector3.Distance(Camera.main.transform.position, transform.position) > 5)
                Camera.main.transform.position = Camera.main.transform.position + Camera.main.transform.forward * zoomspeed * Time.deltaTime;
        }
        else if (Input.mouseScrollDelta.y < 0) //Zoom out
        {
            if( Vector3.Distance(Camera.main.transform.position, transform.position) < 15)
                Camera.main.transform.position = Camera.main.transform.position - Camera.main.transform.forward * zoomspeed * Time.deltaTime;
        }
    }

    void Rotation()
    {
        if (Input.GetKeyDown("q") && (!rotatingLeft && !rotatingRight))
        {
            to = new Vector3(transform.eulerAngles.x, Mathf.Round(transform.eulerAngles.y + 90) % 360, transform.eulerAngles.z);
            rotatingRight = true;
        }
        else if (Input.GetKeyDown("e") && (!rotatingLeft && !rotatingRight))
        {
            if(transform.eulerAngles.y - 90 > 0)
                to = new Vector3(transform.eulerAngles.x, Mathf.Round(transform.eulerAngles.y - 90), transform.eulerAngles.z);
            else
                to = new Vector3(transform.eulerAngles.x, 315, transform.eulerAngles.z);
            rotatingLeft = true;
        }

        if (((transform.eulerAngles.y < to.y) || (to.y < 50 && transform.eulerAngles.y > 300)) && rotatingRight)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + rotationSpeed * Time.deltaTime, transform.eulerAngles.z);
        }
        else
            rotatingRight = false;

        if (((transform.eulerAngles.y > to.y) || (to.y > 300 && transform.eulerAngles.y > 0 && transform.eulerAngles.y < 50)) && rotatingLeft)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - rotationSpeed * Time.deltaTime, transform.eulerAngles.z);
        }
        else
            rotatingLeft = false;
    }

    void Wsadmovement()
    {
        float hori = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        Vector3 mHori = Camera.main.transform.right * hori;
        Vector3 mVer = Camera.main.transform.forward * ver;

        Vector3 add_v = (mHori + mVer).normalized * speed;
        if (ver > 0)
            add_v = (mHori + mVer).normalized * 1.4f * speed;
        //final movement
        velocity = add_v;
    }

    void FixedUpdate()
    {
        PerformMove();
    }

    void PerformMove()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }
}
