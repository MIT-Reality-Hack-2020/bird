using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOffsetter : MonoBehaviour
{
    public Transform camera;
    public Transform environment;
    public Transform sculpt;
    public Transform sculptTargetLocation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OffsetCamera() {
        transform.localPosition = -camera.localPosition;
        environment.localRotation = Quaternion.Euler(0, camera.localEulerAngles.y , 0);

        sculpt.position = sculptTargetLocation.position;
        sculpt.rotation = Quaternion.Euler(0, sculptTargetLocation.eulerAngles.y , 0);
    }
}
