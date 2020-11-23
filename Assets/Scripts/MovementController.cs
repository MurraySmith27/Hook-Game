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

    // stores the last script that impacted the movement of the character
    MonoBehaviour lastMovementInput;
    
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
        rig.useGravity = !gm.IsGrapplingTo();
    }

    void FixedUpdate()
    {

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

    // a wrapper around Rigidbody.AddForce
    // all player movement scripts should call this function instead of
    // directly calling Rigidbody.AddForce
    public void AddForce(Vector3 force, ForceMode mode, MonoBehaviour who)
    {
        SwitchMovementInput(who);
        rig.AddForce(force, mode);
    }

    public void SetVelocity(Vector3 vel, MonoBehaviour who)
    {
        SetVelocity(vel, who, true);
    }

    // 'f' is the Forced parameter, if set to false only impacts
    // from the last movement input source are accepted
    // 'f' defaults to true
    public void SetVelocity(Vector3 vel, MonoBehaviour who, bool f)
    {
        if (!f && who != lastMovementInput)
            return;
        SwitchMovementInput(who);
        rig.velocity = vel;
    }

    void SwitchMovementInput(MonoBehaviour who)
    {
        if (who != lastMovementInput)
        {
            // reset velocity
            if (who == bm)
            {
                rig.velocity = new Vector3(0, rig.velocity.y, 0);
            }
            else if (who == gm)
            {
                rig.velocity = Vector3.zero;
            }

            lastMovementInput = who;
        }
    }
}
