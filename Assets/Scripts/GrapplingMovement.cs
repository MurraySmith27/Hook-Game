using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingMovement : MonoBehaviour
{
    const float StopThreshold = 0.6f;

    Rigidbody rig;
    SpringJoint joint;


    GameObject grappleTo;
    GameObject grappleFrom;

    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && grappleTo == null)
        {
            grappleTo = GetGrapplableUnderCursor();

            if (grappleTo)
                rig.velocity = Vector3.zero;
        }
        else if (Input.GetMouseButtonDown(1) && grappleFrom == null)
        {
            grappleFrom = GetGrapplableUnderCursor();
        }

        ProcessGrappleTo();

    }

    GameObject GetGrapplableUnderCursor()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 8))
        {
            return hit.transform.gameObject;
        }

        return null;
    }

    void ProcessGrappleTo()
    {
        if (grappleTo == null)
            return;

        rig.useGravity = false;

        var src = transform.position;
        var dst = grappleTo.transform.position;

        float distance = Vector3.Distance(src, dst);

        if (distance < StopThreshold)
        {
            grappleTo = null;
            rig.useGravity = true;
        }

        rig.AddForce(dst - src, ForceMode.Acceleration); 

    }

    public bool IsGrapplingTo()
    {
        return grappleTo != null;
    }

}
