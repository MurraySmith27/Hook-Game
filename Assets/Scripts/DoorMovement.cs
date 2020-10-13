using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class DoorMovement : MonoBehaviour
{

    public Transform door;

    public Vector3 closedPosition = new Vector3(0f, 0f, -20f);
    public Vector3 openedPosition = new Vector3(0f, 7f, -20f);

    public float openSpeed = 5;

    //check if door is open
    private bool open = false;

    //check if stage is clear
    private bool clear = false;


    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Enemy") != null)
        {
            yesEnemy();
        }
        else
        {
            noEnemy();
        }

        //move to closed position if open
        //move to open position if closed
        if (open && clear)
        {
            door.position = Vector3.Lerp(door.position,
                openedPosition, Time.deltaTime * openSpeed);
        }
        else
        {
            door.position = Vector3.Lerp(door.position,
                closedPosition, Time.deltaTime * openSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            openedDoor();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            closedDoor();
    }

    public void closedDoor()
    {
        open = false;
    }

    public void openedDoor()
    {
        open = true;
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
