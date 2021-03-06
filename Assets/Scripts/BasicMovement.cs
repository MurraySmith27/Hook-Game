﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    const float MidAirMultiplier = 0.5f;
    const float SpeedLimit = 15f;
    const float InitialSpeed = 8f;
    const float JumpBufferTime = 0.1f;
    const float JumpDistance = 4f;

    // each second the player speeds up by 0.25 m/s
    const float Accel = 2.5f;
    
    int zDirection;
    bool needToJump;
    bool anyKeyUp;

    // jump buffer
    [SerializeField]
    float jumpBuffer;

    CapsuleCollider col;
    Rigidbody rig;

    // used to check if the player is grappling to something
    GrapplingMovement script;
    MovementController controller;

    void Awake()
    {
        col = GetComponent<CapsuleCollider>();
        rig = GetComponent<Rigidbody>();
        script = GetComponent<GrapplingMovement>();
        controller = GetComponent<MovementController>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            needToJump = true;
        }

        // this stuff handles z axis movement 
        zDirection = Input.GetKeyDown(KeyCode.A) ? -1 : (Input.GetKeyDown(KeyCode.D) ? 1 : zDirection);

        if (Input.GetKeyUp(KeyCode.A) && zDirection == -1)
        {
            zDirection = Input.GetKey(KeyCode.D) ? 1 : 0;
        }
        else if (Input.GetKeyUp(KeyCode.D) && zDirection == 1)
        {
            zDirection = Input.GetKey(KeyCode.A) ? -1 : 0;
        }
    }

    void FixedUpdate()
    {
        HandleKeyboardMouseInputs();
    }

    void HandleKeyboardMouseInputs()
    {
        // always reset buffer to JumpBufferTime if not grounded
        if (!controller.isGrounded)
        {
            jumpBuffer = JumpBufferTime;
        }

        if (needToJump && jumpBuffer <= 0)
        {
            controller.AddForce(new Vector3(0, (float)Math.Sqrt(Physics.gravity.y * -2 * JumpDistance), 0), ForceMode.Impulse, this);
        }

        needToJump = false;
        jumpBuffer -= Time.fixedDeltaTime;

        // if direction changes
        if (zDirection == 0 && controller.isGrounded)
        {
            controller.SetVelocity(new Vector3(rig.velocity.x, rig.velocity.y, 0), this);
        }
        else
        {
            if (rig.velocity.z == 0 || rig.velocity.z * zDirection < 0)
            {
                controller.SetVelocity(new Vector3(rig.velocity.x, rig.velocity.y, 0), this, false);
                controller.AddForce(new Vector3(0, 0, zDirection * InitialSpeed * (controller.isGrounded ? 1 : 0.5f)), ForceMode.VelocityChange, this);
            }
            var vel = new Vector3(0, 0, zDirection * Accel * (controller.isGrounded ? 1 : 0.5f));
            rig.AddForce(vel, ForceMode.Acceleration);
        }

    }
}