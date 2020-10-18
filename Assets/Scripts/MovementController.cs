using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class controls and coordinates player movement
public class MovementController : MonoBehaviour
{

    public bool isGrounded = true;

    // whether isGrounded changed on this frame
    public bool groundedChanged = false;

    // components
    Rigidbody rig;

    // scripts
    BasicMovement bm;
    GrapplingMovement gm;
    
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        bm = GetComponent<BasicMovement>();
        gm = GetComponent<GrapplingMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bm.enabled = !gm.IsGrapplingTo();
    }

    void FixedUpdate()
    {
        var isGroundedTemp = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), transform.localScale.y / 2.0f + 0.01f);
        groundedChanged = isGrounded != isGroundedTemp;
        isGrounded = isGroundedTemp;

        if (isGrounded)
        {
            rig.drag = 0;   // no drag on the ground
        }
        else
        {
            rig.drag = 1;
        }
    }
}
