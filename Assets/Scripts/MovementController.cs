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
    CapsuleCollider cc;
    // scripts
    BasicMovement bm;
    GrapplingMovement gm;
    Transform groundDetector;
    
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        bm = GetComponent<BasicMovement>();
        gm = GetComponent<GrapplingMovement>();
        groundDetector = transform.Find("GroundDetector");
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

        // var isGroundedTemp = Physics.Raycast(transform.position, Vector3.down, transform.localScale.y / 2.0f + 0.01f);
        // var isGroundedTemp = Physics.Raycast(cc.center, Vector3.down, cc.bounds.extents.y + cc.transform.localPosition.y + 0.01f);
        // Debug.Log(groundDetector.position.y);
        var isGroundedTemp = Physics.Raycast(groundDetector.position, Vector3.down, 0.15f);
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
