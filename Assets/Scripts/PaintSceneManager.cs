using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintSceneManager : MonoBehaviour
{
    public Transform trailRendererContainer;
    public CameraOffsetter cameraOffsetter;
    public FadeCamera fadeCamera;
    public TrailManager trailManager;
    // Start is called before the first frame update
    void Start()
    {
        HandleHMDMounted();
        fadeCamera.SetToBlack();
    }
    // Update is called once per frame
    void Update()
    {
        OVRManager.HMDMounted += HandleHMDMounted;
        OVRManager.HMDUnmounted += HandleHMDUnmounted;
    }
     
    public void HandleHMDMounted() {
        StartCoroutine(ExampleCoroutine());
        fadeCamera.RedoFade();
    }

    IEnumerator ExampleCoroutine()
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(.4f);

        //After we have waited 5 seconds print the time again.
        cameraOffsetter.OffsetCamera();
    }

    void HandleHMDUnmounted() {
    // Do stuff
        foreach(Transform child in trailRendererContainer) {
            Destroy(child.gameObject);
        }
        trailManager.ResetTrailList();
        fadeCamera.SetToBlack();
    }
}
