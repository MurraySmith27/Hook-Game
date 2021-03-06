﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crab_movement : EnemyMovement
{

    public Animator anim;
    
    // Start is called before the first frame update
    public float anim_speed = 1;
    BoxCollider collider; 
    void Start()
    {
        anim.speed = anim_speed;
        anim.SetInteger("Direction", (int) (moveSpeed / Mathf.Abs(moveSpeed) + 0.1));
        collider = GetComponent<BoxCollider>();
    }

    void FixedUpdate() {
        detectLedge();

        // NOTE: It can potentially make the character pass through other objects if the speed
        //       is set too fast!
        rb.MovePosition(transform.position + ((gameObject.transform.rotation * transform.forward) * moveSpeed * Time.deltaTime));
        
    }

    void detectLedge() {
        //TODO: Make it so that it doesn't check the ledge when falling.
        //TODO: Fix the bug where it reaches the edge by a player pushing it and can't move.
        rayLength = (float)(collider.size.z  + 0.01) / 2;
        if (!Physics.Raycast(transform.position, Vector3.down, rayLength)) 
        {
            moveSpeed = -moveSpeed;
            anim.SetInteger("Direction", anim.GetInteger("Direction") * -1);
        }
    }
}
