using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public GameObject GrabbedGameObject;

    Quaternion initialRotationOffset;
    Vector3 initialPositionOffset;
    Quaternion myInitialRotation;

    bool Grabbing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GrabbedGameObject && Grabbing) {
            GrabbedGameObject.transform.position = transform.position +
                    transform.rotation * Quaternion.Inverse(myInitialRotation) * initialPositionOffset;

            GrabbedGameObject.transform.rotation = transform.rotation * initialRotationOffset; 

            Rigidbody rb =  GrabbedGameObject.GetComponent<Rigidbody>();
            if (rb) { 
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    void OnTriggerEnter(Collider col) {
        if (!GrabbedGameObject) {
            Grabbable grabbable = col.gameObject.GetComponent<Grabbable>();
            if (grabbable != null) {
                GrabbedGameObject = grabbable.grabTarget;
            }
        }
    }

    void OnTriggerExit(Collider col) {
        GrabbedGameObject = null;
    }

    public void AttemptGrab() {
        if (GrabbedGameObject) {
            Grab();
        }
    }

    public void Release() {
        Grabbing = false;
    }

    void Grab() {
        Grabbing = true;
        myInitialRotation = transform.rotation;
        initialPositionOffset = GrabbedGameObject.transform.position - transform.position;
        initialRotationOffset = Quaternion.Inverse(transform.rotation) * GrabbedGameObject.transform.rotation;
    }
}
