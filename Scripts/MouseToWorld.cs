using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseToWorld : MonoBehaviour
{
    Vector3 worldPosition;

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;

        if (Physics.Raycast(ray, out hitData, 1000))
        {
            worldPosition = hitData.point;

            Vector3 new_position = worldPosition;
            new_position.y = 0.25f;

            if (hitData.collider != null)
            {
                transform.position = new_position;
            }

        }
    }
}
