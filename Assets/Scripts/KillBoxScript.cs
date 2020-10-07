using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoxScript : MonoBehaviour
{

    CapsuleCollider collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision){
        
        if (collision.gameObject.layer == 10){
            KillPlayer();
        }
    }

    void KillPlayer(){
        Debug.Log("I'm Dead");
    }
}
