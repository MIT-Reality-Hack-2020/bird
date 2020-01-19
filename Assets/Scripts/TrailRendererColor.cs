using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailRendererColor : MonoBehaviour
{
    public Color color;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Gradient gradient = new Gradient();
        GradientColorKey[] colorKey = new GradientColorKey[2];
        colorKey[0].color = color;
        colorKey[0].time = 0.0f;
        colorKey[1].color = color;
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);

        GetComponent<Unity.Labs.XR.XRTrailRenderer>().colorGradient = gradient;
    }
}
