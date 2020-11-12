using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingScript : MonoBehaviour
{

    private LineRenderer lr;
    private Rigidbody rb;
    private Vector3 GrapplingTo;
    public LayerMask whatIsGrapplable;
    GameObject ObjectBeingPulled;
    public float grapplingVel;
    public Transform grapplePointOnPlayer;
    private Vector3 moveVector;
    public float maxDistanceMultiplierForJoint = 0.8f;
    public float minDistanceMultiplierForJoint = 0.25f;
    public float JointSpring = 4.5f;
    public float JointDamper = 7f;
    public float JointMassScale = 4.5f; 
    public float DeleteJointThreshold = 0.6f;
    float DistanceToPoint;
    bool DestroyJoint;
    bool LeftClick;
    bool RightClick;
    bool CanLeftClick = true;
    bool CanRightClick = true;
    public bool pulling = false;
    public bool grappling = false;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (CanLeftClick){
            LeftClick = Input.GetMouseButtonDown(0);
        }
        if (CanRightClick){
            RightClick = Input.GetMouseButtonDown(1);
        }
        if (pulling){
            DistanceToPoint = Vector3.Distance(ObjectBeingPulled.transform.position, GrapplingTo);
        }
        else{
            DistanceToPoint = Vector3.Distance(transform.position, GrapplingTo);
        }
        DestroyJoint = DistanceToPoint <= DeleteJointThreshold;

        if (LeftClick){
            GrappleToPoint();
        }
        else if (RightClick){
            PullObject();
        }
        if (DestroyJoint){
            lr.positionCount = 0;
            CanLeftClick = true;
            CanRightClick = true;
            pulling = false;
            grappling = false;
            ObjectBeingPulled = null;
            rb.useGravity = true;
        }
    }

    void LateUpdate(){
        DrawRope();
        if (grappling){
            MoveToGrapplingPoint();
        }
    }

    void MoveToGrapplingPoint(){
        rb.velocity = moveVector * grapplingVel * Time.deltaTime;
    }


    void GrappleToPoint(){
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, maxDistance: Mathf.Infinity, layerMask: 1<<8)){
            GrapplingTo = hit.transform.position;
            moveVector = Vector3.Normalize(GrapplingTo - transform.position);
            rb.useGravity = false;
            grappling = true;
            CanLeftClick = false;
            LeftClick = false;
        }
    }

    void PullObject(){
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, maxDistance: Mathf.Infinity, layerMask: 1<<8)){
            pulling = true;
            ObjectBeingPulled = hit.transform.gameObject;
            GrapplingTo = transform.position;

            CanRightClick = false;
            RightClick = false;
        }
    }

    void DrawRope(){
        lr.positionCount = 2;
        if (pulling) {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, ObjectBeingPulled.transform.position);
        } 
        else {
            lr.SetPosition(0, grapplePointOnPlayer.position);
            lr.SetPosition(1, GrapplingTo);
        }
    }
    void DeleteRope(){
        lr.positionCount = 0;
    }
}