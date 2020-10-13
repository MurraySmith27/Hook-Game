using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed;
    public float rayLength;
    public Rigidbody rb;

    private void Start() {
        rb = this.GetComponent<Rigidbody>();
    }
    void FixedUpdate() {
        detectLedge();

        // NOTE: It can potentially make the character pass through other objects if the speed
        //       is set too fast!
        rb.MovePosition(transform.position + (Vector3.forward * moveSpeed * Time.deltaTime));
        
    }

    void detectLedge() {
        //TODO: Make it so that it doesn't check the ledge when falling.
        //TODO: Fix the bug where it reaches the edge by a player pushing it and can't move.
        if (!Physics.Raycast(transform.position, Vector3.down, rayLength)) {
            Debug.Log("Reached Ledge");
            moveSpeed = -moveSpeed;
        }
    }
}
