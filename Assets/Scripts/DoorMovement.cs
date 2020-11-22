using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class DoorMovement : MonoBehaviour
{

    Vector3 closedPosition;
    Vector3 openedPosition;

    float openSpeed = 1;
    private bool clear = false;
    
    void Start()
    {
        Vector3 cP = new Vector3(transform.position.x, transform .position.y, transform.position.z);
        Vector3 oP = new Vector3(transform.position.x, transform.position.y + 6.0f, transform.position.z);
        closedPosition = cP;
        openedPosition = oP;
    }

    void Update()
    {
        if (GameObject.FindWithTag("Enemy") != null)
        {
            yesEnemy();
        }
        else
        {
            noEnemy();
        }

        if (clear)
        {
            transform.position = Vector3.Lerp(transform.position,
                openedPosition, Time.deltaTime * openSpeed);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position,
                closedPosition, Time.deltaTime * openSpeed);
        }
    }


    public void yesEnemy()
    {
        clear = false;
    }

    public void noEnemy()
    {
        clear = true;
    }
}
