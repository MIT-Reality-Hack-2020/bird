using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMatWithObjProximity : MonoBehaviour
{
    public Material fadeMat;
    public GameObject objTracker;

    public float maxDistance = 1.2f;
    public float minDistance = .9f;

    float alpha = 0f;
    public float lerpAmt = .5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(objTracker.transform.position, transform.position);
        float a = 0f;
        if (dist > maxDistance) {
            a = 1f;
        }
        else if (dist >= minDistance) {
            a = (dist - minDistance)/(maxDistance - minDistance);
        }
        alpha = lerpAmt * alpha + (1 - lerpAmt) * a;

        var col = fadeMat.color;
        col.a = alpha;
        fadeMat.color = col;
    }
}
