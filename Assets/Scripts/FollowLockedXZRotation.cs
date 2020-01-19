using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLockedXZRotation : MonoBehaviour
{
    public GameObject toFollow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = toFollow.transform.position;
        transform.rotation = Quaternion.Euler(0, toFollow.transform.eulerAngles.y , 0);
    }
}
