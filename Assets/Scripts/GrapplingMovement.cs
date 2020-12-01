using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingMovement : MonoBehaviour
{
    const float StopThreshold = 0.6f;

    Rigidbody rig;

    GameObject grappleTo;
    GameObject grappleFrom;

    Vector3? grappleFromDst;

    MovementController controller;
    
    // Start is called before the first frame update
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        controller = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && grappleTo == null)
        {
            grappleTo = GetGrapplableUnderCursor();
        }
        else if (Input.GetMouseButtonDown(1) && grappleFrom == null)
        {
            grappleFrom = GetGrapplableUnderCursor();
        }

        ProcessGrappleTo();
        ProcessGrappleFrom();

        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // TODO implement collision checking
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    }

    GameObject GetGrapplableUnderCursor()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // do not grapple if there is an abstacle in between
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)
            && hit.transform.gameObject.layer == 8)
        {
            return hit.transform.gameObject;
        }

        return null;
    }

    void ProcessGrappleTo()
    {
        if (grappleTo == null)
            return;

        var src = transform.position;
        var dst = grappleTo.transform.position;

        float distance = Vector3.Distance(src, dst);

        if (distance < StopThreshold)
        {
            grappleTo = null;
        }

        controller.AddForce(dst - src, ForceMode.Acceleration, this); 
    }

    void ProcessGrappleFrom()
    {
        if (grappleFrom == null)
        {
            return;
        }
        
        if (grappleFromDst == null)
        {
            grappleFromDst = transform.position;
        }
        
        var src = grappleFrom.transform.position;

        float distance = Vector3.Distance(src, grappleFromDst.Value);

        if (distance < StopThreshold)
        {
            grappleFrom = null;
            grappleFromDst = null;
            return;
        }

        grappleFrom.GetComponent<Rigidbody>().AddForce(grappleFromDst.Value - src, ForceMode.Acceleration);
    }

    public bool IsGrapplingTo()
    {
        return grappleTo != null;
    }

}
