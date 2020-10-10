using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed;
    public float rayLength;

    void FixedUpdate() {
        transform.Translate(0,0, moveSpeed * Time.deltaTime);
        detectLedge();
    }

    void detectLedge() {
        if (!Physics.Raycast(transform.position, Vector3.down, rayLength))
        {
            // Debug.Log("Reached Ledge");
            moveSpeed = -moveSpeed;
        }
    }
}
