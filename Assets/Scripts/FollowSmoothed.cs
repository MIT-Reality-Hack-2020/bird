using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSmoothed : MonoBehaviour
{
    public GameObject toFollow;
    public float lerpFactor = .1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, toFollow.transform.position, lerpFactor);
        transform.rotation = Quaternion.Slerp(transform.rotation, toFollow.transform.rotation, lerpFactor);
    }
}
